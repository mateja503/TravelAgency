using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class removeAddressFromCustomerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Address_Country",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Address_LivingAddress",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Country",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_LivingAddress",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
