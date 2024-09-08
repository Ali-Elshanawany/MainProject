using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalBoatSystemRental.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTripStartedAtColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StaredAt",
                table: "Trips",
                newName: "StartedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartedAt",
                table: "Trips",
                newName: "StaredAt");
        }
    }
}
