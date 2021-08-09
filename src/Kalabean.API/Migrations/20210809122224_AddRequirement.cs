using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kalabean.API.Migrations
{
    public partial class AddRequirement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderHeader",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    OrderPrice = table.Column<int>(type: "int", nullable: false),
                    PaymenyLink = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OrderStatus = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    HasImage = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHeader_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Requirement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RequirementStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    TypePricing = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    HasImage = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AdminId = table.Column<long>(type: "bigint", nullable: true),
                    AdminDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requirement_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Requirement_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Num = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_OrderHeader_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductId",
                table: "OrderDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeader_StoreId",
                table: "OrderHeader",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Requirement_CategoryId",
                table: "Requirement",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Requirement_ProductId",
                table: "Requirement",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Requirement");

            migrationBuilder.DropTable(
                name: "OrderHeader");
        }
    }
}
