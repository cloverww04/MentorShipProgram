using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentorShipProgram.Migrations
{
    public partial class changesToAppointments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mentors_Appointments_AppointmentsId",
                table: "Mentors");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Appointments_AppointmentsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AppointmentsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Mentors_AppointmentsId",
                table: "Mentors");

            migrationBuilder.DropColumn(
                name: "AppointmentsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AppointmentsId",
                table: "Mentors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppointmentsId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppointmentsId",
                table: "Mentors",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AppointmentsId",
                table: "Users",
                column: "AppointmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Mentors_AppointmentsId",
                table: "Mentors",
                column: "AppointmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mentors_Appointments_AppointmentsId",
                table: "Mentors",
                column: "AppointmentsId",
                principalTable: "Appointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Appointments_AppointmentsId",
                table: "Users",
                column: "AppointmentsId",
                principalTable: "Appointments",
                principalColumn: "Id");
        }
    }
}
