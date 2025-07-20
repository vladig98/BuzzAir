using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuzzAir.Migrations
{
    /// <inheritdoc />
    public partial class addingTimeZoneToAirport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeZone",
                table: "Airports");

            migrationBuilder.AddColumn<string>(
                name: "TimezoneId",
                table: "Airports",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Airports_TimezoneId",
                table: "Airports",
                column: "TimezoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Airports_Timezones_TimezoneId",
                table: "Airports",
                column: "TimezoneId",
                principalTable: "Timezones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Airports_Timezones_TimezoneId",
                table: "Airports");

            migrationBuilder.DropIndex(
                name: "IX_Airports_TimezoneId",
                table: "Airports");

            migrationBuilder.DropColumn(
                name: "TimezoneId",
                table: "Airports");

            migrationBuilder.AddColumn<string>(
                name: "TimeZone",
                table: "Airports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
