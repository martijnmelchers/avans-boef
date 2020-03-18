using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class Beestje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beestjes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beestjes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accesoire",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accesoire");

            migrationBuilder.DropTable(
                name: "Beestjes");
        }
    }
}
