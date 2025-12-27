using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timely.Migrations
{
    /// <inheritdoc />
    public partial class updateinvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdInvoice",
                table: "Invoices",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Invoices",
                newName: "IdInvoice");
        }
    }
}
