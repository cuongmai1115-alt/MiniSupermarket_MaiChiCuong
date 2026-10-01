Dưới đây là mã nguồn README.md dạng Raw Markdown hoàn chỉnh. Mã nguồn này được thiết kế để khi bạn dán vào editor hay đẩy lên GitHub/GitLab sẽ giữ nguyên toàn bộ cấu trúc xuống dòng, bảng biểu, danh sách và icon mà không bao giờ bị dính liền chữ.   Bạn hãy copy toàn bộ khối bên dưới và dán đè vào file README.md nhé:Markdown# 🛒 HỆ THỐNG QUẢN LÝ SIÊU THỊ MINI (MINISUPERMARKET SYSTEM)

**Buổi thực hành:** Buổi 3 - Tích hợp SQL Server & Entity Framework Core Code-First  
**Tác giả:** Mai Chí Cường - MSSV: 2124110113 - Lớp: CCQ2411D
🏗️ 1. Mô hình Kiến trúc Hệ thống (Client - Server)Dự án được xây dựng theo mô hình phân tầng hiện đại, tách biệt hoàn toàn giữa Backend và Frontend:MiniSupermarket.API (Backend): Dự án ASP.NET Core Web API chịu trách nhiệm xử lý logic nghiệp vụ, bảo mật JWT, kết nối cơ sở dữ liệu quan hệ Microsoft SQL Server thông qua Entity Framework Core (EF Core) và cung cấp các RESTful API chuẩn hóa bằng phương thức bất đồng bộ (async/await).MiniSupermarket.WinForms (Frontend Client): Ứng dụng Windows Forms đóng vai trò là máy trạm POS tại quầy, sử dụng HttpClient kết hợp Bearer Token để tương tác với API, thực hiện các thao tác CRUD danh mục, sản phẩm và khách hàng. Dữ liệu được lưu trữ vĩnh viễn trên CSDL.🔐 2. Cơ chế Bảo mật & Quản lý Dữ liệuXác thực & Phân quyền (JWT Authentication & Authorization)Xác thực (Authentication): Người dùng đăng nhập qua POST /api/auth/login, hệ thống kiểm tra và cấp phát chuỗi JWT Token (Stateless).Phân quyền (Authorization): Token chứa Claim Role để phân quyền sử dụng endpoint:Admin: Toàn quyền Xem, Thêm, Sửa, Xóa trên tất cả các danh mục, sản phẩm, khách hàng.Cashier: Được quyền Xem, Tìm kiếm và Tạo mới thông tin khách hàng/đơn hàng, không có quyền xóa hoặc sửa cấu hình hệ thống.Không có Token: Trả về 401 Unauthorized.Không đủ quyền: Trả về 403 Forbidden.Quản lý Dữ liệu Bền vững (SQL Server & EF Core Code-First)Thay thế hoàn toàn cơ chế lưu tạm trên RAM (In-Memory) ở Buổi 1 & 2.Tự động khởi tạo cấu trúc bảng SQL Server qua EF Core Migrations (Add-Migration, Update-Database).Tích hợp Data Seeding tự động nạp dữ liệu mẫu ban đầu cho danh mục và khách hàng.Tài khoản DemoUsernamePasswordRoleadmin123456Admincashier123456Cashier🛠️ 3. Công nghệ Sử dụngNgôn ngữ: C# (.NET 8.0)Backend: ASP.NET Core Web API, EF Core 8.0, LINQ, Async/AwaitDatabase & ORM: Microsoft SQL Server, Entity Framework Core Code-FirstNuGet Packages Backend:Microsoft.EntityFrameworkCore.SqlServerMicrosoft.EntityFrameworkCore.ToolsMicrosoft.EntityFrameworkCore.DesignMicrosoft.AspNetCore.Authentication.JwtBearerFrontend: Windows Forms (.NET 8.0), System.Net.Http.JsonCông cụ kiểm thử & quản lý CSDL: Swagger UI, SQL Server Management Studio (SSMS)📂 4. Cấu trúc SolutionPlaintextMiniSupermarketSystem/
│
├── MiniSupermarket.API/                  # Dự án Web API (Backend)
│   ├── Controllers/
│   │   ├── AuthController.cs             # Đăng nhập, cấp phát JWT Token
│   │   ├── CategoriesController.cs       # CRUD & Search nhóm hàng từ SQL Server
│   │   └── CustomersController.cs        # CRUD & Search khách hàng thân thiết
│   ├── Data/
│   │   └── SupermarketDbContext.cs       # DbContext ánh xạ CSDL & Data Seeding
│   ├── Migrations/                       # Chứa mã Migration sinh tự động từ EF Core
│   ├── Models/                           # Các thực thể (Entities)
│   │   ├── Category.cs                   # Thực thể Danh mục (Bảng Categories)
│   │   ├── Product.cs                    # Thực thể Sản phẩm (Bảng Products)
│   │   └── Customer.cs                   # Thực thể Khách hàng (Bảng Customers)
│   ├── appsettings.json                  # Cấu hình Chuỗi kết nối SQL Server & JWT Secret
│   └── Program.cs                        # Cấu hình Dependency Injection, DbContext & Middleware
│
└── MiniSupermarket.WinForms/             # Dự án Windows Forms (Frontend Client)
    ├── FormLogin.cs                       # Màn hình đăng nhập
    ├── FormCategoryManagement.cs          # Màn hình quản lý danh mục nhóm hàng
    ├── FormCustomerManagement.cs          # Màn hình quản lý khách hàng thân thiết
    └── SessionManager.cs                  # Lưu trữ JWT Token + Role của phiên đăng nhập
