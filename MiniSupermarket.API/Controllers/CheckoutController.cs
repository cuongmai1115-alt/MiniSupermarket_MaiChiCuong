/*
 * Họ tên sinh viên: Mai Chí Cường
 * Mã sinh viên: Buổi 5 Thực hành
 * Ngày tạo: 22/10/2026
 * Mô tả: API Thanh toán POS đa bảng được bảo vệ bởi SqlTransaction (ACID)
 */
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        // Tiêm IConfiguration để đọc chuỗi kết nối CSDL
        public CheckoutController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // =========================================================================
        // POST: api/checkout
        // Chức năng: Thanh toán hóa đơn, trừ tồn kho và tích điểm trong 1 Transaction
        // =========================================================================
        [HttpPost]
        public async Task<IActionResult> ProcessCheckout([FromBody] CheckoutRequestDto request)
        {
            // 1. Kiểm tra tính hợp lệ của giỏ hàng
            if (request.Items == null || request.Items.Count == 0)
            {
                return BadRequest(new { message = "Giỏ hàng rỗng, không thể tiến hành thanh toán!" });
            }

            if (string.IsNullOrWhiteSpace(request.CashierUsername))
            {
                return BadRequest(new { message = "Thiếu thông tin tài khoản thu ngân lập hóa đơn!" });
            }

            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                // 2. KHỞI TẠO TRANSACTION (Bắt đầu phiên giao dịch khép kín bảo vệ ACID)
                //47.
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Tính toán tổng tiền, tổng số lượng và điểm thưởng tích lũy
                    // Xác định CustomerId (hỗ trợ cả truyền Id hoặc SĐT)
                    int customerId = request.CustomerId;
                    if (customerId <= 0 && !string.IsNullOrWhiteSpace(request.CustomerPhone))
                    {
                        string getCustSql = "SELECT CustomerId FROM Customers WHERE PhoneNumber = @Phone";
                        using var cmdCust = new SqlCommand(getCustSql, connection, transaction);
                        cmdCust.Parameters.AddWithValue("@Phone", request.CustomerPhone.Trim());
                        object? valCust = await cmdCust.ExecuteScalarAsync();
                        if (valCust != null && valCust != DBNull.Value)
                            customerId = Convert.ToInt32(valCust);
                    }
                    if (customerId <= 0) customerId = 21; // Mặc định khách lẻ
                    int totalItems = 0;

                    totalItems = request.Items.Sum(i => i.Quantity);
                    decimal subTotal = request.Items.Sum(i => i.Quantity * i.UnitPrice);

                    // TIÊU CHÍ 47: ĐỔI ĐIỂM THƯỞNG TRỪ TIỀN HÓA ĐƠN (1 điểm = 1.000 đ)
                    decimal discountAmount = 0;
                    int pointsToRedeem = Math.Max(0, request.PointsToRedeem);
                    if (pointsToRedeem > 0 && customerId > 0 && customerId != 21)
                    {
                        string checkPointsSql = "SELECT RewardPoints FROM Customers WHERE CustomerId = @CustomerId";
                        int currentPoints = 0;
                        using (SqlCommand cmdPoints = new SqlCommand(checkPointsSql, connection, transaction))
                        {
                            cmdPoints.Parameters.AddWithValue("@CustomerId", customerId);
                            object? val = await cmdPoints.ExecuteScalarAsync();
                            if (val != null && val != DBNull.Value) currentPoints = Convert.ToInt32(val);
                        }

                        if (currentPoints < pointsToRedeem)
                        {
                            throw new InvalidOperationException($"Khách hàng không đủ điểm tích lũy! (Hiện có: {currentPoints} điểm, yêu cầu đổi: {pointsToRedeem} điểm)");
                        }

                        discountAmount = pointsToRedeem * 1000m;
                    }

                    decimal finalAmount = Math.Max(0, subTotal - discountAmount);
                    totalItems = request.Items.Sum(i => i.Quantity);

                    // Quy tắc tích điểm siêu thị: Cứ mỗi 10.000đ mua hàng được tặng 1 điểm thưởng
                    int rewardPoints = (int)(finalAmount / 10000);

                    // Sinh mã hóa đơn theo cấu trúc: HD-YYYYMMDD-XXXX (4 ký tự ngẫu nhiên)
                    string orderCode = $"HD-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";

      
                    // BƯỚC 1: TẠO HÓA ĐƠN TỔNG THỂ TRONG BẢNG Orders
      
                    // Sử dụng OUTPUT INSERTED.OrderId để lấy về giá trị khóa chính vừa tự động tăng
                    string insertOrderSql = @"
                        INSERT INTO Orders (OrderCode, CreatedDate, CashierUsername, CustomerId, TotalItems, FinalAmount, RewardPoints)
                        OUTPUT INSERTED.OrderId
                        VALUES (@OrderCode, GETDATE(), @CashierUsername, @CustomerId, @TotalItems, @FinalAmount, @RewardPoints)";

                    int orderId = 0;
                    using (SqlCommand cmdOrder = new SqlCommand(insertOrderSql, connection, transaction))
                    {
                        // Gán tham số an toàn
                        cmdOrder.Parameters.AddWithValue("@OrderCode", orderCode);
                        cmdOrder.Parameters.AddWithValue("@CashierUsername", request.CashierUsername.Trim());
                        cmdOrder.Parameters.AddWithValue("@CustomerId", customerId);
                        cmdOrder.Parameters.AddWithValue("@TotalItems", totalItems);
                        cmdOrder.Parameters.AddWithValue("@FinalAmount", finalAmount);
                        cmdOrder.Parameters.AddWithValue("@RewardPoints", rewardPoints);

                        // ExecuteScalar() trả về giá trị OrderId vừa được hệ thống sinh ra
                        orderId = Convert.ToInt32(await cmdOrder.ExecuteScalarAsync());
                    }

                    // =====================================================================
                    // BƯỚC 2 & 3: XỬ LÝ TỪNG MÓN HÀNG (KIỂM TRA, LƯU CHI TIẾT VÀ TRỪ TỒN KHO)
                    // =====================================================================
                    foreach (var item in request.Items)
                    {
                        // 3.1. Kiểm tra số lượng tồn kho thực tế trước khi trừ
                        string checkStockSql = "SELECT StockQuantity, ProductName FROM Products WHERE ProductId = @ProductId";
                        int currentStock = 0;
                        string productName = string.Empty;

                        using (SqlCommand cmdCheck = new SqlCommand(checkStockSql, connection, transaction))
                        {
                            cmdCheck.Parameters.AddWithValue("@ProductId", item.ProductId);
                            using (SqlDataReader reader = await cmdCheck.ExecuteReaderAsync())
                            {
                                if (!await reader.ReadAsync())
                                {
                                    throw new InvalidOperationException($"Không tìm thấy sản phẩm có ID: {item.ProductId}");
                                }
                                currentStock = Convert.ToInt32(reader["StockQuantity"]);
                                productName = reader["ProductName"].ToString() ?? string.Empty;
                            }
                        }

                        // 3.2. ĐIỂM KIỂM SOÁT LOGIC: Nếu số lượng mua vượt quá tồn kho -> Kích hoạt Exception
                        if (currentStock < item.Quantity)
                        {
                            throw new InvalidOperationException(
                                $"Sản phẩm '{productName}' (ID: {item.ProductId}) không đủ tồn kho! " +
                                $"(Tồn kho thực tế: {currentStock}, Số lượng yêu cầu mua: {item.Quantity})");
                        }

                        // 3.3. Thêm dòng chi tiết vào bảng OrderDetails
                        string insertDetailSql = @"
                            INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice, LineTotal)
                            VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice, @LineTotal)";

                        using (SqlCommand cmdDetail = new SqlCommand(insertDetailSql, connection, transaction))
                        {
                            cmdDetail.Parameters.AddWithValue("@OrderId", orderId);
                            cmdDetail.Parameters.AddWithValue("@ProductId", item.ProductId);
                            cmdDetail.Parameters.AddWithValue("@Quantity", item.Quantity);
                            cmdDetail.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                            cmdDetail.Parameters.AddWithValue("@LineTotal", item.Quantity * item.UnitPrice);
                            await cmdDetail.ExecuteNonQueryAsync();
                        }

                        // 3.4. Khấu trừ số lượng tồn kho trong bảng Products
                        string updateStockSql = @"
                            UPDATE Products
                            SET StockQuantity = StockQuantity - @Qty
                            WHERE ProductId = @ProductId";

                        using (SqlCommand cmdStock = new SqlCommand(updateStockSql, connection, transaction))
                        {
                            cmdStock.Parameters.AddWithValue("@Qty", item.Quantity);
                            cmdStock.Parameters.AddWithValue("@ProductId", item.ProductId);
                            await cmdStock.ExecuteNonQueryAsync();
                        }
                    }

                    // =====================================================================
                    // BƯỚC 4: CỘNG ĐIỂM TÍCH LŨY CHO KHÁCH HÀNG TRONG BẢNG Customers
                    // =====================================================================


                    // Tiêu chí 47: Trừ điểm đổi của khách hàng trong cùng SqlTransaction
                    if (pointsToRedeem > 0 && customerId > 0 && customerId != 21)
                    {
                        string deductPointsSql = @"
                            UPDATE Customers
                            SET RewardPoints = RewardPoints - @PointsToRedeem
                            WHERE CustomerId = @CustomerId";

                        using (SqlCommand cmdDeduct = new SqlCommand(deductPointsSql, connection, transaction))
                        {
                            cmdDeduct.Parameters.AddWithValue("@PointsToRedeem", pointsToRedeem);
                            cmdDeduct.Parameters.AddWithValue("@CustomerId", customerId);
                            await cmdDeduct.ExecuteNonQueryAsync();
                        }
                    }

                    if (rewardPoints > 0 && customerId > 0 && customerId != 21)
                    {
                        string updatePointsSql = @"
                            UPDATE Customers
                            SET RewardPoints = RewardPoints + @Points
                            WHERE CustomerId = @CustomerId";

                        using (SqlCommand cmdCustomer = new SqlCommand(updatePointsSql, connection, transaction))
                        {
                            cmdCustomer.Parameters.AddWithValue("@Points", rewardPoints);
                            cmdCustomer.Parameters.AddWithValue("@CustomerId", customerId);
                            await cmdCustomer.ExecuteNonQueryAsync();
                        }
                    }

                    // =====================================================================
                    // BƯỚC 5: XÁC NHẬN LƯU VĨNH VIỄN TOÀN BỘ THAO TÁC (COMMIT)
                    // =====================================================================
                    await transaction.CommitAsync();

                    // Trả về kết quả thành công với mã 200 OK
                    return Ok(new
                    {
                        success = true,
                        message = pointsToRedeem > 0
                            ? $"Thanh toán thành công! Đã dùng {pointsToRedeem} điểm trừ {discountAmount:N0} đ vào hóa đơn."
                            : "Thanh toán hóa đơn POS thành công!",
                        subTotal = subTotal,
                        discountAmount = discountAmount,
                        pointsRedeemed = pointsToRedeem,
                        orderId = orderId,
                        orderCode = orderCode,
                        totalItems = totalItems,
                        finalAmount = finalAmount,
                        rewardPointsAdded = rewardPoints
                    });
                }
                catch (Exception ex)
                {
                    // BƯỚC 6: HOÀN TÁC TOÀN BỘ DỮ LIỆU SẠCH SẼ KHI CÓ BẤT KỲ LỖI NÀO (ROLLBACK)
                    try
                    {
                        await transaction.RollbackAsync();
                    }
                    catch (Exception rollbackEx)
                    {
                        return StatusCode(500, new
                        {
                            success = false,
                            message = "Lỗi nghiêm trọng khi thực thi Rollback: " + rollbackEx.Message
                        });
                    }

                    // Trả về thông báo lỗi thân thiện và mã 400 BadRequest
                    return BadRequest(new
                    {
                        success = false,
                        message = "Giao dịch thanh toán bị hủy và đã được Rollback an toàn!",
                        errorDetail = ex.Message
                    });
                }
            }
        }

        // =========================================================================
        // BÀI TẬP MỞ RỘNG (TIẾT 5): HỦY ĐƠN HÀNG VÀ HOÀN TRẢ TỒN KHO
        // DELETE: api/checkout/cancel/{orderId}
        // =========================================================================
        [HttpDelete("cancel/{orderId}")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // 1. Kiểm tra hóa đơn và lấy thông tin khách hàng, điểm thưởng
                    string getOrderSql = "SELECT CustomerId, RewardPoints FROM Orders WHERE OrderId = @OrderId";
                    int customerId = 0;
                    int pointsToDeduct = 0;

                    using (SqlCommand cmdGet = new SqlCommand(getOrderSql, connection, transaction))
                    {
                        cmdGet.Parameters.AddWithValue("@OrderId", orderId);
                        using (SqlDataReader reader = await cmdGet.ExecuteReaderAsync())
                        {
                            if (!reader.Read())
                            {
                                throw new InvalidOperationException($"Không tìm thấy hóa đơn ID = {orderId} để hủy!");
                            }

                            customerId = Convert.ToInt32(reader["CustomerId"]);
                            pointsToDeduct = Convert.ToInt32(reader["RewardPoints"]);
                        }
                    }

                    // 2. Lấy danh sách sản phẩm đã mua để hoàn trả kho
                    var itemsToRestore = new List<(int ProductId, int Quantity)>();
                    string getDetailsSql = "SELECT ProductId, Quantity FROM OrderDetails WHERE OrderId = @OrderId";

                    using (SqlCommand cmdDetails = new SqlCommand(getDetailsSql, connection, transaction))
                    {
                        cmdDetails.Parameters.AddWithValue("@OrderId", orderId);
                        using (SqlDataReader reader = await cmdDetails.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                itemsToRestore.Add((
                                    Convert.ToInt32(reader["ProductId"]),
                                    Convert.ToInt32(reader["Quantity"])
                                ));
                            }
                        }
                    }

                    // 3. Hoàn trả tồn kho trong bảng Products
                    foreach (var item in itemsToRestore)
                    {
                        string restoreStockSql = @"
                            UPDATE Products
                            SET StockQuantity = StockQuantity + @Qty
                            WHERE ProductId = @ProductId";

                        using (SqlCommand cmdRestore = new SqlCommand(restoreStockSql, connection, transaction))
                        {
                            cmdRestore.Parameters.AddWithValue("@Qty", item.Quantity);
                            cmdRestore.Parameters.AddWithValue("@ProductId", item.ProductId);
                            await cmdRestore.ExecuteNonQueryAsync();
                        }
                    }

                    // 4. Khấu trừ điểm thưởng đã tích của khách hàng
                    if (pointsToDeduct > 0)
                    {
                        string deductPointsSql = @"
                            UPDATE Customers
                            SET RewardPoints = CASE 
                                WHEN RewardPoints >= @Pts THEN RewardPoints - @Pts 
                                ELSE 0 
                            END
                            WHERE CustomerId = @CustomerId";

                        using (SqlCommand cmdDeduct = new SqlCommand(deductPointsSql, connection, transaction))
                        {
                            cmdDeduct.Parameters.AddWithValue("@Pts", pointsToDeduct);
                            cmdDeduct.Parameters.AddWithValue("@CustomerId", customerId);
                            await cmdDeduct.ExecuteNonQueryAsync();
                        }
                    }

                    // 5. Xóa chi tiết hóa đơn trước (tránh vi phạm ràng buộc khóa ngoại)
                    string deleteDetailsSql = "DELETE FROM OrderDetails WHERE OrderId = @OrderId";
                    using (SqlCommand cmdDelDetails = new SqlCommand(deleteDetailsSql, connection, transaction))
                    {
                        cmdDelDetails.Parameters.AddWithValue("@OrderId", orderId);
                        await cmdDelDetails.ExecuteNonQueryAsync();
                    }

                    // 6. Xóa hóa đơn tổng trong bảng Orders
                    string deleteOrderSql = "DELETE FROM Orders WHERE OrderId = @OrderId";
                    using (SqlCommand cmdDelOrder = new SqlCommand(deleteOrderSql, connection, transaction))
                    {
                        cmdDelOrder.Parameters.AddWithValue("@OrderId", orderId);
                        await cmdDelOrder.ExecuteNonQueryAsync();
                    }

                    // 7. Xác nhận lưu vĩnh viễn (Commit)
                    await transaction.CommitAsync();

                    return Ok(new
                    {
                        success = true,
                        message = $"Đã hủy thành công hóa đơn #{orderId} và hoàn trả toàn bộ tồn kho vào CSDL!"
                    });
                }
                catch (Exception ex)
                {
                    // Hoàn tác sạch sẽ khi có lỗi
                    try
                    {
                        await transaction.RollbackAsync();
                    }
                    catch { }

                    return BadRequest(new
                    {
                        success = false,
                        message = "Hủy hóa đơn thất bại, giao dịch đã được Rollback!",
                        errorDetail = ex.Message
                    });
                }
            }
        }
    }
}
