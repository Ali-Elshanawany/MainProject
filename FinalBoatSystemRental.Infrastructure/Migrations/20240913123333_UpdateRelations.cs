using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalBoatSystemRental.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cancellations_Boats_BoatId",
                table: "Cancellations");

            migrationBuilder.AlterColumn<int>(
                name: "BoatId",
                table: "Cancellations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7bdb9275-8cd4-4d86-bea6-bbdb5125e28a",
                column: "Name",
                value: "Admin");

            migrationBuilder.CreateIndex(
                name: "IX_Cancellations_BoatBookingId",
                table: "Cancellations",
                column: "BoatBookingId",
                unique: true,
                filter: "[BoatBookingId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Cancellations_BoatBookings_BoatBookingId",
                table: "Cancellations",
                column: "BoatBookingId",
                principalTable: "BoatBookings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cancellations_Boats_BoatId",
                table: "Cancellations",
                column: "BoatId",
                principalTable: "Boats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cancellations_BoatBookings_BoatBookingId",
                table: "Cancellations");

            migrationBuilder.DropForeignKey(
                name: "FK_Cancellations_Boats_BoatId",
                table: "Cancellations");

            migrationBuilder.DropIndex(
                name: "IX_Cancellations_BoatBookingId",
                table: "Cancellations");

            migrationBuilder.AlterColumn<int>(
                name: "BoatId",
                table: "Cancellations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7bdb9275-8cd4-4d86-bea6-bbdb5125e28a",
                column: "Name",
                value: "admin");

            migrationBuilder.AddForeignKey(
                name: "FK_Cancellations_Boats_BoatId",
                table: "Cancellations",
                column: "BoatId",
                principalTable: "Boats",
                principalColumn: "Id");
        }
    }
}
