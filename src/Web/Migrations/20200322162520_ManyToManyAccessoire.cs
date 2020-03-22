using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class ManyToManyAccessoire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accessoires_Beestjes_BeestjeId",
                table: "Accessoires");

            migrationBuilder.DropIndex(
                name: "IX_Accessoires_BeestjeId",
                table: "Accessoires");

            migrationBuilder.DropColumn(
                name: "BeestjeId",
                table: "Accessoires");

            migrationBuilder.CreateTable(
                name: "BeestjeAccessoires",
                columns: table => new
                {
                    BeestjeId = table.Column<int>(nullable: false),
                    AccessoireId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeestjeAccessoires", x => new { x.BeestjeId, x.AccessoireId });
                    table.ForeignKey(
                        name: "FK_BeestjeAccessoires_Accessoires_AccessoireId",
                        column: x => x.AccessoireId,
                        principalTable: "Accessoires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeestjeAccessoires_Beestjes_BeestjeId",
                        column: x => x.BeestjeId,
                        principalTable: "Beestjes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeestjeAccessoires_AccessoireId",
                table: "BeestjeAccessoires",
                column: "AccessoireId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeestjeAccessoires");

            migrationBuilder.AddColumn<int>(
                name: "BeestjeId",
                table: "Accessoires",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accessoires_BeestjeId",
                table: "Accessoires",
                column: "BeestjeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accessoires_Beestjes_BeestjeId",
                table: "Accessoires",
                column: "BeestjeId",
                principalTable: "Beestjes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
