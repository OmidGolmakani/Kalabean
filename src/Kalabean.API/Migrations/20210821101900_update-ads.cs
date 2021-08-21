using Microsoft.EntityFrameworkCore.Migrations;

namespace Kalabean.API.Migrations
{
    public partial class updateads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertise_Advertise_ParentId",
                table: "Advertise");

            migrationBuilder.DropIndex(
                name: "IX_Advertise_ParentId",
                table: "Advertise");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Advertise");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Advertise");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Advertise",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Advertise",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Advertise",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

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
    }
}
