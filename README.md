# 🛒 HỆ THỐNG QUẢN LÝ SIÊU THỊ MINI

## Buổi 3: Tích hợp SQL Server và Entity Framework Core Code-First

------------------------------------------------------------------------

## 1. Mục tiêu và bối cảnh thực tế

Buổi thực hành 3 tập trung vào việc tích hợp **Microsoft SQL Server** và
**Entity Framework Core Code-First** vào hệ thống quản lý siêu thị mini.

### Mục tiêu

-   Thay thế cơ chế lưu dữ liệu tạm trên RAM (In-Memory) ở Buổi 1 và
    Buổi 2 bằng cơ sở dữ liệu quan hệ Microsoft SQL Server.
-   Làm quen với Entity Framework Core (EF Core) Code-First.
-   Sử dụng các lệnh EF Core Migrations để tạo và cập nhật cơ sở dữ
    liệu.
-   Truy vấn dữ liệu bất đồng bộ bằng LINQ kết hợp `async/await`.
-   Đảm bảo dữ liệu được lưu trữ lâu dài trong SQL Server.

### Bối cảnh thực tế

Khi nhân viên thu ngân thêm hoặc sửa nhóm hàng trên phần mềm WinForms,
dữ liệu sẽ được lưu trực tiếp vào cơ sở dữ liệu SQL Server. Sau khi tắt
máy hoặc khởi động lại API, dữ liệu vẫn được giữ nguyên.

------------------------------------------------------------------------

## 2. Kiến trúc hệ thống

Dự án được xây dựng theo mô hình **Client - Server**, gồm hai thành phần
chính:

### Backend - MiniSupermarket.API

Ứng dụng ASP.NET Core Web API chịu trách nhiệm:

-   Xử lý logic nghiệp vụ.
-   Kết nối Microsoft SQL Server.
-   Sử dụng Entity Framework Core để thao tác với cơ sở dữ liệu.
-   Cung cấp RESTful API.
-   Hỗ trợ xử lý bất đồng bộ bằng `async/await`.

### Frontend - MiniSupermarket.WinForms

Ứng dụng Windows Forms đóng vai trò là máy trạm tại quầy.

Frontend sử dụng `HttpClient` để:

-   Gọi API.
-   Xem danh mục.
-   Thêm danh mục.
-   Cập nhật danh mục.
-   Xóa danh mục.
-   Thực hiện các chức năng quản lý dữ liệu khách hàng theo phần mở
    rộng.

------------------------------------------------------------------------

## 3. Công nghệ sử dụng

### Backend

-   C# / .NET 8.0
-   ASP.NET Core Web API
-   Entity Framework Core 8.0
-   LINQ
-   Async/Await

### Database

-   Microsoft SQL Server
-   Entity Framework Core Code-First
-   EF Core Migrations

### Frontend

-   Windows Forms .NET 8.0
-   `System.Net.Http.Json`
-   `HttpClient`

### Công cụ

-   Visual Studio 2022
-   SQL Server Management Studio (SSMS)
-   Swagger UI

------------------------------------------------------------------------

## 4. Các gói NuGet cần cài đặt

Trong project `MiniSupermarket.API`, cài đặt các package:

``` text
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.Design
```

### Chức năng

-   `Microsoft.EntityFrameworkCore.SqlServer`: Kết nối EF Core với SQL
    Server.
-   `Microsoft.EntityFrameworkCore.Tools`: Hỗ trợ các lệnh Migration.
-   `Microsoft.EntityFrameworkCore.Design`: Hỗ trợ môi trường thiết kế
    và sinh mã EF Core.

------------------------------------------------------------------------

## 5. Cấu trúc Model

### Category

Bảng `Categories` quản lý các nhóm hàng.

Các thông tin chính:

-   `CategoryId`: Khóa chính, tự tăng.
-   `CategoryName`: Tên nhóm hàng.
-   `Description`: Mô tả nhóm hàng.
-   `Products`: Quan hệ một-nhiều với sản phẩm.

