# 🛒 HỆ THỐNG QUẢN LÝ SIÊU THỊ MINI (MINISUPERMARKET MANAGEMENT SYSTEM)
### Đề tài: Phát triển Ứng dụng Thu ngân và Quản lý Hàng hóa cho Mô hình Bán lẻ – Cửa hàng Tiện lợi Alpha Mini

---

## 1. Giới thiệu Tổng quan Đề tài

Trong bối cảnh ngành bán lẻ và chuỗi cửa hàng tiện lợi phát triển nhanh chóng, việc quản trị hàng hóa, kiểm soát tồn kho tức thời và thực hiện thao tác tính tiền nhanh chóng, chính xác tại quầy đóng vai trò quyết định đến hiệu quả kinh doanh và trải nghiệm khách hàng.

Đề tài **"Phát triển Ứng dụng Thu ngân và Quản lý Hàng hóa cho Mô hình Bán lẻ – Cửa hàng Tiện lợi Alpha Mini"** được nghiên cứu và hiện thực nhằm giải quyết trọn vẹn các bài toán nghiệp vụ thực tế của một siêu thị mini:
- **Tốc độ phục vụ:** Tra cứu sản phẩm bằng máy quét mã vạch trong thời gian vài mili-giây, hạn chế tối đa việc khách hàng phải chờ đợi lâu tại quầy thu ngân.
- **Tính toàn vẹn và nhất quán:** Mọi thao tác thanh toán, trừ tồn kho hàng hóa và tích điểm thưởng khách hàng được ràng buộc chặt chẽ trong một phiên giao dịch nguyên tử (ACID Transaction), loại bỏ hoàn toàn nguy cơ sai lệch số liệu kho hoặc bán âm.
- **Phân tách trách nhiệm:** Tách biệt hoàn toàn giữa dịch vụ trung tâm Backend (Web API) và giao diện máy trạm tại quầy Frontend (WinForms POS Client), giúp hệ thống dễ dàng mở rộng nhiều điểm bán (multi-POS) trong tương lai.
- **Bảo mật và phân quyền:** Sử dụng cơ chế mã hóa mật khẩu, xác thực phiên bằng JSON Web Token (JWT) và bảo vệ cơ sở dữ liệu trước các lỗ hổng bảo mật như SQL Injection.

---

## 2. Kiến trúc Hệ thống (Client - Server & Hybrid Data Access)

Hệ thống được thiết kế theo mô hình phân tầng **Client - Server** hiện đại kết hợp cơ chế truy xuất dữ liệu lai (**Hybrid Data Access Architecture**):

```
 ┌────────────────────────────────────────────────────────────────────────┐
 │               MÁY TRẠM THU NGÂN / QUẢN LÝ TẠI QUẦY                     │
 │          MiniSupermarket.WinForms (Frontend Client - .NET 8)           │
 │  ┌───────────────────────┬──────────────────────┬───────────────────┐  │
 │  │ FormLogin (Xác thực)  │ FormMainShell (Menu) │ FormPOS (Bán hàng)│  │
 │  ├───────────────────────┼──────────────────────┼───────────────────┤  │
 │  │ FormProductManagement │ FormCustomerManage   │ FormQuickReport   │  │
 │  └───────────────────────┴──────────────────────┴───────────────────┘  │
 └───────────────────────────────────┬────────────────────────────────────┘
                                     │ HTTP / HTTPS (RESTful API, JSON)
                                     │ Authorization: Bearer <JWT_Token>
                                     ▼
 ┌────────────────────────────────────────────────────────────────────────┐
 │                   MÁY CHỦ DỊCH VỤ TRUNG TÂM                            │
 │             MiniSupermarket.API (ASP.NET Core 8 Web API)               │
 │  ┌──────────────────────────────────────────────────────────────────┐  │
 │  │ Controllers: Auth, Products, Categories, Customers, Orders,      │  │
 │  │              Checkout, AdoProduct, Reports, Users, Roles         │  │
 │  ├─────────────────────────────────┬────────────────────────────────┤  │
 │  │      Entity Framework Core 8    │     ADO.NET Thuần Siêu Tốc     │  │
 │  │  (Quản lý CRUD, Quan hệ bảng,   │ (Quét mã vạch nhanh, Bán hàng  │  │
 │  │   Phân trang, Migration CSDL)   │  POS Transaction, RAM Cache)   │  │
 │  └─────────────────┬───────────────┴────────────────┬───────────────┘  │
 └────────────────────┼────────────────────────────────┼──────────────────┘
                      │ Microsoft.EntityFrameworkCore  │ Microsoft.Data.SqlClient
                      ▼                                ▼
 ┌────────────────────────────────────────────────────────────────────────┐
 │            HỆ QUẢN TRỊ CƠ SỞ DỮ LIỆU SQL SERVER                        │
 │           Cơ sở dữ liệu: ChiCuong_MiniSupermarketDb                    │
 │  ┌───────────────┬────────────────┬───────────────┬─────────────────┐  │
 │  │ Users (NV)    │ Categories     │ Products      │ Customers       │  │
 │  ├───────────────┴────────────────┼───────────────┴─────────────────┤  │
 │  │ Orders (Hóa đơn)               │ OrderDetails (Chi tiết hàng)    │  │
 │  └────────────────────────────────┴─────────────────────────────────┘  │
 └────────────────────────────────────────────────────────────────────────┘
```

