using System.ComponentModel.DataAnnotations;
using System.Text;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        // LẬP TRÌNH BẤT ĐỒNG BỘ (ASYNC / AWAIT)
        // Toàn bộ các hàm trong Controller đều áp dụng async Task<IActionResult> và await

        private readonly SupermarketDbContext _db;
        public ProductsController(SupermarketDbContext db) { _db = db; }

        // Trả về dạng phẳng (có CategoryName) để WinForms hiển thị thẳng lên lưới
        private IQueryable<ProductResponseDto> Project(IQueryable<Product> q) => q.Select(p => new ProductResponseDto
        {
            ProductId = p.ProductId,
            Barcode = p.Barcode,
            ProductName = p.ProductName,
            Price = p.Price,
            StockQuantity = p.StockQuantity,
            CategoryId = p.CategoryId,
            CategoryName = p.Category != null ? p.Category.CategoryName : ""
        });
        // BUỔI 6: API PHÂN TRANG LINQ & EAGER LOADING (.Include)
        // GET: api/products/paged?keyword=sua&pageNumber=1&pageSize=5&categoryId=1
        [HttpGet("paged")]
        [AllowAnonymous] // Cho phép kiểm thử trực tiếp trên Swagger UI và kết nối Client
      
        public async Task<IActionResult> GetPagedProducts(
            [FromQuery] string? keyword,   // PHÂN TRANG LINQ
            [FromQuery] int? categoryId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            // 1. Kiểm tra tham số trang đầu vào hợp lệ
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 50) pageSize = 5; // Mặc định 5 dòng, tối đa 50 dòng

            // 2. Khởi tạo truy vấn LINQ IQueryable kết hợp Eager Loading (.Include)
            // AsNoTracking giúp tối ưu hóa bộ nhớ RAM cho các truy vấn chỉ đọc dữ liệu
            var query = _db.Products
                .Include(p => p.Category)
                .AsNoTracking() // hàm tối ưu hóa bộ nhớ RAM
                .AsQueryable();

            // 22. Lọc theo từ khóa tìm kiếm (nếu người dùng có nhập)
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string search = keyword.Trim().ToLower();
                query = query.Where(p => p.ProductName.ToLower().Contains(search)
                                      || p.Barcode.Contains(search));
            }

            // Bổ sung bộ lọc Sản phẩm theo Nhóm hàng
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            // 4. Đếm tổng số lượng bản ghi thỏa mãn điều kiện
            int totalItems = await query.CountAsync();

            // 5. Tính toán tổng số trang = Ceiling(totalItems / pageSize)
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            //6. Áp dụng toán tử Skip và Take để phân trang trực tiếp dưới SQL Server
            var items = await query
                .OrderBy(p => p.ProductId) //Phân Trang Bắt buộc phải OrderBy trước khi Skip/Take
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductItemDto // để hiển thị kèm tên nhóm hàng của sản phẩm không bị null
                {
                    ProductId = p.ProductId,
                    Barcode = p.Barcode,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category != null ? p.Category.CategoryName : string.Empty
                })
                .ToListAsync();

            // 7. Đóng gói kết quả trả về cho Client
            var response = new PagedResult<ProductItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            };

            return Ok(response);
        }

        // GET api/products?keyword=&categoryId=
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? keyword, [FromQuery] int? categoryId)
        {
            var q = _db.Products.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                q = q.Where(p => p.Barcode.Contains(keyword) || p.ProductName.Contains(keyword));
            }
            if (categoryId.HasValue && categoryId > 0)
                q = q.Where(p => p.CategoryId == categoryId);

            return Ok(await Project(q.OrderBy(p => p.ProductId)).ToListAsync());
        }

        // =========================================================================
        // TÌM KIẾM SẢN PHẨM: GET api/products/search?keyword=...&categoryId=...
        // =========================================================================
        [HttpGet("search")]
        [AllowAnonymous] // Cho phép kiểm thử trực tiếp trên Swagger UI và kết nối Client
        public async Task<IActionResult> Search([FromQuery] string? keyword, [FromQuery] int? categoryId)
        {
            var query = _db.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string search = keyword.Trim();
                query = query.Where(p => p.ProductName.Contains(search) || p.Barcode.Contains(search));
            }

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            var list = await Project(query.OrderBy(p => p.ProductId)).ToListAsync();
            return Ok(list);
        }

       
        // XUẤT BÁO CÁO DANH MỤC HÀNG HÓA & TỒN KHO RA EXCEL (.XLSX)
        // Kỹ thuật: Sử dụng thư viện ClosedXML
        // Nghiệp vụ: Quản lý bấm "Xuất Excel" tải toàn bộ mặt hàng kèm số lượng tồn kho
        [HttpGet("export-excel")]
        [AllowAnonymous] // Cho phép kiểm thử Swagger UI & WinForms Client
        public async Task<IActionResult> ExportToExcel([FromQuery] int? categoryId)
        {
            var query = _db.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .AsQueryable();

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            var list = await query.OrderBy(p => p.ProductId).ToListAsync();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Danh mục tồn kho");

            // 1. Tiêu đề chính
            worksheet.Cell(1, 1).Value = "BÁO CÁO DANH MỤC HÀNG HÓA VÀ SỐ LƯỢNG TỒN KHO";
            worksheet.Range("A1:G1").Merge();
            worksheet.Cell(1, 1).Style.Font.Bold = true;
            worksheet.Cell(1, 1).Style.Font.FontSize = 16;
            worksheet.Cell(1, 1).Style.Font.FontColor = XLColor.White;
            worksheet.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.FromHtml("#1E3A8A");
            worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            worksheet.Row(1).Height = 36;

            // 2. Thông tin phụ
            int totalStock = list.Sum(p => p.StockQuantity);
            worksheet.Cell(2, 1).Value = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss} | Tổng mặt hàng: {list.Count:N0} | Tổng tồn kho: {totalStock:N0} sản phẩm";
            worksheet.Range("A2:G2").Merge();
            worksheet.Cell(2, 1).Style.Font.Italic = true;
            worksheet.Cell(2, 1).Style.Font.FontSize = 10;
            worksheet.Cell(2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(2, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            worksheet.Row(2).Height = 22;

            // 3. Tiêu đề các cột
            string[] headers = { "STT", "Mã vạch (Barcode)", "Tên sản phẩm", "Nhóm hàng", "Đơn giá (VNĐ)", "Tồn kho", "Trạng thái tồn kho" };
            int headerRow = 4;
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = worksheet.Cell(headerRow, i + 1);
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Font.FontSize = 11;
                cell.Style.Font.FontColor = XLColor.White;
                cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#2563EB");
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }
            worksheet.Row(headerRow).Height = 28;

            // 4. Đổ dữ liệu
            int row = 5;
            for (int i = 0; i < list.Count; i++)
            {
                var p = list[i];
                worksheet.Cell(row, 1).Value = i + 1;
                worksheet.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(row, 2).SetValue(p.Barcode);
                worksheet.Cell(row, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(row, 3).Value = p.ProductName;

                worksheet.Cell(row, 4).Value = p.Category?.CategoryName ?? string.Empty;
                worksheet.Cell(row, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                worksheet.Cell(row, 5).Value = p.Price;
                worksheet.Cell(row, 5).Style.NumberFormat.Format = "#,##0";
                worksheet.Cell(row, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                worksheet.Cell(row, 6).Value = p.StockQuantity;
                worksheet.Cell(row, 6).Style.NumberFormat.Format = "#,##0";
                worksheet.Cell(row, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                string status = p.StockQuantity <= 0 ? "Hết hàng" : (p.StockQuantity <= 20 ? "Sắp hết hàng" : "Bình thường");
                var statusCell = worksheet.Cell(row, 7);
                statusCell.Value = status;
                statusCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                if (p.StockQuantity <= 0)
                {
                    statusCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#FEE2E2");
                    statusCell.Style.Font.FontColor = XLColor.FromHtml("#991B1B");
                    statusCell.Style.Font.Bold = true;
                }
                else if (p.StockQuantity <= 20)
                {
                    statusCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#FEF3C7");
                    statusCell.Style.Font.FontColor = XLColor.FromHtml("#92400E");
                    statusCell.Style.Font.Bold = true;
                }

                for (int c = 1; c <= 7; c++)
                {
                    worksheet.Cell(row, c).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(row, c).Style.Border.OutsideBorderColor = XLColor.FromHtml("#D1D5DB");
                }

                row++;
            }

            // 5. Dòng tổng cộng ở cuối bảng
            worksheet.Cell(row, 1).Value = "TỔNG CỘNG";
            worksheet.Range(row, 1, row, 4).Merge();
            worksheet.Cell(row, 1).Style.Font.Bold = true;
            worksheet.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(row, 1).Style.Fill.BackgroundColor = XLColor.FromHtml("#F3F4F6");

            worksheet.Cell(row, 5).Value = "";
            worksheet.Cell(row, 5).Style.Fill.BackgroundColor = XLColor.FromHtml("#F3F4F6");

            if (list.Count > 0)
            {
                worksheet.Cell(row, 6).FormulaA1 = $"=SUM(F5:F{row - 1})";
            }
            else
            {
                worksheet.Cell(row, 6).Value = 0;
            }
            worksheet.Cell(row, 6).Style.Font.Bold = true;
            worksheet.Cell(row, 6).Style.NumberFormat.Format = "#,##0";
            worksheet.Cell(row, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            worksheet.Cell(row, 6).Style.Fill.BackgroundColor = XLColor.FromHtml("#F3F4F6");

            worksheet.Cell(row, 7).Value = $"{list.Count:N0} mặt hàng";
            worksheet.Cell(row, 7).Style.Font.Bold = true;
            worksheet.Cell(row, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(row, 7).Style.Fill.BackgroundColor = XLColor.FromHtml("#F3F4F6");

            for (int c = 1; c <= 7; c++)
            {
                worksheet.Cell(row, c).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(
                content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"DanhSach_SanPham_TonKho_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            );
        }

        // =========================================================================
        // TIÊU CHÍ 48: XUẤT BÁO CÁO DANH MỤC HÀNG HÓA RA TỆP CSV
        // =========================================================================
        [HttpGet("export-csv")]
        [AllowAnonymous]
        public async Task<IActionResult> ExportToCsv([FromQuery] int? categoryId)
        {
            var query = _db.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .AsQueryable();

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            var list = await query.OrderBy(p => p.ProductId).ToListAsync();

            static string Esc(string? v) => "\"" + (v ?? "").Replace("\"", "\"\"") + "\"";

            var sb = new StringBuilder();
            sb.AppendLine("STT,Mã vạch,Tên sản phẩm,Nhóm hàng,Đơn giá,Số lượng tồn kho,Trạng thái");
            for (int i = 0; i < list.Count; i++)
            {
                var p = list[i];
                string status = p.StockQuantity <= 0 ? "Hết hàng" : (p.StockQuantity <= 20 ? "Sắp hết hàng" : "Bình thường");
                sb.AppendLine($"{i + 1},{Esc(p.Barcode)},{Esc(p.ProductName)},{Esc(p.Category?.CategoryName)},{p.Price},{p.StockQuantity},{Esc(status)}");
            }

            var bytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(sb.ToString())).ToArray();
            return File(bytes, "text/csv; charset=utf-8", $"DanhSach_SanPham_TonKho_{DateTime.Now:yyyyMMdd_HHmmss}.csv");
        }

        // GET api/products/barcode/8934567890123  (POS dùng)
        [HttpGet("barcode/{barcode}")]
        public async Task<IActionResult> GetByBarcode(string barcode)
        {
            var p = await Project(_db.Products.AsNoTracking().Where(x => x.Barcode == barcode.Trim())).FirstOrDefaultAsync();
            if (p == null) return NotFound(new { message = "Không tìm thấy sản phẩm có mã vạch này!" });
            return Ok(p);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var p = await Project(_db.Products.AsNoTracking().Where(x => x.ProductId == id)).FirstOrDefaultAsync();
            if (p == null) return NotFound(new { message = "Không tìm thấy sản phẩm!" });
            return Ok(p);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Create([FromBody] ProductRequest r)
        {
            if (string.IsNullOrWhiteSpace(r.Barcode) || string.IsNullOrWhiteSpace(r.ProductName))
                return BadRequest(new { message = "Mã vạch và tên sản phẩm không được trống!" });
            if (r.Price <= 0)
                return BadRequest(new { message = "Giá bán phải lớn hơn 0!" });
            if (r.StockQuantity < 0)
                return BadRequest(new { message = "Số lượng tồn kho không được âm!" });
            if (!await _db.Categories.AnyAsync(c => c.CategoryId == r.CategoryId))
                return BadRequest(new { message = "Danh mục không tồn tại!" });
            if (await _db.Products.AnyAsync(p => p.Barcode == r.Barcode.Trim()))
                return Conflict(new { message = $"Mã vạch {r.Barcode} đã tồn tại!" });

            var p = new Product
            {
                Barcode = r.Barcode.Trim(),
                ProductName = r.ProductName.Trim(),
                Price = r.Price,
                StockQuantity = r.StockQuantity,
                CategoryId = r.CategoryId
            };
            _db.Products.Add(p); //Thêm mới sản phẩm
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = p.ProductId }, new { p.ProductId });
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductRequest r)
        {
            var p = await _db.Products.FindAsync(id);
            if (p == null) return NotFound(new { message = "Không tìm thấy sản phẩm!" });
            if (r.Price <= 0)
                return BadRequest(new { message = "Giá bán phải lớn hơn 0!" });
            if (r.StockQuantity < 0)
                return BadRequest(new { message = "Số lượng tồn kho không được âm!" });
            if (await _db.Products.AnyAsync(x => x.Barcode == r.Barcode.Trim() && x.ProductId != id))
                return Conflict(new { message = $"Mã vạch {r.Barcode} đã thuộc sản phẩm khác!" });

            p.Barcode = r.Barcode.Trim();
            p.ProductName = r.ProductName.Trim();
            p.Price = r.Price;
            p.StockQuantity = r.StockQuantity;
            p.CategoryId = r.CategoryId;
            await _db.SaveChangesAsync(); 
            return Ok(new { success = true, message = "Cập nhật sản phẩm thành công!" });
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _db.Products.FindAsync(id);
            if (p == null) return NotFound(new { message = "Không tìm thấy sản phẩm!" });
            _db.Products.Remove(p); //xóa 
            await _db.SaveChangesAsync();
            return Ok(new { success = true, message = "Xóa sản phẩm thành công!" });
        }
    }

    public class ProductRequest
    {
        [Required(ErrorMessage = "Mã vạch không được để trống!")]
        [StringLength(50, ErrorMessage = "Mã vạch tối đa 50 ký tự!")]
        public string Barcode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên sản phẩm không được để trống!")]
        [StringLength(150, ErrorMessage = "Tên sản phẩm tối đa 150 ký tự!")]
        public string ProductName { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Giá bán phải lớn hơn 0!")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn kho không được âm!")]
        public int StockQuantity { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn danh mục hợp lệ!")]
        public int CategoryId { get; set; }
    }

    public class ProductResponseDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
