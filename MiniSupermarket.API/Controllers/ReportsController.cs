using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using System.Data;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // Yêu cầu JWT Token có Role Admin
    public class ReportsController : ControllerBase
    {
        private const int LowStockThreshold = 20;   // trùng Product.IsLowStock

        private readonly SupermarketDbContext _db;
        private readonly IConfiguration _configuration;

        public ReportsController(SupermarketDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        // GET api/reports/summary?fromDate=2026-10-01&toDate=2026-10-15
        // Toàn bộ số liệu lấy từ bảng Orders / OrderDetails (dữ liệu thật, không còn random).
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            var start = (fromDate ?? DateTime.Today).Date;
            var end = (toDate ?? DateTime.Today).Date;
            if (start > end)
                return BadRequest(new { message = "Ngày bắt đầu không được lớn hơn ngày kết thúc." });

            var endExclusive = end.AddDays(1);

            var orders = _db.Orders.AsNoTracking()
                .Where(o => o.CreatedDate >= start && o.CreatedDate < endExclusive);
            var details = _db.OrderDetails.AsNoTracking()
                .Where(d => d.Order!.CreatedDate >= start && d.Order!.CreatedDate < endExclusive);

            // --- 4 thẻ tổng quan ---
            int totalOrders = await orders.CountAsync();
            decimal totalRevenue = await orders.SumAsync(o => (decimal?)o.FinalAmount) ?? 0m;
            int totalProductsSold = await details.SumAsync(d => (int?)d.Quantity) ?? 0;

            // --- So sánh với kỳ trước (cùng số ngày, liền ngay trước kỳ này) ---
            int days = (end - start).Days + 1;
            var prevStart = start.AddDays(-days);
            decimal previousRevenue = await _db.Orders.AsNoTracking()
                .Where(o => o.CreatedDate >= prevStart && o.CreatedDate < start)
                .SumAsync(o => (decimal?)o.FinalAmount) ?? 0m;
            decimal? revenueChangePercent = previousRevenue > 0
                ? Math.Round((totalRevenue - previousRevenue) / previousRevenue * 100m, 1)
                : null;

            // --- Top 5 bán chạy (theo số lượng, hòa thì xét doanh thu) ---
            var topProducts = await details
                .GroupBy(d => new { d.ProductId, d.Product!.ProductName })
                .Select(g => new
                {
                    productName = g.Key.ProductName,
                    quantitySold = g.Sum(x => x.Quantity),
                    totalAmount = g.Sum(x => x.LineTotal)
                })
                .OrderByDescending(x => x.quantitySold)
                .ThenByDescending(x => x.totalAmount)
                .Take(5)
                .ToListAsync();

            // --- Doanh thu theo ngày (ngày không có đơn vẫn hiện 0 để biểu đồ liên tục) ---
            var byDay = await orders
                .GroupBy(o => o.CreatedDate.Date)
                .Select(g => new { date = g.Key, orders = g.Count(), revenue = g.Sum(x => x.FinalAmount) })
                .ToListAsync();

            var dailyRevenue = new List<object>();
            if (days <= 92)
            {
                var map = byDay.ToDictionary(x => x.date);
                for (var day = start; day <= end; day = day.AddDays(1))
                    dailyRevenue.Add(map.TryGetValue(day, out var v)
                        ? new { date = day, v.orders, v.revenue }
                        : new { date = day, orders = 0, revenue = 0m });
            }
            else
            {
                dailyRevenue.AddRange(byDay.OrderBy(x => x.date).Select(x => (object)x));
            }

            // --- Theo thu ngân ---
            var byCashier = await orders
                .GroupBy(o => o.CashierUsername)
                .Select(g => new { cashier = g.Key, orders = g.Count(), revenue = g.Sum(x => x.FinalAmount) })
                .OrderByDescending(x => x.revenue)
                .ToListAsync();

            // --- Theo nhóm hàng (kèm % đóng góp doanh thu) ---
            var catRaw = await details
                .GroupBy(d => d.Product!.Category!.CategoryName)
                .Select(g => new { category = g.Key, quantity = g.Sum(x => x.Quantity), revenue = g.Sum(x => x.LineTotal) })
                .OrderByDescending(x => x.revenue)
                .ToListAsync();
            decimal catTotal = catRaw.Sum(x => x.revenue);
            var byCategory = catRaw.Select(x => new
            {
                x.category,
                x.quantity,
                x.revenue,
                percent = catTotal > 0 ? Math.Round(x.revenue / catTotal * 100m, 1) : 0m
            }).ToList();

            // --- Hàng sắp hết (không phụ thuộc khoảng ngày) ---
            var lowStockProducts = await _db.Products.AsNoTracking()
                .Where(p => p.StockQuantity <= LowStockThreshold)
                .OrderBy(p => p.StockQuantity).ThenBy(p => p.ProductName)
                .Take(30)
                .Select(p => new { productName = p.ProductName, stockQuantity = p.StockQuantity })
                .ToListAsync();

            return Ok(new
            {
                totalRevenue,
                totalOrders,
                totalProductsSold,
                previousRevenue,
                revenueChangePercent,
                topProducts,
                dailyRevenue,
                byCashier,
                byCategory,
                lowStockProducts
            });
        }

        // GET api/reports/orders?fromDate=&toDate=  - danh sách hóa đơn trong kỳ (mới nhất trước, tối đa 300)
        [HttpGet("orders")]
        public async Task<IActionResult> GetOrders([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            var start = (fromDate ?? DateTime.Today).Date;
            var end = (toDate ?? DateTime.Today).Date;
            if (start > end)
                return BadRequest(new { message = "Ngày bắt đầu không được lớn hơn ngày kết thúc." });
            var endExclusive = end.AddDays(1);

            var list = await _db.Orders.AsNoTracking()
                .Where(o => o.CreatedDate >= start && o.CreatedDate < endExclusive)
                .OrderByDescending(o => o.CreatedDate)
                .Take(300)
                .Select(o => new
                {
                    o.OrderId,
                    o.OrderCode,
                    o.CreatedDate,
                    customerName = o.Customer!.CustomerName,
                    cashier = o.CashierUsername,
                    o.TotalItems,
                    o.FinalAmount
                })
                .ToListAsync();
            return Ok(list);
        }

        // GET api/reports/orders/5  - chi tiết 1 hóa đơn (các dòng hàng)
        [HttpGet("orders/{id:int}")]
        public async Task<IActionResult> GetOrderDetail(int id)
        {
            var o = await _db.Orders.AsNoTracking()
                .Where(x => x.OrderId == id)
                .Select(x => new
                {
                    x.OrderCode,
                    x.CreatedDate,
                    customerName = x.Customer!.CustomerName,
                    cashier = x.CashierUsername,
                    x.TotalItems,
                    x.FinalAmount,
                    x.RewardPoints,
                    lines = x.OrderDetails.Select(d => new
                    {
                        productName = d.Product!.ProductName,
                        d.Quantity,
                        d.UnitPrice,
                        d.LineTotal
                    }).ToList()
                })
                .FirstOrDefaultAsync();
            if (o == null) return NotFound(new { message = "Không tìm thấy hóa đơn!" });
            return Ok(o);
        }

        // GET api/reports/daily?date=2026-10-07  - báo cáo nhanh 1 ngày (dữ liệu thật)
        [HttpGet("daily")]
        public async Task<IActionResult> GetDaily([FromQuery] DateTime? date)
        {
            var day = (date ?? DateTime.Today).Date;
            var next = day.AddDays(1);

            var orders = _db.Orders.AsNoTracking().Where(o => o.CreatedDate >= day && o.CreatedDate < next);
            int totalOrders = await orders.CountAsync();
            decimal totalRevenue = await orders.SumAsync(o => (decimal?)o.FinalAmount) ?? 0m;

            var best = await _db.OrderDetails.AsNoTracking()
                .Where(d => d.Order!.CreatedDate >= day && d.Order!.CreatedDate < next)
                .GroupBy(d => d.Product!.ProductName)
                .Select(g => new { name = g.Key, qty = g.Sum(x => x.Quantity) })
                .OrderByDescending(x => x.qty)
                .FirstOrDefaultAsync();

            return Ok(new
            {
                date = day.ToString("yyyy-MM-dd"),
                totalOrders,
                totalRevenue,
                bestSeller = best?.name ?? "Chưa có dữ liệu",
                bestSellerQuantity = best?.qty ?? 0,
                isDemoData = false
            });
        }
        // BUỔI 5 - TIẾT 3: BÁO CÁO DOANH THU CA BÁN LẺ (STORED PROCEDURE)
        // GET: api/reports/revenue-by-cashier
        // Chức năng: Gọi Stored Procedure sp_GetRevenueByCashier qua SqlDataReader

        [HttpGet("revenue-by-cashier")]
        [AllowAnonymous] // Cho phép kiểm thử trực tiếp trên Swagger UI theo yêu cầu Tiết 3
        public IActionResult GetRevenueByCashier()
        {
            var reportList = new List<CashierRevenueDto>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetRevenueByCashier", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new CashierRevenueDto
                                {
                                    CashierUsername = reader["CashierUsername"].ToString() ?? string.Empty,
                                    TotalOrders = Convert.ToInt32(reader["TotalOrders"]),
                                    TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]),
                                    TotalPointsAwarded = reader["TotalPointsAwarded"] != DBNull.Value
                                        ? Convert.ToInt32(reader["TotalPointsAwarded"])
                                        : 0
                                };

                                reportList.Add(item);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        return StatusCode(500, new
                        {
                            success = false,
                            message = "Lỗi thực thi Stored Procedure trên SQL Server: " + ex.Message
                        });
                    }
                }
            }

            return Ok(new
            {
                success = true,
                generatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                totalCashiers = reportList.Count,
                grandTotalRevenue = reportList.Sum(x => x.TotalRevenue),
                data = reportList
            });
        }

        [HttpGet("revenue-by-date-range")]
        [AllowAnonymous]
        public IActionResult GetRevenueByDateRange([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var reportList = new List<object>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetRevenueByDateRange", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                    command.Parameters.AddWithValue("@ToDate", toDate.Date.AddDays(1).AddSeconds(-1));

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reportList.Add(new
                                {
                                    cashierUsername = reader["CashierUsername"].ToString() ?? string.Empty,
                                    totalOrders = Convert.ToInt32(reader["TotalOrders"]),
                                    totalRevenue = Convert.ToDecimal(reader["TotalRevenue"])
                                });
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        return StatusCode(500, new { success = false, message = "Lỗi SQL: " + ex.Message });
                    }
                }
            }

            return Ok(new
            {
                success = true,
                fromDate = fromDate.ToString("yyyy-MM-dd"),
                toDate = toDate.ToString("yyyy-MM-dd"),
                data = reportList
            });
        }
    }
}
