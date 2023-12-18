using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuzzAir.Migrations
{
    /// <inheritdoc />
    public partial class addingFlightSeatAndRemovingAircraftSeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AircraftSeats");

            migrationBuilder.CreateTable(
                name: "FlightSeats",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FlightId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightSeats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightSeats_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightSeats_FlightId",
                table: "FlightSeats",
                column: "FlightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightSeats");

            migrationBuilder.CreateTable(
                name: "AircraftSeats",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AircraftId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AircraftSeats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AircraftSeats_Aircrafts_AircraftId",
                        column: x => x.AircraftId,
                        principalTable: "Aircrafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AircraftSeats_AircraftId",
                table: "AircraftSeats",
                column: "AircraftId");
        }
    }
}
