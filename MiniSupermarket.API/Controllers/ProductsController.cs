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
        private IQueryable<object> Project(IQueryable<Product> q) => q.Select(p => (object)new
        {
            p.ProductId,
            p.Barcode,
            p.ProductName,
            p.Price,
            p.StockQuantity,
            p.CategoryId,
            CategoryName = p.Category != null ? p.Category.CategoryName : ""
        });

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
}
