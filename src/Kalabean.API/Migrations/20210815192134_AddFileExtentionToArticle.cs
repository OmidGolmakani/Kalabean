using Microsoft.EntityFrameworkCore.Migrations;

namespace Kalabean.API.Migrations
{
    public partial class AddFileExtentionToArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequirementUserSeen_AspNetUsers_UserId",
                table: "RequirementUserSeen");

            migrationBuilder.DropForeignKey(
                name: "FK_RequirementUserSeen_Requirement_RequirementId",
                table: "RequirementUserSeen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequirementUserSeen",
                table: "RequirementUserSeen");

            migrationBuilder.RenameTable(
                name: "RequirementUserSeen",
                newName: "RequirementUserSeens");

            migrationBuilder.RenameIndex(
                name: "IX_RequirementUserSeen_UserId",
                table: "RequirementUserSeens",
                newName: "IX_RequirementUserSeens_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RequirementUserSeen_RequirementId",
                table: "RequirementUserSeens",
                newName: "IX_RequirementUserSeens_RequirementId");

            migrationBuilder.AddColumn<string>(
                name: "FileExtention",
                table: "Article",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequirementUserSeens",
                table: "RequirementUserSeens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RequirementUserSeens_AspNetUsers_UserId",
                table: "RequirementUserSeens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequirementUserSeens_Requirement_RequirementId",
                table: "RequirementUserSeens",
                column: "RequirementId",
                principalTable: "Requirement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequirementUserSeens_AspNetUsers_UserId",
                table: "RequirementUserSeens");

            migrationBuilder.DropForeignKey(
                name: "FK_RequirementUserSeens_Requirement_RequirementId",
                table: "RequirementUserSeens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequirementUserSeens",
                table: "RequirementUserSeens");

            migrationBuilder.DropColumn(
                name: "FileExtention",
                table: "Article");

            migrationBuilder.RenameTable(
                name: "RequirementUserSeens",
                newName: "RequirementUserSeen");

            migrationBuilder.RenameIndex(
                name: "IX_RequirementUserSeens_UserId",
                table: "RequirementUserSeen",
                newName: "IX_RequirementUserSeen_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RequirementUserSeens_RequirementId",
                table: "RequirementUserSeen",
                newName: "IX_RequirementUserSeen_RequirementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequirementUserSeen",
                table: "RequirementUserSeen",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RequirementUserSeen_AspNetUsers_UserId",
                table: "RequirementUserSeen",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequirementUserSeen_Requirement_RequirementId",
                table: "RequirementUserSeen",
                column: "RequirementId",
                principalTable: "Requirement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
