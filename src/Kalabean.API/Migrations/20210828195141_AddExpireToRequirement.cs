using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kalabean.API.Migrations
{
    public partial class AddExpireToRequirement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeader_AspNetUsers_UserId",
                table: "OrderHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_Requirement_Product_ProductId",
                table: "Requirement");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "OrderHeader",
                newName: "ToUserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderHeader_UserId",
                table: "OrderHeader",
                newName: "IX_OrderHeader_ToUserId");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "Requirement",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateChangeStatus",
                table: "Requirement",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Exprie",
                table: "Requirement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Requirement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "FromUserId",
                table: "OrderHeader",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeader_FromUserId",
                table: "OrderHeader",
                column: "FromUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeader_AspNetUsers_FromUserId",
                table: "OrderHeader",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeader_AspNetUsers_ToUserId",
                table: "OrderHeader",
                column: "ToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requirement_Product_ProductId",
                table: "Requirement",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeader_AspNetUsers_FromUserId",
                table: "OrderHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeader_AspNetUsers_ToUserId",
                table: "OrderHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_Requirement_Product_ProductId",
                table: "Requirement");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeader_FromUserId",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "DateChangeStatus",
                table: "Requirement");

            migrationBuilder.DropColumn(
                name: "Exprie",
                table: "Requirement");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Requirement");

            migrationBuilder.DropColumn(
                name: "FromUserId",
                table: "OrderHeader");

            migrationBuilder.RenameColumn(
                name: "ToUserId",
                table: "OrderHeader",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderHeader_ToUserId",
                table: "OrderHeader",
                newName: "IX_OrderHeader_UserId");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "Requirement",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeader_AspNetUsers_UserId",
                table: "OrderHeader",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requirement_Product_ProductId",
                table: "Requirement",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");
        }
    }
}