### Điểm nhấn kiến trúc:
1. **Frontend Client (`MiniSupermarket.WinForms`):**
   - Đóng vai trò là phần mềm tại quầy tính tiền và máy quản lý cửa hàng.
   - Giao tiếp với Server qua `HttpClient` bất đồng bộ (`async / await`), giúp giao diện người dùng mượt mà, không bị giật lag khi tải lượng dữ liệu lớn.
   - Quản lý phiên làm việc thông qua `SessionManager` lưu trữ thông tin nhân viên, vai trò và JWT Token.
2. **Backend API (`MiniSupermarket.API`):**
   - Đóng vai trò bộ não xử lý toàn bộ logic nghiệp vụ (Business Logic Layer) và cung cấp dữ liệu qua chuẩn RESTful API.
   - Hỗ trợ Swagger UI giúp kiểm thử và tài liệu hóa toàn diện các endpoint.
   - Được bảo vệ bằng Middleware xác thực JWT Bearer Token và phân quyền theo Role (`Admin`, `Cashier`).
3. **Mô hình Truy xuất Dữ liệu Lai (Hybrid Data Access):**
   - **Entity Framework Core 8:** Sử dụng cho các tác vụ quản trị thông thường (CRUD Danh mục, Khách hàng, Sản phẩm, Tài khoản), tận dụng khả năng ánh xạ đối tượng ORM mạnh mẽ và cơ chế LINQ trực quan.
   - **ADO.NET Thuần (`Microsoft.Data.SqlClient`):** Sử dụng chuyên biệt cho quầy thu ngân POS: tra cứu mã vạch siêu tốc (`SqlDataReader`), lưu đệm dữ liệu ngắt kết nối (`DataTable`/`SqlDataAdapter`), và điều khiển giao dịch nguyên tử (`SqlTransaction`) đảm bảo an toàn tuyệt đối khi thanh toán đa bảng.

---

## 3. Cấu trúc Solution & Repository

