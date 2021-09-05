using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _2daApi.Migrations
{
    public partial class pruebaPersonas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApellidoUsu",
                table: "usuarios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaNacimiento",
                table: "personas",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Soltero",
                table: "personas",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApellidoUsu",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "FechaNacimiento",
                table: "personas");

            migrationBuilder.DropColumn(
                name: "Soltero",
                table: "personas");
        }
    }
}