🗄️ 5. Cấu trúc CSDL (Mô hình 6 Bảng Cốt lõi)Hệ thống được thiết kế theo sơ đồ cơ sở dữ liệu chuẩn gồm 6 bảng:Users: Quản lý tài khoản, mật khẩu và vai trò (Admin/Cashier).Categories: Quản lý danh mục nhóm hàng (Khóa chính: CategoryId).Products: Quản lý thông tin mặt hàng, giá bán, tồn kho, mã vạch (Khóa ngoại: CategoryId).Customers: Quản lý khách hàng thân thiết, điểm thưởng, hạng thẻ (Khóa chính: CustomerId).Orders: Quản lý hóa đơn bán hàng tại quầy POS (Khóa ngoại: UserId, CustomerId).OrderDetails: Chi tiết từng mặt hàng trong hóa đơn (Khóa ngoại: OrderId, ProductId).🚀 6. Hướng dẫn Thiết lập & Chạy Dự ánBước 1: Cấu hình Chuỗi kết nối Database (appsettings.json)Mở file appsettings.json trong project MiniSupermarket.API và chỉnh sửa chuỗi kết nối:Sử dụng Windows Authentication:JSON"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TeoNguyenMiniSupermarketDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
Sử dụng Tài khoản SQL (sa):JSON"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=TeoNguyenMiniSupermarketDb;User Id=sa;Password=123456;TrustServerCertificate=True;MultipleActiveResultSets=true;"
}
Bước 2: Thực thi EF Core MigrationsMở cửa sổ Package Manager Console trong Visual Studio (Tools $\rightarrow$ NuGet Package Manager $\rightarrow$ Package Manager Console).Chọn Default project là MiniSupermarket.API.Chạy lần lượt các lệnh:PowerShellAdd-Migration InitialCreateDatabase
Update-Database
Lưu ý: Hệ thống sẽ tự động tạo CSDL TeoNguyenMiniSupermarketDb trên SQL Server cùng các bảng và dữ liệu mồi (Data Seeding).Bước 3: Chạy Backend (Web API)Nhấp chuột phải vào project MiniSupermarket.API chọn Set as Startup Project.Nhấn F5 để khởi chạy. Trình duyệt sẽ mở Swagger UI (https://localhost:7123/swagger).Thực hiện kiểm thử các API Categories và Customers.Bước 4: Chạy Frontend (WinForms Client)Nhấp chuột phải vào MiniSupermarket.WinForms chọn Debug $\rightarrow$ Start new instance.Đăng nhập bằng tài khoản mẫu admin / 123456 hoặc cashier / 123456.Kiểm thử các chức năng Xem, Thêm, Sửa, Xóa, Tìm kiếm khách hàng và danh mục. Tắt ứng dụng và mở lại để kiểm chứng dữ liệu đã được lưu bền vững vào SQL Server.🗺️ 7. Lộ trình Phát triển (Roadmap)[x] Buổi 1: CRUD Categories cơ bản (In-Memory)[x] Buổi 2: Bảo mật JWT Authentication + Phân quyền Admin/Cashier[x] Buổi 3: Tích hợp SQL Server + EF Core Code-First + Bài tập Mở rộng Customers[ ] Buổi 4: Phân hệ Sản phẩm (Products) & Nghiệp vụ Quản lý Kho hàng (Eager Loading với .Include())[ ] Buổi 5: Phân hệ Bán hàng POS (Orders & OrderDetails) & In hóa đơn👨‍💻 8. Thông tin Tác giảHọ và tên: Mai Chí CườngMã sinh viên: 2124110113Lớp học phần: CCQ2411D
