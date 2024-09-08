using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalBoatSystemRental.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateboatsBoatBookingRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoatId",
                table: "BoatBookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BoatBookings_BoatId",
                table: "BoatBookings",
                column: "BoatId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoatBookings_Boats_BoatId",
                table: "BoatBookings",
                column: "BoatId",
                principalTable: "Boats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoatBookings_Boats_BoatId",
                table: "BoatBookings");

            migrationBuilder.DropIndex(
                name: "IX_BoatBookings_BoatId",
                table: "BoatBookings");

            migrationBuilder.DropColumn(
                name: "BoatId",
                table: "BoatBookings");
        }
    }
}
