using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class AddedBookingRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookinmBeestjes",
                columns: table => new
                {
                    BookingId = table.Column<int>(nullable: false),
                    BeestjeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookinmBeestjes", x => new { x.BookingId, x.BeestjeId });
                    table.ForeignKey(
                        name: "FK_BookinmBeestjes_Beestjes_BeestjeId",
                        column: x => x.BeestjeId,
                        principalTable: "Beestjes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookinmBeestjes_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookinmBeestjes_BeestjeId",
                table: "BookinmBeestjes",
                column: "BeestjeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookinmBeestjes");
        }
    }
}
