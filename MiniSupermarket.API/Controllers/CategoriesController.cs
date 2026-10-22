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

            _context.Categories.Add(category);
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryRequestDto updateCat)
        {
            if (string.IsNullOrWhiteSpace(updateCat.CategoryName))
            {
                return BadRequest(new { message = "Tên nhóm hàng không được để trống!" });
            }

            var cat = await _context.Categories.FindAsync(id);
            if (cat == null)
            {
                return NotFound(new { message = "Không tìm thấy nhóm hàng cần sửa!" });
            }

            cat.CategoryName = updateCat.CategoryName.Trim();
            cat.Description = updateCat.Description?.Trim();

            await _context.SaveChangesAsync();
            return NoContent();
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

            _context.Categories.Remove(cat);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 7. Kiểm tra quyền Admin (Chỉ tài khoản có Role = Admin mới được gọi)
        [HttpGet("admin-dashboard")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAdminDashboard()
        {
            return Ok(new { message = "Chào mừng Admin! Bạn có toàn quyền quản trị hệ thống siêu thị mini." });
        }

        // 8. Kiểm tra quyền chung cho nhân viên (Cả Admin và Cashier đều gọi được)
        [HttpGet("staff-pos")]
        [Authorize(Roles = "Admin,Cashier")]
        public IActionResult GetStaffPos()
        {
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
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