### Product

Bảng `Products` quản lý các mặt hàng trong siêu thị.

Các thông tin chính:

-   `ProductId`: Khóa chính, tự tăng.
-   `Barcode`: Mã vạch sản phẩm.
-   `ProductName`: Tên sản phẩm.
-   `Price`: Giá bán.
-   `StockQuantity`: Số lượng tồn kho.
-   `CategoryId`: Khóa ngoại liên kết với `Categories`.

Quan hệ:

``` text
Category 1 -------- N Product
```

Một danh mục có thể có nhiều sản phẩm.

------------------------------------------------------------------------

## 6. SupermarketDbContext

File:

``` text
MiniSupermarket.API/Data/SupermarketDbContext.cs
```

`SupermarketDbContext` là lớp trung gian giúp ứng dụng làm việc với cơ
sở dữ liệu SQL Server thông qua Entity Framework Core.

Các bảng được ánh xạ:

``` csharp
public DbSet<Category> Categories { get; set; }
public DbSet<Product> Products { get; set; }
```

### Data Seeding

Hệ thống có sẵn 5 danh mục mẫu:

    ID Tên danh mục                 Mô tả
  ---- ---------------------------- ---------------------------------
     1 Bánh kẹo & Đồ ăn vặt         Snack, bánh quy, kẹo dẻo
     2 Nước giải khát & Trà         Nước ngọt, nước khoáng, trà
     3 Sữa & Sản phẩm từ sữa        Sữa tươi, sữa chua, phô mai
     4 Mì gói & Thực phẩm ăn liền   Mì ăn liền, phở khô, cháo gói
     5 Gia vị & Dầu ăn              Nước mắm, hạt nêm, dầu thực vật

------------------------------------------------------------------------

## 7. Cấu hình SQL Server

Mở file:

``` text
MiniSupermarket.API/appsettings.json
```

### Sử dụng Windows Authentication

``` json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MiniSupermarketDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### Sử dụng tài khoản SQL Server `sa`

Nếu SQL Server sử dụng tài khoản `sa`, có thể cấu hình:

``` json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=MiniSupermarketDb;User Id=sa;Password=123456;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

Nếu SQL Server Express được sử dụng, Server có thể đổi thành:

``` text
.\SQLEXPRESS
```

### Đặt mật khẩu cho tài khoản sa

Trong SSMS, có thể thực hiện:

``` sql
ALTER LOGIN sa WITH PASSWORD = '123456';
```

------------------------------------------------------------------------

## 8. Đăng ký DbContext trong Program.cs

Trong `Program.cs`, đăng ký `SupermarketDbContext`:

``` csharp
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SupermarketDbContext>(options =>
    options.UseSqlServer(connectionString));
```

Sau đó ứng dụng có thể sử dụng `SupermarketDbContext` thông qua
Dependency Injection.

------------------------------------------------------------------------

## 9. EF Core Migrations

Mở:

``` text
Tools
→ NuGet Package Manager
→ Package Manager Console
```

Đảm bảo **Default project** là:

``` text
MiniSupermarket.API
```

### Tạo Migration

``` powershell
Add-Migration InitialCreateDatabase
```

### Cập nhật Database

``` powershell
Update-Database
```

EF Core sẽ dựa trên các Model và `DbContext` để tạo cấu trúc cơ sở dữ
liệu SQL Server.

------------------------------------------------------------------------

## 10. CategoriesController

Controller quản lý danh mục:

``` text
MiniSupermarket.API/Controllers/CategoriesController.cs
```

Các API chính:

  Phương thức   Endpoint                               Chức năng
  ------------- -------------------------------------- ----------------------
  GET           `/api/categories`                      Lấy tất cả danh mục
  GET           `/api/categories/{id}`                 Lấy danh mục theo ID
  GET           `/api/categories/search?keyword=...`   Tìm kiếm danh mục
  POST          `/api/categories`                      Thêm danh mục
  PUT           `/api/categories/{id}`                 Cập nhật danh mục
  DELETE        `/api/categories/{id}`                 Xóa danh mục

