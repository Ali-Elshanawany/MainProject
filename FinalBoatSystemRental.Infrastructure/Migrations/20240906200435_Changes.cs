using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalBoatSystemRental.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoatId",
                table: "Cancellations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "Boats",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Cancellations_BoatId",
                table: "Cancellations",
                column: "BoatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cancellations_Boats_BoatId",
                table: "Cancellations",
                column: "BoatId",
                principalTable: "Boats",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cancellations_Boats_BoatId",
                table: "Cancellations");

            migrationBuilder.DropIndex(
                name: "IX_Cancellations_BoatId",
                table: "Cancellations");

            migrationBuilder.DropColumn(
                name: "BoatId",
                table: "Cancellations");

            migrationBuilder.AlterColumn<string>(
                name: "Capacity",
                table: "Boats",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
