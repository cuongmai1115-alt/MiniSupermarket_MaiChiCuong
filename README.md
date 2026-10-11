# 🛒 HỆ THỐNG QUẢN LÝ SIÊU THỊ MINI

## Buổi 4: Kiến trúc giao diện WinForms Shell (Sidebar & Main Workspace) và điều hướng phân quyền Admin / Cashier / Warehouse

------------------------------------------------------------------------

## 1. Mục tiêu và bối cảnh thực tế

Buổi thực hành 4 xây dựng **khung giao diện chính (FormMainShell)** cho ứng
dụng WinForms theo kiến trúc đơn khung (Single-Form Architecture), đồng thời
áp dụng cơ chế **phân quyền theo vai trò (Role-Based Access Control - RBAC)**
cho cả giao diện lẫn API.

### Mục tiêu

-   Thiết kế giao diện đơn khung hiện đại gồm thanh chức năng bên trái
    (**Sidebar**) và vùng làm việc trung tâm (**Main Content**).
-   Nhúng động Form con vào vùng chứa bằng `TopLevel = false` và
    `Dock = Fill`, không mở thêm cửa sổ rời rạc.
-   Hiện thực phân quyền theo 3 vai trò: **Admin**, **Cashier** (Thu ngân),
    **Warehouse** (Thủ kho): ẩn/hiện nút chức năng ngay sau khi đăng nhập.
-   Xây dựng các Form con nghiệp vụ: POS, Sản phẩm, Khách hàng, Báo cáo,
    Quản trị tài khoản.
-   Bảo vệ phân quyền hai lớp: **ẩn nút ở giao diện** và **chặn ở API
    bằng JWT** (`[Authorize(Roles = ...)]`).

### Bối cảnh thực tế

Trong siêu thị mini, nhân viên thu ngân chỉ được vào màn hình bán hàng
(POS), thủ kho chỉ quản lý sản phẩm và nhóm hàng, còn quản trị viên có toàn
quyền, bao gồm báo cáo tài chính và quản lý tài khoản. Sau khi đăng nhập
thành công, màn hình điều khiển trung tâm tự thích ứng theo vai trò của
người dùng và mở sẵn màn hình đúng chuyên môn.

------------------------------------------------------------------------

## 2. Kiến trúc hệ thống

Dự án tiếp tục theo mô hình **Client - Server**:

### Backend - MiniSupermarket.API

-   ASP.NET Core Web API (.NET 8), Entity Framework Core 8, SQL Server.
-   Xác thực **JWT Bearer**, token chứa `Name` và `Role`, hết hạn sau 2 giờ.
-   Mỗi Controller gắn `[Authorize]` theo vai trò, API là lớp bảo vệ cuối
    cùng dù giao diện đã ẩn nút.

### Frontend - MiniSupermarket.WinForms

-   Windows Forms .NET 8, gọi API bằng `HttpClient` dùng chung
    (`ApiClientService.Client`), tự gắn Bearer Token.
-   `SessionManager` lưu phiên đăng nhập: `JwtToken`, `CurrentUsername`,
    `CurrentFullName`, `CurrentRole`.
-   `FormMainShell` là khung chính, các Form con được nhúng vào
    `panelMainContent`.

### Sơ đồ giao diện FormMainShell

``` text
┌─────────────────────────────────────────────────────────────────────────────┐
│                             FormMainShell                                   │
├────────────────────┬────────────────────────────────────────────────────────┤
│  panelSidebar      │  panelTopHeader (lblTitle & lblUserInfo)               │
│  (Width = 230)     ├────────────────────────────────────────────────────────┤
│  - Logo MiniMart   │                                                        │
│  - btnPOS          │                                                        │
│  - btnCategory     │  panelMainContent                                      │
│  - btnProduct      │  (Dock = Fill)                                         │
│  - btnCustomer     │                                                        │
│  - btnReports      │  [Vùng nhúng động các Form con]                        │
│  - btnUserManage   │                                                        │
│  ───────────────   │                                                        │
│  - panelUserFooter │                                                        │
│    (btnLogout)     │                                                        │
└────────────────────┴────────────────────────────────────────────────────────┘
```

