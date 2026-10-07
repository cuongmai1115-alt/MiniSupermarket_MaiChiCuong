using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class chaylan2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "Description",
                value: "Nước ngọt, nước khoáng, nước trái cây");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "Description",
                value: "Bánh quy, bánh ngọt, kẹo các loại");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                column: "Description",
                value: "Nước mắm, dầu ăn, hạt nêm, đường");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6,
                column: "Description",
                value: "Kem đánh răng, dầu gội, sữa tắm");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7,
                column: "Description",
                value: "Đồ dùng gia đình và vật dụng sinh hoạt");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8,
                column: "Description",
                value: "Xúc xích, sandwich, hamburger và đồ ăn nhanh");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 9, "Thực phẩm đông lạnh", "Thực phẩm đông lạnh và đồ chế biến sẵn" },
                    { 10, "Trái cây", "Các loại trái cây tươi" },
                    { 11, "Rau củ", "Rau củ quả tươi" },
                    { 12, "Đồ hộp", "Cá hộp, thịt hộp và thực phẩm đóng hộp" },
                    { 13, "Cà phê và trà", "Cà phê, trà túi lọc và trà đóng chai" },
                    { 14, "Văn phòng phẩm", "Bút, vở, giấy và dụng cụ học tập" },
                    { 15, "Chăm sóc nhà cửa", "Nước giặt, nước rửa chén và chất tẩy rửa" }
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                columns: new[] { "Address", "PhoneNumber" },
                values: new object[] { "12 Nguyễn Huệ, Quận 1, TP.HCM", "0901000001" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                columns: new[] { "Address", "PhoneNumber" },
                values: new object[] { "25 Lê Lợi, Quận 1, TP.HCM", "0901000002" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                columns: new[] { "Address", "CustomerName", "PhoneNumber", "RewardPoints" },
                values: new object[] { "38 Điện Biên Phủ, Bình Thạnh, TP.HCM", "Lê Văn Cường", "0901000003", 120 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4,
                columns: new[] { "Address", "PhoneNumber", "RewardPoints" },
                values: new object[] { "45 Nguyễn Thị Minh Khai, Quận 3, TP.HCM", "0901000004", 720 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5,
                columns: new[] { "Address", "CustomerName", "PhoneNumber" },
                values: new object[] { "56 Xô Viết Nghệ Tĩnh, Bình Thạnh, TP.HCM", "Hoàng Văn Đức", "0901000005" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6,
                columns: new[] { "Address", "CustomerName", "PhoneNumber", "RewardPoints" },
                values: new object[] { "67 Cách Mạng Tháng 8, Quận 10, TP.HCM", "Võ Thị Hạnh", "0901000006", 90 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7,
                columns: new[] { "Address", "CustomerName", "MembershipRank", "PhoneNumber", "RewardPoints" },
                values: new object[] { "78 Phan Văn Trị, Gò Vấp, TP.HCM", "Đặng Minh Hoàng", "Bạc", "0901000007", 480 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 8,
                columns: new[] { "Address", "CustomerName", "MembershipRank", "PhoneNumber", "RewardPoints" },
                values: new object[] { "89 Quang Trung, Gò Vấp, TP.HCM", "Bùi Thị Lan", "Vàng", "0901000008", 920 });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "CustomerName", "MembershipRank", "PhoneNumber", "RewardPoints" },
                values: new object[,]
                {
                    { 9, "91 Lạc Long Quân, Tân Bình, TP.HCM", "Ngô Văn Minh", "Chuẩn", "0901000009", 160 },
                    { 10, "102 Hoàng Văn Thụ, Tân Bình, TP.HCM", "Đỗ Thị Ngọc", "Bạc", "0901000010", 390 },
                    { 11, "115 Âu Cơ, Tân Phú, TP.HCM", "Phan Văn Phúc", "Chuẩn", "0901000011", 75 },
                    { 12, "126 Tân Kỳ Tân Quý, Tân Phú, TP.HCM", "Nguyễn Thị Quỳnh", "Vàng", "0901000012", 780 },
                    { 13, "137 Nguyễn Oanh, Gò Vấp, TP.HCM", "Trương Văn Sơn", "Bạc", "0901000013", 510 },
                    { 14, "148 Phạm Văn Đồng, Thủ Đức, TP.HCM", "Lý Thị Thảo", "Chuẩn", "0901000014", 210 },
                    { 15, "159 Võ Văn Ngân, Thủ Đức, TP.HCM", "Mai Văn Thành", "Bạc", "0901000015", 440 },
                    { 16, "162 Kha Vạn Cân, Thủ Đức, TP.HCM", "Huỳnh Thị Uyên", "Vàng", "0901000016", 1100 },
                    { 17, "173 Nguyễn Văn Luông, Quận 6, TP.HCM", "Dương Văn Việt", "Chuẩn", "0901000017", 130 },
                    { 18, "184 Hậu Giang, Quận 6, TP.HCM", "Phan Thị Xuân", "Bạc", "0901000018", 560 },
                    { 19, "195 Kinh Dương Vương, Bình Tân, TP.HCM", "Nguyễn Quốc Anh", "Chuẩn", "0901000019", 180 },
                    { 20, "206 Tỉnh Lộ 10, Bình Tân, TP.HCM", "Trần Minh Khoa", "Vàng", "0901000020", 950 }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Barcode", "Price", "ProductName" },
                values: new object[] { "8930000000011", 10000m, "Nước ngọt Coca Cola lon 330ml" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "Barcode", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000028", "Nước ngọt Pepsi lon 330ml", 180 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000035", 6000m, "Nước khoáng Lavie chai 500ml", 250 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000042", 2, 32000m, "Sữa tươi Vinamilk 1L", 80 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000059", 2, 7000m, "Sữa chua Vinamilk hộp", 150 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "Barcode", "Price", "ProductName" },
                values: new object[] { "8930000000066", 38000m, "Phô mai lát 140g" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName" },
                values: new object[] { "8930000000073", 3, 25000m, "Bánh quy bơ 150g" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName" },
                values: new object[] { "8930000000080", 3, 18000m, "Bánh xốp chocolate 120g" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000097", 3, 18000m, "Kẹo dẻo trái cây 100g", 120 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000103", 4, 5000m, "Mì Hảo Hảo tôm chua cay 75g", 300 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000110", 4, 9000m, "Mì Omachi sườn hầm ngũ quả", 150 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000127", 4, 9000m, "Phở bò ăn liền 65g", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000134", 5, 45000m, "Nước mắm cá cơm 500ml", 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000141", 5, 55000m, "Dầu ăn thực vật 1L", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000158", 5, 38000m, "Hạt nêm thịt thăn 400g", 75 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 16,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000165", 6, 32000m, "Kem đánh răng P/S 180g", 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 17,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000172", 6, 85000m, "Dầu gội Sunsilk 650g", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 18,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000189", 6, 90000m, "Sữa tắm Lifebuoy 800g", 45 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 19,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000196", 7, 18000m, "Khăn giấy đa năng", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 20,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000202", 7, 25000m, "Túi rác tự phân hủy", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 21,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000219", 7, 35000m, "Hộp đựng thực phẩm 1L", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 22,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000226", 8, 28000m, "Xúc xích tiệt trùng 175g", 90 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 23,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName" },
                values: new object[] { "8930000000233", 8, 25000m, "Sandwich sandwich thịt nguội" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 24,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000240", 8, 15000m, "Bánh bao nhân thịt", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 25,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000257", 9, 45000m, "Cá viên chiên đông lạnh 500g", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 26,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000264", 9, 65000m, "Xúc xích Đức đông lạnh 500g", 35 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 27,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000271", 9, 55000m, "Há cảo đông lạnh 300g", 30 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 28,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000288", 10, 65000m, "Táo Fuji nhập khẩu 1kg", 30 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 29,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000295", 10, 30000m, "Chuối tiêu 1kg", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 30,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000301", 10, 55000m, "Cam vàng 1kg", 35 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 31,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000318", 11, 18000m, "Cà rốt Đà Lạt 500g", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 32,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000325", 11, 28000m, "Khoai tây 1kg", 45 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[,]
                {
                    { 33, "8930000000332", 11, 15000m, "Rau cải xanh 500g", 40 },
                    { 34, "8930000000349", 12, 32000m, "Cá ngừ đóng hộp 185g", 60 },
                    { 35, "8930000000356", 12, 35000m, "Thịt heo hầm đóng hộp 150g", 50 },
                    { 36, "8930000000363", 12, 28000m, "Đậu Hà Lan đóng hộp 400g", 45 },
                    { 37, "8930000000370", 13, 45000m, "Cà phê hòa tan 3in1", 80 },
                    { 38, "8930000000387", 13, 65000m, "Cà phê rang xay 250g", 50 },
                    { 39, "8930000000394", 13, 38000m, "Trà túi lọc 25 gói", 70 },
                    { 40, "8930000000400", 14, 5000m, "Bút bi xanh Thiên Long", 200 },
                    { 41, "8930000000417", 14, 18000m, "Vở học sinh 200 trang", 100 },
                    { 42, "8930000000424", 14, 4000m, "Bút chì HB", 150 },
                    { 43, "8930000000431", 15, 32000m, "Nước rửa chén Sunlight 750ml", 70 },
                    { 44, "8930000000448", 15, 125000m, "Nước giặt OMO 2.6kg", 40 },
                    { 45, "8930000000455", 15, 45000m, "Nước lau sàn 1L", 50 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 45);

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

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "Description",
                value: "Nước ngọt, nước suối, trà đóng chai");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "Description",
                value: "Bánh quy, bánh ngọt, kẹo và đồ ăn vặt");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                column: "Description",
                value: "Nước mắm, dầu ăn, đường, muối, hạt nêm");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6,
                column: "Description",
                value: "Dầu gội, sữa tắm, kem đánh răng");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7,
                column: "Description",
                value: "Khăn giấy, túi rác, nước rửa chén");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8,
                column: "Description",
                value: "Xúc xích, sandwich, cơm hộp và thực phẩm ăn liền");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                columns: new[] { "Address", "PhoneNumber" },
                values: new object[] { "Quận 1, TP.HCM", "0901234567" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                columns: new[] { "Address", "PhoneNumber" },
                values: new object[] { "Quận 3, TP.HCM", "0912345678" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                columns: new[] { "Address", "CustomerName", "PhoneNumber", "RewardPoints" },
                values: new object[] { "Quận Bình Thạnh, TP.HCM", "Lê Minh Cường", "0987654321", 80 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4,
                columns: new[] { "Address", "PhoneNumber", "RewardPoints" },
                values: new object[] { "Quận 10, TP.HCM", "0938123456", 1050 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5,
                columns: new[] { "Address", "CustomerName", "PhoneNumber" },
                values: new object[] { "Quận Tân Bình, TP.HCM", "Hoàng Văn Em", "0978456123" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6,
                columns: new[] { "Address", "CustomerName", "PhoneNumber", "RewardPoints" },
                values: new object[] { "TP. Thủ Đức, TP.HCM", "Võ Thị Hà", "0967123456", 120 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7,
                columns: new[] { "Address", "CustomerName", "MembershipRank", "PhoneNumber", "RewardPoints" },
                values: new object[] { "Quận Gò Vấp, TP.HCM", "Đặng Quốc Huy", "Vàng", "0945678123", 720 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 8,
                columns: new[] { "Address", "CustomerName", "MembershipRank", "PhoneNumber", "RewardPoints" },
                values: new object[] { "Quận Phú Nhuận, TP.HCM", "Nguyễn Thị Lan", "Bạc", "0923456789", 280 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Barcode", "Price", "ProductName" },
                values: new object[] { "8938505970011", 6000m, "Nước suối Aquafina 500ml" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "Barcode", "ProductName", "StockQuantity" },
                values: new object[] { "8935049500028", "Coca Cola lon 330ml", 150 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8935049500035", 10000m, "Pepsi lon 330ml", 140 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8935049500042", 1, 12000m, "Sting dâu chai 330ml", 120 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8935049500059", 1, 10000m, "Trà xanh 0 độ 455ml", 130 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "Barcode", "Price", "ProductName" },
                values: new object[] { "8934673500066", 34000m, "Sữa tươi Vinamilk 1L" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName" },
                values: new object[] { "8934673500073", 2, 8000m, "Sữa tươi Vinamilk ít đường 180ml" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName" },
                values: new object[] { "8934673500080", 2, 7000m, "Sữa chua Vinamilk có đường" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8934673500097", 2, 9000m, "Sữa chua uống Probi", 80 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8935001700103", 3, 32000m, "Bánh quy bơ Danisa 90g", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8935001700110", 3, 38000m, "Bánh Chocopie hộp 6 cái", 45 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8935001700127", 3, 12000m, "Snack khoai tây vị tự nhiên", 110 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8935001700134", 3, 18000m, "Kẹo dẻo trái cây 100g", 75 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8934563100141", 4, 5000m, "Mì Hảo Hảo tôm chua cay", 300 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8934563100158", 4, 9000m, "Mì Omachi sườn hầm ngũ quả", 180 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 16,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8934563100165", 4, 8000m, "Phở bò ăn liền", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 17,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8934563100172", 4, 7000m, "Cháo thịt bằm ăn liền", 90 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 18,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8934804100189", 5, 42000m, "Nước mắm Nam Ngư 500ml", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 19,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8934804100196", 5, 48000m, "Dầu ăn Neptune 1L", 55 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 20,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8934804100202", 5, 36000m, "Hạt nêm Knorr 400g", 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 21,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8934804100219", 5, 25000m, "Đường tinh luyện 1kg", 80 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 22,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8936002100226", 6, 98000m, "Dầu gội Clear bạc hà 650g", 35 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 23,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName" },
                values: new object[] { "8936002100233", 6, 85000m, "Sữa tắm Lifebuoy 527ml" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 24,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8936002100240", 6, 35000m, "Kem đánh răng P/S 180g", 65 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 25,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8936003100257", 7, 28000m, "Khăn giấy hộp 180 tờ", 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 26,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8936003100264", 7, 32000m, "Nước rửa chén Sunlight 750ml", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 27,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8936003100271", 7, 22000m, "Túi rác tự hủy cuộn 30 túi", 45 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 28,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8937004200288", 8, 15000m, "Xúc xích tiệt trùng CP", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 29,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8937004200295", 8, 25000m, "Sandwich sandwich kẹp thịt", 35 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 30,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8937004200301", 8, 35000m, "Cơm hộp gà teriyaki", 25 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 31,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8937004200318", 8, 20000m, "Bánh mì xúc xích", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 32,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8937004200325", 8, 12000m, "Trứng luộc đóng hộp", 50 });
        }
    }
}
