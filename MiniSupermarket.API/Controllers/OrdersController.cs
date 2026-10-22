using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Cashier")]
    public class OrdersController : ControllerBase
    {
        // Id của "Khách lẻ" trong seed (Customer 21) - dùng khi khách không đưa số điện thoại thành viên
        private const int WalkInCustomerId = 21;

        private readonly SupermarketDbContext _db;
        public OrdersController(SupermarketDbContext db) { _db = db; }

        // POST api/orders/checkout
        //  - kiểm tra tồn kho, TRỪ tồn kho
        //  - LƯU hóa đơn (Orders) + từng dòng hàng (OrderDetails) -> báo cáo doanh thu dùng dữ liệu thật
        //  - cộng điểm thưởng cho khách thành viên (10.000đ = 1 điểm)
        // Giá lấy từ DB, thu ngân lấy từ JWT: không tin dữ liệu client gửi lên.
        // Toàn bộ chạy trong 1 transaction: lỗi giữa chừng thì không trừ kho, không mất hóa đơn.
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest req)
        {
            if (req.Items == null || req.Items.Count == 0)
                return BadRequest(new { message = "Giỏ hàng trống!" });
            if (req.Items.Any(i => i.Quantity <= 0))
                return BadRequest(new { message = "Số lượng phải lớn hơn 0!" });

            // Gộp các dòng trùng sản phẩm để kiểm tra tồn kho theo TỔNG số lượng
            var lines = req.Items.GroupBy(i => i.ProductId)
                .Select(g => new { ProductId = g.Key, Quantity = g.Sum(x => x.Quantity) })
                .ToList();

            var ids = lines.Select(l => l.ProductId).ToList();
            var products = await _db.Products.Where(p => ids.Contains(p.ProductId)).ToDictionaryAsync(p => p.ProductId);

            foreach (var l in lines)
            {
                if (!products.TryGetValue(l.ProductId, out var p))
                    return BadRequest(new { message = $"Sản phẩm ID {l.ProductId} không tồn tại!" });
                if (p.StockQuantity < l.Quantity)
                    return BadRequest(new { message = $"'{p.ProductName}' chỉ còn {p.StockQuantity} trong kho!" });
            }

            // Khách thành viên (theo SĐT) hoặc khách lẻ
            Customer? member = null;
            if (!string.IsNullOrWhiteSpace(req.CustomerPhone))
                member = await _db.Customers.FirstOrDefaultAsync(x => x.PhoneNumber == req.CustomerPhone.Trim());

            var customer = member ?? await _db.Customers.FirstOrDefaultAsync(x => x.CustomerId == WalkInCustomerId);
            if (customer == null)
                return BadRequest(new { message = "Chưa có dữ liệu 'Khách lẻ' (Id 21). Hãy chạy Update-Database." });

            await using var tx = await _db.Database.BeginTransactionAsync();

            var now = DateTime.Now;
            var prefix = $"HD{now:yyyyMMdd}-";
            int todayCount = await _db.Orders.CountAsync(o => o.OrderCode.StartsWith(prefix));

            var order = new Order
            {
                OrderCode = prefix + (todayCount + 1).ToString("000"),
                CreatedDate = now,
                CustomerId = customer.CustomerId,
                CashierUsername = User.Identity?.Name ?? req.CashierUsername ?? "unknown"
            };

            decimal total = 0;
            int totalItems = 0;
            foreach (var l in lines)
            {
                var p = products[l.ProductId];
                p.StockQuantity -= l.Quantity;

                decimal lineTotal = p.Price * l.Quantity;
                total += lineTotal;
                totalItems += l.Quantity;

                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = p.ProductId,
                    Quantity = l.Quantity,
                    UnitPrice = p.Price,
                    LineTotal = lineTotal
                });
            }

            int earned = member != null ? (int)(total / 10000m) : 0;
            if (member != null) member.RewardPoints += earned;

            order.TotalItems = totalItems;
            order.FinalAmount = total;
            order.RewardPoints = earned;

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            await tx.CommitAsync();

            return Ok(new
            {
                orderId = order.OrderId,
                orderCode = order.OrderCode,
                totalAmount = total,
                pointsEarned = earned,
                cashier = order.CashierUsername
            });
        }
    }

    public class CheckoutRequest
    {
        public string? CashierUsername { get; set; }
        public string? CustomerPhone { get; set; }
        public List<CheckoutItem> Items { get; set; } = new();
    }

    public class CheckoutItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}