```text
D:\MiniSupermarket_MaiChiCuong/
│
├── MiniSupermarket_MaiChiCuong.sln       # Solution chính của toàn bộ đồ án
├── README.md                             # Tài liệu tổng hợp đồ án
│
├── MiniSupermarket.API/                  # DỰ ÁN BACKEND (ASP.NET Core 8 Web API)
│   ├── Controllers/                      # Bộ điều khiển tiếp nhận & phản hồi HTTP Requests
│   │   ├── AuthController.cs             # API Xác thực đăng nhập, cấp phát JWT Token
│   │   ├── ProductsController.cs         # API Quản lý hàng hóa, tìm kiếm, phân trang
│   │   ├── CategoriesController.cs       # API Quản lý nhóm ngành hàng
│   │   ├── CustomersController.cs        # API Khách hàng thân thiết & quản lý điểm thưởng
│   │   ├── OrdersController.cs           # API Lịch sử đơn hàng, xem chi tiết hóa đơn
│   │   ├── CheckoutController.cs         # API Xử lý thanh toán quầy POS với SqlTransaction
│   │   ├── AdoProductController.cs       # API Tra cứu mã vạch & cache RAM bằng ADO.NET thuần
│   │   ├── ReportsController.cs          # API Thống kê doanh thu, sản phẩm bán chạy
│   │   ├── UsersController.cs            # API Quản lý danh sách nhân viên
│   │   └── RolesController.cs            # API Danh mục vai trò trong hệ thống
│   │
│   ├── Data/
│   │   └── SupermarketDbContext.cs       # DbContext Entity Framework Core, cấu hình Fluent API & Seed Data
│   │
│   ├── DTOs/                             # Data Transfer Objects trao đổi dữ liệu an toàn
│   │   ├── CheckoutDtos.cs               # DTO gửi giỏ hàng thanh toán và thông tin khách hàng
│   │   ├── PagedResponseDto.cs           # DTO phân trang kết quả danh sách
│   │   ├── CashierRevenueDto.cs          # DTO báo cáo doanh thu theo thu ngân
│   │   └── Role.cs                       # Định nghĩa vai trò hệ thống
│   │
│   ├── Models/                           # Các thực thể CSDL (Domain Entities)
│   │   ├── User.cs                       # Thực thể nhân viên người dùng
│   │   ├── Category.cs                   # Thực thể nhóm ngành hàng
│   │   ├── Product.cs                    # Thực thể sản phẩm hàng hóa
│   │   ├── Customer.cs                   # Thực thể khách hàng thân thiết
│   │   ├── Order.cs                      # Thực thể hóa đơn bán hàng
│   │   └── OrderDetail.cs                # Thực thể dòng chi tiết hóa đơn
│   │
│   ├── Migrations/                       # Lịch sử di trú lược đồ CSDL Entity Framework Core
│   ├── appsettings.json                  # Cấu hình chuỗi kết nối SQL Server, Secret Key JWT
│   └── Program.cs                        # Cấu hình Services Container, JWT, Swagger & Middleware
│
└── MiniSupermarket.WinForms/             # DỰ ÁN FRONTEND (Desktop Windows Forms .NET 8)
    ├── FormLogin.cs                      # Giao diện Đăng nhập xác thực tài khoản & phân quyền
    ├── FormMainShell.cs                  # Khung làm việc chính (Sidebar navigation, Header)
    ├── FormPOS.cs                        # Giao diện Bán hàng tại quầy (Quét mã vạch, Giỏ hàng, Tính tiền)
    ├── FormProductManagement.cs          # Giao diện Quản lý kho hàng & danh mục sản phẩm
    ├── FormCategoryManagement.cs         # Giao diện Quản lý nhóm hàng hóa
    ├── FormCustomerManagement.cs         # Giao diện Quản lý thông tin & điểm thưởng khách hàng
    ├── FormUserManagement.cs             # Giao diện Quản trị nhân viên & phân quyền hệ thống
    ├── FormQuickReport.cs                # Giao diện Báo cáo nhanh doanh số ca trực & biểu đồ
    ├── SessionManager.cs                 # Quản lý phiên đăng nhập tĩnh & ApiClientService dùng chung
    └── Program.cs                        # Điểm khởi động ứng dụng Desktop
```

---

## 4. Cơ sở Dữ liệu & Thiết kế Thực thể (Database Schema)

Cơ sở dữ liệu chính thức của đồ án mang tên **`ChiCuong_MiniSupermarketDb`**, được thiết kế chuẩn hóa với 6 thực thể có mối quan hệ chặt chẽ:

