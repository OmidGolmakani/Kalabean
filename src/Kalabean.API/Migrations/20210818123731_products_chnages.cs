using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kalabean.API.Migrations
{
    public partial class products_chnages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateArchive",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "DatePublish",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "Publish",
                table: "Product",
                newName: "IsEnabled");

            migrationBuilder.RenameColumn(
                name: "Creator",
                table: "Product",
                newName: "Manufacturer");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArchivingDate",
                table: "Product",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HtmlContent",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keywords",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishingDate",
                table: "Product",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArchivingDate",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "HtmlContent",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Keywords",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "PublishingDate",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "Manufacturer",
                table: "Product",
                newName: "Creator");

            migrationBuilder.RenameColumn(
                name: "IsEnabled",
                table: "Product",
                newName: "Publish");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateArchive",
                table: "Product",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DatePublish",
                table: "Product",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
