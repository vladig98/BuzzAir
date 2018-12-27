using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuzzAir.Data.Migrations
{
    public partial class AddedNewClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airports_DestinationId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airports_OriginId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_DestinationId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_OriginId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "OriginId",
                table: "Flights");

            migrationBuilder.CreateTable(
                name: "AirportFlights",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AirportId = table.Column<int>(nullable: false),
                    FlightId = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirportFlights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirportFlights_Airports_AirportId",
                        column: x => x.AirportId,
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AirportFlights_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirportFlights_AirportId",
                table: "AirportFlights",
                column: "AirportId");

            migrationBuilder.CreateIndex(
                name: "IX_AirportFlights_FlightId",
                table: "AirportFlights",
                column: "FlightId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirportFlights");

            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "Flights",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OriginId",
                table: "Flights",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DestinationId",
                table: "Flights",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_OriginId",
                table: "Flights",
                column: "OriginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airports_DestinationId",
                table: "Flights",
                column: "DestinationId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airports_OriginId",
                table: "Flights",
                column: "OriginId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
