namespace BuzzAir.Data.Migrations;

/// <inheritdoc />
public partial class MakingIATANullable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        if (migrationBuilder is null)
        {
            return;
        }

        _ = migrationBuilder.AlterColumn<string>(
            name: "IATA",
            table: "Airports",
            type: "character varying(3)",
            maxLength: 3,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(3)",
            oldMaxLength: 3);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        if (migrationBuilder is null)
        {
            return;
        }

        _ = migrationBuilder.AlterColumn<string>(
            name: "IATA",
            table: "Airports",
            type: "character varying(3)",
            maxLength: 3,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "character varying(3)",
            oldMaxLength: 3,
            oldNullable: true);
    }
}
