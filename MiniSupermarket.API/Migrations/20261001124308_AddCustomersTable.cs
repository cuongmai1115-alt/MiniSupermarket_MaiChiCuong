using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 8);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
