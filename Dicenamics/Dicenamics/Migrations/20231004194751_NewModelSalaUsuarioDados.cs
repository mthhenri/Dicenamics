using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dicenamics.Migrations
{
    public partial class NewModelSalaUsuarioDados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModificadoresFixos",
                columns: table => new
                {
                    ModificadorFixoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Valor = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModificadoresFixos", x => x.ModificadorFixoId);
                });

            migrationBuilder.CreateTable(
                name: "DadosSimples",
                columns: table => new
                {
                    DadoSimplesId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Faces = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    Condicao = table.Column<string>(type: "TEXT", nullable: true),
                    SalaId = table.Column<int>(type: "INTEGER", nullable: true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosSimples", x => x.DadoSimplesId);
                });

            migrationBuilder.CreateTable(
                name: "ModificadoresVariaveis",
                columns: table => new
                {
                    ModificadorVariavelId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    DadoSimplesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModificadoresVariaveis", x => x.ModificadorVariavelId);
                    table.ForeignKey(
                        name: "FK_ModificadoresVariaveis_DadosSimples_DadoSimplesId",
                        column: x => x.DadoSimplesId,
                        principalTable: "DadosSimples",
                        principalColumn: "DadoSimplesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Salas",
                columns: table => new
                {
                    SalaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    IdSimples = table.Column<int>(type: "INTEGER", nullable: false),
                    IdLink = table.Column<string>(type: "TEXT", nullable: false),
                    UsuarioMestreId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salas", x => x.SalaId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Nickname = table.Column<string>(type: "TEXT", nullable: true),
                    Senha = table.Column<string>(type: "TEXT", nullable: false),
                    SalaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Salas_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Salas",
                        principalColumn: "SalaId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DadosSimples_SalaId",
                table: "DadosSimples",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosSimples_UsuarioId",
                table: "DadosSimples",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificadoresVariaveis_DadoSimplesId",
                table: "ModificadoresVariaveis",
                column: "DadoSimplesId");

            migrationBuilder.CreateIndex(
                name: "IX_Salas_UsuarioMestreId",
                table: "Salas",
                column: "UsuarioMestreId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_SalaId",
                table: "Usuarios",
                column: "SalaId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Salas_Usuarios_UsuarioMestreId",
                table: "Salas",
                column: "UsuarioMestreId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Salas_SalaId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "ModificadoresFixos");

            migrationBuilder.DropTable(
                name: "ModificadoresVariaveis");

            migrationBuilder.DropTable(
                name: "DadosSimples");

            migrationBuilder.DropTable(
                name: "Salas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
