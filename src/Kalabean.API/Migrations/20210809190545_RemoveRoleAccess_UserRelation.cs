using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kalabean.API.Migrations
{
    public partial class RemoveRoleAccess_UserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AccessRules_AccessRuleId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_AccessRules_AccessRuleId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCenterTypes_AccessRules_AccessRuleId",
                table: "ShoppingCenterTypes");

            migrationBuilder.DropTable(
                name: "AccessRules");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCenterTypes_AccessRuleId",
                table: "ShoppingCenterTypes");

            migrationBuilder.DropIndex(
                name: "IX_Cities_AccessRuleId",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Categories_AccessRuleId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "AccessRuleId",
                table: "ShoppingCenterTypes");

            migrationBuilder.DropColumn(
                name: "AccessRuleId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "AccessRuleId",
                table: "Categories");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Stores",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Requirement",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Requirement",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Requirement",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Requirement",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Product",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Product",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "OrderHeader",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "OrderHeader",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "OrderHeader",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "OrderHeader",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stores_UserId",
                table: "Stores",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Requirement_AdminId",
                table: "Requirement",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Requirement_UserId",
                table: "Requirement",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeader_UserId",
                table: "OrderHeader",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeader_AspNetUsers_UserId",
                table: "OrderHeader",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requirement_AspNetUsers_AdminId",
                table: "Requirement",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requirement_AspNetUsers_UserId",
                table: "Requirement",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_AspNetUsers_UserId",
                table: "Stores",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeader_AspNetUsers_UserId",
                table: "OrderHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_Requirement_AspNetUsers_AdminId",
                table: "Requirement");

            migrationBuilder.DropForeignKey(
                name: "FK_Requirement_AspNetUsers_UserId",
                table: "Requirement");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_AspNetUsers_UserId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_UserId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Requirement_AdminId",
                table: "Requirement");

            migrationBuilder.DropIndex(
                name: "IX_Requirement_UserId",
                table: "Requirement");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeader_UserId",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Requirement");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Requirement");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Requirement");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Requirement");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "OrderHeader");

            migrationBuilder.AddColumn<Guid>(
                name: "AccessRuleId",
                table: "ShoppingCenterTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AccessRuleId",
                table: "Cities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AccessRuleId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccessRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRules", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCenterTypes_AccessRuleId",
                table: "ShoppingCenterTypes",
                column: "AccessRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_AccessRuleId",
                table: "Cities",
                column: "AccessRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_AccessRuleId",
                table: "Categories",
                column: "AccessRuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AccessRules_AccessRuleId",
                table: "Categories",
                column: "AccessRuleId",
                principalTable: "AccessRules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_AccessRules_AccessRuleId",
                table: "Cities",
                column: "AccessRuleId",
                principalTable: "AccessRules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCenterTypes_AccessRules_AccessRuleId",
                table: "ShoppingCenterTypes",
                column: "AccessRuleId",
                principalTable: "AccessRules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
