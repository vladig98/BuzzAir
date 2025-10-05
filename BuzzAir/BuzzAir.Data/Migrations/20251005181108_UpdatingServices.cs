namespace BuzzAir.Data.Migrations;

/// <inheritdoc />
public partial class UpdatingServices : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        if (migrationBuilder is null)
        {
            return;
        }

        _ = migrationBuilder.DropForeignKey(
            name: "FK_PassengerServices_Services_ServiceId",
            table: "PassengerServices");

        _ = migrationBuilder.DropPrimaryKey(
            name: "PK_Services",
            table: "Services");

        _ = migrationBuilder.RenameTable(
            name: "Services",
            newName: "Service");

        _ = migrationBuilder.AddPrimaryKey(
            name: "PK_Service",
            table: "Service",
            column: "Id");

        _ = migrationBuilder.AddForeignKey(
            name: "FK_PassengerServices_Service_ServiceId",
            table: "PassengerServices",
            column: "ServiceId",
            principalTable: "Service",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        if (migrationBuilder is null)
        {
            return;
        }

        _ = migrationBuilder.DropForeignKey(
            name: "FK_PassengerServices_Service_ServiceId",
            table: "PassengerServices");

        _ = migrationBuilder.DropPrimaryKey(
            name: "PK_Service",
            table: "Service");

        _ = migrationBuilder.RenameTable(
            name: "Service",
            newName: "Services");

        _ = migrationBuilder.AddPrimaryKey(
            name: "PK_Services",
            table: "Services",
            column: "Id");

        _ = migrationBuilder.AddForeignKey(
            name: "FK_PassengerServices_Services_ServiceId",
            table: "PassengerServices",
            column: "ServiceId",
            principalTable: "Services",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }
}
