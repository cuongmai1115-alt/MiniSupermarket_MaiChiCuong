using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class csdllan3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CashierUsername = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TotalItems = table.Column<int>(type: "int", nullable: false),
                    FinalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RewardPoints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LineTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CashierUsername", "CreatedDate", "CustomerId", "FinalAmount", "OrderCode", "RewardPoints", "TotalItems" },
                values: new object[,]
                {
                    { 1, "cashier01", new DateTime(2026, 10, 1, 8, 30, 0, 0, DateTimeKind.Unspecified), 1, 62000m, "HD20261001-001", 6, 3 },
                    { 2, "cashier01", new DateTime(2026, 10, 1, 9, 15, 0, 0, DateTimeKind.Unspecified), 2, 36000m, "HD20261001-002", 3, 2 },
                    { 3, "cashier02", new DateTime(2026, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 3, 50000m, "HD20261001-003", 5, 10 },
                    { 4, "cashier02", new DateTime(2026, 10, 2, 8, 45, 0, 0, DateTimeKind.Unspecified), 4, 144000m, "HD20261002-001", 14, 4 },
                    { 5, "cashier03", new DateTime(2026, 10, 2, 11, 20, 0, 0, DateTimeKind.Unspecified), 5, 68000m, "HD20261002-002", 6, 5 },
                    { 6, "cashier03", new DateTime(2026, 10, 3, 14, 10, 0, 0, DateTimeKind.Unspecified), 6, 42000m, "HD20261003-001", 4, 3 },
                    { 7, "cashier04", new DateTime(2026, 10, 3, 16, 40, 0, 0, DateTimeKind.Unspecified), 7, 30000m, "HD20261003-002", 3, 5 },
                    { 8, "cashier04", new DateTime(2026, 10, 4, 9, 5, 0, 0, DateTimeKind.Unspecified), 8, 103000m, "HD20261004-001", 10, 2 },
                    { 9, "cashier05", new DateTime(2026, 10, 4, 13, 30, 0, 0, DateTimeKind.Unspecified), 9, 35000m, "HD20261004-002", 3, 3 },
                    { 10, "cashier05", new DateTime(2026, 10, 5, 10, 50, 0, 0, DateTimeKind.Unspecified), 10, 124000m, "HD20261005-001", 12, 4 },
                    { 11, "cashier06", new DateTime(2026, 10, 5, 17, 15, 0, 0, DateTimeKind.Unspecified), 11, 36000m, "HD20261005-002", 3, 3 },
                    { 12, "cashier06", new DateTime(2026, 10, 6, 8, 20, 0, 0, DateTimeKind.Unspecified), 12, 82000m, "HD20261006-001", 8, 3 },
                    { 13, "cashier01", new DateTime(2026, 10, 6, 15, 0, 0, 0, DateTimeKind.Unspecified), 13, 74000m, "HD20261006-002", 7, 3 },
                    { 14, "cashier02", new DateTime(2026, 10, 7, 12, 10, 0, 0, DateTimeKind.Unspecified), 14, 58000m, "HD20261007-001", 5, 1 },
                    { 15, "cashier03", new DateTime(2026, 10, 7, 18, 45, 0, 0, DateTimeKind.Unspecified), 15, 40000m, "HD20261007-002", 4, 3 }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderDetailId", "LineTotal", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 12000m, 1, 1, 1, 12000m },
                    { 2, 14000m, 1, 7, 1, 14000m },
                    { 3, 36000m, 1, 4, 1, 36000m },
                    { 4, 24000m, 2, 8, 1, 24000m },
                    { 5, 12000m, 2, 1, 1, 12000m },
                    { 6, 50000m, 3, 10, 10, 5000m },
                    { 7, 144000m, 4, 4, 4, 36000m },
                    { 8, 36000m, 5, 4, 1, 36000m },
                    { 9, 32000m, 5, 5, 4, 8000m },
                    { 10, 36000m, 6, 9, 2, 18000m },
                    { 11, 6000m, 6, 3, 1, 6000m },
                    { 12, 30000m, 7, 3, 5, 6000m },
                    { 13, 58000m, 8, 26, 1, 58000m },
                    { 14, 45000m, 8, 25, 1, 45000m },
                    { 15, 11000m, 9, 24, 1, 11000m },
                    { 16, 24000m, 9, 22, 2, 12000m },
                    { 17, 48000m, 10, 8, 2, 24000m },
                    { 18, 76000m, 10, 38, 2, 38000m },
                    { 19, 36000m, 11, 11, 3, 12000m },
                    { 20, 70000m, 12, 13, 2, 35000m },
                    { 21, 12000m, 12, 1, 1, 12000m },
                    { 22, 34000m, 13, 34, 1, 34000m },
                    { 23, 40000m, 13, 16, 2, 20000m },
                    { 24, 58000m, 14, 26, 1, 58000m },
                    { 25, 28000m, 15, 7, 2, 14000m },
                    { 26, 12000m, 15, 1, 1, 12000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Barcode",
                table: "Products",
                column: "Barcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PhoneNumber",
                table: "Customers",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderCode",
                table: "Orders",
                column: "OrderCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Products_Barcode",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Customers_PhoneNumber",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories");
        }
    }
}
