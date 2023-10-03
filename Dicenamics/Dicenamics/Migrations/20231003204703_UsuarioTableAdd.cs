using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dicenamics.Migrations
{
    public partial class UsuarioTableAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "Usuarios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Usuarios",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Usuarios",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "DadosSimples",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DadosSimples_UsuarioId",
                table: "DadosSimples",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_DadosSimples_Usuarios_UsuarioId",
                table: "DadosSimples",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DadosSimples_Usuarios_UsuarioId",
                table: "DadosSimples");

            migrationBuilder.DropIndex(
                name: "IX_DadosSimples_UsuarioId",
                table: "DadosSimples");

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "DadosSimples");
        }
    }
}
