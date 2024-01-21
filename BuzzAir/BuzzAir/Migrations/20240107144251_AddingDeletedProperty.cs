using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuzzAir.Migrations
{
    /// <inheritdoc />
    public partial class AddingDeletedProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "Bookings",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Flights",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Airports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Aircrafts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Airports");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Aircrafts");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Bookings",
                newName: "Deleted");
        }
    }
}
