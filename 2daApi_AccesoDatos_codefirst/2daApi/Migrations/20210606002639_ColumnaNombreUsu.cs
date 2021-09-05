using Microsoft.EntityFrameworkCore.Migrations;

namespace _2daApi.Migrations
{
    public partial class ColumnaNombreUsu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "nombreUsu",
                table: "usuarios",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nombreUsu",
                table: "usuarios");
        }
    }
}
