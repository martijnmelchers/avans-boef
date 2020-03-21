using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class AddedBookingRelationsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookinmBeestjes_Beestjes_BeestjeId",
                table: "BookinmBeestjes");

            migrationBuilder.DropForeignKey(
                name: "FK_BookinmBeestjes_Bookings_BookingId",
                table: "BookinmBeestjes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookinmBeestjes",
                table: "BookinmBeestjes");

            migrationBuilder.RenameTable(
                name: "BookinmBeestjes",
                newName: "BookingBeestjes");

            migrationBuilder.RenameIndex(
                name: "IX_BookinmBeestjes_BeestjeId",
                table: "BookingBeestjes",
                newName: "IX_BookingBeestjes_BeestjeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingBeestjes",
                table: "BookingBeestjes",
                columns: new[] { "BookingId", "BeestjeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookingBeestjes_Beestjes_BeestjeId",
                table: "BookingBeestjes",
                column: "BeestjeId",
                principalTable: "Beestjes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingBeestjes_Bookings_BookingId",
                table: "BookingBeestjes",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingBeestjes_Beestjes_BeestjeId",
                table: "BookingBeestjes");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingBeestjes_Bookings_BookingId",
                table: "BookingBeestjes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingBeestjes",
                table: "BookingBeestjes");

            migrationBuilder.RenameTable(
                name: "BookingBeestjes",
                newName: "BookinmBeestjes");

            migrationBuilder.RenameIndex(
                name: "IX_BookingBeestjes_BeestjeId",
                table: "BookinmBeestjes",
                newName: "IX_BookinmBeestjes_BeestjeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookinmBeestjes",
                table: "BookinmBeestjes",
                columns: new[] { "BookingId", "BeestjeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookinmBeestjes_Beestjes_BeestjeId",
                table: "BookinmBeestjes",
                column: "BeestjeId",
                principalTable: "Beestjes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookinmBeestjes_Bookings_BookingId",
                table: "BookinmBeestjes",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
