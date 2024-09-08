using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalBoatSystemRental.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangingCreatedAtCloumnNameInTripTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatesAt",
                table: "Trips",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Trips",
                newName: "CreatesAt");
        }
    }
}
