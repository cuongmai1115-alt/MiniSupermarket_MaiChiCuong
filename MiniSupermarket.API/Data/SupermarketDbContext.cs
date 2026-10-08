using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
    /// <summary>
    /// DbContext - Cửa hàng Tiện lợi Alpha Mini
    /// (Ứng dụng Thu ngân và Quản lý Hàng hóa cho Mô hình Bán lẻ)
    /// </summary>
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext(
            DbContextOptions<SupermarketDbContext> options
        ) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================================================
            // 1. NHÓM HÀNG - 15 DỮ LIỆU (mô hình cửa hàng tiện lợi)
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
            // 2. SẢN PHẨM - 45 DỮ LIỆU (3 sản phẩm / nhóm hàng)
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
            // 3. CẤU HÌNH KHÁCH HÀNG
            // =========================================================
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(c => c.RewardPoints)
                    .HasDefaultValue(0);

                entity.Property(c => c.MembershipRank)
                    .HasDefaultValue("Chuẩn");
            });

            // =========================================================
            // 4. KHÁCH HÀNG THÀNH VIÊN - 20 DỮ LIỆU
            //    Địa chỉ theo đơn vị hành chính hiện hành (từ 01/07/2025):
            //    không còn cấp Quận/Huyện, ghi: số nhà, đường, Phường, TP. Hồ Chí Minh
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
                }
            );
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<User>().Property(u => u.Username).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.Role).HasMaxLength(20);

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

        }
    }
}