------------------------------------------------------------------------

## 3. Công nghệ sử dụng

### Backend

-   C# / .NET 8.0
-   ASP.NET Core Web API
-   Entity Framework Core 8.0 (SQL Server)
-   JWT Bearer Authentication
-   Swagger UI (có nút Authorize nhập token)

### Frontend

-   Windows Forms .NET 8.0
-   `HttpClient`, `System.Net.Http.Json`
-   `DataGridView`, `TableLayoutPanel`, `ErrorProvider`

### Công cụ

-   Visual Studio 2022
-   SQL Server Management Studio (SSMS)
-   Swagger UI

------------------------------------------------------------------------

## 4. Các gói NuGet

Trong project `MiniSupermarket.API`:

``` text
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.Design
Microsoft.AspNetCore.Authentication.JwtBearer
System.IdentityModel.Tokens.Jwt
Swashbuckle.AspNetCore
```

### Chức năng

-   `Microsoft.AspNetCore.Authentication.JwtBearer`: xác thực token JWT
    trong request.
-   `System.IdentityModel.Tokens.Jwt`: tạo và ký token khi đăng nhập.
-   `Swashbuckle.AspNetCore`: Swagger UI để kiểm thử API.

------------------------------------------------------------------------

## 5. Bộ dữ liệu tài khoản mẫu (15 tài khoản kiểm thử)

Mật khẩu ban đầu của tất cả tài khoản: `123456`

| STT | Username     | Họ và tên         | Vai trò   | Phạm vi chức năng                        |
|----:|--------------|-------------------|-----------|------------------------------------------|
|   1 | admin01      | Nguyễn Quản Trị   | Admin     | Toàn quyền toàn bộ hệ thống              |
|   2 | admin02      | Trần Giám Đốc     | Admin     | Toàn quyền toàn bộ hệ thống              |
|   3 | cashier01    | Lê Thu Ngân       | Cashier   | Màn hình POS, Khách hàng                 |
|   4 | cashier02    | Phạm Bán Hàng     | Cashier   | Màn hình POS, Khách hàng                 |
|   5 | cashier03    | Hoàng Thu Ngân    | Cashier   | Màn hình POS, Khách hàng                 |
|   6 | cashier04    | Vũ Thị Quầy       | Cashier   | Màn hình POS, Khách hàng                 |
|   7 | cashier05    | Đỗ Bán Lẻ         | Cashier   | Màn hình POS, Khách hàng                 |
|   8 | ware01       | Ngô Quản Kho      | Warehouse | Sản phẩm, Nhóm hàng                      |
|   9 | ware02       | Bùi Kiểm Kê       | Warehouse | Sản phẩm, Nhóm hàng                      |
|  10 | ware03       | Dương Thủ Kho     | Warehouse | Sản phẩm, Nhóm hàng                      |
|  11 | ware04       | Lý Nhập Hàng      | Warehouse | Sản phẩm, Nhóm hàng                      |
|  12 | admin_backup | Đặng Hỗ Trợ       | Admin     | Toàn quyền toàn bộ hệ thống              |
|  13 | cashier06    | Hồ Ca Chiều       | Cashier   | Màn hình POS, Khách hàng                 |
|  14 | ware05       | Trương Vận Chuyển | Warehouse | Sản phẩm, Nhóm hàng                      |
|  15 | supervisor   | Mai Giám Sát      | Admin     | Toàn quyền toàn bộ hệ thống              |

Dữ liệu được nạp tự động bằng `HasData` trong `SupermarketDbContext`
(bảng `Users`).

------------------------------------------------------------------------

## 6. Nội dung thực hành từng bước

### Bước 1: Thiết kế khung FormMainShell

Thuộc tính của `FormMainShell`:

| Thuộc tính    | Giá trị                                               |
|---------------|-------------------------------------------------------|
| Size          | 1280 x 720                                            |
| StartPosition | CenterScreen                                          |
| Text          | Hệ thống Quản lý Bán lẻ & Tồn kho Siêu thị Mini       |

Các vùng giao diện:

