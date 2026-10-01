using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class lan2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RewardPoints = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MembershipRank = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Chuẩn")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 6, "Thực phẩm đông lạnh", "Kem, xúc xích đông lạnh, chả giò" },
                    { 7, "Thực phẩm tươi sống", "Thịt, hải sản, rau củ quả tươi" },
                    { 8, "Gạo & Các loại hạt", "Gạo tẻ, gạo lứt, hạt ngũ cốc" },
                    { 9, "Mỹ phẩm & Chăm sóc cá nhân", "Sữa tắm, dầu gội, kem đánh răng" },
                    { 10, "Chăm sóc nhà cửa", "Nước lau sàn, nước rửa chén, bột giặt" },
                    { 11, "Mẹ & Bé", "Sữa bột, tã bỉm, đồ chơi trẻ em" },
                    { 12, "Chăm sóc sức khỏe", "Khẩu trang, vitamin, băng cá nhân" },
                    { 13, "Đồ dùng gia đình", "Khăn giấy, màng bọc thực phẩm, ly nhựa" },
                    { 14, "Văn phòng phẩm", "Bút, tập vở, giấy in, kéo" },
                    { 15, "Thức ăn thú cưng", "Thức ăn hạt, pate cho chó mèo" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "CustomerName", "MembershipRank", "PhoneNumber", "RewardPoints" },
                values: new object[,]
                {
                    { 1, null, "Nguyễn Văn A", "Vàng", "0901122334", 150 },
                    { 2, null, "Trần Thị B", "Bạc", "0918877665", 50 },
                    { 3, null, "Lê Văn C", "Chuẩn", "0983344556", 10 },
                    { 4, null, "Phạm Hoàng D", "Kim Cương", "0934567890", 520 },
                    { 5, null, "Hoàng Thị E", "Bạc", "0976543210", 85 }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "CustomerName", "MembershipRank", "PhoneNumber" },
                values: new object[] { 6, null, "Đỗ Minh F", "Chuẩn", "0909123456" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "CustomerName", "MembershipRank", "PhoneNumber", "RewardPoints" },
                values: new object[,]
                {
                    { 7, null, "Vũ Thị G", "Vàng", "0912345678", 210 },
                    { 8, null, "Bùi Văn H", "Chuẩn", "0987654321", 25 },
                    { 9, null, "Đặng Thị I", "Bạc", "0945678901", 90 },
                    { 10, null, "Ngoạn Văn K", "Vàng", "0923456789", 180 },
                    { 11, null, "Dương Thi L", "Chuẩn", "0961234567", 5 },
                    { 12, null, "Lý Văn M", "Kim Cương", "0931122334", 600 },
                    { 13, null, "Ngô Thị N", "Bạc", "0971122334", 65 },
                    { 14, null, "Trịnh Văn O", "Chuẩn", "0908877665", 15 },
                    { 15, null, "Đoàn Thị P", "Vàng", "0919988776", 310 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "8930000000011", 1, 12000m, "Snack khoai tây vị tự nhiên 50g", 120 },
                    { 2, "8930000000028", 1, 25000m, "Bánh quy bơ 150g", 80 },
                    { 3, "8930000000035", 1, 18000m, "Kẹo dẻo trái cây 100g", 95 },
                    { 4, "8930000000042", 2, 10000m, "Nước ngọt có ga lon 330ml", 200 },
                    { 5, "8930000000059", 2, 6000m, "Nước khoáng chai 500ml", 250 },
                    { 6, "8930000000066", 2, 12000m, "Trà xanh đóng chai 455ml", 150 },
                    { 7, "8930000000073", 3, 32000m, "Sữa tươi tiệt trùng hộp 1L", 60 },
                    { 8, "8930000000080", 3, 8000m, "Sữa chua uống men sống 100ml", 140 },
                    { 9, "8930000000097", 3, 38000m, "Phô mai lát 140g", 45 },
                    { 10, "8930000000103", 4, 5000m, "Mì gói tôm chua cay 75g", 300 },
                    { 11, "8930000000110", 4, 9000m, "Phở bò ăn liền 65g", 110 },
                    { 12, "8930000000127", 4, 7000m, "Cháo gói thịt bằm 50g", 90 },
                    { 13, "8930000000134", 5, 45000m, "Nước mắm cá cơm 500ml", 50 },
                    { 14, "8930000000141", 5, 38000m, "Hạt nêm thịt thăn 400g", 70 },
                    { 15, "8930000000158", 5, 55000m, "Dầu ăn thực vật chai 1L", 40 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15);
        }
    }
}
