using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalBoatSystemRental.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateboatBookingRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoatBookings_Owners_OwnerId",
                table: "BoatBookings");

            migrationBuilder.DropIndex(
                name: "IX_BoatBookings_OwnerId",
                table: "BoatBookings");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "BoatBookings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "BoatBookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BoatBookings_OwnerId",
                table: "BoatBookings",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoatBookings_Owners_OwnerId",
                table: "BoatBookings",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
