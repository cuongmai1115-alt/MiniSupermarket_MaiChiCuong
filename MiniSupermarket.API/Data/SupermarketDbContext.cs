using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext(DbContextOptions<SupermarketDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Bánh kẹo & Đồ ăn vặt", Description = "Snack, bánh quy, kẹo dẻo" },
                new Category { CategoryId = 2, CategoryName = "Nước giải khát & Trà", Description = "Nước ngọt, nước khoáng, trà" },
                new Category { CategoryId = 3, CategoryName = "Sữa & Sản phẩm từ sữa", Description = "Sữa tươi, sữa chua, phô mai" },
                new Category { CategoryId = 4, CategoryName = "Mì gói & Thực phẩm ăn liền", Description = "Mì ăn liền, phở khô, cháo gói" },
                new Category { CategoryId = 5, CategoryName = "Gia vị & Dầu ăn", Description = "Nước mắm, hạt nêm, dầu thực vật" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Barcode = "8930000000011", ProductName = "Snack khoai tây vị tự nhiên 50g", Price = 12000m, StockQuantity = 120, CategoryId = 1 },
                new Product { ProductId = 2, Barcode = "8930000000028", ProductName = "Bánh quy bơ 150g", Price = 25000m, StockQuantity = 80, CategoryId = 1 },
                new Product { ProductId = 3, Barcode = "8930000000035", ProductName = "Kẹo dẻo trái cây 100g", Price = 18000m, StockQuantity = 95, CategoryId = 1 },
                new Product { ProductId = 4, Barcode = "8930000000042", ProductName = "Nước ngọt có ga lon 330ml", Price = 10000m, StockQuantity = 200, CategoryId = 2 },
                new Product { ProductId = 5, Barcode = "8930000000059", ProductName = "Nước khoáng chai 500ml", Price = 6000m, StockQuantity = 250, CategoryId = 2 },
                new Product { ProductId = 6, Barcode = "8930000000066", ProductName = "Trà xanh đóng chai 455ml", Price = 12000m, StockQuantity = 150, CategoryId = 2 },
                new Product { ProductId = 7, Barcode = "8930000000073", ProductName = "Sữa tươi tiệt trùng hộp 1L", Price = 32000m, StockQuantity = 60, CategoryId = 3 },
                new Product { ProductId = 8, Barcode = "8930000000080", ProductName = "Sữa chua uống men sống 100ml", Price = 8000m, StockQuantity = 140, CategoryId = 3 },
                new Product { ProductId = 9, Barcode = "8930000000097", ProductName = "Phô mai lát 140g", Price = 38000m, StockQuantity = 45, CategoryId = 3 },
                new Product { ProductId = 10, Barcode = "8930000000103", ProductName = "Mì gói tôm chua cay 75g", Price = 5000m, StockQuantity = 300, CategoryId = 4 },
                new Product { ProductId = 11, Barcode = "8930000000110", ProductName = "Phở bò ăn liền 65g", Price = 9000m, StockQuantity = 110, CategoryId = 4 },
                new Product { ProductId = 12, Barcode = "8930000000127", ProductName = "Cháo gói thịt bằm 50g", Price = 7000m, StockQuantity = 90, CategoryId = 4 },
                new Product { ProductId = 13, Barcode = "8930000000134", ProductName = "Nước mắm cá cơm 500ml", Price = 45000m, StockQuantity = 50, CategoryId = 5 },
                new Product { ProductId = 14, Barcode = "8930000000141", ProductName = "Hạt nêm thịt thăn 400g", Price = 38000m, StockQuantity = 70, CategoryId = 5 },
                new Product { ProductId = 15, Barcode = "8930000000158", ProductName = "Dầu ăn thực vật chai 1L", Price = 55000m, StockQuantity = 40, CategoryId = 5 }
            );

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(c => c.RewardPoints).HasDefaultValue(0);
                entity.Property(c => c.MembershipRank).HasDefaultValue("Chuẩn");
            });

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    CustomerName = "Nguyễn Văn A",
                    PhoneNumber = "0901122334",
                    Address = "123 Lê Lợi, Phường Bến Nghé, Quận 1, TP.HCM",
                    MembershipRank = "Vàng",
                    RewardPoints = 150
                },
                new Customer
                {
                    CustomerId = 2,
                    CustomerName = "Trần Thị B",
                    PhoneNumber = "0918877665",
                    Address = "456 Nguyễn Thị Minh Khai, Phường 5, Quận 3, TP.HCM",
                    MembershipRank = "Bạc",
                    RewardPoints = 50
                },
                new Customer
                {
                    CustomerId = 3,
                    CustomerName = "Lê Văn C",
                    PhoneNumber = "0983344556",
                    Address = "789 Điện Biên Phủ, Phường 25, Quận Bình Thạnh, TP.HCM",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 10
                }
            );
        }
    }
}