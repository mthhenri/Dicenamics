using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dicenamics.Migrations
{
    public partial class DatabaseNewTable : Migration
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
                name: "DadoCompostoModFixo",
                columns: table => new
                {
                    DadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ModificadorId = table.Column<int>(type: "INTEGER", nullable: false),
                    DadoCompostoSalaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadoCompostoModFixo", x => new { x.DadoId, x.ModificadorId });
                    table.ForeignKey(
                        name: "FK_DadoCompostoModFixo_ModificadoresFixos_DadoId",
                        column: x => x.DadoId,
                        principalTable: "ModificadoresFixos",
                        principalColumn: "ModificadorFixoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DadoCompostoModVar",
                columns: table => new
                {
                    ConectDadoVarId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ModificadorId = table.Column<int>(type: "INTEGER", nullable: false),
                    DadoCompostoSalaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadoCompostoModVar", x => x.ConectDadoVarId);
                });

            migrationBuilder.CreateTable(
                name: "DadosCompostos",
                columns: table => new
                {
                    DadoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Faces = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    Condicao = table.Column<string>(type: "TEXT", nullable: true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosCompostos", x => x.DadoId);
                });

            migrationBuilder.CreateTable(
                name: "DadosCompostosSalas",
                columns: table => new
                {
                    DadoCompostoSalaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AcessoPrivado = table.Column<bool>(type: "INTEGER", nullable: false),
                    CriadorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Faces = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    Condicao = table.Column<string>(type: "TEXT", nullable: true),
                    SalaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosCompostosSalas", x => x.DadoCompostoSalaId);
                });

            migrationBuilder.CreateTable(
                name: "DadosSimples",
                columns: table => new
                {
                    DadoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Faces = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    ModificadorVariavelId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosSimples", x => x.DadoId);
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
                        principalColumn: "DadoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DadosSimplesSalas",
                columns: table => new
                {
                    DadoSimplesSalaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AcessoPrivado = table.Column<bool>(type: "INTEGER", nullable: false),
                    CriadorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Faces = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    ModificadorVariavelId = table.Column<int>(type: "INTEGER", nullable: false),
                    SalaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosSimplesSalas", x => x.DadoSimplesSalaId);
                    table.ForeignKey(
                        name: "FK_DadosSimplesSalas_ModificadoresVariaveis_ModificadorVariavelId",
                        column: x => x.ModificadorVariavelId,
                        principalTable: "ModificadoresVariaveis",
                        principalColumn: "ModificadorVariavelId",
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
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    Nickname = table.Column<string>(type: "TEXT", nullable: true),
                    Senha = table.Column<string>(type: "TEXT", nullable: true),
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
                name: "IX_DadoCompostoModFixo_DadoCompostoSalaId",
                table: "DadoCompostoModFixo",
                column: "DadoCompostoSalaId");

            migrationBuilder.CreateIndex(
                name: "IX_DadoCompostoModFixo_ModificadorId",
                table: "DadoCompostoModFixo",
                column: "ModificadorId");

            migrationBuilder.CreateIndex(
                name: "IX_DadoCompostoModVar_DadoCompostoSalaId",
                table: "DadoCompostoModVar",
                column: "DadoCompostoSalaId");

            migrationBuilder.CreateIndex(
                name: "IX_DadoCompostoModVar_DadoId",
                table: "DadoCompostoModVar",
                column: "DadoId");

            migrationBuilder.CreateIndex(
                name: "IX_DadoCompostoModVar_ModificadorId",
                table: "DadoCompostoModVar",
                column: "ModificadorId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosCompostos_UsuarioId",
                table: "DadosCompostos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosCompostosSalas_CriadorId",
                table: "DadosCompostosSalas",
                column: "CriadorId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosCompostosSalas_SalaId",
                table: "DadosCompostosSalas",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosSimples_UsuarioId",
                table: "DadosSimples",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosSimplesSalas_CriadorId",
                table: "DadosSimplesSalas",
                column: "CriadorId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosSimplesSalas_ModificadorVariavelId",
                table: "DadosSimplesSalas",
                column: "ModificadorVariavelId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosSimplesSalas_SalaId",
                table: "DadosSimplesSalas",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificadoresVariaveis_DadoSimplesId",
                table: "ModificadoresVariaveis",
                column: "DadoSimplesId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Salas_UsuarioMestreId",
                table: "Salas",
                column: "UsuarioMestreId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_SalaId",
                table: "Usuarios",
                column: "SalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DadoCompostoModFixo_DadosCompostos_ModificadorId",
                table: "DadoCompostoModFixo",
                column: "ModificadorId",
                principalTable: "DadosCompostos",
                principalColumn: "DadoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DadoCompostoModFixo_DadosCompostosSalas_DadoCompostoSalaId",
                table: "DadoCompostoModFixo",
                column: "DadoCompostoSalaId",
                principalTable: "DadosCompostosSalas",
                principalColumn: "DadoCompostoSalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DadoCompostoModVar_DadosCompostos_DadoId",
                table: "DadoCompostoModVar",
                column: "DadoId",
                principalTable: "DadosCompostos",
                principalColumn: "DadoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DadoCompostoModVar_DadosCompostosSalas_DadoCompostoSalaId",
                table: "DadoCompostoModVar",
                column: "DadoCompostoSalaId",
                principalTable: "DadosCompostosSalas",
                principalColumn: "DadoCompostoSalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DadoCompostoModVar_ModificadoresVariaveis_ModificadorId",
                table: "DadoCompostoModVar",
                column: "ModificadorId",
                principalTable: "ModificadoresVariaveis",
                principalColumn: "ModificadorVariavelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DadosCompostos_Usuarios_UsuarioId",
                table: "DadosCompostos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_DadosCompostosSalas_Salas_SalaId",
                table: "DadosCompostosSalas",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "SalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DadosCompostosSalas_Usuarios_CriadorId",
                table: "DadosCompostosSalas",
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
                name: "FK_DadosSimplesSalas_Salas_SalaId",
                table: "DadosSimplesSalas",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "SalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DadosSimplesSalas_Usuarios_CriadorId",
                table: "DadosSimplesSalas",
                column: "CriadorId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Salas_Usuarios_UsuarioMestreId",
                table: "Salas");

            migrationBuilder.DropTable(
                name: "DadoCompostoModFixo");

            migrationBuilder.DropTable(
                name: "DadoCompostoModVar");

            migrationBuilder.DropTable(
                name: "DadosSimplesSalas");

            migrationBuilder.DropTable(
                name: "ModificadoresFixos");

            migrationBuilder.DropTable(
                name: "DadosCompostos");

            migrationBuilder.DropTable(
                name: "DadosCompostosSalas");

            migrationBuilder.DropTable(
                name: "ModificadoresVariaveis");

            migrationBuilder.DropTable(
                name: "DadosSimples");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Salas");
        }
    }
}
