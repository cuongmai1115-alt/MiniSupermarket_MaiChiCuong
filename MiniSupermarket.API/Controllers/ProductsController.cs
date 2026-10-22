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

        // =========================================================================
        // BUỔI 6: API PHÂN TRANG LINQ & EAGER LOADING (.Include)
        // GET: api/products/paged?keyword=sua&pageNumber=1&pageSize=5&categoryId=1
        // =========================================================================
        [HttpGet("paged")]
        [AllowAnonymous] // Cho phép kiểm thử trực tiếp trên Swagger UI và kết nối Client
        public async Task<IActionResult> GetPagedProducts(
            [FromQuery] string? keyword,
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
                .AsNoTracking()
                .AsQueryable();

            // 3. Lọc theo từ khóa tìm kiếm (nếu người dùng có nhập)
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string search = keyword.Trim().ToLower();
                query = query.Where(p => p.ProductName.ToLower().Contains(search)
                                      || p.Barcode.Contains(search));
            }

            // Bài tập 6.2: Bổ sung bộ lọc Sản phẩm theo Nhóm hàng
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            // 4. Đếm tổng số lượng bản ghi thỏa mãn điều kiện
            int totalItems = await query.CountAsync();

            // 5. Tính toán tổng số trang = Ceiling(totalItems / pageSize)
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // 6. Áp dụng toán tử Skip và Take để phân trang trực tiếp dưới SQL Server
            var items = await query
                .OrderBy(p => p.ProductId) // Bắt buộc phải OrderBy trước khi Skip/Take
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductItemDto
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
            if (r.Price < 0 || r.StockQuantity < 0)
                return BadRequest(new { message = "Giá và tồn kho không được âm!" });
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
            _db.Products.Add(p);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = p.ProductId }, new { p.ProductId });
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductRequest r)
        {
            var p = await _db.Products.FindAsync(id);
            if (p == null) return NotFound(new { message = "Không tìm thấy sản phẩm!" });
            if (r.Price < 0 || r.StockQuantity < 0)
                return BadRequest(new { message = "Giá và tồn kho không được âm!" });
            if (await _db.Products.AnyAsync(x => x.Barcode == r.Barcode.Trim() && x.ProductId != id))
                return Conflict(new { message = $"Mã vạch {r.Barcode} đã thuộc sản phẩm khác!" });

            p.Barcode = r.Barcode.Trim();
            p.ProductName = r.ProductName.Trim();
            p.Price = r.Price;
            p.StockQuantity = r.StockQuantity;
            p.CategoryId = r.CategoryId;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _db.Products.FindAsync(id);
            if (p == null) return NotFound(new { message = "Không tìm thấy sản phẩm!" });
            _db.Products.Remove(p);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }

    public class ProductRequest
    {
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
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
