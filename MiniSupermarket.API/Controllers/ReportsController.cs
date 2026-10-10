using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // Yêu cầu JWT Token có Role Admin
    public class ReportsController : ControllerBase
    {
        private readonly SupermarketDbContext _db;

        public ReportsController(SupermarketDbContext db)
        {
            _db = db;
        }
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            var start = (fromDate ?? DateTime.Today).Date;
            var end = (toDate ?? DateTime.Today).Date;

            if (start > end)
            {
                return BadRequest("Ngày bắt đầu không được lớn hơn ngày kết thúc.");
            }

            var products = await _db.Products.AsNoTracking().OrderBy(p => p.ProductId).ToListAsync();

            if (products.Count == 0)
            {
                return Ok(new
                {
                    totalRevenue = 0m,
                    totalOrders = 0,
                    totalProductsSold = 0,
                    topProducts = new List<object>()
                });
            }

            decimal totalRevenue = 0;
            int totalOrders = 0;
            int totalProductsSold = 0;
            var productSalesMap = new Dictionary<string, (int Qty, decimal Revenue)>();

            for (var day = start; day <= end; day = day.AddDays(1))
            {
                var rnd = new Random(day.Year * 10000 + day.Month * 100 + day.Day);
                int dayOrders = rnd.Next(20, 81);
                totalOrders += dayOrders;

                for (int i = 0; i < dayOrders; i++)
                {
                    int lines = rnd.Next(1, 5);
                    for (int j = 0; j < lines; j++)
                    {
                        var p = products[rnd.Next(products.Count)];
                        int qty = rnd.Next(1, 4);
                        decimal lineTotal = p.Price * qty;

                        totalRevenue += lineTotal;
                        totalProductsSold += qty;

                        if (!productSalesMap.ContainsKey(p.ProductName))
                        {
                            productSalesMap[p.ProductName] = (0, 0m);
                        }

                        var current = productSalesMap[p.ProductName];
                        productSalesMap[p.ProductName] = (current.Qty + qty, current.Revenue + lineTotal);
                    }
                }
            }

            var topProducts = productSalesMap
                .Select(kv => new
                {
                    productName = kv.Key,
                    quantitySold = kv.Value.Qty,
                    totalAmount = kv.Value.Revenue
                })
                .OrderByDescending(x => x.quantitySold)
                .Take(5)
                .ToList();

            return Ok(new
            {
                totalRevenue,
                totalOrders,
                totalProductsSold,
                topProducts
            });
        }

        [HttpGet("daily")]
        public async Task<IActionResult> GetDaily([FromQuery] DateTime? date)
        {
            var day = (date ?? DateTime.Today).Date;
            var products = await _db.Products.AsNoTracking().OrderBy(p => p.ProductId).ToListAsync();

            if (products.Count == 0)
            {
                return Ok(new
                {
                    date = day.ToString("yyyy-MM-dd"),
                    totalOrders = 0,
                    totalRevenue = 0m,
                    bestSeller = "Chưa có dữ liệu",
                    bestSellerQuantity = 0,
                    isDemoData = true
                });
            }

            var rnd = new Random(day.Year * 10000 + day.Month * 100 + day.Day);
            int totalOrders = rnd.Next(20, 81);
            decimal totalRevenue = 0;
            var soldQty = new Dictionary<string, int>();

            for (int i = 0; i < totalOrders; i++)
            {
                int lines = rnd.Next(1, 5);
                for (int j = 0; j < lines; j++)
                {
                    var p = products[rnd.Next(products.Count)];
                    int qty = rnd.Next(1, 4);
                    totalRevenue += p.Price * qty;
                    soldQty[p.ProductName] = soldQty.GetValueOrDefault(p.ProductName) + qty;
                }
            }

            var best = soldQty.OrderByDescending(kv => kv.Value).First();
            return Ok(new
            {
                date = day.ToString("yyyy-MM-dd"),
                totalOrders,
                totalRevenue,
                bestSeller = best.Key,
                bestSellerQuantity = best.Value,
                isDemoData = true
            });
        }
    }
}