| Control            | Cấu hình                                                       |
|--------------------|----------------------------------------------------------------|
| `panelSidebar`     | Dock = Left, Width = 230, nền xanh đen `(24, 30, 48)`          |
| `panelTopHeader`   | Dock = Top, Height = 60, nền trắng, chứa `lblTitle` (tiêu đề màn hình) và `lblUserInfo` (thông tin nhân viên) |
| `panelMainContent` | Dock = Fill, nền `(244, 245, 247)`, vùng nhúng Form con        |
| `panelUserFooter`  | Dock = Bottom, Height = 60, chứa `btnLogout`                   |

Các nút điều hướng trên Sidebar (`FlatStyle = Flat`, `BorderSize = 0`,
chữ trắng, cao 45, căn trái):

| Nút             | Chức năng               |
|-----------------|-------------------------|
| `btnPOS`        | Bán hàng (POS)          |
| `btnCategory`   | Quản lý Danh mục        |
| `btnProduct`    | Quản lý Sản phẩm        |
| `btnCustomer`   | Quản lý Khách hàng      |
| `btnReports`    | Báo cáo Doanh thu       |
| `btnUserManage` | Quản trị Tài khoản      |
| `btnLogout`     | Đăng xuất               |

### Bước 2: Cơ chế nạp Form con động vào Main Panel

Hàm `OpenChildForm` trong `FormMainShell.cs`:

``` csharp
private void OpenChildForm(Form childForm, string screenTitle, Button senderButton)
{
    ActiveControl = null;                      // tránh lỗi focus khi đóng form cũ

    if (_activeForm != null)
    {
        if (_activeForm == _posForm) _activeForm.Hide();   // POS ẩn để giữ giỏ hàng
        else _activeForm.Close();
    }

    HighlightActiveButton(senderButton);

    _activeForm = childForm;
    childForm.TopLevel = false;                        // không phải cửa sổ độc lập
    childForm.FormBorderStyle = FormBorderStyle.None;  // bỏ viền
    childForm.Dock = DockStyle.Fill;                   // tràn vùng chứa

    panelMainContent.Controls.Clear();
    panelMainContent.Controls.Add(childForm);
    lblTitle.Text = screenTitle;                       // đồng bộ tiêu đề header
    childForm.BringToFront();
    childForm.Show();
}
```

Hàm `HighlightActiveButton` trả các nút Sidebar về màu gốc rồi tô màu xanh
`(41, 100, 180)` cho nút đang chọn (duyệt đệ quy cả panel con, bỏ qua
`btnLogout`).

Các sự kiện click được gắn bằng code trong `WireEvents()` theo kiểu gỡ
trước rồi gắn lại (`-=` rồi `+=`) nên không bao giờ chạy 2 lần.

### Bước 3: Logic phân quyền theo vai trò

Sau khi đăng nhập, `FormMainShell_Load` thực hiện 3 việc:

1.  Hiển thị `Nhân viên: <Họ tên> (<username>) | Vai trò: [<Role>]`.
2.  Gọi `ApplyRolePermissions(role)` để ẩn/hiện nút theo vai trò.
3.  Gọi `OpenDefaultScreenByRole(role)` để mở màn hình mặc định.

``` csharp
switch ((role ?? string.Empty).ToUpper())
{
    case "ADMIN":      // toàn bộ 6 nút đều hiện
    case "CASHIER":    // chỉ btnPOS, btnCustomer
    case "WAREHOUSE":  // chỉ btnCategory, btnProduct
    default:           // vai trò lạ: báo cảnh báo và đóng form
}
```

### Bước 4: Các Form con nghiệp vụ

| Form                    | Dành cho           | Chức năng chính                                                                 |
|-------------------------|--------------------|---------------------------------------------------------------------------------|
| `FormCategoryManagement`| Admin, Warehouse   | Xem nhóm hàng, Admin mới được thêm/sửa/xóa                                      |
| `FormProductManagement` | Admin, Warehouse   | Tìm theo mã vạch/tên, thêm, sửa, xóa sản phẩm, chọn danh mục, kiểm tra dữ liệu  |
| `FormCustomerManagement`| Admin, Cashier     | Quản lý khách hàng thân thiết và điểm tích lũy                                  |
| `FormPOS`               | Admin, Cashier     | Quét mã vạch, giỏ hàng, tính tiền thừa, thanh toán (phím tắt F9, F2, Delete, +/-) |
| `FormQuickReport`       | Chỉ Admin          | Báo cáo doanh thu theo khoảng ngày (`reports/summary`)                          |
| `FormUserManagement`    | Chỉ Admin          | Quản trị tài khoản (xem mục 8)                                                  |

