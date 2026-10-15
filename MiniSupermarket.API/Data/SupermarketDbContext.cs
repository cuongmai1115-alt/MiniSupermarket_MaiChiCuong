using System;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
    /// <summary>
    /// DbContext - Cửa hàng Tiện lợi Alpha Mini (Ứng dụng Thu ngân và Quản lý Hàng hóa)
    /// Quản lý 6 thực thể: Users, Categories, Products, Customers, Orders, OrderDetails.
    /// </summary>
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext(DbContextOptions<SupermarketDbContext> options) : base(options)
        {
        }

        // ---------- 6 BẢNG (DbSet) ----------
        public DbSet<User> Users { get; set; }                   // Tài khoản nhân viên
        public DbSet<Category> Categories { get; set; }          // Nhóm hàng
        public DbSet<Product> Products { get; set; }             // Hàng hóa & tồn kho
        public DbSet<Customer> Customers { get; set; }           // Khách hàng thân thiết
        public DbSet<Order> Orders { get; set; }                 // Hóa đơn bán lẻ
        public DbSet<OrderDetail> OrderDetails { get; set; }     // Chi tiết hóa đơn

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================================================
            // A. CẤU HÌNH RÀNG BUỘC (Unique Index, độ dài, giá trị mặc định)
            // Phải khai báo TRƯỚC phần seed. Sau khi sửa phần này cần Add-Migration mới.
            // =========================================================

            // User: tên đăng nhập duy nhất
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<User>().Property(u => u.Username).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.Role).HasMaxLength(20);

            // Category: tên nhóm hàng duy nhất
            modelBuilder.Entity<Category>().HasIndex(c => c.CategoryName).IsUnique();

            // Product: mã vạch duy nhất (quét tại quầy POS)
            modelBuilder.Entity<Product>().HasIndex(p => p.Barcode).IsUnique();

            // Customer: số điện thoại duy nhất (tra cứu tích điểm) + giá trị mặc định
            modelBuilder.Entity<Customer>().HasIndex(c => c.PhoneNumber).IsUnique();
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(c => c.RewardPoints).HasDefaultValue(0);
                entity.Property(c => c.MembershipRank).HasDefaultValue("Chuẩn");
            });

            // Order: mã hóa đơn duy nhất (VD: HD20261001-001)
            modelBuilder.Entity<Order>().HasIndex(o => o.OrderCode).IsUnique();

            // =========================================================
            // B. DATA SEEDING - THỨ TỰ: bảng cha trước, bảng con sau
            // Users -> Categories -> Products -> Customers -> Orders -> OrderDetails
            // (Products cần Categories; Orders cần Customers; OrderDetails cần Orders + Products)
            // =========================================================

            // =========================================================
            // 1. USERS - 15 tài khoản (Admin / Cashier / Warehouse)
            // Cashier01..06 được dùng ở cột CashierUsername của bảng Orders.
            // =========================================================
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin01", Password = "123456", FullName = "Nguyễn Quản Trị", Role = "Admin", IsActive = true },
                new User { Id = 2, Username = "admin02", Password = "123456", FullName = "Trần Giám Đốc", Role = "Admin", IsActive = true },
                new User { Id = 3, Username = "cashier01", Password = "123456", FullName = "Lê Thu Ngân", Role = "Cashier", IsActive = true },
                new User { Id = 4, Username = "cashier02", Password = "123456", FullName = "Phạm Bán Hàng", Role = "Cashier", IsActive = true },
                new User { Id = 5, Username = "cashier03", Password = "123456", FullName = "Hoàng Thu Ngân", Role = "Cashier", IsActive = true },
                new User { Id = 6, Username = "cashier04", Password = "123456", FullName = "Vũ Thị Quầy", Role = "Cashier", IsActive = true },
                new User { Id = 7, Username = "cashier05", Password = "123456", FullName = "Đỗ Bán Lẻ", Role = "Cashier", IsActive = true },
                new User { Id = 8, Username = "ware01", Password = "123456", FullName = "Ngô Quản Kho", Role = "Warehouse", IsActive = true },
                new User { Id = 9, Username = "ware02", Password = "123456", FullName = "Bùi Kiểm Kê", Role = "Warehouse", IsActive = true },
                new User { Id = 10, Username = "ware03", Password = "123456", FullName = "Dương Thủ Kho", Role = "Warehouse", IsActive = true },
                new User { Id = 11, Username = "ware04", Password = "123456", FullName = "Lý Nhập Hàng", Role = "Warehouse", IsActive = true },
                new User { Id = 12, Username = "admin_backup", Password = "123456", FullName = "Đặng Hỗ Trợ", Role = "Admin", IsActive = true },
                new User { Id = 13, Username = "cashier06", Password = "123456", FullName = "Hồ Ca Chiều", Role = "Cashier", IsActive = true },
                new User { Id = 14, Username = "ware05", Password = "123456", FullName = "Trương Vận Chuyển", Role = "Warehouse", IsActive = true },
                new User { Id = 15, Username = "supervisor", Password = "123456", FullName = "Mai Giám Sát", Role = "Admin", IsActive = true }
            );

            // =========================================================
            // 2. CATEGORIES - 15 nhóm hàng
            // =========================================================
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Nước giải khát",
                    Description = "Nước ngọt, nước suối, nước tăng lực, trà đóng chai"
                },

                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Sữa và sản phẩm từ sữa",
                    Description = "Sữa tươi, sữa chua, sữa hộp, phô mai"
                },

                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Bánh kẹo và snack",
                    Description = "Bánh quy, snack khoai tây, kẹo, chocolate"
                },

                new Category
                {
                    CategoryId = 4,
                    CategoryName = "Mì, phở, cháo ăn liền",
                    Description = "Mì gói, mì ly, phở và cháo ăn liền"
                },

                new Category
                {
                    CategoryId = 5,
                    CategoryName = "Cơm hộp và món ăn sẵn",
                    Description = "Cơm hộp, cơm cuộn, xôi, món ăn hâm nóng tại quầy"
                },

                new Category
                {
                    CategoryId = 6,
                    CategoryName = "Bánh mì và bánh tươi",
                    Description = "Bánh mì, sandwich, bánh bao, bánh ngọt trong ngày"
                },

                new Category
                {
                    CategoryId = 7,
                    CategoryName = "Kem và thực phẩm đông lạnh",
                    Description = "Kem ốc quế, xúc xích, cá viên và món chiên đông lạnh"
                },

                new Category
                {
                    CategoryId = 8,
                    CategoryName = "Cà phê và trà",
                    Description = "Cà phê lon, cà phê hòa tan, trà đóng chai"
                },

                new Category
                {
                    CategoryId = 9,
                    CategoryName = "Gia vị và thực phẩm khô",
                    Description = "Nước mắm, dầu ăn, đường, gia vị đóng gói nhỏ"
                },

                new Category
                {
                    CategoryId = 10,
                    CategoryName = "Đồ hộp và thực phẩm đóng gói",
                    Description = "Cá hộp, pate, rong biển ăn liền, thực phẩm đóng gói"
                },

                new Category
                {
                    CategoryId = 11,
                    CategoryName = "Trái cây và rau củ tiện lợi",
                    Description = "Trái cây tươi, trái cây cắt sẵn, salad đóng hộp"
                },

                new Category
                {
                    CategoryId = 12,
                    CategoryName = "Chăm sóc cá nhân",
                    Description = "Kem đánh răng, dầu gội, sữa tắm, khăn ướt"
                },

                new Category
                {
                    CategoryId = 13,
                    CategoryName = "Đồ gia dụng và vệ sinh",
                    Description = "Khăn giấy, túi rác, nước rửa chén, vật dụng sinh hoạt"
                },

                new Category
                {
                    CategoryId = 14,
                    CategoryName = "Văn phòng phẩm",
                    Description = "Bút, vở, băng keo và dụng cụ học tập"
                },

                new Category
                {
                    CategoryId = 15,
                    CategoryName = "Phụ kiện tiện ích",
                    Description = "Pin, cáp sạc, khẩu trang và phụ kiện dùng nhanh"
                }
            );

            // =========================================================
            // 3. PRODUCTS - 45 sản phẩm (3 sản phẩm / nhóm hàng)
            // CategoryId phải nằm trong 1..15. Barcode không được trùng.
            // =========================================================
            modelBuilder.Entity<Product>().HasData(
                // ----- NHÓM 1: NƯỚC GIẢI KHÁT -----
                new Product
                {
                    ProductId = 1,
                    Barcode = "8930000000011",
                    ProductName = "Nước ngọt Coca-Cola lon 330ml",
                    Price = 12000m,
                    StockQuantity = 240,
                    CategoryId = 1
                },

                new Product
                {
                    ProductId = 2,
                    Barcode = "8930000000028",
                    ProductName = "Nước tăng lực Number 1 chai 330ml",
                    Price = 11000m,
                    StockQuantity = 150,
                    CategoryId = 1
                },

                new Product
                {
                    ProductId = 3,
                    Barcode = "8930000000035",
                    ProductName = "Nước suối Aquafina chai 500ml",
                    Price = 6000m,
                    StockQuantity = 300,
                    CategoryId = 1
                },

                // ----- NHÓM 2: SỮA VÀ SẢN PHẨM TỪ SỮA -----
                new Product
                {
                    ProductId = 4,
                    Barcode = "8930000000042",
                    ProductName = "Sữa tươi Vinamilk 100% hộp 1L",
                    Price = 36000m,
                    StockQuantity = 60,
                    CategoryId = 2
                },

                new Product
                {
                    ProductId = 5,
                    Barcode = "8930000000059",
                    ProductName = "Sữa chua Vinamilk có đường hộp 100g",
                    Price = 8000m,
                    StockQuantity = 120,
                    CategoryId = 2
                },

                new Product
                {
                    ProductId = 6,
                    Barcode = "8930000000066",
                    ProductName = "Sữa tươi TH true MILK hộp 180ml",
                    Price = 9000m,
                    StockQuantity = 150,
                    CategoryId = 2
                },

                // ----- NHÓM 3: BÁNH KẸO VÀ SNACK -----
                new Product
                {
                    ProductId = 7,
                    Barcode = "8930000000073",
                    ProductName = "Snack khoai tây Lay's vị tự nhiên 52g",
                    Price = 14000m,
                    StockQuantity = 110,
                    CategoryId = 3
                },

                new Product
                {
                    ProductId = 8,
                    Barcode = "8930000000080",
                    ProductName = "Bánh quy Oreo vani 133g",
                    Price = 24000m,
                    StockQuantity = 90,
                    CategoryId = 3
                },

                new Product
                {
                    ProductId = 9,
                    Barcode = "8930000000097",
                    ProductName = "Chocolate KitKat 4 thanh 35g",
                    Price = 18000m,
                    StockQuantity = 80,
                    CategoryId = 3
                },

                // ----- NHÓM 4: MÌ, PHỞ, CHÁO ĂN LIỀN -----
                new Product
                {
                    ProductId = 10,
                    Barcode = "8930000000103",
                    ProductName = "Mì Hảo Hảo tôm chua cay gói 75g",
                    Price = 5000m,
                    StockQuantity = 300,
                    CategoryId = 4
                },

                new Product
                {
                    ProductId = 11,
                    Barcode = "8930000000110",
                    ProductName = "Mì ly Modern lẩu thái 65g",
                    Price = 12000m,
                    StockQuantity = 140,
                    CategoryId = 4
                },

                new Product
                {
                    ProductId = 12,
                    Barcode = "8930000000127",
                    ProductName = "Phở bò Vifon ăn liền gói 65g",
                    Price = 11000m,
                    StockQuantity = 100,
                    CategoryId = 4
                },

                // ----- NHÓM 5: CƠM HỘP VÀ MÓN ĂN SẴN -----
                new Product
                {
                    ProductId = 13,
                    Barcode = "8930000000134",
                    ProductName = "Cơm hộp gà teriyaki 300g",
                    Price = 35000m,
                    StockQuantity = 30,
                    CategoryId = 5
                },

                new Product
                {
                    ProductId = 14,
                    Barcode = "8930000000141",
                    ProductName = "Cơm cuộn rong biển (kimbap) 200g",
                    Price = 25000m,
                    StockQuantity = 35,
                    CategoryId = 5
                },

                new Product
                {
                    ProductId = 15,
                    Barcode = "8930000000158",
                    ProductName = "Xôi mặn gói 150g",
                    Price = 20000m,
                    StockQuantity = 30,
                    CategoryId = 5
                },

                // ----- NHÓM 6: BÁNH MÌ VÀ BÁNH TƯƠI -----
                new Product
                {
                    ProductId = 16,
                    Barcode = "8930000000165",
                    ProductName = "Bánh mì thịt nguội",
                    Price = 20000m,
                    StockQuantity = 40,
                    CategoryId = 6
                },

                new Product
                {
                    ProductId = 17,
                    Barcode = "8930000000172",
                    ProductName = "Sandwich gà phô mai",
                    Price = 25000m,
                    StockQuantity = 40,
                    CategoryId = 6
                },

                new Product
                {
                    ProductId = 18,
                    Barcode = "8930000000189",
                    ProductName = "Bánh bao nhân thịt trứng cút",
                    Price = 18000m,
                    StockQuantity = 50,
                    CategoryId = 6
                },

                // ----- NHÓM 7: KEM VÀ THỰC PHẨM ĐÔNG LẠNH -----
                new Product
                {
                    ProductId = 19,
                    Barcode = "8930000000196",
                    ProductName = "Kem ốc quế Cornetto vị chocolate",
                    Price = 22000m,
                    StockQuantity = 60,
                    CategoryId = 7
                },

                new Product
                {
                    ProductId = 20,
                    Barcode = "8930000000202",
                    ProductName = "Xúc xích nướng que hâm nóng",
                    Price = 15000m,
                    StockQuantity = 70,
                    CategoryId = 7
                },

                new Product
                {
                    ProductId = 21,
                    Barcode = "8930000000219",
                    ProductName = "Cá viên chiên đông lạnh 500g",
                    Price = 52000m,
                    StockQuantity = 35,
                    CategoryId = 7
                },

                // ----- NHÓM 8: CÀ PHÊ VÀ TRÀ -----
                new Product
                {
                    ProductId = 22,
                    Barcode = "8930000000226",
                    ProductName = "Cà phê sữa NESCAFÉ lon 170ml",
                    Price = 12000m,
                    StockQuantity = 120,
                    CategoryId = 8
                },

                new Product
                {
                    ProductId = 23,
                    Barcode = "8930000000233",
                    ProductName = "Cà phê hòa tan G7 3in1 hộp 16 gói",
                    Price = 48000m,
                    StockQuantity = 70,
                    CategoryId = 8
                },

                new Product
                {
                    ProductId = 24,
                    Barcode = "8930000000240",
                    ProductName = "Trà xanh Không Độ chai 455ml",
                    Price = 11000m,
                    StockQuantity = 130,
                    CategoryId = 8
                },

                // ----- NHÓM 9: GIA VỊ VÀ THỰC PHẨM KHÔ -----
                new Product
                {
                    ProductId = 25,
                    Barcode = "8930000000257",
                    ProductName = "Nước mắm Nam Ngư chai 500ml",
                    Price = 45000m,
                    StockQuantity = 60,
                    CategoryId = 9
                },

                new Product
                {
                    ProductId = 26,
                    Barcode = "8930000000264",
                    ProductName = "Dầu ăn Neptune 1L",
                    Price = 58000m,
                    StockQuantity = 50,
                    CategoryId = 9
                },

                new Product
                {
                    ProductId = 27,
                    Barcode = "8930000000271",
                    ProductName = "Đường tinh luyện 1kg",
                    Price = 26000m,
                    StockQuantity = 60,
                    CategoryId = 9
                },

                // ----- NHÓM 10: ĐỒ HỘP VÀ THỰC PHẨM ĐÓNG GÓI -----
                new Product
                {
                    ProductId = 28,
                    Barcode = "8930000000288",
                    ProductName = "Cá ngừ ngâm dầu Hạ Long 185g",
                    Price = 35000m,
                    StockQuantity = 60,
                    CategoryId = 10
                },

                new Product
                {
                    ProductId = 29,
                    Barcode = "8930000000295",
                    ProductName = "Pate gan Hạ Long hộp 150g",
                    Price = 30000m,
                    StockQuantity = 50,
                    CategoryId = 10
                },

                new Product
                {
                    ProductId = 30,
                    Barcode = "8930000000301",
                    ProductName = "Rong biển sấy giòn ăn liền 5g",
                    Price = 8000m,
                    StockQuantity = 120,
                    CategoryId = 10
                },

                // ----- NHÓM 11: TRÁI CÂY VÀ RAU CỦ TIỆN LỢI -----
                new Product
                {
                    ProductId = 31,
                    Barcode = "8930000000318",
                    ProductName = "Chuối tiêu 1kg",
                    Price = 32000m,
                    StockQuantity = 40,
                    CategoryId = 11
                },

                new Product
                {
                    ProductId = 32,
                    Barcode = "8930000000325",
                    ProductName = "Trái cây cắt sẵn hộp 250g",
                    Price = 30000m,
                    StockQuantity = 30,
                    CategoryId = 11
                },

                new Product
                {
                    ProductId = 33,
                    Barcode = "8930000000332",
                    ProductName = "Salad rau trộn hộp 200g",
                    Price = 28000m,
                    StockQuantity = 25,
                    CategoryId = 11
                },

                // ----- NHÓM 12: CHĂM SÓC CÁ NHÂN -----
                new Product
                {
                    ProductId = 34,
                    Barcode = "8930000000349",
                    ProductName = "Kem đánh răng P/S 180g",
                    Price = 34000m,
                    StockQuantity = 70,
                    CategoryId = 12
                },

                new Product
                {
                    ProductId = 35,
                    Barcode = "8930000000356",
                    ProductName = "Dầu gội Sunsilk chai 170g",
                    Price = 38000m,
                    StockQuantity = 50,
                    CategoryId = 12
                },

                new Product
                {
                    ProductId = 36,
                    Barcode = "8930000000363",
                    ProductName = "Khăn ướt Mama gói 20 tờ",
                    Price = 15000m,
                    StockQuantity = 90,
                    CategoryId = 12
                },

                // ----- NHÓM 13: ĐỒ GIA DỤNG VÀ VỆ SINH -----
                new Product
                {
                    ProductId = 37,
                    Barcode = "8930000000370",
                    ProductName = "Khăn giấy rút Pulppy 100 tờ",
                    Price = 22000m,
                    StockQuantity = 100,
                    CategoryId = 13
                },

                new Product
                {
                    ProductId = 38,
                    Barcode = "8930000000387",
                    ProductName = "Nước rửa chén Sunlight chai 750ml",
                    Price = 38000m,
                    StockQuantity = 60,
                    CategoryId = 13
                },

                new Product
                {
                    ProductId = 39,
                    Barcode = "8930000000394",
                    ProductName = "Túi rác đen tự hủy cuộn 1kg",
                    Price = 25000m,
                    StockQuantity = 60,
                    CategoryId = 13
                },

                // ----- NHÓM 14: VĂN PHÒNG PHẨM -----
                new Product
                {
                    ProductId = 40,
                    Barcode = "8930000000400",
                    ProductName = "Bút bi Thiên Long TL-027 xanh",
                    Price = 5000m,
                    StockQuantity = 200,
                    CategoryId = 14
                },

                new Product
                {
                    ProductId = 41,
                    Barcode = "8930000000417",
                    ProductName = "Vở Campus 96 trang",
                    Price = 14000m,
                    StockQuantity = 100,
                    CategoryId = 14
                },

                new Product
                {
                    ProductId = 42,
                    Barcode = "8930000000424",
                    ProductName = "Băng keo trong 2.4cm",
                    Price = 8000m,
                    StockQuantity = 120,
                    CategoryId = 14
                },

                // ----- NHÓM 15: PHỤ KIỆN TIỆN ÍCH -----
                new Product
                {
                    ProductId = 43,
                    Barcode = "8930000000431",
                    ProductName = "Pin AA Panasonic vỉ 2 viên",
                    Price = 28000m,
                    StockQuantity = 80,
                    CategoryId = 15
                },

                new Product
                {
                    ProductId = 44,
                    Barcode = "8930000000448",
                    ProductName = "Cáp sạc USB-C 1m",
                    Price = 69000m,
                    StockQuantity = 40,
                    CategoryId = 15
                },

                new Product
                {
                    ProductId = 45,
                    Barcode = "8930000000455",
                    ProductName = "Khẩu trang y tế 4 lớp hộp 10 cái",
                    Price = 18000m,
                    StockQuantity = 100,
                    CategoryId = 15
                }
            );

            // =========================================================
            // 4. CUSTOMERS - 20 khách thành viên + 1 khách lẻ (Id 21)
            // Địa chỉ theo đơn vị hành chính từ 01/07/2025 (không còn cấp Quận/Huyện).
            // =========================================================
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    CustomerName = "Nguyễn Văn An",
                    PhoneNumber = "0901000001",
                    Address = "18 Nguyễn Huệ, Phường Sài Gòn, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 850
                },

                new Customer
                {
                    CustomerId = 2,
                    CustomerName = "Trần Thị Bình",
                    PhoneNumber = "0901000002",
                    Address = "45 Lê Thánh Tôn, Phường Sài Gòn, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 420
                },

                new Customer
                {
                    CustomerId = 3,
                    CustomerName = "Lê Văn Cường",
                    PhoneNumber = "0901000003",
                    Address = "120 Phạm Ngũ Lão, Phường Bến Thành, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 120
                },

                new Customer
                {
                    CustomerId = 4,
                    CustomerName = "Phạm Thị Dung",
                    PhoneNumber = "0901000004",
                    Address = "62 Bùi Viện, Phường Bến Thành, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 720
                },

                new Customer
                {
                    CustomerId = 5,
                    CustomerName = "Hoàng Văn Đức",
                    PhoneNumber = "0901000005",
                    Address = "160 Nguyễn Trãi, Phường Bến Thành, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 350
                },

                new Customer
                {
                    CustomerId = 6,
                    CustomerName = "Võ Thị Hạnh",
                    PhoneNumber = "0901000006",
                    Address = "75 Nguyễn Thái Học, Phường Cầu Ông Lãnh, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 90
                },

                new Customer
                {
                    CustomerId = 7,
                    CustomerName = "Đặng Minh Hoàng",
                    PhoneNumber = "0901000007",
                    Address = "30 Cô Giang, Phường Cầu Ông Lãnh, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 480
                },

                new Customer
                {
                    CustomerId = 8,
                    CustomerName = "Bùi Thị Lan",
                    PhoneNumber = "0901000008",
                    Address = "27 Trần Quang Khải, Phường Tân Định, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 920
                },

                new Customer
                {
                    CustomerId = 9,
                    CustomerName = "Ngô Văn Minh",
                    PhoneNumber = "0901000009",
                    Address = "95 Nguyễn Văn Nguyễn, Phường Tân Định, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 160
                },

                new Customer
                {
                    CustomerId = 10,
                    CustomerName = "Đỗ Thị Ngọc",
                    PhoneNumber = "0901000010",
                    Address = "38 Tôn Thất Thuyết, Phường Vĩnh Hội, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 390
                },

                new Customer
                {
                    CustomerId = 11,
                    CustomerName = "Phan Văn Phúc",
                    PhoneNumber = "0901000011",
                    Address = "220 Hoàng Diệu, Phường Khánh Hội, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 75
                },

                new Customer
                {
                    CustomerId = 12,
                    CustomerName = "Nguyễn Thị Quỳnh",
                    PhoneNumber = "0901000012",
                    Address = "346 Quang Trung, Phường Gò Vấp, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 780
                },

                new Customer
                {
                    CustomerId = 13,
                    CustomerName = "Trương Văn Sơn",
                    PhoneNumber = "0901000013",
                    Address = "88 Võ Văn Ngân, Phường Thủ Đức, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 510
                },

                new Customer
                {
                    CustomerId = 14,
                    CustomerName = "Lý Thị Thảo",
                    PhoneNumber = "0901000014",
                    Address = "15 Lê Văn Thọ, Phường Thông Tây Hội, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 210
                },

                new Customer
                {
                    CustomerId = 15,
                    CustomerName = "Mai Văn Thành",
                    PhoneNumber = "0901000015",
                    Address = "212 Nguyễn Hữu Cảnh, Phường Thạnh Mỹ Tây, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 440
                },

                new Customer
                {
                    CustomerId = 16,
                    CustomerName = "Huỳnh Thị Uyên",
                    PhoneNumber = "0901000016",
                    Address = "85 Xô Viết Nghệ Tĩnh, Phường Thạnh Mỹ Tây, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 1100
                },

                new Customer
                {
                    CustomerId = 17,
                    CustomerName = "Dương Văn Việt",
                    PhoneNumber = "0901000017",
                    Address = "40 Phan Xích Long, Phường Cầu Kiệu, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 130
                },

                new Customer
                {
                    CustomerId = 18,
                    CustomerName = "Phan Thị Xuân",
                    PhoneNumber = "0901000018",
                    Address = "150 Hoàng Văn Thụ, Phường Tân Sơn Nhất, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 560
                },

                new Customer
                {
                    CustomerId = 19,
                    CustomerName = "Nguyễn Quốc Anh",
                    PhoneNumber = "0901000019",
                    Address = "55 Huỳnh Tấn Phát, Phường Tân Thuận, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 180
                },

                new Customer
                {
                    CustomerId = 20,
                    CustomerName = "Trần Minh Khoa",
                    PhoneNumber = "0901000020",
                    Address = "120 Lê Văn Việt, Phường Tăng Nhơn Phú, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 950
                },

                // Khách lẻ (không có thẻ thành viên): Orders.CustomerId bắt buộc nên hóa đơn của khách vãng lai gắn vào dòng này.
                // OrdersController.Checkout dùng CustomerId = 21 này. KHÔNG xóa / đổi Id.
                new Customer
                {
                    CustomerId = 21,
                    CustomerName = "Khách lẻ",
                    PhoneNumber = "0000000000",
                    Address = null,
                    MembershipRank = "Chuẩn",
                    RewardPoints = 0
                }
            );

            // =========================================================
            // 5. ORDERS - 15 hóa đơn (ngày 01-07/10/2026)
            // CustomerId trong 1..20; CashierUsername là cashier01..06.
            // TotalItems = tổng Quantity, FinalAmount = tổng LineTotal ở OrderDetails; điểm thưởng = FinalAmount / 10.000.
            // =========================================================
            modelBuilder.Entity<Order>().HasData(
                    new Order { OrderId = 1, OrderCode = "HD20261001-001", CreatedDate = new DateTime(2026, 10, 1, 8, 30, 0), CustomerId = 1, CashierUsername = "cashier01", TotalItems = 3, FinalAmount = 62000m, RewardPoints = 6 },
                    new Order { OrderId = 2, OrderCode = "HD20261001-002", CreatedDate = new DateTime(2026, 10, 1, 9, 15, 0), CustomerId = 2, CashierUsername = "cashier01", TotalItems = 2, FinalAmount = 36000m, RewardPoints = 3 },
                    new Order { OrderId = 3, OrderCode = "HD20261001-003", CreatedDate = new DateTime(2026, 10, 1, 10, 0, 0), CustomerId = 3, CashierUsername = "cashier02", TotalItems = 10, FinalAmount = 50000m, RewardPoints = 5 },
                    new Order { OrderId = 4, OrderCode = "HD20261002-001", CreatedDate = new DateTime(2026, 10, 2, 8, 45, 0), CustomerId = 4, CashierUsername = "cashier02", TotalItems = 4, FinalAmount = 144000m, RewardPoints = 14 },
                    new Order { OrderId = 5, OrderCode = "HD20261002-002", CreatedDate = new DateTime(2026, 10, 2, 11, 20, 0), CustomerId = 5, CashierUsername = "cashier03", TotalItems = 5, FinalAmount = 68000m, RewardPoints = 6 },
                    new Order { OrderId = 6, OrderCode = "HD20261003-001", CreatedDate = new DateTime(2026, 10, 3, 14, 10, 0), CustomerId = 6, CashierUsername = "cashier03", TotalItems = 3, FinalAmount = 42000m, RewardPoints = 4 },
                    new Order { OrderId = 7, OrderCode = "HD20261003-002", CreatedDate = new DateTime(2026, 10, 3, 16, 40, 0), CustomerId = 7, CashierUsername = "cashier04", TotalItems = 5, FinalAmount = 30000m, RewardPoints = 3 },
                    new Order { OrderId = 8, OrderCode = "HD20261004-001", CreatedDate = new DateTime(2026, 10, 4, 9, 5, 0), CustomerId = 8, CashierUsername = "cashier04", TotalItems = 2, FinalAmount = 103000m, RewardPoints = 10 },
                    new Order { OrderId = 9, OrderCode = "HD20261004-002", CreatedDate = new DateTime(2026, 10, 4, 13, 30, 0), CustomerId = 9, CashierUsername = "cashier05", TotalItems = 3, FinalAmount = 35000m, RewardPoints = 3 },
                    new Order { OrderId = 10, OrderCode = "HD20261005-001", CreatedDate = new DateTime(2026, 10, 5, 10, 50, 0), CustomerId = 10, CashierUsername = "cashier05", TotalItems = 4, FinalAmount = 124000m, RewardPoints = 12 },
                    new Order { OrderId = 11, OrderCode = "HD20261005-002", CreatedDate = new DateTime(2026, 10, 5, 17, 15, 0), CustomerId = 11, CashierUsername = "cashier06", TotalItems = 3, FinalAmount = 36000m, RewardPoints = 3 },
                    new Order { OrderId = 12, OrderCode = "HD20261006-001", CreatedDate = new DateTime(2026, 10, 6, 8, 20, 0), CustomerId = 12, CashierUsername = "cashier06", TotalItems = 3, FinalAmount = 82000m, RewardPoints = 8 },
                    new Order { OrderId = 13, OrderCode = "HD20261006-002", CreatedDate = new DateTime(2026, 10, 6, 15, 0, 0), CustomerId = 13, CashierUsername = "cashier01", TotalItems = 3, FinalAmount = 74000m, RewardPoints = 7 },
                    new Order { OrderId = 14, OrderCode = "HD20261007-001", CreatedDate = new DateTime(2026, 10, 7, 12, 10, 0), CustomerId = 14, CashierUsername = "cashier02", TotalItems = 1, FinalAmount = 58000m, RewardPoints = 5 },
                    new Order { OrderId = 15, OrderCode = "HD20261007-002", CreatedDate = new DateTime(2026, 10, 7, 18, 45, 0), CustomerId = 15, CashierUsername = "cashier03", TotalItems = 3, FinalAmount = 40000m, RewardPoints = 4 }
            );

            // =========================================================
            // 6. ORDERDETAILS - 26 dòng chi tiết
            // OrderId trong 1..15, ProductId lấy từ bảng Products; UnitPrice = giá hiện tại của sản phẩm;
            // LineTotal = Quantity * UnitPrice.
            // =========================================================
            modelBuilder.Entity<OrderDetail>().HasData(
                    new OrderDetail { OrderDetailId = 1, OrderId = 1, ProductId = 1, Quantity = 1, UnitPrice = 12000m, LineTotal = 12000m },
                    new OrderDetail { OrderDetailId = 2, OrderId = 1, ProductId = 7, Quantity = 1, UnitPrice = 14000m, LineTotal = 14000m },
                    new OrderDetail { OrderDetailId = 3, OrderId = 1, ProductId = 4, Quantity = 1, UnitPrice = 36000m, LineTotal = 36000m },
                    new OrderDetail { OrderDetailId = 4, OrderId = 2, ProductId = 8, Quantity = 1, UnitPrice = 24000m, LineTotal = 24000m },
                    new OrderDetail { OrderDetailId = 5, OrderId = 2, ProductId = 1, Quantity = 1, UnitPrice = 12000m, LineTotal = 12000m },
                    new OrderDetail { OrderDetailId = 6, OrderId = 3, ProductId = 10, Quantity = 10, UnitPrice = 5000m, LineTotal = 50000m },
                    new OrderDetail { OrderDetailId = 7, OrderId = 4, ProductId = 4, Quantity = 4, UnitPrice = 36000m, LineTotal = 144000m },
                    new OrderDetail { OrderDetailId = 8, OrderId = 5, ProductId = 4, Quantity = 1, UnitPrice = 36000m, LineTotal = 36000m },
                    new OrderDetail { OrderDetailId = 9, OrderId = 5, ProductId = 5, Quantity = 4, UnitPrice = 8000m, LineTotal = 32000m },
                    new OrderDetail { OrderDetailId = 10, OrderId = 6, ProductId = 9, Quantity = 2, UnitPrice = 18000m, LineTotal = 36000m },
                    new OrderDetail { OrderDetailId = 11, OrderId = 6, ProductId = 3, Quantity = 1, UnitPrice = 6000m, LineTotal = 6000m },
                    new OrderDetail { OrderDetailId = 12, OrderId = 7, ProductId = 3, Quantity = 5, UnitPrice = 6000m, LineTotal = 30000m },
                    new OrderDetail { OrderDetailId = 13, OrderId = 8, ProductId = 26, Quantity = 1, UnitPrice = 58000m, LineTotal = 58000m },
                    new OrderDetail { OrderDetailId = 14, OrderId = 8, ProductId = 25, Quantity = 1, UnitPrice = 45000m, LineTotal = 45000m },
                    new OrderDetail { OrderDetailId = 15, OrderId = 9, ProductId = 24, Quantity = 1, UnitPrice = 11000m, LineTotal = 11000m },
                    new OrderDetail { OrderDetailId = 16, OrderId = 9, ProductId = 22, Quantity = 2, UnitPrice = 12000m, LineTotal = 24000m },
                    new OrderDetail { OrderDetailId = 17, OrderId = 10, ProductId = 8, Quantity = 2, UnitPrice = 24000m, LineTotal = 48000m },
                    new OrderDetail { OrderDetailId = 18, OrderId = 10, ProductId = 38, Quantity = 2, UnitPrice = 38000m, LineTotal = 76000m },
                    new OrderDetail { OrderDetailId = 19, OrderId = 11, ProductId = 11, Quantity = 3, UnitPrice = 12000m, LineTotal = 36000m },
                    new OrderDetail { OrderDetailId = 20, OrderId = 12, ProductId = 13, Quantity = 2, UnitPrice = 35000m, LineTotal = 70000m },
                    new OrderDetail { OrderDetailId = 21, OrderId = 12, ProductId = 1, Quantity = 1, UnitPrice = 12000m, LineTotal = 12000m },
                    new OrderDetail { OrderDetailId = 22, OrderId = 13, ProductId = 34, Quantity = 1, UnitPrice = 34000m, LineTotal = 34000m },
                    new OrderDetail { OrderDetailId = 23, OrderId = 13, ProductId = 16, Quantity = 2, UnitPrice = 20000m, LineTotal = 40000m },
                    new OrderDetail { OrderDetailId = 24, OrderId = 14, ProductId = 26, Quantity = 1, UnitPrice = 58000m, LineTotal = 58000m },
                    new OrderDetail { OrderDetailId = 25, OrderId = 15, ProductId = 7, Quantity = 2, UnitPrice = 14000m, LineTotal = 28000m },
                    new OrderDetail { OrderDetailId = 26, OrderId = 15, ProductId = 1, Quantity = 1, UnitPrice = 12000m, LineTotal = 12000m }
            );
        }
    }
}