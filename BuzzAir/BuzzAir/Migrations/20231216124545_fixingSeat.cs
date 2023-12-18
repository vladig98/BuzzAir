using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuzzAir.Migrations
{
    /// <inheritdoc />
    public partial class fixingSeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeatNumber",
                table: "Services");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeatNumber",
                table: "Services",
                type: "int",
                nullable: true);
        }
    }
}
