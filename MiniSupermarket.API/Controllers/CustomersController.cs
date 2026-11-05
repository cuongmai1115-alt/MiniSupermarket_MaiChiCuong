using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Cashier")] // Admin và Cashier được xem/thêm/sửa khách hàng
    public class CustomersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public CustomersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // =========================================================================
        // BUỔI 6 - BÀI TẬP 6.1: PHÂN TRANG DANH SÁCH KHÁCH HÀNG THÂN THIẾT
        // GET: api/customers/paged?keyword=...&pageNumber=1&pageSize=5
        // =========================================================================
        [HttpGet("paged")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPagedCustomers(
            [FromQuery] string? keyword,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 50) pageSize = 5;

            var query = _context.Customers.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string search = keyword.Trim().ToLower();
                query = query.Where(c => c.CustomerName.ToLower().Contains(search) || c.PhoneNumber.Contains(search));
            }

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var items = await query
                .OrderByDescending(c => c.RewardPoints) // Sắp xếp theo thứ tự giảm dần của điểm thưởng
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CustomerResponseDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    RewardPoints = c.RewardPoints,
                    MembershipRank = c.MembershipRank
                })
                .ToListAsync();

            return Ok(new PagedResult<CustomerResponseDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            });
        }

        // GET: api/customers (Trả về DTO)
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var list = await _context.Customers.AsNoTracking()
                .OrderBy(c => c.CustomerId)
                .Select(c => new CustomerResponseDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    RewardPoints = c.RewardPoints,
                    MembershipRank = c.MembershipRank
                })
                .ToListAsync();
            return Ok(list);
        }

        // GET: api/customers/5 (Trả về DTO)
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound(new { message = $"Không tìm thấy khách hàng có ID = {id}." });

            return Ok(new CustomerResponseDto
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address,
                RewardPoints = customer.RewardPoints,
                MembershipRank = customer.MembershipRank
            });
        }

        // GET: api/customers/search?keyword=abc (Trả về DTO)
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            var query = _context.Customers.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                query = query.Where(c => c.CustomerName.Contains(keyword) || c.PhoneNumber.Contains(keyword));
            }

            var list = await query.OrderBy(c => c.CustomerId)
                .Select(c => new CustomerResponseDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    RewardPoints = c.RewardPoints,
                    MembershipRank = c.MembershipRank
                })
                .ToListAsync();
            return Ok(list);
        }

        // POST: api/customers (Nhận DTO, Trả DTO)
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] CustomerRequestDto customerDto)
        {
            if (string.IsNullOrWhiteSpace(customerDto.CustomerName) || string.IsNullOrWhiteSpace(customerDto.PhoneNumber))
                return BadRequest(new { message = "Tên khách hàng và số điện thoại không được để trống." });

            if (await _context.Customers.AnyAsync(c => c.PhoneNumber == customerDto.PhoneNumber.Trim()))
                return Conflict(new { message = $"Số điện thoại {customerDto.PhoneNumber} đã tồn tại." });

            var customer = new Customer
            {
                CustomerName = customerDto.CustomerName.Trim(),
                PhoneNumber = customerDto.PhoneNumber.Trim(),
                Address = customerDto.Address?.Trim(),
                RewardPoints = Math.Max(0, customerDto.RewardPoints),
                MembershipRank = string.IsNullOrWhiteSpace(customerDto.MembershipRank) ? "Chuẩn" : customerDto.MembershipRank.Trim()
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var responseDto = new CustomerResponseDto
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address,
                RewardPoints = customer.RewardPoints,
                MembershipRank = customer.MembershipRank
            };

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerId }, responseDto);
        }

        // PUT: api/customers/5 (Nhận DTO)
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutCustomer(int id, [FromBody] CustomerRequestDto customerDto)
        {
            if (string.IsNullOrWhiteSpace(customerDto.CustomerName) || string.IsNullOrWhiteSpace(customerDto.PhoneNumber))
                return BadRequest(new { message = "Tên khách hàng và số điện thoại không được để trống." });

            var existing = await _context.Customers.FindAsync(id);
            if (existing == null)
                return NotFound(new { message = $"Không tìm thấy khách hàng có ID = {id}." });

            if (await _context.Customers.AnyAsync(c => c.PhoneNumber == customerDto.PhoneNumber.Trim() && c.CustomerId != id))
                return Conflict(new { message = $"Số điện thoại {customerDto.PhoneNumber} đã thuộc về khách hàng khác." });

            existing.CustomerName = customerDto.CustomerName.Trim();
            existing.PhoneNumber = customerDto.PhoneNumber.Trim();
            existing.Address = customerDto.Address?.Trim();
            existing.RewardPoints = Math.Max(0, customerDto.RewardPoints);
            existing.MembershipRank = string.IsNullOrWhiteSpace(customerDto.MembershipRank) ? "Chuẩn" : customerDto.MembershipRank.Trim();

            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Cập nhật khách hàng thành công!" });
        }

        // DELETE: api/customers/5
        [Authorize(Roles = "Admin")] // Chỉ Admin được xóa khách hàng
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound(new { message = $"Không tìm thấy khách hàng có ID = {id}." });

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Xóa khách hàng thành công!" });
        }
    }

    // ================= DTO CLASSES CHO CUSTOMER =================
    public class CustomerResponseDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; }
        public int RewardPoints { get; set; }
        public string MembershipRank { get; set; } = "Chuẩn";
    }

    public class CustomerRequestDto
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống.")]
        [StringLength(100, ErrorMessage = "Tên khách hàng tối đa 100 ký tự.")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [StringLength(15, ErrorMessage = "Số điện thoại tối đa 15 ký tự.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Địa chỉ tối đa 200 ký tự.")]
        public string? Address { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Điểm thưởng không được âm.")]
        public int RewardPoints { get; set; }

        [StringLength(50, ErrorMessage = "Hạng thẻ tối đa 50 ký tự.")]
        public string MembershipRank { get; set; } = "Chuẩn";
    }
}
