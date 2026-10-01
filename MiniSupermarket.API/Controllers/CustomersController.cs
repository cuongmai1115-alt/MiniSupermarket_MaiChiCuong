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
    public class CustomersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public CustomersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.OrderBy(c => c.CustomerId).ToListAsync();
        }

        // GET: api/customers/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound(new { message = $"Không tìm thấy khách hàng có ID = {id}." });
            return customer;
        }

        // GET: api/customers/search?keyword=abc
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Customer>>> Search([FromQuery] string? keyword)
        {
            var query = _context.Customers.AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                query = query.Where(c => c.CustomerName.Contains(keyword) || c.PhoneNumber.Contains(keyword));
            }
            return await query.OrderBy(c => c.CustomerId).ToListAsync();
        }

        // POST: api/customers
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            if (await _context.Customers.AnyAsync(c => c.PhoneNumber == customer.PhoneNumber))
                return Conflict(new { message = $"Số điện thoại {customer.PhoneNumber} đã tồn tại." });

            customer.CustomerId = 0; // để SQL Server tự tăng
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerId }, customer);
        }

        // PUT: api/customers/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerId)
                return BadRequest(new { message = "ID trên đường dẫn không khớp với ID trong dữ liệu gửi lên." });

            var existing = await _context.Customers.FindAsync(id);
            if (existing == null)
                return NotFound(new { message = $"Không tìm thấy khách hàng có ID = {id}." });

            if (await _context.Customers.AnyAsync(c => c.PhoneNumber == customer.PhoneNumber && c.CustomerId != id))
                return Conflict(new { message = $"Số điện thoại {customer.PhoneNumber} đã thuộc về khách hàng khác." });

            existing.CustomerName = customer.CustomerName;
            existing.PhoneNumber = customer.PhoneNumber;
            existing.Address = customer.Address;
            existing.RewardPoints = customer.RewardPoints;
            existing.MembershipRank = customer.MembershipRank;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/customers/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound(new { message = $"Không tìm thấy khách hàng có ID = {id}." });

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}