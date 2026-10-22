/*
 * Họ tên sinh viên: Mai Chí Cường
 * Mã sinh viên: Buổi 5 Thực hành
 * Ngày tạo: 22/10/2026
 * Mô tả: Controller ADO.NET thuần - Tra cứu mã vạch siêu tốc và demo DataTable
 */
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MiniSupermarket.API.Controllers
{
    // DTO tra cứu sản phẩm cho quầy POS
    public class ProductLookupDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }

    [Route("api/ado-products")]
    [ApiController]
    public class AdoProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        // Constructor tiêm IConfiguration để đọc chuỗi kết nối từ appsettings.json
        public AdoProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // =========================================================================
        // THAO TÁC 1: TRA CỨU THEO MÃ VẠCH (CONNECTED MODE - SQLDATAREADER)
        // =========================================================================
        // GET: api/ado-products/barcode/893456789001
        [HttpGet("barcode/{barcode}")]
        public IActionResult GetProductByBarcode(string barcode)
        {
            // 1. Kiểm tra tính hợp lệ của tham số đầu vào
            if (string.IsNullOrWhiteSpace(barcode))
            {
                return BadRequest(new { message = "Mã vạch không được để trống!" });
            }

            // 2. Lấy chuỗi kết nối tới SQL Server
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            // 3. Sử dụng khối using để đảm bảo SqlConnection luôn được đóng và giải phóng
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // 4. Câu lệnh SELECT có tham số hóa @Barcode chống SQL Injection
                string query = @"SELECT ProductId, Barcode, ProductName, Price, StockQuantity 
                                 FROM Products 
                                 WHERE Barcode = @Barcode";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // 5. Gán giá trị an toàn qua SqlParameter
                    command.Parameters.AddWithValue("@Barcode", barcode.Trim());

                    try
                    {
                        // 6. Mở kết nối vật lý tới SQL Server
                        connection.Open();

                        // 7. Khởi tạo luồng đọc nhanh Forward-Only SqlDataReader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // 8. Đọc bản ghi: reader.Read() trả về true nếu tìm thấy dòng dữ liệu
                            if (reader.Read())
                            {
                                var productDto = new ProductLookupDto
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString() ?? string.Empty,
                                    ProductName = reader["ProductName"].ToString() ?? string.Empty,
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    StockQuantity = Convert.ToInt32(reader["StockQuantity"])
                                };

                                // Trả về mã 200 OK kèm dữ liệu sản phẩm
                                return Ok(productDto);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Bắt lỗi nếu kết nối CSDL gặp sự cố
                        return StatusCode(500, new { message = "Lỗi truy vấn SQL: " + ex.Message });
                    }
                }
            } // Kết thúc using, connection.Close() tự động được thực thi tại đây

            // 9. Nếu không tìm thấy sản phẩm nào khớp mã vạch -> Trả về 404 Not Found
            return NotFound(new { message = $"Không tìm thấy sản phẩm có mã vạch: {barcode}" });
        }

        // =========================================================================
        // THAO TÁC 2: TẢI DANH SÁCH LƯU ĐỆM (DISCONNECTED MODE - DATATABLE & ADAPTER)
        // =========================================================================
        // GET: api/ado-products/disconnected-cache
        [HttpGet("disconnected-cache")]
        public IActionResult GetProductsDisconnected()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            // 1. Khởi tạo bảng ảo DataTable trên RAM
            DataTable productTable = new DataTable("CachedProducts");

            // 2. Mở kết nối và sử dụng SqlDataAdapter để nạp dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT ProductId, Barcode, ProductName, Price, StockQuantity 
                                 FROM Products 
                                 ORDER BY ProductId ASC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // 3. SqlDataAdapter làm cầu nối giữa Command và DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        // 4. Phương thức Fill(): Tự động Open -> Đọc nạp vào DataTable -> Tự động Close
                        adapter.Fill(productTable);
                    }
                }
            } // Tại đây, kết nối CSDL ĐÃ ĐƯỢC ĐÓNG HOÀN TOÀN!

            // 5. Thao tác độc lập trên bộ nhớ RAM (Không cần kết nối tới SQL Server nữa)
            var resultList = new List<ProductLookupDto>();

            // Duyệt qua từng dòng (DataRow) trong tập hợp Rows của DataTable
            foreach (DataRow row in productTable.Rows)
            {
                resultList.Add(new ProductLookupDto
                {
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    Barcode = row["Barcode"].ToString() ?? string.Empty,
                    ProductName = row["ProductName"].ToString() ?? string.Empty,
                    Price = Convert.ToDecimal(row["Price"]),
                    StockQuantity = Convert.ToInt32(row["StockQuantity"])
                });
            }

            // Trả về danh sách sản phẩm đã được lưu đệm trên RAM
            return Ok(new
            {
                totalLoaded = productTable.Rows.Count,
                cachedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                data = resultList
            });
        }

        // =========================================================================
        // BÀI TẬP 2.1: LỌC SẢN PHẨM THEO TỒN KHO TỐI THIỂU (SQLDATAREADER)
        // =========================================================================
        // GET: api/ado-products/filter-stock?minStock=50
        [HttpGet("filter-stock")]
        public IActionResult FilterByStock([FromQuery] int minStock = 50)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;
            var list = new List<ProductLookupDto>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT ProductId, Barcode, ProductName, Price, StockQuantity 
                                 FROM Products 
                                 WHERE StockQuantity <= @MinStock 
                                 ORDER BY StockQuantity ASC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MinStock", minStock);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new ProductLookupDto
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString() ?? string.Empty,
                                    ProductName = reader["ProductName"].ToString() ?? string.Empty,
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    StockQuantity = Convert.ToInt32(reader["StockQuantity"])
                                });
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        return StatusCode(500, new { message = "Lỗi SQL: " + ex.Message });
                    }
                }
            }

            return Ok(new
            {
                threshold = minStock,
                totalFound = list.Count,
                data = list
            });
        }

        // =========================================================================
        // BÀI TẬP 2.2: ĐẾM SỐ LƯỢNG SẢN PHẨM THEO TỪNG NHÓM DANH MỤC (DATATABLE)
        // =========================================================================
        // GET: api/ado-products/count-by-category
        [HttpGet("count-by-category")]
        public IActionResult CountByCategory()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;
            DataTable table = new DataTable("CategoryCounts");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT CategoryId, COUNT(*) AS Total 
                                 FROM Products 
                                 GROUP BY CategoryId 
                                 ORDER BY CategoryId ASC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
            }

            var result = new List<object>();
            foreach (DataRow row in table.Rows)
            {
                result.Add(new
                {
                    categoryId = Convert.ToInt32(row["CategoryId"]),
                    totalProducts = Convert.ToInt32(row["Total"])
                });
            }

            return Ok(result);
        }
    }
}
