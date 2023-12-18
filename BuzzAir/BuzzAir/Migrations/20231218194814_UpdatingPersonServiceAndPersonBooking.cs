using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuzzAir.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingPersonServiceAndPersonBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingPassengers_People_PersonId",
                table: "BookingPassengers");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonServices_People_PassengerId",
                table: "PersonServices");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonServices_People_PersonId",
                table: "PersonServices");

            migrationBuilder.DropIndex(
                name: "IX_PersonServices_PersonId",
                table: "PersonServices");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "PersonServices");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "BookingPassengers",
                newName: "PassengerId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingPassengers_PersonId",
                table: "BookingPassengers",
                newName: "IX_BookingPassengers_PassengerId");

            migrationBuilder.AlterColumn<string>(
                name: "PassengerId",
                table: "PersonServices",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPassengers_People_PassengerId",
                table: "BookingPassengers",
                column: "PassengerId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonServices_People_PassengerId",
                table: "PersonServices",
                column: "PassengerId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingPassengers_People_PassengerId",
                table: "BookingPassengers");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonServices_People_PassengerId",
                table: "PersonServices");

            migrationBuilder.RenameColumn(
                name: "PassengerId",
                table: "BookingPassengers",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingPassengers_PassengerId",
                table: "BookingPassengers",
                newName: "IX_BookingPassengers_PersonId");

            migrationBuilder.AlterColumn<string>(
                name: "PassengerId",
                table: "PersonServices",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "PersonId",
                table: "PersonServices",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PersonServices_PersonId",
                table: "PersonServices",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPassengers_People_PersonId",
                table: "BookingPassengers",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonServices_People_PassengerId",
                table: "PersonServices",
                column: "PassengerId",
                principalTable: "People",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonServices_People_PersonId",
                table: "PersonServices",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
