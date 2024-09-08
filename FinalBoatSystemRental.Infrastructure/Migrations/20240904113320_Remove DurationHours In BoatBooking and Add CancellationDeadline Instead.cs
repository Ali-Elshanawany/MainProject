using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalBoatSystemRental.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDurationHoursInBoatBookingandAddCancellationDeadlineInstead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationHour",
                table: "BoatBookings");

            migrationBuilder.AddColumn<DateTime>(
                name: "CancellationDeadLine",
                table: "BoatBookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancellationDeadLine",
                table: "BoatBookings");

            migrationBuilder.AddColumn<int>(
                name: "DurationHour",
                table: "BoatBookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