### Bước 5: Gắn các Form vào Sidebar

``` csharp
private void btnCategory_Click(object? s, EventArgs e) =>
    OpenChildForm(new FormCategoryManagement(), "QUẢN LÝ DANH MỤC NHÓM HÀNG", btnCategory);

private void btnPOS_Click(object? s, EventArgs e)
{
    _posForm ??= new FormPOS();    // giữ nguyên giỏ hàng khi chuyển màn hình
    OpenChildForm(_posForm, "HỆ THỐNG QUẦY BÁN HÀNG & THU NGÂN (POS)", btnPOS);
}

private void btnProduct_Click(object? s, EventArgs e) =>
    OpenChildForm(new FormProductManagement(), "QUẢN LÝ THÔNG TIN SẢN PHẨM & KHO HÀNG", btnProduct);

private void btnCustomer_Click(object? s, EventArgs e) =>
    OpenChildForm(new FormCustomerManagement(), "QUẢN LÝ KHÁCH HÀNG & TÍCH ĐIỂM", btnCustomer);
```

### Bước 6: Lớp bảo vệ logic ở cấp hành động

Ngoài việc ẩn nút, các nút nhạy cảm còn kiểm tra vai trò trong sự kiện
click:

``` csharp
private void btnReports_Click(object? sender, EventArgs e)
{
    if (!string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase))
    {
        MessageBox.Show("Bạn không có quyền xem dữ liệu tài chính của siêu thị!",
            "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        return;
    }
    OpenChildForm(new FormQuickReport(), "BÁO CÁO DOANH THU & HIỆU SUẤT", btnReports);
}
```

`btnUserManage_Click` bảo vệ tương tự (chỉ Admin).

### Bước 7: Đăng xuất

`btnLogout_Click` hỏi xác nhận, gọi `SessionManager.Clear()` (xóa token,
username, họ tên, vai trò) rồi đóng khung chính để `FormLogin` hiện lại.

------------------------------------------------------------------------

## 7. Phân quyền hai lớp

### Lớp 1: Giao diện WinForms

| Vai trò   | Form mở mặc định        | Các nút hiển thị                                              |
|-----------|-------------------------|---------------------------------------------------------------|
| Admin     | FormCategoryManagement  | POS, Danh mục, Sản phẩm, Khách hàng, Báo cáo, Quản trị tài khoản |
| Cashier   | FormPOS                 | POS, Khách hàng                                               |
| Warehouse | FormProductManagement   | Danh mục, Sản phẩm                                            |

### Lớp 2: Web API (`[Authorize]`)

| Controller           | Quyền truy cập                                                                 |
|----------------------|--------------------------------------------------------------------------------|
| `AuthController`     | `POST /api/auth/login` công khai, trả token + role; tài khoản bị khóa trả 403  |
| `CategoriesController` | Đã đăng nhập thì xem được; `POST/PUT/DELETE` chỉ Admin                       |
| `ProductsController` | Đã đăng nhập thì xem được; `POST/PUT/DELETE` chỉ Admin, Warehouse              |
| `CustomersController`| Admin, Cashier; riêng `DELETE` chỉ Admin                                       |
| `OrdersController`   | Admin, Cashier (`POST /api/orders/checkout`)                                   |
| `ReportsController`  | Chỉ Admin                                                                      |
| `UsersController`    | Chỉ Admin                                                                      |

Kết quả khi gọi sai quyền: không có token trả **401**, sai vai trò trả
**403**.

### Bảng tổng kết vai trò và trách nhiệm

