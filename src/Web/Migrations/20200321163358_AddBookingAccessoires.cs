using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class AddBookingAccessoires : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingAccessoires",
                columns: table => new
                {
                    BookingId = table.Column<int>(nullable: false),
                    AccessoireId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingAccessoires", x => new { x.BookingId, x.AccessoireId });
                    table.ForeignKey(
                        name: "FK_BookingAccessoires_Accessoires_AccessoireId",
                        column: x => x.AccessoireId,
                        principalTable: "Accessoires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingAccessoires_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingAccessoires_AccessoireId",
                table: "BookingAccessoires",
                column: "AccessoireId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingAccessoires");
        }
    }
}
