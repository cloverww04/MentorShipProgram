using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentorShipProgram.Migrations
{
    /// <inheritdoc />
    public partial class newSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: "2023-11-02T10:00");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateTime",
                value: "2023-10-31T13:00");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: "2023/11/01 10:00AM");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateTime",
                value: "2023/10/31 12:00PM");
        }
    }
}
