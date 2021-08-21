using Microsoft.EntityFrameworkCore.Migrations;

namespace Kalabean.API.Migrations
{
    public partial class AddParentIdToAd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Advertise",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Advertise_ParentId",
                table: "Advertise",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertise_Advertise_ParentId",
                table: "Advertise",
                column: "ParentId",
                principalTable: "Advertise",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertise_Advertise_ParentId",
                table: "Advertise");

            migrationBuilder.DropIndex(
                name: "IX_Advertise_ParentId",
                table: "Advertise");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Advertise");
        }
    }
}
