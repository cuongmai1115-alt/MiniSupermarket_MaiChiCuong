using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")] // Định tuyến URL cho Controller, ví dụ: api/categories
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        // Tiêm DbContext thông qua Constructor Injection
        public CategoriesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // 1. READ: Lấy toàn bộ danh mục từ SQL Server (Trả về DTO, không trả nguyên Entity)
        [HttpGet]
        // Toàn bộ các hàm trong Controller đều áp dụng async Task<IActionResult> và await
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.Categories.AsNoTracking()
                .Select(c => new CategoryResponseDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Description = c.Description
                })
                .ToListAsync();
            return Ok(list);
        }

        // API PHÂN TRANG LINQ CHO DANH MỤC NHÓM HÀNG (GIỐNG SẢN PHẨM)
        // GET: api/categories/paged?keyword=banh&pageNumber=1&pageSize=5
        [HttpGet("paged")]
        [AllowAnonymous] // Cho phép kiểm thử trực tiếp trên Swagger UI và kết nối Client
        public async Task<IActionResult> GetPagedCategories(
            [FromQuery] string? keyword,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 50) pageSize = 5;

            var query = _context.Categories.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string search = keyword.Trim().ToLower();
                query = query.Where(c => c.CategoryName.ToLower().Contains(search)
                                      || (c.Description != null && c.Description.ToLower().Contains(search)));
            }

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var items = await query
                .OrderBy(c => c.CategoryId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CategoryResponseDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Description = c.Description
                })
                .ToListAsync();

            var response = new PagedResult<CategoryResponseDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            };

            return Ok(response);
        }

        // 2. READ: Lấy chi tiết 1 danh mục theo ID (Trả về DTO)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound(new { message = "Không tìm thấy nhóm hàng trong CSDL!" });
            }

            var dto = new CategoryResponseDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description
            };
            return Ok(dto);
        }

        // 3. SEARCH: Tìm kiếm qua Query String trên SQL Server (Trả về DTO)
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new { message = "Vui lòng nhập từ khóa tìm kiếm!" });
            }

            var result = await _context.Categories.AsNoTracking()
                .Where(c => c.CategoryName.Contains(keyword))
                .Select(c => new CategoryResponseDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Description = c.Description
                })
                .ToListAsync();

            return Ok(result);
        }

        // 4. CREATE: Thêm mới nhóm hàng vào Database (Nhận DTO, Trả DTO)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CategoryRequestDto newCat)
        {
            if (string.IsNullOrWhiteSpace(newCat.CategoryName))
            {
                return BadRequest(new { message = "Tên nhóm hàng không được để trống!" });
            }

            var category = new Category
            {
                CategoryName = newCat.CategoryName.Trim(),
                Description = newCat.Description?.Trim()
            };

            _context.Categories.Add(category); //Thêm mới
            await _context.SaveChangesAsync(); // Lưu thay đổi vào SQL Server

            var responseDto = new CategoryResponseDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description
            };

            return CreatedAtAction(nameof(GetById), new { id = category.CategoryId }, responseDto);
        }

        // 5. UPDATE: Cập nhật nhóm hàng vào Database (Nhận DTO)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]  //30. Chỉ Admin mới được phép sửa nhóm hàng
        public async Task<IActionResult> Update(int id, [FromBody] CategoryRequestDto updateCat)
        {
            if (string.IsNullOrWhiteSpace(updateCat.CategoryName))
            {
                return BadRequest(new { message = "Tên nhóm hàng không được để trống!" });
            }

            var cat = await _context.Categories.FindAsync(id); //Cập nhật
            if (cat == null)
            {
                return NotFound(new { message = "Không tìm thấy nhóm hàng cần sửa!" });
            }

            cat.CategoryName = updateCat.CategoryName.Trim();
            cat.Description = updateCat.Description?.Trim();

            await _context.SaveChangesAsync(); //sửa dữ liệu
            return Ok(new { success = true, message = "Cập nhật nhóm hàng thành công!" });
        }

        // 6. DELETE: Xóa nhóm hàng khỏi Database
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null)
            {
                return NotFound(new { message = "Không tìm thấy nhóm hàng cần xóa!" });
            }

            _context.Categories.Remove(cat);//Xóa dữ liệu
            await _context.SaveChangesAsync(); 
            return Ok(new { success = true, message = "Xóa nhóm hàng thành công!" });
        }

        // 7. Kiểm tra quyền Admin (Chỉ tài khoản có Role = Admin mới được gọi)
        [HttpGet("admin-dashboard")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAdminDashboard()
        {
            await Task.Yield();
            return Ok(new { message = "Chào mừng Admin! Bạn có toàn quyền quản trị hệ thống siêu thị mini." });
        }

        // 8. Kiểm tra quyền chung cho nhân viên (Cả Admin và Cashier đều gọi được)
        [HttpGet("staff-pos")]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> GetStaffPos()
        {
            await Task.Yield();
            return Ok(new { message = "Màn hình POS Thu ngân sẵn sàng phục vụ bán hàng." });
        }
    }

    // ================= DTO CLASSES CHO CATEGORY =================
    public class CategoryResponseDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class CategoryRequestDto
    {
        [Required(ErrorMessage = "Tên nhóm hàng không được để trống!")]
        [StringLength(100, ErrorMessage = "Tên nhóm hàng không vượt quá 100 ký tự")]
        public string CategoryName { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Mô tả tối đa 255 ký tự")]
        public string? Description { get; set; }
    }
}
