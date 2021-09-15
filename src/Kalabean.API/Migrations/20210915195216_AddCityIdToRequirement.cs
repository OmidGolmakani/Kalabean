using Microsoft.EntityFrameworkCore.Migrations;

namespace Kalabean.API.Migrations
{
    public partial class AddCityIdToRequirement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Requirement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql("UPDATE Requirement  set CityId=(select top(1) id from Cities)");

            migrationBuilder.CreateIndex(
                name: "IX_Requirement_CityId",
                table: "Requirement",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requirement_Cities_CityId",
                table: "Requirement",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requirement_Cities_CityId",
                table: "Requirement");

            migrationBuilder.DropIndex(
                name: "IX_Requirement_CityId",
                table: "Requirement");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Requirement");
        }
    }
}
