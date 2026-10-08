using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class lan16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "Description",
                value: "Nước ngọt, nước suối, nước tăng lực, trà đóng chai");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "Description",
                value: "Sữa tươi, sữa chua, sữa hộp, phô mai");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Bánh kẹo và snack", "Bánh quy, snack khoai tây, kẹo, chocolate" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Mì, phở, cháo ăn liền", "Mì gói, mì ly, phở và cháo ăn liền" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Cơm hộp và món ăn sẵn", "Cơm hộp, cơm cuộn, xôi, món ăn hâm nóng tại quầy" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Bánh mì và bánh tươi", "Bánh mì, sandwich, bánh bao, bánh ngọt trong ngày" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Kem và thực phẩm đông lạnh", "Kem ốc quế, xúc xích, cá viên và món chiên đông lạnh" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Cà phê và trà", "Cà phê lon, cà phê hòa tan, trà đóng chai" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 9,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Gia vị và thực phẩm khô", "Nước mắm, dầu ăn, đường, gia vị đóng gói nhỏ" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 10,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Đồ hộp và thực phẩm đóng gói", "Cá hộp, pate, rong biển ăn liền, thực phẩm đóng gói" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 11,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Trái cây và rau củ tiện lợi", "Trái cây tươi, trái cây cắt sẵn, salad đóng hộp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 12,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Chăm sóc cá nhân", "Kem đánh răng, dầu gội, sữa tắm, khăn ướt" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 13,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Đồ gia dụng và vệ sinh", "Khăn giấy, túi rác, nước rửa chén, vật dụng sinh hoạt" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 14,
                column: "Description",
                value: "Bút, vở, băng keo và dụng cụ học tập");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 15,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Phụ kiện tiện ích", "Pin, cáp sạc, khẩu trang và phụ kiện dùng nhanh" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "Address",
                value: "18 Nguyễn Huệ, Phường Sài Gòn, TP. Hồ Chí Minh");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "Address",
                value: "45 Lê Thánh Tôn, Phường Sài Gòn, TP. Hồ Chí Minh");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                column: "Address",
                value: "120 Phạm Ngũ Lão, Phường Bến Thành, TP. Hồ Chí Minh");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4,
                column: "Address",
                value: "62 Bùi Viện, Phường Bến Thành, TP. Hồ Chí Minh");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5,
                column: "Address",
                value: "160 Nguyễn Trãi, Phường Bến Thành, TP. Hồ Chí Minh");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6,
                column: "Address",
                value: "75 Nguyễn Thái Học, Phường Cầu Ông Lãnh, TP. Hồ Chí Minh");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7,
                column: "Address",
                value: "30 Cô Giang, Phường Cầu Ông Lãnh, TP. Hồ Chí Minh");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 8,
                column: "Address",
                value: "27 Trần Quang Khải, Phường Tân Định, TP. Hồ Chí Minh");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 9,
                column: "Address",
                value: "95 Nguyễn Văn Nguyễn, Phường Tân Định, TP. Hồ Chí Minh");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 10,
                column: "Address",
                value: "38 Tôn Thất Thuyết, Phường Vĩnh Hội, TP. Hồ Chí Minh");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 11,
                column: "Address",
                value: "220 Hoàng Diệu, Phường Khánh Hội, TP. Hồ Chí Minh");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 12,
                column: "Address",
                value: "346 Quang Trung, Phường Gò Vấp, TP. Hồ Chí Minh");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 13,
                column: "Address",
                value: "88 Võ Văn Ngân, Phường Thủ Đức, TP. Hồ Chí Minh");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 14,
                column: "Address",
                value: "15 Lê Văn Thọ, Phường Thông Tây Hội, TP. Hồ Chí Minh");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 15,
                column: "Address",
                value: "212 Nguyễn Hữu Cảnh, Phường Thạnh Mỹ Tây, TP. Hồ Chí Minh");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 16,
                column: "Address",
                value: "85 Xô Viết Nghệ Tĩnh, Phường Thạnh Mỹ Tây, TP. Hồ Chí Minh");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 17,
                column: "Address",
                value: "40 Phan Xích Long, Phường Cầu Kiệu, TP. Hồ Chí Minh");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 18,
                column: "Address",
                value: "150 Hoàng Văn Thụ, Phường Tân Sơn Nhất, TP. Hồ Chí Minh");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 19,
                column: "Address",
                value: "55 Huỳnh Tấn Phát, Phường Tân Thuận, TP. Hồ Chí Minh");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 20,
                column: "Address",
                value: "120 Lê Văn Việt, Phường Tăng Nhơn Phú, TP. Hồ Chí Minh");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 12000m, "Nước ngọt Coca-Cola lon 330ml", 240 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 11000m, "Nước tăng lực Number 1 chai 330ml", 150 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "ProductName", "StockQuantity" },
                values: new object[] { "Nước suối Aquafina chai 500ml", 300 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 36000m, "Sữa tươi Vinamilk 100% hộp 1L", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 8000m, "Sữa chua Vinamilk có đường hộp 100g", 120 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 9000m, "Sữa tươi TH true MILK hộp 180ml", 150 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 14000m, "Snack khoai tây Lay's vị tự nhiên 52g", 110 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "Price", "ProductName" },
                values: new object[] { 24000m, "Bánh quy Oreo vani 133g" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                columns: new[] { "ProductName", "StockQuantity" },
                values: new object[] { "Chocolate KitKat 4 thanh 35g", 80 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                column: "ProductName",
                value: "Mì Hảo Hảo tôm chua cay gói 75g");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 12000m, "Mì ly Modern lẩu thái 65g", 140 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12,
                columns: new[] { "Price", "ProductName" },
                values: new object[] { 11000m, "Phở bò Vifon ăn liền gói 65g" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 35000m, "Cơm hộp gà teriyaki 300g", 30 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 25000m, "Cơm cuộn rong biển (kimbap) 200g", 35 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 20000m, "Xôi mặn gói 150g", 30 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 16,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 20000m, "Bánh mì thịt nguội", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 17,
                columns: new[] { "Price", "ProductName" },
                values: new object[] { 25000m, "Sandwich gà phô mai" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 18,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 18000m, "Bánh bao nhân thịt trứng cút", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 19,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 22000m, "Kem ốc quế Cornetto vị chocolate", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 20,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 15000m, "Xúc xích nướng que hâm nóng", 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 21,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 52000m, "Cá viên chiên đông lạnh 500g", 35 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 22,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 12000m, "Cà phê sữa NESCAFÉ lon 170ml", 120 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 23,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 48000m, "Cà phê hòa tan G7 3in1 hộp 16 gói", 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 24,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 11000m, "Trà xanh Không Độ chai 455ml", 130 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 25,
                columns: new[] { "ProductName", "StockQuantity" },
                values: new object[] { "Nước mắm Nam Ngư chai 500ml", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 26,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 58000m, "Dầu ăn Neptune 1L", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 27,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 26000m, "Đường tinh luyện 1kg", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 28,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 35000m, "Cá ngừ ngâm dầu Hạ Long 185g", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 29,
                columns: new[] { "ProductName", "StockQuantity" },
                values: new object[] { "Pate gan Hạ Long hộp 150g", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 30,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 8000m, "Rong biển sấy giòn ăn liền 5g", 120 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 31,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 32000m, "Chuối tiêu 1kg", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 32,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 30000m, "Trái cây cắt sẵn hộp 250g", 30 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 33,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 28000m, "Salad rau trộn hộp 200g", 25 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 34,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 34000m, "Kem đánh răng P/S 180g", 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 35,
                columns: new[] { "Price", "ProductName" },
                values: new object[] { 38000m, "Dầu gội Sunsilk chai 170g" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 36,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 15000m, "Khăn ướt Mama gói 20 tờ", 90 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 37,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 22000m, "Khăn giấy rút Pulppy 100 tờ", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 38,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 38000m, "Nước rửa chén Sunlight chai 750ml", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 39,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 25000m, "Túi rác đen tự hủy cuộn 1kg", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 40,
                column: "ProductName",
                value: "Bút bi Thiên Long TL-027 xanh");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 41,
                columns: new[] { "Price", "ProductName" },
                values: new object[] { 14000m, "Vở Campus 96 trang" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 42,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 8000m, "Băng keo trong 2.4cm", 120 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 43,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 28000m, "Pin AA Panasonic vỉ 2 viên", 80 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 44,
                columns: new[] { "Price", "ProductName" },
                values: new object[] { 69000m, "Cáp sạc USB-C 1m" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 45,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 18000m, "Khẩu trang y tế 4 lớp hộp 10 cái", 100 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FullName", "IsActive", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "Nguyễn Quản Trị", true, "123456", "Admin", "admin01" },
                    { 2, "Trần Giám Đốc", true, "123456", "Admin", "admin02" },
                    { 3, "Lê Thu Ngân", true, "123456", "Cashier", "cashier01" },
                    { 4, "Phạm Bán Hàng", true, "123456", "Cashier", "cashier02" },
                    { 5, "Hoàng Thu Ngân", true, "123456", "Cashier", "cashier03" },
                    { 6, "Vũ Thị Quầy", true, "123456", "Cashier", "cashier04" },
                    { 7, "Đỗ Bán Lẻ", true, "123456", "Cashier", "cashier05" },
                    { 8, "Ngô Quản Kho", true, "123456", "Warehouse", "ware01" },
                    { 9, "Bùi Kiểm Kê", true, "123456", "Warehouse", "ware02" },
                    { 10, "Dương Thủ Kho", true, "123456", "Warehouse", "ware03" },
                    { 11, "Lý Nhập Hàng", true, "123456", "Warehouse", "ware04" },
                    { 12, "Đặng Hỗ Trợ", true, "123456", "Admin", "admin_backup" },
                    { 13, "Hồ Ca Chiều", true, "123456", "Cashier", "cashier06" },
                    { 14, "Trương Vận Chuyển", true, "123456", "Warehouse", "ware05" },
                    { 15, "Mai Giám Sát", true, "123456", "Admin", "supervisor" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "Description",
                value: "Nước ngọt, nước khoáng, nước trái cây");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "Description",
                value: "Sữa tươi, sữa chua, phô mai");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Bánh kẹo", "Bánh quy, bánh ngọt, kẹo các loại" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Mì và thực phẩm ăn liền", "Mì gói, phở, cháo và thực phẩm ăn liền" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Gia vị và thực phẩm khô", "Nước mắm, dầu ăn, hạt nêm, đường" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Đồ dùng cá nhân", "Kem đánh răng, dầu gội, sữa tắm" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Đồ gia dụng", "Đồ dùng gia đình và vật dụng sinh hoạt" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Đồ ăn nhanh", "Xúc xích, sandwich, hamburger và đồ ăn nhanh" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 9,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Thực phẩm đông lạnh", "Thực phẩm đông lạnh và đồ chế biến sẵn" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 10,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Trái cây", "Các loại trái cây tươi" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 11,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Rau củ", "Rau củ quả tươi" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 12,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Đồ hộp", "Cá hộp, thịt hộp và thực phẩm đóng hộp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 13,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Cà phê và trà", "Cà phê, trà túi lọc và trà đóng chai" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 14,
                column: "Description",
                value: "Bút, vở, giấy và dụng cụ học tập");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 15,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Chăm sóc nhà cửa", "Nước giặt, nước rửa chén và chất tẩy rửa" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "Address",
                value: "12 Nguyễn Huệ, Quận 1, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "Address",
                value: "25 Lê Lợi, Quận 1, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                column: "Address",
                value: "38 Điện Biên Phủ, Bình Thạnh, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4,
                column: "Address",
                value: "45 Nguyễn Thị Minh Khai, Quận 3, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5,
                column: "Address",
                value: "56 Xô Viết Nghệ Tĩnh, Bình Thạnh, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6,
                column: "Address",
                value: "67 Cách Mạng Tháng 8, Quận 10, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7,
                column: "Address",
                value: "78 Phan Văn Trị, Gò Vấp, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 8,
                column: "Address",
                value: "89 Quang Trung, Gò Vấp, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 9,
                column: "Address",
                value: "91 Lạc Long Quân, Tân Bình, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 10,
                column: "Address",
                value: "102 Hoàng Văn Thụ, Tân Bình, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 11,
                column: "Address",
                value: "115 Âu Cơ, Tân Phú, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 12,
                column: "Address",
                value: "126 Tân Kỳ Tân Quý, Tân Phú, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 13,
                column: "Address",
                value: "137 Nguyễn Oanh, Gò Vấp, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 14,
                column: "Address",
                value: "148 Phạm Văn Đồng, Thủ Đức, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 15,
                column: "Address",
                value: "159 Võ Văn Ngân, Thủ Đức, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 16,
                column: "Address",
                value: "162 Kha Vạn Cân, Thủ Đức, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 17,
                column: "Address",
                value: "173 Nguyễn Văn Luông, Quận 6, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 18,
                column: "Address",
                value: "184 Hậu Giang, Quận 6, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 19,
                column: "Address",
                value: "195 Kinh Dương Vương, Bình Tân, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 20,
                column: "Address",
                value: "206 Tỉnh Lộ 10, Bình Tân, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 10000m, "Nước ngọt Coca Cola lon 330ml", 200 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 10000m, "Nước ngọt Pepsi lon 330ml", 180 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "ProductName", "StockQuantity" },
                values: new object[] { "Nước khoáng Lavie chai 500ml", 250 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 32000m, "Sữa tươi Vinamilk 1L", 80 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 7000m, "Sữa chua Vinamilk hộp", 150 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 38000m, "Phô mai lát 140g", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 25000m, "Bánh quy bơ 150g", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "Price", "ProductName" },
                values: new object[] { 18000m, "Bánh xốp chocolate 120g" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                columns: new[] { "ProductName", "StockQuantity" },
                values: new object[] { "Kẹo dẻo trái cây 100g", 120 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                column: "ProductName",
                value: "Mì Hảo Hảo tôm chua cay 75g");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 9000m, "Mì Omachi sườn hầm ngũ quả", 150 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12,
                columns: new[] { "Price", "ProductName" },
                values: new object[] { 9000m, "Phở bò ăn liền 65g" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 45000m, "Nước mắm cá cơm 500ml", 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 55000m, "Dầu ăn thực vật 1L", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 38000m, "Hạt nêm thịt thăn 400g", 75 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 16,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 32000m, "Kem đánh răng P/S 180g", 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 17,
                columns: new[] { "Price", "ProductName" },
                values: new object[] { 85000m, "Dầu gội Sunsilk 650g" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 18,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 90000m, "Sữa tắm Lifebuoy 800g", 45 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 19,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 18000m, "Khăn giấy đa năng", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 20,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 25000m, "Túi rác tự phân hủy", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 21,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 35000m, "Hộp đựng thực phẩm 1L", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 22,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 28000m, "Xúc xích tiệt trùng 175g", 90 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 23,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 25000m, "Sandwich sandwich thịt nguội", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 24,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 15000m, "Bánh bao nhân thịt", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 25,
                columns: new[] { "ProductName", "StockQuantity" },
                values: new object[] { "Cá viên chiên đông lạnh 500g", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 26,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 65000m, "Xúc xích Đức đông lạnh 500g", 35 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 27,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 55000m, "Há cảo đông lạnh 300g", 30 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 28,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 65000m, "Táo Fuji nhập khẩu 1kg", 30 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 29,
                columns: new[] { "ProductName", "StockQuantity" },
                values: new object[] { "Chuối tiêu 1kg", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 30,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 55000m, "Cam vàng 1kg", 35 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 31,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 18000m, "Cà rốt Đà Lạt 500g", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 32,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 28000m, "Khoai tây 1kg", 45 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 33,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 15000m, "Rau cải xanh 500g", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 34,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 32000m, "Cá ngừ đóng hộp 185g", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 35,
                columns: new[] { "Price", "ProductName" },
                values: new object[] { 35000m, "Thịt heo hầm đóng hộp 150g" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 36,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 28000m, "Đậu Hà Lan đóng hộp 400g", 45 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 37,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 45000m, "Cà phê hòa tan 3in1", 80 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 38,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 65000m, "Cà phê rang xay 250g", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 39,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 38000m, "Trà túi lọc 25 gói", 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 40,
                column: "ProductName",
                value: "Bút bi xanh Thiên Long");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 41,
                columns: new[] { "Price", "ProductName" },
                values: new object[] { 18000m, "Vở học sinh 200 trang" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 42,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 4000m, "Bút chì HB", 150 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 43,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 32000m, "Nước rửa chén Sunlight 750ml", 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 44,
                columns: new[] { "Price", "ProductName" },
                values: new object[] { 125000m, "Nước giặt OMO 2.6kg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 45,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 45000m, "Nước lau sàn 1L", 50 });
        }
    }
}
