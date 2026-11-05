using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ĐĂNG KÝ DBCONTEXT (SQL Server) qua Dependency Injection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SupermarketDbContext>(options =>
    options.UseSqlServer(connectionString));

// Cấu hình dịch vụ xác thực JWT Bearer
var jwtSecret = builder.Configuration["JwtSettings:Secret"] ?? "SupermarketSecretKeyDoAnMonHoc2026SecureString!!";
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// CẤU HÌNH SWAGGER + JWT
builder.Services.AddSwaggerGen(options =>
{
    // Khai báo nút Authorize
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Nhập token JWT vào ô bên dưới (không cần gõ chữ Bearer)."
    });

    // Yêu cầu Swagger sử dụng JWT
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// CẤU HÌNH CORS (Cross-Origin Resource Sharing)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Tự động kiểm tra và nạp 2 Stored Procedure vào CSDL SQL Server khi khởi động API
try
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<SupermarketDbContext>();

    db.Database.ExecuteSqlRaw(@"
        CREATE OR ALTER PROCEDURE sp_GetRevenueByCashier
        AS
        BEGIN
            SET NOCOUNT ON;
            SELECT 
                CashierUsername,
                COUNT(OrderId) AS TotalOrders,
                ISNULL(SUM(FinalAmount), 0) AS TotalRevenue,
                ISNULL(SUM(RewardPoints), 0) AS TotalPointsAwarded
            FROM Orders
            GROUP BY CashierUsername
            ORDER BY TotalRevenue DESC;
        END;
    ");

    db.Database.ExecuteSqlRaw(@"
        CREATE OR ALTER PROCEDURE sp_GetRevenueByDateRange
            @FromDate DATETIME,
            @ToDate DATETIME
        AS
        BEGIN
            SET NOCOUNT ON;
            SELECT 
                CashierUsername,
                COUNT(OrderId) AS TotalOrders,
                ISNULL(SUM(FinalAmount), 0) AS TotalRevenue
            FROM Orders
            WHERE CreatedDate >= @FromDate AND CreatedDate <= @ToDate
            GROUP BY CashierUsername
            ORDER BY TotalRevenue DESC;
        END;
    ");
}
catch (Exception ex)
{
    Console.WriteLine("Lỗi nạp Stored Procedure tự động: " + ex.Message);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Kích hoạt Middleware CORS (phải đặt trước Authentication và Authorization)
app.UseCors("AllowAll");

// Bắt buộc gọi UseAuthentication trước UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();