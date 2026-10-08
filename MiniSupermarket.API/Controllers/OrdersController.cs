using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Cashier")]
    public class OrdersController : ControllerBase
    {
        private readonly SupermarketDbContext _db;
        public OrdersController(SupermarketDbContext db) { _db = db; }

        // POST api/orders/checkout
        // Hệ thống chưa có bảng Orders/OrderDetails (Buổi 5) nên checkout hiện tại:
        //  - kiểm tra tồn kho, TRỪ tồn kho
        //  - cộng điểm thưởng cho khách (10.000đ = 1 điểm)
        // Giá luôn lấy từ DB, không tin giá client gửi lên.
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest req)
        {
            if (req.Items == null || req.Items.Count == 0)
                return BadRequest(new { message = "Giỏ hàng trống!" });

            var ids = req.Items.Select(i => i.ProductId).Distinct().ToList();
            var products = await _db.Products.Where(p => ids.Contains(p.ProductId)).ToDictionaryAsync(p => p.ProductId);

            decimal total = 0;
            foreach (var item in req.Items)
            {
                if (item.Quantity <= 0)
                    return BadRequest(new { message = "Số lượng phải lớn hơn 0!" });
                if (!products.TryGetValue(item.ProductId, out var p))
                    return BadRequest(new { message = $"Sản phẩm ID {item.ProductId} không tồn tại!" });
                if (p.StockQuantity < item.Quantity)
                    return BadRequest(new { message = $"'{p.ProductName}' chỉ còn {p.StockQuantity} trong kho!" });
            }

            foreach (var item in req.Items)
            {
                var p = products[item.ProductId];
                p.StockQuantity -= item.Quantity;
                total += p.Price * item.Quantity;
            }

            int earned = 0;
            if (!string.IsNullOrWhiteSpace(req.CustomerPhone))
            {
                var c = await _db.Customers.FirstOrDefaultAsync(x => x.PhoneNumber == req.CustomerPhone.Trim());
                if (c != null)
                {
                    earned = (int)(total / 10000m);
                    c.RewardPoints += earned;
                }
            }

            await _db.SaveChangesAsync();
            return Ok(new { totalAmount = total, pointsEarned = earned, cashier = User.Identity?.Name });
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
