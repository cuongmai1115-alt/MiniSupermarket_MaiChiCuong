# 🛒 HỆ THỐNG QUẢN LÝ SIÊU THỊ MINI (MINISUPERMARKET SYSTEM)

> **Môn học:** Lập trình Ứng dụng .NET Core (Mã môn: 229162)
> **Buổi thực hành:** Buổi 1 - Xây dựng Web API quản lý danh mục và kết nối WinForms Client (CRUD)
> **Buổi thực hành:** Buổi 2 - Bảo mật & Phân quyền JWT cho Web API

---

## 🏗️ 1. Mô hình Kiến trúc Hệ thống (Client - Server)

Dự án được xây dựng theo mô hình phân tầng hiện đại, tách biệt hoàn toàn giữa Backend và Frontend:

* **`MiniSupermarket.API` (Backend):** Dự án ASP.NET Core Web API chịu trách nhiệm xử lý logic nghiệp vụ, xác thực bảo mật JWT, quản lý dữ liệu và cung cấp các RESTful API chuẩn hóa.
* **`MiniSupermarket.WinForms` (Frontend Client):** Ứng dụng Windows Forms đóng vai trò là máy trạm POS tại quầy, có màn hình đăng nhập, lưu phiên làm việc (Token) và sử dụng `HttpClient` gắn kèm Bearer Token để gọi dữ liệu từ API, hiển thị trực quan lên `DataGridView`.

---

## 🔐 2. Cơ chế Bảo mật (JWT Authentication & Authorization)

* **Xác thực (Authentication):** Người dùng đăng nhập qua `POST /api/auth/login`, hệ thống trả về JWT Token (Stateless — server không lưu Session).
* **Phân quyền (Authorization):** Token chứa Claim `Role`, các endpoint CRUD được bảo vệ bằng `[Authorize(Roles = "Admin")]`:
  * **Admin**: toàn quyền Thêm / Sửa / Xóa nhóm hàng.
  * **Cashier**: chỉ được Xem và Tìm kiếm.
* **Không có Token** → `401 Unauthorized`. **Sai quyền** → `403 Forbidden`.

### Tài khoản demo

| Username | Password | Role |
|---|---|---|
| admin | 123456 | Admin |
| cashier | 123456 | Cashier |

---

## 🛠️ 3. Công nghệ Sử dụng

* **Ngôn ngữ:** C# (.NET 8.0)
* **Backend:** ASP.NET Core Web API, Controllers, In-Memory Data, LINQ
* **Bảo mật:** JWT Bearer Authentication (`System.IdentityModel.Tokens.Jwt`, `Microsoft.AspNetCore.Authentication.JwtBearer`)
* **Frontend:** Windows Forms (.NET 8.0), `System.Net.Http.Json`
* **Công cụ kiểm thử:** Swagger UI

---

## 📂 4. Cấu trúc Solution

```text
MiniSupermarketSystem/
│
├── MiniSupermarket.API/          # Dự án Web API (Backend)
│   ├── Controllers/
│   │   ├── AuthController.cs         # Đăng nhập, cấp phát JWT Token
│   │   └── CategoriesController.cs   # CRUD & Search nhóm hàng, phân quyền Admin/Cashier
│   ├── Models/                   # Chứa lớp thực thể Category.cs
│   └── Program.cs                # Cấu hình dịch vụ, JWT Middleware
│
└── MiniSupermarket.WinForms/     # Dự án Windows Forms (Frontend Client)
    ├── FormLogin.cs               # Màn hình đăng nhập
    ├── FormCategoryManagement.cs  # Giao diện quản lý danh mục CRUD
    └── SessionManager.cs          # Lưu trữ JWT Token + Role của phiên đăng nhập
```

---

## 🚀 5. Hướng dẫn Chạy và Kiểm thử Dự án

### Bước 1: Chạy phía Backend (Web API)
1. Mở Solution bằng Visual Studio 2022.
2. Nhấp chuột phải vào project `MiniSupermarket.API` chọn **Set as Startup Project**.
3. Nhấn `F5` để chạy. Trình duyệt sẽ tự động mở giao diện Swagger UI để kiểm tra các phương thức GET, POST, PUT, DELETE.
4. Ghi nhớ cổng (port) đang chạy, ví dụ `https://localhost:7132`.

### Bước 2: Kiểm chứng bảo mật trên Swagger
1. Gọi `POST /api/auth/login` với tài khoản mẫu → copy `token` trả về.
2. Bấm nút **Authorize** (icon ổ khóa) trên Swagger UI → dán `Bearer <token>`.
3. Thử gọi `POST/PUT/DELETE /api/categories` bằng tài khoản `cashier` → nhận `403 Forbidden`.

### Bước 3: Chạy phía Frontend (WinForms Client)
1. Đảm bảo `BaseAddress` trong `FormLogin.cs` và `FormCategoryManagement.cs` khớp với cổng `https://localhost:XXXXX` của Web API đang chạy.
2. Nhấp chuột phải vào project `MiniSupermarket.WinForms` chọn **Debug -> Start new instance**.
3. Đăng nhập bằng tài khoản demo ở trên.
4. Thử nghiệm các chức năng: Tải danh sách, Thêm mới, Sửa, Xóa và Tìm kiếm nhóm hàng theo đúng phân quyền Role.

---

## 🗺️ 6. Roadmap

- [x] Buổi 1: CRUD Categories cơ bản (In-Memory)
- [x] Buổi 2: Bảo mật JWT + Phân quyền Admin/Cashier
- [ ] Buổi 3: Tích hợp SQL Server + Entity Framework Core (Code-First, Migrations)
- [ ] Mở rộng các bảng: Users, Products, Customers, Orders, OrderDetails

---

## 👨‍💻 7. Tác giả

* **Họ tên sinh viên:** Mai Chí Cường
* **Mã sinh viên:** 2124110113
* **Lớp học phần:** CCQ2411D