Controller sử dụng:

``` csharp
ToListAsync()
FindAsync()
SaveChangesAsync()
```

để thực hiện truy vấn và cập nhật dữ liệu bất đồng bộ.

------------------------------------------------------------------------

## 11. Kiểm thử Backend bằng Swagger

Sau khi chạy project `MiniSupermarket.API`:

1.  Nhấn `F5`.
2.  Mở Swagger UI.
3.  Kiểm tra API:

``` text
GET /api/categories
```

4.  Thử thêm danh mục bằng:

``` text
POST /api/categories
```

5.  Kiểm tra lại dữ liệu trong SQL Server Management Studio.

Dữ liệu được tạo hoặc thay đổi thông qua API sẽ được lưu vào SQL Server.

------------------------------------------------------------------------

## 12. Kiểm thử WinForms Client

Khởi chạy:

``` text
MiniSupermarket.WinForms
```

Thực hiện các chức năng:

-   Xem danh mục.
-   Thêm danh mục.
-   Cập nhật danh mục.
-   Xóa danh mục.
-   Làm việc với dữ liệu thông qua Web API.

Sau khi thao tác, có thể tắt cả WinForms và Backend API rồi mở lại để
kiểm tra tính bền vững của dữ liệu.

------------------------------------------------------------------------

## 13. Phân hệ Customers - Khách hàng thân thiết

Phần mở rộng của Buổi 3 xây dựng phân hệ quản lý khách hàng.

Bảng `Customers` dùng để lưu:

-   Thông tin khách hàng.
-   Số điện thoại.
-   Địa chỉ.
-   Điểm tích lũy.
-   Hạng thành viên.

### Các trường dữ liệu

  Trường           Kiểu dữ liệu    Ý nghĩa
  ---------------- --------------- ---------------------
  CustomerId       INT             Khóa chính, tự tăng
  CustomerName     NVARCHAR(100)   Tên khách hàng
  PhoneNumber      VARCHAR(15)     Số điện thoại
  Address          NVARCHAR(200)   Địa chỉ
  RewardPoints     INT             Điểm tích lũy
  MembershipRank   NVARCHAR(50)    Hạng thành viên

### Dữ liệu mẫu

Hệ thống có thể sử dụng 3 khách hàng mẫu:

  Khách hàng     Số điện thoại   Hạng      Điểm
  -------------- --------------- ------- ------
  Nguyễn Văn A   0901122334      Vàng       150
  Trần Thị B     0918877665      Bạc         50
  Lê Văn C       0983344556      Chuẩn       10

------------------------------------------------------------------------

## 14. Customers API

Các endpoint cần xây dựng:

``` text
GET    /api/customers
GET    /api/customers/{id}
GET    /api/customers/search?keyword=...
POST   /api/customers
PUT    /api/customers/{id}
DELETE /api/customers/{id}
```

### Chức năng

-   Lấy danh sách khách hàng.
-   Xem thông tin khách hàng theo ID.
-   Tìm kiếm theo tên hoặc số điện thoại.
-   Thêm khách hàng.
-   Cập nhật thông tin và hạng thành viên.
-   Xóa khách hàng.

------------------------------------------------------------------------

## 15. Giao diện FormCustomerManagement

Form quản lý khách hàng:

``` text
FormCustomerManagement.cs
```

Các thành phần chính:

### DataGridView

``` text
dgvCustomers
```

Dùng để hiển thị danh sách khách hàng.

### Ô nhập liệu

``` text
txtCustomerId
txtCustomerName
txtPhoneNumber
txtAddress
txtRewardPoints
txtMembershipRank
```

### Các nút chức năng

``` text
btnLoad
btnAdd
btnUpdate
btnDelete
btnSearch
```

