namespace BuzzAir.Data.Migrations;

/// <inheritdoc />
public partial class iso3ToCountry : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        if (migrationBuilder is null)
        {
            return;
        }

        _ = migrationBuilder.RenameColumn(
            name: "ISO",
            table: "Countries",
            newName: "ISOA2");

        _ = migrationBuilder.AddColumn<string>(
            name: "ISOA3",
            table: "Countries",
            type: "character varying(3)",
            maxLength: 3,
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        if (migrationBuilder is null)
        {
            return;
        }

        _ = migrationBuilder.DropColumn(
            name: "ISOA3",
            table: "Countries");

        _ = migrationBuilder.RenameColumn(
            name: "ISOA2",
            table: "Countries",
            newName: "ISO");
    }
}
