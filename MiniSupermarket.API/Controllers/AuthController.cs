using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiniSupermarket.API.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SupermarketDbContext _db;

        public AuthController(IConfiguration configuration, SupermarketDbContext db)
        {
            _configuration = configuration;
            _db = db;
        }

        // POST /api/auth/login  -> kiểm tra tài khoản trong bảng Users (15 tài khoản seed)
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var username = (request.Username ?? string.Empty).Trim();

            var user = await _db.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == request.Password);

            if (user == null)
                return Unauthorized(new { success = false, message = "Sai tài khoản hoặc mật khẩu!" });

            if (!user.IsActive)
                return StatusCode(StatusCodes.Status403Forbidden,
                    new { success = false, message = "Tài khoản đã bị khóa. Vui lòng liên hệ Admin!" });

            var token = GenerateJwtToken(user.Username, user.Role);
            return Ok(new
            {
                success = true,
                token,
                role = user.Role,
                username = user.Username,
                fullName = user.FullName
            });
        }

        private string GenerateJwtToken(string username, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"] ?? "SupermarketSecretKeyDoAnMonHoc2026SecureString!!");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class LoginRequestDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}