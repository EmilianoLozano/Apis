using Microsoft.EntityFrameworkCore.Migrations;

namespace _2daApi.Migrations
{
    public partial class fkRoll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idRol",
                table: "usuarios",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_idRol",
                table: "usuarios",
                column: "idRol");

            migrationBuilder.AddForeignKey(
                name: "FK_usuarios_roles_idRol",
                table: "usuarios",
                column: "idRol",
                principalTable: "roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_usuarios_roles_idRol",
                table: "usuarios");

            migrationBuilder.DropIndex(
                name: "IX_usuarios_idRol",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "idRol",
                table: "usuarios");
        }
    }
}