```
  ┌──────────────────┐               ┌──────────────────┐
  │    Categories    │ 1           N │     Products     │
  ├──────────────────┼───────────────┼──────────────────┤
  │ PK  CategoryId   │               │ PK  ProductId    │
  │     CategoryName │               │     Barcode (UQ) │
  │     Description  │               │     ProductName  │
  └──────────────────┘               │     Price        │
                                     │     StockQuantity│
                                     │ FK  CategoryId   │
                                     └────────┬─────────┘
                                              │ 1
                                              │
                                              │ N
  ┌──────────────────┐               ┌────────┴─────────┐
  │    Customers     │ 1           N │   OrderDetails   │
  ├──────────────────┼───────────────┼──────────────────┤
  │ PK  CustomerId   │               │ PK  OrderDetailId│
  │     FullName     │               │ FK  OrderId      │
  │     Phone (UQ)   │               │ FK  ProductId    │
  │     RewardPoints │               │     Quantity     │
  │     Membership   │               │     UnitPrice    │
  └────────┬─────────┘               │     LineTotal    │
           │ 1                       └────────┬─────────┘
           │                                  │ N
           │ N                                │
  ┌────────┴─────────┐                        │ 1
  │      Orders      ├────────────────────────┘
  ├──────────────────┤
  │ PK  OrderId      │
  │     OrderCode(UQ)│
  │     CreatedDate  │               ┌──────────────────┐
  │     CashierUser  │               │      Users       │
  │ FK  CustomerId   │               ├──────────────────┤
  │     TotalItems   │               │ PK  UserId       │
  │     FinalAmount  │               │     Username(UQ) │
  │     RewardPoints │               │     PasswordHash │
  └──────────────────┘               │     FullName     │
                                     │     Role         │
                                     └──────────────────┘
```

### Chi tiết các bảng dữ liệu:
1. **`Users` (Tài khoản nhân viên):** Lưu thông tin người dùng hệ thống (`UserId`, `Username` UNIQUE, `PasswordHash`, `FullName`, `Role`, `IsActive`). Hỗ trợ phân quyền `Admin` và `Cashier`.
2. **`Categories` (Nhóm ngành hàng):** Phân loại sản phẩm (`CategoryId`, `CategoryName` UNIQUE, `Description`), ví dụ: Nước giải khát, Bánh kẹo, Sữa tươi, Hóa mỹ phẩm.
3. **`Products` (Hàng hóa & Tồn kho):** Thực thể trung tâm quản lý hàng hóa (`ProductId`, `Barcode` UNIQUE có đánh Index tìm kiếm siêu tốc, `ProductName`, `Price`, `StockQuantity`, `CategoryId`).
4. **`Customers` (Khách hàng thân thiết):** Quản lý hội viên tích điểm (`CustomerId`, `FullName`, `PhoneNumber` UNIQUE, `RewardPoints`, `MembershipRank`). Cứ 10.000đ mua hàng tích 1 điểm; 1 điểm đổi được 1.000đ khi thanh toán.
5. **`Orders` (Hóa đơn bán lẻ):** Lưu thông tin đơn hàng (`OrderId`, `OrderCode` UNIQUE dạng `HD-YYYYMMDD-XXXX`, `CreatedDate`, `CashierUsername`, `CustomerId`, `TotalItems`, `FinalAmount`, `RewardPoints`).
6. **`OrderDetails` (Chi tiết hóa đơn):** Lưu từng mặt hàng trong đơn (`OrderDetailId`, `OrderId`, `ProductId`, `Quantity`, `UnitPrice`, `LineTotal = Quantity * UnitPrice`).

---

## 5. Các Phân hệ Chức năng Chính

### 5.1 Phân hệ Xác thực & Phân quyền
- **Màn hình Đăng nhập (`FormLogin`):** Kiểm tra tên đăng nhập và mật khẩu với Web API (`POST /api/auth/login`).
- **Bảo mật JWT Bearer Token:** Sau khi đăng nhập thành công, Server sinh chuỗi JSON Web Token mang thông tin người dùng và quyền hạn (`Role`), Client lưu vào `SessionManager` để tự động đính kèm vào Header của mọi HTTP Request tiếp theo.
- **Phân quyền linh hoạt:**
  - **Quản trị viên (Admin):** Toàn quyền truy cập mọi tính năng: Bán hàng, Quản lý sản phẩm, Quản lý danh mục, Quản lý nhân sự, Quản lý khách hàng, Báo cáo doanh thu chuyên sâu.
  - **Thu ngân (Cashier):** Được cấp quyền vào màn hình Bán hàng POS, tra cứu thông tin khách hàng và xem báo cáo doanh thu ca trực của chính mình.

