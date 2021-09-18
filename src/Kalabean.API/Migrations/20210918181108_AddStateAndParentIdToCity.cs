using Microsoft.EntityFrameworkCore.Migrations;

namespace Kalabean.API.Migrations
{
    public partial class AddStateAndParentIdToCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "HasImage",
                table: "Cities",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Cities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "State",
                table: "Cities",
                type: "tinyint",
                nullable: true,
                defaultValue: (byte)1);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ParentId",
                table: "Cities",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Cities_ParentId",
                table: "Cities",
                column: "ParentId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Cities_ParentId",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_ParentId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Cities");

            migrationBuilder.AlterColumn<bool>(
                name: "HasImage",
                table: "Cities",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);
        }
    }
}