Frontend sử dụng `HttpClient` và các phương thức:

``` csharp
GetFromJsonAsync
PostAsJsonAsync
PutAsJsonAsync
DeleteAsync
```

để giao tiếp với Web API.

------------------------------------------------------------------------

## 16. Cấu trúc thư mục dự án

``` text
MiniSupermarketSystem/
│
├── MiniSupermarket.API/
│   ├── Controllers/
│   │   ├── CategoriesController.cs
│   │   └── CustomersController.cs
│   │
│   ├── Data/
│   │   └── SupermarketDbContext.cs
│   │
│   ├── Migrations/
│   │
│   ├── Models/
│   │   ├── Category.cs
│   │   ├── Product.cs
│   │   └── Customer.cs
│   │
│   ├── appsettings.json
│   └── Program.cs
│
└── MiniSupermarket.WinForms/
    ├── FormCategoryManagement.cs
    ├── FormCustomerManagement.cs
    └── ...
```

------------------------------------------------------------------------

## 17. Kiến thức đạt được

Sau Buổi 3, hệ thống đã chuyển từ cơ chế lưu trữ In-Memory sang SQL
Server.

Các kiến thức chính:

-   Entity Framework Core.
-   Code-First.
-   SQL Server.
-   DbContext.
-   Data Seeding.
-   EF Core Migrations.
-   LINQ.
-   `async/await`.
-   `ToListAsync()`.
-   `FindAsync()`.
-   `SaveChangesAsync()`.
-   RESTful API.
-   CRUD.
-   Kết nối WinForms với Web API.

------------------------------------------------------------------------

## 18. Checklist nghiệm thu

### Cơ sở dữ liệu

-   [ ] SQL Server hoạt động.
-   [ ] Database được tạo bằng EF Core Migration.
-   [ ] Bảng Categories được tạo.
-   [ ] Bảng Products được tạo.
-   [ ] Bảng Customers được tạo.
-   [ ] Dữ liệu Seed được nạp thành công.

### Backend

-   [ ] API Categories hoạt động.
-   [ ] CRUD Categories hoạt động.
-   [ ] API Customers hoạt động.
-   [ ] Tìm kiếm khách hàng hoạt động.
-   [ ] Swagger kiểm thử thành công.

### WinForms

-   [ ] Hiển thị danh mục.
-   [ ] Thêm danh mục.
-   [ ] Sửa danh mục.
-   [ ] Xóa danh mục.
-   [ ] Hiển thị khách hàng.
-   [ ] Thêm khách hàng.
-   [ ] Sửa khách hàng.
-   [ ] Xóa khách hàng.
-   [ ] Tìm kiếm khách hàng.

### Tính bền vững

-   [ ] Dữ liệu được lưu trực tiếp vào SQL Server.
-   [ ] Tắt và mở lại ứng dụng dữ liệu vẫn còn.
-   [ ] Có thể kiểm tra dữ liệu trực tiếp bằng SSMS.

------------------------------------------------------------------------

## 19. Định hướng Buổi 4

Trọng tâm tiếp theo là:

**Phân hệ Sản phẩm và Quản lý kho hàng.**

Các nội dung dự kiến:

-   Xây dựng đầy đủ CRUD cho `ProductsController`.
-   Liên kết `Product` với `Category`.
-   Hiển thị nhóm hàng bằng ComboBox trên WinForms.
-   Tra cứu sản phẩm theo mã vạch Barcode.
-   Lọc sản phẩm theo nhóm hàng.
-   Tối ưu truy vấn quan hệ bằng:

``` csharp
.Include(...)
```

------------------------------------------------------------------------

## 20. Tác giả

Họ và tên: Mai Chí Cường
MSSV:2124110113
Lớp học phần:CCQ2411D
Đề tài: Phát triển Ứng dụng Thu ngân và Quản lý Hàng hóa cho Mô hình Bán lẻ -- Cửa hàng Tiện lợi Alpha Mini
