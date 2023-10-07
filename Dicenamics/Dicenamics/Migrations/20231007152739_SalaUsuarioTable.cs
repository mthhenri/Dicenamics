using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dicenamics.Migrations
{
    public partial class SalaUsuarioTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Salas_SalaId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_SalaId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "SalaId",
                table: "Usuarios");

            migrationBuilder.CreateTable(
                name: "SalaUsuario",
                columns: table => new
                {
                    SalaUsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SalaId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaUsuario", x => x.SalaUsuarioId);
                    table.ForeignKey(
                        name: "FK_SalaUsuario_Salas_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Salas",
                        principalColumn: "SalaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalaUsuario_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalaUsuario_SalaId",
                table: "SalaUsuario",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaUsuario_UsuarioId",
                table: "SalaUsuario",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalaUsuario");

            migrationBuilder.AddColumn<int>(
                name: "SalaId",
                table: "Usuarios",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_SalaId",
                table: "Usuarios",
                column: "SalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Salas_SalaId",
                table: "Usuarios",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "SalaId");
        }
    }
}
