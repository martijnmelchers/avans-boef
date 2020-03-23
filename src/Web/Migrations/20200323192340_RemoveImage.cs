using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class RemoveImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Accessoires");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Accessoires",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Accessoires",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Accessoires",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
