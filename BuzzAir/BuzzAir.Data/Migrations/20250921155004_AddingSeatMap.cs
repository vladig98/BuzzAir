namespace BuzzAir.Data.Migrations;

/// <inheritdoc />
public partial class AddingSeatMap : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        if (migrationBuilder is null)
        {
            return;
        }

        _ = migrationBuilder.DropPrimaryKey(
            name: "PK_FlightPassengers",
            table: "FlightPassengers");

        _ = migrationBuilder.AddPrimaryKey(
            name: "PK_FlightPassengers",
            table: "FlightPassengers",
            columns: ["FlightId", "PassengerId"]);

        _ = migrationBuilder.CreateTable(
            name: "SeatMaps",
            columns: table => new
            {
                AircraftId = table.Column<string>(type: "character varying(450)", nullable: false),
                SeatNumber = table.Column<int>(type: "integer", nullable: false),
                SeatType = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_SeatMaps", x => new { x.AircraftId, x.SeatNumber });
                _ = table.ForeignKey(
                    name: "FK_SeatMaps_Aircrafts_AircraftId",
                    column: x => x.AircraftId,
                    principalTable: "Aircrafts",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        _ = migrationBuilder.CreateIndex(
            name: "IX_FlightPassengers_FlightId",
            table: "FlightPassengers",
            column: "FlightId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        if (migrationBuilder is null)
        {
            return;
        }

        _ = migrationBuilder.DropTable(
            name: "SeatMaps");

        _ = migrationBuilder.DropPrimaryKey(
            name: "PK_FlightPassengers",
            table: "FlightPassengers");

        _ = migrationBuilder.DropIndex(
            name: "IX_FlightPassengers_FlightId",
            table: "FlightPassengers");

        _ = migrationBuilder.AddPrimaryKey(
            name: "PK_FlightPassengers",
            table: "FlightPassengers",
            columns: ["FlightId", "PassengerId", "SeatNumber"]);
    }
}
