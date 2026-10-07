using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class chaylan1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Nước giải khát", "Nước ngọt, nước suối, trà đóng chai" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Sữa và sản phẩm từ sữa", "Sữa tươi, sữa chua, phô mai" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Bánh kẹo", "Bánh quy, bánh ngọt, kẹo và đồ ăn vặt" });

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
                values: new object[] { "Gia vị và thực phẩm khô", "Nước mắm, dầu ăn, đường, muối, hạt nêm" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 6, "Đồ dùng cá nhân", "Dầu gội, sữa tắm, kem đánh răng" },
                    { 7, "Đồ gia dụng", "Khăn giấy, túi rác, nước rửa chén" },
                    { 8, "Đồ ăn nhanh", "Xúc xích, sandwich, cơm hộp và thực phẩm ăn liền" }
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                columns: new[] { "Address", "CustomerName", "PhoneNumber", "RewardPoints" },
                values: new object[] { "Quận 1, TP.HCM", "Nguyễn Văn An", "0901234567", 850 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                columns: new[] { "Address", "CustomerName", "PhoneNumber", "RewardPoints" },
                values: new object[] { "Quận 3, TP.HCM", "Trần Thị Bình", "0912345678", 420 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                columns: new[] { "Address", "CustomerName", "PhoneNumber", "RewardPoints" },
                values: new object[] { "Quận Bình Thạnh, TP.HCM", "Lê Minh Cường", "0987654321", 80 });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "CustomerName", "MembershipRank", "PhoneNumber", "RewardPoints" },
                values: new object[,]
                {
                    { 4, "Quận 10, TP.HCM", "Phạm Thị Dung", "Vàng", "0938123456", 1050 },
                    { 5, "Quận Tân Bình, TP.HCM", "Hoàng Văn Em", "Bạc", "0978456123", 350 },
                    { 6, "TP. Thủ Đức, TP.HCM", "Võ Thị Hà", "Chuẩn", "0967123456", 120 },
                    { 7, "Quận Gò Vấp, TP.HCM", "Đặng Quốc Huy", "Vàng", "0945678123", 720 },
                    { 8, "Quận Phú Nhuận, TP.HCM", "Nguyễn Thị Lan", "Bạc", "0923456789", 280 }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8938505970011", 6000m, "Nước suối Aquafina 500ml", 200 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8935049500028", 10000m, "Coca Cola lon 330ml", 150 });

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
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8934673500066", 34000m, "Sữa tươi Vinamilk 1L", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8934673500073", 2, 8000m, "Sữa tươi Vinamilk ít đường 180ml", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8934673500080", 2, 7000m, "Sữa chua Vinamilk có đường", 90 });

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

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[,]
                {
                    { 16, "8934563100165", 4, 8000m, "Phở bò ăn liền", 100 },
                    { 17, "8934563100172", 4, 7000m, "Cháo thịt bằm ăn liền", 90 },
                    { 18, "8934804100189", 5, 42000m, "Nước mắm Nam Ngư 500ml", 60 },
                    { 19, "8934804100196", 5, 48000m, "Dầu ăn Neptune 1L", 55 },
                    { 20, "8934804100202", 5, 36000m, "Hạt nêm Knorr 400g", 70 },
                    { 21, "8934804100219", 5, 25000m, "Đường tinh luyện 1kg", 80 },
                    { 22, "8936002100226", 6, 98000m, "Dầu gội Clear bạc hà 650g", 35 },
                    { 23, "8936002100233", 6, 85000m, "Sữa tắm Lifebuoy 527ml", 40 },
                    { 24, "8936002100240", 6, 35000m, "Kem đánh răng P/S 180g", 65 },
                    { 25, "8936003100257", 7, 28000m, "Khăn giấy hộp 180 tờ", 70 },
                    { 26, "8936003100264", 7, 32000m, "Nước rửa chén Sunlight 750ml", 50 },
                    { 27, "8936003100271", 7, 22000m, "Túi rác tự hủy cuộn 30 túi", 45 },
                    { 28, "8937004200288", 8, 15000m, "Xúc xích tiệt trùng CP", 100 },
                    { 29, "8937004200295", 8, 25000m, "Sandwich sandwich kẹp thịt", 35 },
                    { 30, "8937004200301", 8, 35000m, "Cơm hộp gà teriyaki", 25 },
                    { 31, "8937004200318", 8, 20000m, "Bánh mì xúc xích", 40 },
                    { 32, "8937004200325", 8, 12000m, "Trứng luộc đóng hộp", 50 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 32);

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

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Bánh kẹo & Đồ ăn vặt", "Snack, bánh quy, kẹo dẻo" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Nước giải khát & Trà", "Nước ngọt, nước khoáng, trà" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Sữa & Sản phẩm từ sữa", "Sữa tươi, sữa chua, phô mai" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Mì gói & Thực phẩm ăn liền", "Mì ăn liền, phở khô, cháo gói" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Gia vị & Dầu ăn", "Nước mắm, hạt nêm, dầu thực vật" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                columns: new[] { "Address", "CustomerName", "PhoneNumber", "RewardPoints" },
                values: new object[] { "123 Lê Lợi, Phường Bến Nghé, Quận 1, TP.HCM", "Nguyễn Văn A", "0901122334", 150 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                columns: new[] { "Address", "CustomerName", "PhoneNumber", "RewardPoints" },
                values: new object[] { "456 Nguyễn Thị Minh Khai, Phường 5, Quận 3, TP.HCM", "Trần Thị B", "0918877665", 50 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                columns: new[] { "Address", "CustomerName", "PhoneNumber", "RewardPoints" },
                values: new object[] { "789 Điện Biên Phủ, Phường 25, Quận Bình Thạnh, TP.HCM", "Lê Văn C", "0983344556", 10 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000011", 12000m, "Snack khoai tây vị tự nhiên 50g", 120 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000028", 25000m, "Bánh quy bơ 150g", 80 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000035", 18000m, "Kẹo dẻo trái cây 100g", 95 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000042", 2, 10000m, "Nước ngọt có ga lon 330ml", 200 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000059", 2, 6000m, "Nước khoáng chai 500ml", 250 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000066", 12000m, "Trà xanh đóng chai 455ml", 150 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000073", 3, 32000m, "Sữa tươi tiệt trùng hộp 1L", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000080", 3, 8000m, "Sữa chua uống men sống 100ml", 140 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000097", 3, 38000m, "Phô mai lát 140g", 45 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000103", 4, 5000m, "Mì gói tôm chua cay 75g", 300 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000110", 4, 9000m, "Phở bò ăn liền 65g", 110 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000127", 4, 7000m, "Cháo gói thịt bằm 50g", 90 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000134", 5, 45000m, "Nước mắm cá cơm 500ml", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000141", 5, 38000m, "Hạt nêm thịt thăn 400g", 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8930000000158", 5, 55000m, "Dầu ăn thực vật chai 1L", 40 });
        }
    }
}
