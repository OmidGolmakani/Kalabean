using Microsoft.EntityFrameworkCore.Migrations;

namespace Kalabean.API.Migrations
{
    public partial class ChangeExpireDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exprie",
                table: "Requirement");

            migrationBuilder.AddColumn<double>(
                name: "Expire",
                table: "Requirement",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expire",
                table: "Requirement");

            migrationBuilder.AddColumn<int>(
                name: "Exprie",
                table: "Requirement",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
