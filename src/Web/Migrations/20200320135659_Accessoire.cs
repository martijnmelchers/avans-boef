using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class Accessoire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accesoire");

            migrationBuilder.CreateTable(
                name: "Accessoires",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    BeestjeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessoires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accessoires_Beestjes_BeestjeId",
                        column: x => x.BeestjeId,
                        principalTable: "Beestjes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accessoires_BeestjeId",
                table: "Accessoires",
                column: "BeestjeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accessoires");

            migrationBuilder.CreateTable(
                name: "Accesoire",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BeestjeId = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accesoire", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accesoire_Beestjes_BeestjeId",
                        column: x => x.BeestjeId,
                        principalTable: "Beestjes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accesoire_BeestjeId",
                table: "Accesoire",
                column: "BeestjeId");
        }
    }
}