| Vai trò   | Form khởi chạy mặc định | Các Form được phép mở                                                                      | Bị ngăn chặn                                                         |
|-----------|-------------------------|--------------------------------------------------------------------------------------------|----------------------------------------------------------------------|
| Admin     | FormCategoryManagement  | POS, Danh mục, Sản phẩm, Khách hàng, Báo cáo, Tài khoản                                    | Không bị chặn                                                        |
| Cashier   | FormPOS                 | FormPOS, FormCustomerManagement                                                            | Quản lý kho, sửa giá sản phẩm, xem báo cáo, quản lý tài khoản        |
| Warehouse | FormProductManagement   | FormProductManagement, FormCategoryManagement                                              | Bán hàng POS, xem doanh thu, tạo tài khoản, quản lý khách hàng       |

------------------------------------------------------------------------

## 8. Màn hình Quản trị Tài khoản (FormUserManagement)

Giao diện cùng phong cách với màn Quản lý sản phẩm: thanh tìm kiếm phía
trên, danh sách bên trái, khung thông tin và các nút bên phải.

### Tính năng

-   Tìm theo tên đăng nhập hoặc họ tên, lọc theo vai trò, dòng trạng thái
    báo số tài khoản và số tài khoản đang bị khóa.
-   Danh sách gồm: Mã, Tên đăng nhập, Họ và tên, Quyền hạn, Trạng thái.
    Tài khoản bị khóa hiển thị chữ xám, trạng thái màu đỏ.
-   **Thêm mới**: kiểm tra tên đăng nhập (từ 4 ký tự, không khoảng trắng,
    không trùng), mật khẩu (từ 6 ký tự), họ tên, vai trò.
-   **Cập nhật**: sửa họ tên và vai trò (không đổi tên đăng nhập).
-   **Khóa / Mở khóa** tài khoản (có hỏi xác nhận).
-   **Đặt lại MK**: nhập mật khẩu mới trong hộp thoại riêng.
-   **Nhập mới**: bỏ chọn và quay về chế độ thêm.
-   Báo lỗi theo từng ô bằng `ErrorProvider`, thông báo API rõ nguyên nhân
    và mã HTTP.

### API sử dụng

| Phương thức | Endpoint                          | Chức năng                         |
|-------------|-----------------------------------|-----------------------------------|
| GET         | `/api/users`                      | Lấy danh sách (không trả mật khẩu)|
| POST        | `/api/users`                      | Thêm tài khoản                    |
| PUT         | `/api/users/{id}`                 | Sửa họ tên, vai trò               |
| PUT         | `/api/users/{id}/toggle-lock`     | Khóa / mở khóa                    |
| PUT         | `/api/users/{id}/reset-password`  | Đặt lại mật khẩu                  |

### Ràng buộc bảo vệ ở server

-   Không cho tự khóa tài khoản đang đăng nhập.
-   Không cho tự hạ quyền tài khoản đang đăng nhập.
-   Tên đăng nhập trùng trả `409 Conflict`; vai trò không hợp lệ trả
    `400 Bad Request`.

------------------------------------------------------------------------

## 9. Hướng dẫn chạy và kiểm thử

### Khởi chạy

1.  Mở solution bằng Visual Studio 2022.
2.  Kiểm tra chuỗi kết nối trong `MiniSupermarket.API/appsettings.json`,
    chạy `Update-Database` nếu chưa có CSDL.
3.  Chạy project `MiniSupermarket.API` (profile https, cổng `7132`).
4.  Kiểm tra `BaseUrl` trong `SessionManager.cs` khớp cổng API:

``` csharp
private const string BaseUrl = "https://localhost:7132/api/";
```

5.  Chạy project `MiniSupermarket.WinForms` và đăng nhập.

### Kiểm thử phân quyền trên WinForms

| Tài khoản   | Kết quả mong đợi                                                          |
|-------------|---------------------------------------------------------------------------|
| `cashier01` | Sidebar chỉ có Bán hàng (POS) và Khách hàng; mở thẳng màn POS             |
| `ware01`    | Sidebar chỉ có Danh mục và Sản phẩm; mở thẳng màn Sản phẩm                |
| `admin01`   | Hiện đủ 6 nút; mở thẳng màn Danh mục                                      |
| Tài khoản bị khóa | Đăng nhập báo "Tài khoản đã bị khóa. Vui lòng liên hệ Admin!"       |

### Kiểm thử phân quyền trên Swagger

