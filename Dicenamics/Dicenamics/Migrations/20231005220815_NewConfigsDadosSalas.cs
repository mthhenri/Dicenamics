using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dicenamics.Migrations
{
    public partial class NewConfigsDadosSalas : Migration
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
                name: "DadosCompostos",
                columns: table => new
                {
                    DadoCompostoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: true),
                    DadoCompostoSalaId = table.Column<int>(type: "INTEGER", nullable: true),
                    AcessoPrivado = table.Column<bool>(type: "INTEGER", nullable: true),
                    CriadorId = table.Column<int>(type: "INTEGER", nullable: true),
                    SalaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosCompostos", x => x.DadoCompostoId);
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
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: true),
                    DadoSimplesSalaId = table.Column<int>(type: "INTEGER", nullable: true),
                    AcessoPrivado = table.Column<bool>(type: "INTEGER", nullable: true),
                    CriadorId = table.Column<int>(type: "INTEGER", nullable: true),
                    SalaId = table.Column<int>(type: "INTEGER", nullable: true)
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
                name: "IX_DadosCompostos_CriadorId",
                table: "DadosCompostos",
                column: "CriadorId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosCompostos_SalaId",
                table: "DadosCompostos",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosCompostos_UsuarioId",
                table: "DadosCompostos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosSimples_CriadorId",
                table: "DadosSimples",
                column: "CriadorId");

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
                name: "FK_DadosCompostos_Salas_SalaId",
                table: "DadosCompostos",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "SalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DadosCompostos_Usuarios_CriadorId",
                table: "DadosCompostos",
                column: "CriadorId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DadosCompostos_Usuarios_UsuarioId",
                table: "DadosCompostos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_DadosSimples_Salas_SalaId",
                table: "DadosSimples",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "SalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DadosSimples_Usuarios_CriadorId",
                table: "DadosSimples",
                column: "CriadorId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

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
                name: "DadosCompostos");

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
