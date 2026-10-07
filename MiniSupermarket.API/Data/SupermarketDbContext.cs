using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================================================
            // 1. DANH MỤC - 15 DỮ LIỆU
            // =========================================================
            modelBuilder.Entity<Category>().HasData(

                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Nước giải khát",
                    Description = "Nước ngọt, nước khoáng, nước trái cây"
                },

                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Sữa và sản phẩm từ sữa",
                    Description = "Sữa tươi, sữa chua, phô mai"
                },

                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Bánh kẹo",
                    Description = "Bánh quy, bánh ngọt, kẹo các loại"
                },

                new Category
                {
                    CategoryId = 4,
                    CategoryName = "Mì và thực phẩm ăn liền",
                    Description = "Mì gói, phở, cháo và thực phẩm ăn liền"
                },

                new Category
                {
                    CategoryId = 5,
                    CategoryName = "Gia vị và thực phẩm khô",
                    Description = "Nước mắm, dầu ăn, hạt nêm, đường"
                },

                new Category
                {
                    CategoryId = 6,
                    CategoryName = "Đồ dùng cá nhân",
                    Description = "Kem đánh răng, dầu gội, sữa tắm"
                },

                new Category
                {
                    CategoryId = 7,
                    CategoryName = "Đồ gia dụng",
                    Description = "Đồ dùng gia đình và vật dụng sinh hoạt"
                },

                new Category
                {
                    CategoryId = 8,
                    CategoryName = "Đồ ăn nhanh",
                    Description = "Xúc xích, sandwich, hamburger và đồ ăn nhanh"
                },

                new Category
                {
                    CategoryId = 9,
                    CategoryName = "Thực phẩm đông lạnh",
                    Description = "Thực phẩm đông lạnh và đồ chế biến sẵn"
                },

                new Category
                {
                    CategoryId = 10,
                    CategoryName = "Trái cây",
                    Description = "Các loại trái cây tươi"
                },

                new Category
                {
                    CategoryId = 11,
                    CategoryName = "Rau củ",
                    Description = "Rau củ quả tươi"
                },

                new Category
                {
                    CategoryId = 12,
                    CategoryName = "Đồ hộp",
                    Description = "Cá hộp, thịt hộp và thực phẩm đóng hộp"
                },

                new Category
                {
                    CategoryId = 13,
                    CategoryName = "Cà phê và trà",
                    Description = "Cà phê, trà túi lọc và trà đóng chai"
                },

                new Category
                {
                    CategoryId = 14,
                    CategoryName = "Văn phòng phẩm",
                    Description = "Bút, vở, giấy và dụng cụ học tập"
                },

                new Category
                {
                    CategoryId = 15,
                    CategoryName = "Chăm sóc nhà cửa",
                    Description = "Nước giặt, nước rửa chén và chất tẩy rửa"
                }
            );


            // =========================================================
            // 2. SẢN PHẨM - 45 DỮ LIỆU
            // =========================================================
            modelBuilder.Entity<Product>().HasData(

                // -----------------------------------------------------
                // DANH MỤC 1 - NƯỚC GIẢI KHÁT
                // -----------------------------------------------------

                new Product
                {
                    ProductId = 1,
                    Barcode = "8930000000011",
                    ProductName = "Nước ngọt Coca Cola lon 330ml",
                    Price = 10000m,
                    StockQuantity = 200,
                    CategoryId = 1
                },

                new Product
                {
                    ProductId = 2,
                    Barcode = "8930000000028",
                    ProductName = "Nước ngọt Pepsi lon 330ml",
                    Price = 10000m,
                    StockQuantity = 180,
                    CategoryId = 1
                },

                new Product
                {
                    ProductId = 3,
                    Barcode = "8930000000035",
                    ProductName = "Nước khoáng Lavie chai 500ml",
                    Price = 6000m,
                    StockQuantity = 250,
                    CategoryId = 1
                },


                // -----------------------------------------------------
                // DANH MỤC 2 - SỮA
                // -----------------------------------------------------

                new Product
                {
                    ProductId = 4,
                    Barcode = "8930000000042",
                    ProductName = "Sữa tươi Vinamilk 1L",
                    Price = 32000m,
                    StockQuantity = 80,
                    CategoryId = 2
                },

                new Product
                {
                    ProductId = 5,
                    Barcode = "8930000000059",
                    ProductName = "Sữa chua Vinamilk hộp",
                    Price = 7000m,
                    StockQuantity = 150,
                    CategoryId = 2
                },

                new Product
                {
                    ProductId = 6,
                    Barcode = "8930000000066",
                    ProductName = "Phô mai lát 140g",
                    Price = 38000m,
                    StockQuantity = 60,
                    CategoryId = 2
                },


                // -----------------------------------------------------
                // DANH MỤC 3 - BÁNH KẸO
                // -----------------------------------------------------

                new Product
                {
                    ProductId = 7,
                    Barcode = "8930000000073",
                    ProductName = "Bánh quy bơ 150g",
                    Price = 25000m,
                    StockQuantity = 100,
                    CategoryId = 3
                },

                new Product
                {
                    ProductId = 8,
                    Barcode = "8930000000080",
                    ProductName = "Bánh xốp chocolate 120g",
                    Price = 18000m,
                    StockQuantity = 90,
                    CategoryId = 3
                },

                new Product
                {
                    ProductId = 9,
                    Barcode = "8930000000097",
                    ProductName = "Kẹo dẻo trái cây 100g",
                    Price = 18000m,
                    StockQuantity = 120,
                    CategoryId = 3
                },


                // -----------------------------------------------------
                // DANH MỤC 4 - MÌ VÀ THỰC PHẨM ĂN LIỀN
                // -----------------------------------------------------

                new Product
                {
                    ProductId = 10,
                    Barcode = "8930000000103",
                    ProductName = "Mì Hảo Hảo tôm chua cay 75g",
                    Price = 5000m,
                    StockQuantity = 300,
                    CategoryId = 4
                },

                new Product
                {
                    ProductId = 11,
                    Barcode = "8930000000110",
                    ProductName = "Mì Omachi sườn hầm ngũ quả",
                    Price = 9000m,
                    StockQuantity = 150,
                    CategoryId = 4
                },

                new Product
                {
                    ProductId = 12,
                    Barcode = "8930000000127",
                    ProductName = "Phở bò ăn liền 65g",
                    Price = 9000m,
                    StockQuantity = 100,
                    CategoryId = 4
                },


                // -----------------------------------------------------
                // DANH MỤC 5 - GIA VỊ
                // -----------------------------------------------------

                new Product
                {
                    ProductId = 13,
                    Barcode = "8930000000134",
                    ProductName = "Nước mắm cá cơm 500ml",
                    Price = 45000m,
                    StockQuantity = 70,
                    CategoryId = 5
                },

                new Product
                {
                    ProductId = 14,
                    Barcode = "8930000000141",
                    ProductName = "Dầu ăn thực vật 1L",
                    Price = 55000m,
                    StockQuantity = 50,
                    CategoryId = 5
                },

                new Product
                {
                    ProductId = 15,
                    Barcode = "8930000000158",
                    ProductName = "Hạt nêm thịt thăn 400g",
                    Price = 38000m,
                    StockQuantity = 75,
                    CategoryId = 5
                },


                // -----------------------------------------------------
                // DANH MỤC 6 - ĐỒ DÙNG CÁ NHÂN
                // -----------------------------------------------------

                new Product
                {
                    ProductId = 16,
                    Barcode = "8930000000165",
                    ProductName = "Kem đánh răng P/S 180g",
                    Price = 32000m,
                    StockQuantity = 70,
                    CategoryId = 6
                },

                new Product
                {
                    ProductId = 17,
                    Barcode = "8930000000172",
                    ProductName = "Dầu gội Sunsilk 650g",
                    Price = 85000m,
                    StockQuantity = 40,
                    CategoryId = 6
                },

                new Product
                {
                    ProductId = 18,
                    Barcode = "8930000000189",
                    ProductName = "Sữa tắm Lifebuoy 800g",
                    Price = 90000m,
                    StockQuantity = 45,
                    CategoryId = 6
                },


                // -----------------------------------------------------
                // DANH MỤC 7 - ĐỒ GIA DỤNG
                // -----------------------------------------------------

                new Product
                {
                    ProductId = 19,
                    Barcode = "8930000000196",
                    ProductName = "Khăn giấy đa năng",
                    Price = 18000m,
                    StockQuantity = 100,
                    CategoryId = 7
                },

                new Product
                {
                    ProductId = 20,
                    Barcode = "8930000000202",
                    ProductName = "Túi rác tự phân hủy",
                    Price = 25000m,
                    StockQuantity = 60,
                    CategoryId = 7
                },

                new Product
                {
                    ProductId = 21,
                    Barcode = "8930000000219",
                    ProductName = "Hộp đựng thực phẩm 1L",
                    Price = 35000m,
                    StockQuantity = 50,
                    CategoryId = 7
                },


                // -----------------------------------------------------
                // DANH MỤC 8 - ĐỒ ĂN NHANH
                // -----------------------------------------------------

                new Product
                {
                    ProductId = 22,
                    Barcode = "8930000000226",
                    ProductName = "Xúc xích tiệt trùng 175g",
                    Price = 28000m,
                    StockQuantity = 90,
                    CategoryId = 8
                },

                new Product
                {
                    ProductId = 23,
                    Barcode = "8930000000233",
                    ProductName = "Sandwich sandwich thịt nguội",
                    Price = 25000m,
                    StockQuantity = 40,
                    CategoryId = 8
                },

                new Product
                {
                    ProductId = 24,
                    Barcode = "8930000000240",
                    ProductName = "Bánh bao nhân thịt",
                    Price = 15000m,
                    StockQuantity = 60,
                    CategoryId = 8
                },


                // -----------------------------------------------------
                // DANH MỤC 9 - THỰC PHẨM ĐÔNG LẠNH
                // -----------------------------------------------------

                new Product
                {
                    ProductId = 25,
                    Barcode = "8930000000257",
                    ProductName = "Cá viên chiên đông lạnh 500g",
                    Price = 45000m,
                    StockQuantity = 40,
                    CategoryId = 9
                },

                new Product
                {
                    ProductId = 26,
                    Barcode = "8930000000264",
                    ProductName = "Xúc xích Đức đông lạnh 500g",
                    Price = 65000m,
                    StockQuantity = 35,
                    CategoryId = 9
                },

                new Product
                {
                    ProductId = 27,
                    Barcode = "8930000000271",
                    ProductName = "Há cảo đông lạnh 300g",
                    Price = 55000m,
                    StockQuantity = 30,
                    CategoryId = 9
                },


                // -----------------------------------------------------
                // DANH MỤC 10 - TRÁI CÂY
                // -----------------------------------------------------

                new Product
                {
                    ProductId = 28,
                    Barcode = "8930000000288",
                    ProductName = "Táo Fuji nhập khẩu 1kg",
                    Price = 65000m,
                    StockQuantity = 30,
                    CategoryId = 10
                },

                new Product
                {
                    ProductId = 29,
                    Barcode = "8930000000295",
                    ProductName = "Chuối tiêu 1kg",
                    Price = 30000m,
                    StockQuantity = 40,
                    CategoryId = 10
                },

                new Product
                {
                    ProductId = 30,
                    Barcode = "8930000000301",
                    ProductName = "Cam vàng 1kg",
                    Price = 55000m,
                    StockQuantity = 35,
                    CategoryId = 10
                },


                // -----------------------------------------------------
                // DANH MỤC 11 - RAU CỦ
                // -----------------------------------------------------

                new Product
                {
                    ProductId = 31,
                    Barcode = "8930000000318",
                    ProductName = "Cà rốt Đà Lạt 500g",
                    Price = 18000m,
                    StockQuantity = 50,
                    CategoryId = 11
                },

                new Product
                {
                    ProductId = 32,
                    Barcode = "8930000000325",
                    ProductName = "Khoai tây 1kg",
                    Price = 28000m,
                    StockQuantity = 45,
                    CategoryId = 11
                },

                new Product
                {
                    ProductId = 33,
                    Barcode = "8930000000332",
                    ProductName = "Rau cải xanh 500g",
                    Price = 15000m,
                    StockQuantity = 40,
                    CategoryId = 11
                },


                // -----------------------------------------------------
                // DANH MỤC 12 - ĐỒ HỘP
                // -----------------------------------------------------

                new Product
                {
                    ProductId = 34,
                    Barcode = "8930000000349",
                    ProductName = "Cá ngừ đóng hộp 185g",
                    Price = 32000m,
                    StockQuantity = 60,
                    CategoryId = 12
                },

                new Product
                {
                    ProductId = 35,
                    Barcode = "8930000000356",
                    ProductName = "Thịt heo hầm đóng hộp 150g",
                    Price = 35000m,
                    StockQuantity = 50,
                    CategoryId = 12
                },

                new Product
                {
                    ProductId = 36,
                    Barcode = "8930000000363",
                    ProductName = "Đậu Hà Lan đóng hộp 400g",
                    Price = 28000m,
                    StockQuantity = 45,
                    CategoryId = 12
                },


                // -----------------------------------------------------
                // DANH MỤC 13 - CÀ PHÊ VÀ TRÀ
                // -----------------------------------------------------

                new Product
                {
                    ProductId = 37,
                    Barcode = "8930000000370",
                    ProductName = "Cà phê hòa tan 3in1",
                    Price = 45000m,
                    StockQuantity = 80,
                    CategoryId = 13
                },

                new Product
                {
                    ProductId = 38,
                    Barcode = "8930000000387",
                    ProductName = "Cà phê rang xay 250g",
                    Price = 65000m,
                    StockQuantity = 50,
                    CategoryId = 13
                },

                new Product
                {
                    ProductId = 39,
                    Barcode = "8930000000394",
                    ProductName = "Trà túi lọc 25 gói",
                    Price = 38000m,
                    StockQuantity = 70,
                    CategoryId = 13
                },


                // -----------------------------------------------------
                // DANH MỤC 14 - VĂN PHÒNG PHẨM
                // -----------------------------------------------------

                new Product
                {
                    ProductId = 40,
                    Barcode = "8930000000400",
                    ProductName = "Bút bi xanh Thiên Long",
                    Price = 5000m,
                    StockQuantity = 200,
                    CategoryId = 14
                },

                new Product
                {
                    ProductId = 41,
                    Barcode = "8930000000417",
                    ProductName = "Vở học sinh 200 trang",
                    Price = 18000m,
                    StockQuantity = 100,
                    CategoryId = 14
                },

                new Product
                {
                    ProductId = 42,
                    Barcode = "8930000000424",
                    ProductName = "Bút chì HB",
                    Price = 4000m,
                    StockQuantity = 150,
                    CategoryId = 14
                },


                // -----------------------------------------------------
                // DANH MỤC 15 - CHĂM SÓC NHÀ CỬA
                // -----------------------------------------------------

                new Product
                {
                    ProductId = 43,
                    Barcode = "8930000000431",
                    ProductName = "Nước rửa chén Sunlight 750ml",
                    Price = 32000m,
                    StockQuantity = 70,
                    CategoryId = 15
                },

                new Product
                {
                    ProductId = 44,
                    Barcode = "8930000000448",
                    ProductName = "Nước giặt OMO 2.6kg",
                    Price = 125000m,
                    StockQuantity = 40,
                    CategoryId = 15
                },

                new Product
                {
                    ProductId = 45,
                    Barcode = "8930000000455",
                    ProductName = "Nước lau sàn 1L",
                    Price = 45000m,
                    StockQuantity = 50,
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
            // 4. KHÁCH HÀNG - 20 DỮ LIỆU
            // =========================================================
            modelBuilder.Entity<Customer>().HasData(

                new Customer
                {
                    CustomerId = 1,
                    CustomerName = "Nguyễn Văn An",
                    PhoneNumber = "0901000001",
                    Address = "12 Nguyễn Huệ, Quận 1, TP.HCM",
                    MembershipRank = "Vàng",
                    RewardPoints = 850
                },

                new Customer
                {
                    CustomerId = 2,
                    CustomerName = "Trần Thị Bình",
                    PhoneNumber = "0901000002",
                    Address = "25 Lê Lợi, Quận 1, TP.HCM",
                    MembershipRank = "Bạc",
                    RewardPoints = 420
                },

                new Customer
                {
                    CustomerId = 3,
                    CustomerName = "Lê Văn Cường",
                    PhoneNumber = "0901000003",
                    Address = "38 Điện Biên Phủ, Bình Thạnh, TP.HCM",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 120
                },

                new Customer
                {
                    CustomerId = 4,
                    CustomerName = "Phạm Thị Dung",
                    PhoneNumber = "0901000004",
                    Address = "45 Nguyễn Thị Minh Khai, Quận 3, TP.HCM",
                    MembershipRank = "Vàng",
                    RewardPoints = 720
                },

                new Customer
                {
                    CustomerId = 5,
                    CustomerName = "Hoàng Văn Đức",
                    PhoneNumber = "0901000005",
                    Address = "56 Xô Viết Nghệ Tĩnh, Bình Thạnh, TP.HCM",
                    MembershipRank = "Bạc",
                    RewardPoints = 350
                },

                new Customer
                {
                    CustomerId = 6,
                    CustomerName = "Võ Thị Hạnh",
                    PhoneNumber = "0901000006",
                    Address = "67 Cách Mạng Tháng 8, Quận 10, TP.HCM",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 90
                },

                new Customer
                {
                    CustomerId = 7,
                    CustomerName = "Đặng Minh Hoàng",
                    PhoneNumber = "0901000007",
                    Address = "78 Phan Văn Trị, Gò Vấp, TP.HCM",
                    MembershipRank = "Bạc",
                    RewardPoints = 480
                },

                new Customer
                {
                    CustomerId = 8,
                    CustomerName = "Bùi Thị Lan",
                    PhoneNumber = "0901000008",
                    Address = "89 Quang Trung, Gò Vấp, TP.HCM",
                    MembershipRank = "Vàng",
                    RewardPoints = 920
                },

                new Customer
                {
                    CustomerId = 9,
                    CustomerName = "Ngô Văn Minh",
                    PhoneNumber = "0901000009",
                    Address = "91 Lạc Long Quân, Tân Bình, TP.HCM",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 160
                },

                new Customer
                {
                    CustomerId = 10,
                    CustomerName = "Đỗ Thị Ngọc",
                    PhoneNumber = "0901000010",
                    Address = "102 Hoàng Văn Thụ, Tân Bình, TP.HCM",
                    MembershipRank = "Bạc",
                    RewardPoints = 390
                },

                new Customer
                {
                    CustomerId = 11,
                    CustomerName = "Phan Văn Phúc",
                    PhoneNumber = "0901000011",
                    Address = "115 Âu Cơ, Tân Phú, TP.HCM",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 75
                },

                new Customer
                {
                    CustomerId = 12,
                    CustomerName = "Nguyễn Thị Quỳnh",
                    PhoneNumber = "0901000012",
                    Address = "126 Tân Kỳ Tân Quý, Tân Phú, TP.HCM",
                    MembershipRank = "Vàng",
                    RewardPoints = 780
                },

                new Customer
                {
                    CustomerId = 13,
                    CustomerName = "Trương Văn Sơn",
                    PhoneNumber = "0901000013",
                    Address = "137 Nguyễn Oanh, Gò Vấp, TP.HCM",
                    MembershipRank = "Bạc",
                    RewardPoints = 510
                },

                new Customer
                {
                    CustomerId = 14,
                    CustomerName = "Lý Thị Thảo",
                    PhoneNumber = "0901000014",
                    Address = "148 Phạm Văn Đồng, Thủ Đức, TP.HCM",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 210
                },

                new Customer
                {
                    CustomerId = 15,
                    CustomerName = "Mai Văn Thành",
                    PhoneNumber = "0901000015",
                    Address = "159 Võ Văn Ngân, Thủ Đức, TP.HCM",
                    MembershipRank = "Bạc",
                    RewardPoints = 440
                },

                new Customer
                {
                    CustomerId = 16,
                    CustomerName = "Huỳnh Thị Uyên",
                    PhoneNumber = "0901000016",
                    Address = "162 Kha Vạn Cân, Thủ Đức, TP.HCM",
                    MembershipRank = "Vàng",
                    RewardPoints = 1100
                },

                new Customer
                {
                    CustomerId = 17,
                    CustomerName = "Dương Văn Việt",
                    PhoneNumber = "0901000017",
                    Address = "173 Nguyễn Văn Luông, Quận 6, TP.HCM",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 130
                },

                new Customer
                {
                    CustomerId = 18,
                    CustomerName = "Phan Thị Xuân",
                    PhoneNumber = "0901000018",
                    Address = "184 Hậu Giang, Quận 6, TP.HCM",
                    MembershipRank = "Bạc",
                    RewardPoints = 560
                },

                new Customer
                {
                    CustomerId = 19,
                    CustomerName = "Nguyễn Quốc Anh",
                    PhoneNumber = "0901000019",
                    Address = "195 Kinh Dương Vương, Bình Tân, TP.HCM",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 180
                },

                new Customer
                {
                    CustomerId = 20,
                    CustomerName = "Trần Minh Khoa",
                    PhoneNumber = "0901000020",
                    Address = "206 Tỉnh Lộ 10, Bình Tân, TP.HCM",
                    MembershipRank = "Vàng",
                    RewardPoints = 950
                }
            );
        }
    }
}