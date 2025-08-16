using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTravelPackageIdFromBookingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_TravelPackages_TravelPackageId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_TravelPackageId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "TravelPackageId",
                table: "Bookings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TravelPackageId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TravelPackageId",
                table: "Bookings",
                column: "TravelPackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_TravelPackages_TravelPackageId",
                table: "Bookings",
                column: "TravelPackageId",
                principalTable: "TravelPackages",
                principalColumn: "Id");
        }
    }
}