### 5.2 Phân hệ Bán hàng POS tại Quầy (Point of Sale)
Đây là phân hệ cốt lõi và quan trọng nhất của hệ thống siêu thị mini:
- **Tương thích đầu đọc mã vạch (Barcode Scanner):** Ô quét mã vạch luôn sẵn sàng nhận dữ liệu. Khi quét xong, hệ thống tự động gọi API tra cứu tức thời và thêm sản phẩm vào giỏ hàng.
- **Giỏ hàng động (Smart Cart):**
  - Tự động cộng dồn số lượng nếu sản phẩm đã có sẵn trong giỏ.
  - Cho phép điều chỉnh số lượng hoặc xóa món hàng nhanh chóng.
  - Tự động tính toán tổng số lượng mặt hàng và tổng tiền tức thì.
- **Tích hợp Hội viên & Đổi điểm thưởng:**
  - Nhập SĐT khách hàng: Tự động tra cứu tên, hạng thành viên và số điểm tích lũy hiện có.
  - Hỗ trợ đổi điểm thưởng: Trừ trực tiếp tiền hóa đơn (1 điểm = 1.000đ).
  - Tự động tích lũy điểm thưởng mới cho đơn hàng hiện tại.
- **Tính toán tiền thanh toán:** Tự động tính tiền thừa khi thu ngân nhập số tiền khách đưa.
- **Thanh toán bảo vệ bởi Transaction:** Gửi toàn bộ giỏ hàng lên `POST /api/checkout` để thực thi lưu hóa đơn, chi tiết và trừ tồn kho trong một giao dịch nguyên tử.

### 5.3 Phân hệ Quản lý Hàng hóa & Danh mục
- **Màn hình Quản lý Sản phẩm (`FormProductManagement`):**
  - Hiển thị danh sách hàng hóa phân trang từ Web API.
  - Tìm kiếm hàng hóa theo tên, mã vạch hoặc lọc theo nhóm danh mục.
  - Thêm mới, chỉnh sửa đơn giá, giá vốn và cập nhật số lượng tồn kho.
  - Hệ thống tự động đánh dấu cảnh báo màu đỏ đối với các sản phẩm có tồn kho thấp dưới mức an toàn.
- **Màn hình Quản lý Danh mục (`FormCategoryManagement`):** Thêm, sửa, xóa các nhóm ngành hàng trong siêu thị.

### 5.4 Phân hệ Khách hàng Thân thiết & Tích điểm
- **Màn hình Khách hàng (`FormCustomerManagement`):**
  - Quản lý danh bạ khách hàng thân thiết theo số điện thoại duy nhất.
  - Xem số điểm tích lũy và xếp hạng thành viên: Chuẩn, Bạc, Vàng, Kim Cương.
  - Tra cứu lịch sử mua hàng theo mã khách hàng.

### 5.5 Phân hệ Quản lý Nhân sự & Tài khoản
- **Màn hình Quản trị Người dùng (`FormUserManagement`):**
  - Dành riêng cho tài khoản Quản trị viên (Admin).
  - Tạo mới tài khoản nhân viên thu ngân, cấp mật khẩu khởi tạo.
  - Phân quyền vai trò và khóa/mở khóa trạng thái hoạt động của tài khoản khi nhân viên nghỉ việc.

### 5.6 Phân hệ Báo cáo & Thống kê Doanh số
- **Màn hình Báo cáo Nhanh (`FormQuickReport`):**
  - Thống kê doanh thu theo ngày, theo tháng hoặc theo khoảng thời gian tùy chọn.
  - Báo cáo doanh số bán lẻ chi tiết của từng nhân viên thu ngân trong ca làm việc.
  - Danh sách Top 10 sản phẩm bán chạy nhất giúp chủ siêu thị kịp thời lên kế hoạch nhập hàng.

---

## 6. Giải pháp Kỹ thuật Nổi bật & Tối ưu Hiệu năng

### 6.1 Tối ưu tốc độ Quét mã vạch với ADO.NET thuần & SqlDataReader
Tại quầy POS, tốc độ phản hồi tính bằng mili-giây là yếu tố sống còn. Để không bị ảnh hưởng bởi độ trễ ánh xạ ORM, tác giả đã xây dựng endpoint `GET /api/ado-products/barcode/{barcode}` trong `AdoProductController.cs` bằng ADO.NET thuần:

