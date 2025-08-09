using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddressPropertyAddedToTheCustomerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price_TypeCurrencty",
                table: "TravelPackages",
                newName: "Price_TypeCurrency");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price_TypeCurrency",
                table: "TravelPackages",
                newName: "Price_TypeCurrencty");
        }
    }
}
