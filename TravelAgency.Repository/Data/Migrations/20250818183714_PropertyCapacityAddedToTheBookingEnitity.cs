using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class PropertyCapacityAddedToTheBookingEnitity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Itineraries_ItineraryId",
                table: "Bookings");

            migrationBuilder.AlterColumn<int>(
                name: "ItineraryId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Itineraries_ItineraryId",
                table: "Bookings",
                column: "ItineraryId",
                principalTable: "Itineraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Itineraries_ItineraryId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Bookings");

            migrationBuilder.AlterColumn<int>(
                name: "ItineraryId",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Itineraries_ItineraryId",
                table: "Bookings",
                column: "ItineraryId",
                principalTable: "Itineraries",
                principalColumn: "Id");
        }
    }
}