```csharp
// Sử dụng SqlDataReader Forward-Only, Read-Only để đạt tốc độ đọc cực đại
using (SqlConnection connection = new SqlConnection(connectionString))
{
    string query = @"SELECT ProductId, Barcode, ProductName, Price, StockQuantity 
                     FROM Products 
                     WHERE Barcode = @Barcode";

    using (SqlCommand command = new SqlCommand(query, connection))
    {
        command.Parameters.AddWithValue("@Barcode", barcode.Trim());
        await connection.OpenAsync();

        using (SqlDataReader reader = await command.ExecuteReaderAsync())
        {
            if (await reader.ReadAsync())
            {
                return Ok(new ProductLookupDto
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
}
```

### 6.2 Xử lý Giao dịch Thanh toán Đa bảng với SqlTransaction (ACID)
Quá trình thanh toán tác động đồng thời lên 4 bảng dữ liệu: Tạo `Orders`, ghi nhiều dòng vào `OrderDetails`, trừ tồn kho trong `Products`, và tích/đổi điểm trong `Customers`.

Để đảm bảo nguyên tắc toàn vẹn **ACID**, toàn bộ nghiệp vụ được bao bọc trong một đối tượng `SqlTransaction` tại `CheckoutController.cs`:
1. Bắt đầu: `SqlTransaction transaction = connection.BeginTransaction();`
2. Tạo hóa đơn, sử dụng `OUTPUT INSERTED.OrderId` kết hợp `ExecuteScalarAsync()` để lấy khóa chính vừa sinh.
3. Duyệt từng món hàng trong giỏ: Kiểm tra số lượng tồn kho thực tế, nếu không đủ sẽ lập tức ném Exception để Rollback toàn bộ.
4. Ghi chi tiết hóa đơn và thực hiện lệnh trừ tồn kho:
   ```sql
   UPDATE Products SET StockQuantity = StockQuantity - @Qty WHERE ProductId = @ProductId
   ```
5. Cập nhật điểm thưởng khách hàng.
6. Kết thúc thành công: `transaction.Commit();`  
   Nếu có bất kỳ lỗi nào xảy ra trong chuỗi thao tác: `transaction.Rollback();` đảm bảo CSDL không bao giờ bị sai lệch.

### 6.3 Cơ chế Bộ nhớ đệm Độc lập (Disconnected Cache) với DataTable
Tại `AdoProductController.cs` (endpoint `/api/ado-products/disconnected-cache`), dữ liệu danh mục hàng hóa được nạp vào đối tượng bộ nhớ đệm `DataTable` thông qua `SqlDataAdapter`:
- Phương thức `adapter.Fill(dataTable)` tự động mở kết nối vật lý, nạp toàn bộ dữ liệu vào RAM server và lập tức đóng kết nối.
- Giúp giải phóng hoàn toàn kết nối về Connection Pool, giảm tải tối đa cho máy chủ CSDL SQL Server khi có hàng trăm máy trạm cùng truy vấn danh mục.

### 6.4 Bảo mật Truy vấn Chống tấn công SQL Injection
Toàn bộ các truy vấn SQL trực tiếp đều được tham số hóa 100% bằng `SqlParameter` hoặc `command.Parameters.AddWithValue()`. Hệ thống tuyệt đối không sử dụng kỹ thuật cộng/nối chuỗi thô sơ, loại bỏ hoàn toàn nguy cơ kẻ xấu khai thác lỗ hổng SQL Injection thông qua mã vạch hay dữ liệu nhập từ bàn phím.

---

## 7. Công nghệ và Công cụ Phát triển

