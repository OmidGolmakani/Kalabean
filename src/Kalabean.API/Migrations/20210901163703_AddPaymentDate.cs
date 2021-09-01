using Microsoft.EntityFrameworkCore.Migrations;

namespace Kalabean.API.Migrations
{
    public partial class AddPaymentDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymenyLink",
                table: "OrderHeader",
                newName: "PaymentLink");

            migrationBuilder.RenameColumn(
                name: "PaymenyDate",
                table: "OrderHeader",
                newName: "PaymentDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentLink",
                table: "OrderHeader",
                newName: "PaymenyLink");

            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "OrderHeader",
                newName: "PaymenyDate");
        }
    }
}