1.  Gọi `POST /api/auth/login` để lấy `token`.
2.  Bấm **Authorize**, dán token (không cần gõ chữ Bearer).
3.  Với token `cashier01`: gọi `GET /api/reports/summary` phải trả **403**.
4.  Với token `ware01`: gọi `POST /api/products` thành công, gọi
    `GET /api/users` phải trả **403**.
5.  Với token `admin01`: tất cả API trên đều thành công.

### Checklist đánh giá

-   [ ] Ứng dụng không mở hàng loạt cửa sổ rời rạc, mọi nghiệp vụ chạy
    trong `panelMainContent`.
-   [ ] Phân quyền đúng với 3 tài khoản `cashier01`, `ware01`, `admin01`.
-   [ ] Nút đang chọn đổi màu nổi bật, tiêu đề `lblTitle` đúng tên màn hình.
-   [ ] Cashier và Warehouse bị từ chối ở cả giao diện lẫn API khi vào
    chức năng của Admin.
-   [ ] Đăng xuất xóa phiên và quay về màn hình đăng nhập.

------------------------------------------------------------------------

## 10. Bài tập mở rộng: Báo cáo doanh thu nhanh (FormQuickReport)

Form con nhúng vào `panelMainContent`, chỉ Admin được mở:

-   Chọn khoảng ngày (Từ ngày - Đến ngày) xem báo cáo, nút **Xem báo cáo**.
-   Các thẻ tóm tắt: tổng doanh thu, tổng số hóa đơn, tổng số sản phẩm đã
    bán.
-   Hai lớp bảo vệ: nút `btnReports` bị ẩn với vai trò khác, và
    `btnReports_Click` kiểm tra lại vai trò trước khi mở form. API
    `reports/summary` cũng yêu cầu vai trò Admin.

------------------------------------------------------------------------

## 11. Lưu ý và hạn chế hiện tại

-   Mật khẩu đang lưu dạng văn bản thô trong bảng `Users` để phục vụ bài
    thực hành. Hệ thống thực tế cần băm mật khẩu (BCrypt/PBKDF2).
-   Khóa bí mật JWT và chuỗi kết nối SQL Server đang đặt trong mã/cấu hình,
    không nên đưa lên kho mã công khai khi triển khai thật.
-   Thanh toán ở Buổi 4 mới kiểm tra và trừ tồn kho, cộng điểm khách hàng
    (10.000đ = 1 điểm). Lưu hóa đơn vào bảng `Orders/OrderDetails` sẽ làm ở
    Buổi 5, khi đó báo cáo sẽ dùng số liệu thật từ hóa đơn.

------------------------------------------------------------------------

## 12. Cấu trúc thư mục dự án

``` text
MiniSupermarketSystem/
│
├── MiniSupermarket.API/
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── CategoriesController.cs
│   │   ├── ProductsController.cs
│   │   ├── CustomersController.cs
│   │   ├── OrdersController.cs
│   │   ├── ReportsController.cs
│   │   └── UsersController.cs
│   │
│   ├── Data/
│   │   └── SupermarketDbContext.cs
│   │
│   ├── Migrations/
│   │
│   ├── Models/
│   │   ├── Category.cs
│   │   ├── Product.cs
│   │   ├── Customer.cs
│   │   ├── Role.cs
│   │   └── User.cs
│   │
│   ├── appsettings.json
│   └── Program.cs
│
└── MiniSupermarket.WinForms/
    ├── FormLogin.cs
    ├── FormMainShell.cs
    ├── FormPOS.cs
    ├── FormCategoryManagement.cs
    ├── FormProductManagement.cs
    ├── FormCustomerManagement.cs
    ├── FormQuickReport.cs
    ├── FormUserManagement.cs
    ├── SessionManager.cs
    └── Program.cs
```

------------------------------------------------------------------------

## 13. Tác giả

- **Đề tài:** Phát triển Ứng dụng Thu ngân và Quản lý Hàng hóa cho Mô hình Bán lẻ – Cửa hàng Tiện lợi Alpha Mini
- **Họ tên sinh viên:** Mai Chí Cường
- **Mã sinh viên:** 2124110113
- **Lớp học phần:** CCQ2411D
