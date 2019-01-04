using Microsoft.EntityFrameworkCore.Migrations;

namespace BuzzAir.Data.Migrations
{
    public partial class UpdatedClassProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBookings_AspNetUsers_ApplicationUserId1",
                table: "UserBookings");

            migrationBuilder.DropIndex(
                name: "IX_UserBookings_ApplicationUserId1",
                table: "UserBookings");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "UserBookings");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "UserBookings",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_UserBookings_ApplicationUserId",
                table: "UserBookings",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBookings_AspNetUsers_ApplicationUserId",
                table: "UserBookings",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBookings_AspNetUsers_ApplicationUserId",
                table: "UserBookings");

            migrationBuilder.DropIndex(
                name: "IX_UserBookings_ApplicationUserId",
                table: "UserBookings");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "UserBookings",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "UserBookings",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserBookings_ApplicationUserId1",
                table: "UserBookings",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBookings_AspNetUsers_ApplicationUserId1",
                table: "UserBookings",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
