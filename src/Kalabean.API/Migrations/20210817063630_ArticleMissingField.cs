using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kalabean.API.Migrations
{
    public partial class ArticleMissingField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ArchiveDateTime",
                table: "Article",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HtmlBody",
                table: "Article",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishDateTime",
                table: "Article",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArchiveDateTime",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "HtmlBody",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "PublishDateTime",
                table: "Article");
        }
    }
}
