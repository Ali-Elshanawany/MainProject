using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalBoatSystemRental.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnRefundedYetInCancellationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RefundedYet",
                table: "Cancellations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefundedYet",
                table: "Cancellations");
        }
    }
}
