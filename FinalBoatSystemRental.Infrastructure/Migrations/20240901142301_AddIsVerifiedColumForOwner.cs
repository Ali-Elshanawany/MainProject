using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalBoatSystemRental.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsVerifiedColumForOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Owners",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f117b498-2e53-4686-86dc-d3c13072850e",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Customer", "CUSTOMER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Owners");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f117b498-2e53-4686-86dc-d3c13072850e",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "member", "MEMBER" });
        }
    }
}
