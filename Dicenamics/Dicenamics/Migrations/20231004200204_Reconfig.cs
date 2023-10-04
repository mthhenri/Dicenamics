using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dicenamics.Migrations
{
    public partial class Reconfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DadosSimples_Salas_SalaId",
                table: "DadosSimples");

            migrationBuilder.DropForeignKey(
                name: "FK_DadosSimples_Usuarios_UsuarioId",
                table: "DadosSimples");

            migrationBuilder.DropIndex(
                name: "IX_DadosSimples_SalaId",
                table: "DadosSimples");

            migrationBuilder.DropIndex(
                name: "IX_DadosSimples_UsuarioId",
                table: "DadosSimples");

            migrationBuilder.DropColumn(
                name: "SalaId",
                table: "DadosSimples");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "DadosSimples");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalaId",
                table: "DadosSimples",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "DadosSimples",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DadosSimples_SalaId",
                table: "DadosSimples",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosSimples_UsuarioId",
                table: "DadosSimples",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_DadosSimples_Salas_SalaId",
                table: "DadosSimples",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "SalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DadosSimples_Usuarios_UsuarioId",
                table: "DadosSimples",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId");
        }
    }
}