| Hạng mục | Công nghệ / Công cụ | Phiên bản | Vai trò trong dự án |
| :--- | :--- | :--- | :--- |
| **Ngôn ngữ** | C# | C# 12 | Ngôn ngữ phát triển toàn diện cả Backend lẫn Frontend |
| **Nền tảng** | .NET | .NET 8.0 LTS | Framework hiện đại, đa nền tảng, hiệu năng cao |
| **Backend API** | ASP.NET Core Web API | 8.0 | Xây dựng RESTful API, Routing, Dependency Injection |
| **ORM** | Entity Framework Core | 8.0.8 | Quản lý lược đồ CSDL, Migration, thao tác CRUD quản trị |
| **Data Access** | ADO.NET (`Microsoft.Data.SqlClient`) | 5.2.2 | Truy xuất dữ liệu native tốc độ cao, xử lý Transaction |
| **Bảo mật** | JWT Bearer & Hashing | System.IdentityModel | Xác thực token không trạng thái (Stateless), phân quyền Role |
| **Tài liệu API** | Swagger UI / Swashbuckle | 6.6.2 | Khởi tạo trang tài liệu tương tác kiểm thử API trực quan |
| **Frontend UI** | Windows Forms (WinForms) | .NET 8.0 | Giao diện máy trạm tại quầy POS, tối ưu thao tác chuột & phím |
| **Giao tiếp mạng** | `System.Net.Http.Json` | .NET 8.0 | Gửi nhận dữ liệu JSON bất đồng bộ giữa WinForms và API |
| **Hệ quản trị CSDL** | Microsoft SQL Server | 2019 / 2022 | Lưu trữ dữ liệu quan hệ, đảm bảo tính toàn vẹn dữ liệu |
| **Công cụ CSDL** | SQL Server Management Studio | SSMS v19+ | Quản trị, viết script và giám sát hiệu năng CSDL |
| **Môi trường Dev** | Microsoft Visual Studio | 2022 (v17.x) | IDE chính thức để lập trình, gỡ lỗi và đóng gói dự án |

---

## 8. Hướng dẫn Cài đặt, Cấu hình và Vận hành Hệ thống

### Bước 1: Chuẩn bị Cơ sở Dữ liệu SQL Server
1. Mở **SQL Server Management Studio (SSMS)** và đăng nhập vào máy chủ SQL Server của bạn.
2. Mở file cấu hình `MiniSupermarket.API/appsettings.json` và kiểm tra chuỗi kết nối:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=.;Database=ChiCuong_MiniSupermarketDb;User Id=sa;Password=123456;MultipleActiveResultSets=true;TrustServerCertificate=True"
     }
   }
   ```
   *(Lưu ý: Bạn có thể điều chỉnh `Server`, `User Id` và `Password` khớp với cấu hình SQL Server trên máy tính cá nhân).*
3. Khởi tạo CSDL và Seed dữ liệu mẫu:
   - Mở **Package Manager Console** trong Visual Studio, chọn Default project là `MiniSupermarket.API`.
   - Chạy lệnh: `Update-Database`  
   - Hệ thống sẽ tự động tạo database `ChiCuong_MiniSupermarketDb`, tạo 6 bảng dữ liệu và nạp sẵn dữ liệu mẫu về hàng hóa, danh mục và tài khoản quản trị.

### Bước 2: Chạy Dịch vụ Backend Web API
1. Trong Visual Studio, nhấp chuột phải vào dự án **`MiniSupermarket.API`** -> chọn **Set as Startup Project**.
2. Nhấn **F5** (hoặc `Ctrl + F5`) để khởi chạy.
3. Trình duyệt tự động mở giao diện **Swagger UI** tại địa chỉ:
   ```text
   https://localhost:7132/swagger (hoặc cổng cấu hình trong launchSettings.json)
   ```
4. Kiểm tra các nhóm API hoạt động bình thường trên giao diện Swagger.

### Bước 3: Khởi chạy Máy trạm Frontend WinForms Client
1. Trong khi Web API đang chạy, tại cửa sổ **Solution Explorer**, nhấp chuột phải vào dự án **`MiniSupermarket.WinForms`** -> chọn **Debug** -> **Start New Instance**.
2. Cửa sổ **Đăng nhập (`FormLogin`)** xuất hiện:
   - **Tài khoản Quản trị:** `admin` / Mật khẩu: `123456` (Truy cập đầy đủ tính năng hệ thống).
   - **Tài khoản Thu ngân:** `cashier1` / Mật khẩu: `123456` (Truy cập bán hàng POS và báo cáo ca trực).
3. Sau khi đăng nhập thành công, màn hình điều hướng chính **`FormMainShell`** sẽ mở ra, cho phép nhân viên thao tác bán hàng, quét mã vạch và quản lý hàng hóa.

---

## 9. Bảng Tra cứu Mã lỗi Thường gặp & Hướng Khắc phục

| STT | Hiện tượng lỗi | Nguyên nhân gốc rễ | Hướng xử lý và khắc phục |
| :---: | :--- | :--- | :--- |
| **1** | Lỗi đăng nhập: *"Lỗi kết nối đến Server... kiểm tra port 7132"* | Backend Web API chưa chạy hoặc chạy sai cổng HTTPS | Đảm bảo `MiniSupermarket.API` đang chạy; kiểm tra `BaseUrl` trong `SessionManager.cs` khớp với cổng của API (`https://localhost:7132/api/`). |
| **2** | `SqlException: Cannot open database "ChiCuong_MiniSupermarketDb"` | CSDL chưa được khởi tạo trên SQL Server | Mở Package Manager Console và chạy lệnh `Update-Database` để tự động tạo CSDL. |
| **3** | `SqlException: Login failed for user 'sa'` | Sai mật khẩu tài khoản SQL Server hoặc chưa bật Mixed Mode Authentication | Kiểm tra lại thông tin `Password` trong chuỗi kết nối `appsettings.json` hoặc chuyển sang xác thực Windows: `Integrated Security=True;`. |
| **4** | Lỗi *"Không đủ số lượng hàng tồn kho"* khi bấm Thanh toán | Mặt hàng trong kho đã hết hoặc số lượng mua vượt quá số lượng hiện có | Kiểm tra số lượng tồn kho trên màn hình Quản lý Sản phẩm và tiến hành cập nhật/nhập thêm hàng. |
| **5** | Không tra cứu được thông tin khách hàng bằng SĐT | Khách hàng chưa đăng ký số điện thoại trong hệ thống | Thêm mới khách hàng tại màn hình `FormCustomerManagement` hoặc để trống SĐT để hệ thống tự động ghi nhận là Khách vãng lai. |

