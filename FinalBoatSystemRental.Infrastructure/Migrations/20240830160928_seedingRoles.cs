using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinalBoatSystemRental.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7bdb9275-8cd4-4d86-bea6-bbdb5125e28a", null, "admin", "ADMIN" },
                    { "936c5f84-e463-49c2-bb6a-93347bbd5103", null, "owner", "OWNER" },
                    { "f117b498-2e53-4686-86dc-d3c13072850e", null, "member", "MEMBER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7bdb9275-8cd4-4d86-bea6-bbdb5125e28a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936c5f84-e463-49c2-bb6a-93347bbd5103");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f117b498-2e53-4686-86dc-d3c13072850e");
        }
    }
}
