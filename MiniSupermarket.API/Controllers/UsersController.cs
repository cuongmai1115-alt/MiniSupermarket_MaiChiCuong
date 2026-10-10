using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]   // Chỉ Admin quản trị tài khoản
    public class UsersController : ControllerBase
    {
        private static readonly string[] ValidRoles = { "Admin", "Cashier", "Warehouse" };
        private readonly SupermarketDbContext _db;
        public UsersController(SupermarketDbContext db) { _db = db; }

        // KHÔNG trả về Password
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _db.Users.AsNoTracking().OrderBy(u => u.Id)
                .Select(u => new { u.Id, u.Username, u.FullName, u.Role, u.IsActive })
                .ToListAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest r)
        {
            if (string.IsNullOrWhiteSpace(r.Username) || string.IsNullOrWhiteSpace(r.Password))
                return BadRequest(new { message = "Tên đăng nhập và mật khẩu không được trống!" });
            if (!ValidRoles.Contains(r.Role))
                return BadRequest(new { message = "Vai trò không hợp lệ!" });
            if (await _db.Users.AnyAsync(u => u.Username == r.Username.Trim()))
                return Conflict(new { message = "Tên đăng nhập đã tồn tại!" });

            var u = new User
            {
                Username = r.Username.Trim(),
                Password = r.Password,          // Demo: lưu thô giống seed hiện có
                FullName = r.FullName?.Trim() ?? "",
                Role = r.Role,
                IsActive = true
            };
            _db.Users.Add(u);
            await _db.SaveChangesAsync();
            return Ok(new { u.Id });
        }

        // PUT api/users/5/toggle-lock
        [HttpPut("{id:int}/toggle-lock")]
        public async Task<IActionResult> ToggleLock(int id)
        {
            var u = await _db.Users.FindAsync(id);
            if (u == null) return NotFound(new { message = "Không tìm thấy tài khoản!" });
            if (string.Equals(u.Username, User.Identity?.Name, StringComparison.OrdinalIgnoreCase))
                return BadRequest(new { message = "Không thể tự khóa tài khoản đang đăng nhập!" });

            u.IsActive = !u.IsActive;
            await _db.SaveChangesAsync();
            return Ok(new { u.IsActive });
        }

        // PUT api/users/5/reset-password
        [HttpPut("{id:int}/reset-password")]
        public async Task<IActionResult> ResetPassword(int id, [FromBody] ResetPasswordRequest r)
        {
            if (string.IsNullOrWhiteSpace(r.NewPassword))
                return BadRequest(new { message = "Mật khẩu mới không được trống!" });
            var u = await _db.Users.FindAsync(id);
            if (u == null) return NotFound(new { message = "Không tìm thấy tài khoản!" });
            u.Password = r.NewPassword;
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }

    public class CreateUserRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public string Role { get; set; } = "Cashier";
    }

    public class ResetPasswordRequest
    {
        public string NewPassword { get; set; } = string.Empty;
    }
}