---

## 10. Kết quả Đạt được của Đồ án

1. **Về mặt nghiệp vụ:**
   - Hoàn thành đầy đủ quy trình bán hàng khép kín tại cửa hàng tiện lợi: từ quét mã vạch, quản lý giỏ hàng, tra cứu thành viên, áp dụng đổi điểm thưởng, đến thanh toán và trừ tồn kho tự động.
   - Quản lý danh mục hàng hóa, giá bán, giá vốn và cảnh báo hàng sắp hết chuẩn xác.
   - Thống kê doanh thu theo ngày và theo thu ngân rõ ràng, minh bạch.
2. **Về mặt công nghệ và kiến trúc phần mềm:**
   - Xây dựng thành công kiến trúc phân tầng chuẩn mực giữa ASP.NET Core Web API và Windows Forms Client.
   - Kết hợp hài hòa, hiệu quả giữa **Entity Framework Core** (tiện lợi cho CRUD) và **ADO.NET thuần** (tối ưu hiệu năng cực đại cho POS và bảo vệ giao dịch đa bảng bằng `SqlTransaction`).
   - Ứng dụng các chuẩn thiết kế hiện đại: RESTful API, DTOs, Repository/Pattern, Asynchronous Programming (`async / await`), JWT Authentication.

---

## 11. Hướng Phát triển Mở rộng

- **Tích hợp Cổng thanh toán Điện tử:** Mở rộng thanh toán không tiền mặt qua mã QR Động (VietQR, MoMo, ZaloPay).
- **In Hóa đơn Trực tiếp:** Kết nối với máy in nhiệt khổ K80/K58 tại quầy để xuất phiếu hóa đơn tức thời cho khách hàng.
- **Mở rộng Đa điểm bán (Multi-Branch):** Phát triển thêm ứng dụng Web Client (Blazor / React) để chủ chuỗi cửa hàng theo dõi doanh thu từ xa trên thiết bị di động.

---

## 12. Tác Giả

- **Đề tài:** Phát triển Ứng dụng Thu ngân và Quản lý Hàng hóa cho Mô hình Bán lẻ – Cửa hàng Tiện lợi Alpha Mini
- **Họ tên sinh viên:** Mai Chí Cường
- **Mã sinh viên:** 2124110113
- **Lớp học phần:** CCQ2411D
