using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kalabean.API.Migrations
{
    public partial class AddTargetTypeIdToProductAndReqirement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TargetTypeId",
                table: "Requirement",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TargetTypeId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TargetType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requirement_TargetTypeId",
                table: "Requirement",
                column: "TargetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_TargetTypeId",
                table: "Product",
                column: "TargetTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_TargetType_TargetTypeId",
                table: "Product",
                column: "TargetTypeId",
                principalTable: "TargetType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requirement_TargetType_TargetTypeId",
                table: "Requirement",
                column: "TargetTypeId",
                principalTable: "TargetType",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_TargetType_TargetTypeId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Requirement_TargetType_TargetTypeId",
                table: "Requirement");

            migrationBuilder.DropTable(
                name: "TargetType");

            migrationBuilder.DropIndex(
                name: "IX_Requirement_TargetTypeId",
                table: "Requirement");

            migrationBuilder.DropIndex(
                name: "IX_Product_TargetTypeId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "TargetTypeId",
                table: "Requirement");

            migrationBuilder.DropColumn(
                name: "TargetTypeId",
                table: "Product");
        }
    }
}
