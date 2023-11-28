using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    public partial class Entrega : Migration
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
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    Nickname = table.Column<string>(type: "TEXT", nullable: true),
                    Senha = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
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
                    table.ForeignKey(
                        name: "FK_DadosCompostos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId");
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
                    table.ForeignKey(
                        name: "FK_DadosSimples_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId");
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
                    table.ForeignKey(
                        name: "FK_Salas_Usuarios_UsuarioMestreId",
                        column: x => x.UsuarioMestreId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DadoCompostoModFixo",
                columns: table => new
                {
                    DadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ModificadorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadoCompostoModFixo", x => new { x.DadoId, x.ModificadorId });
                    table.ForeignKey(
                        name: "FK_DadoCompostoModFixo_DadosCompostos_DadoId",
                        column: x => x.DadoId,
                        principalTable: "DadosCompostos",
                        principalColumn: "DadoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DadoCompostoModFixo_ModificadoresFixos_ModificadorId",
                        column: x => x.ModificadorId,
                        principalTable: "ModificadoresFixos",
                        principalColumn: "ModificadorFixoId",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_DadosCompostosSalas_Salas_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Salas",
                        principalColumn: "SalaId");
                    table.ForeignKey(
                        name: "FK_DadosCompostosSalas_Usuarios_CriadorId",
                        column: x => x.CriadorId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
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
                    SalaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosSimplesSalas", x => x.DadoSimplesSalaId);
                    table.ForeignKey(
                        name: "FK_DadosSimplesSalas_Salas_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Salas",
                        principalColumn: "SalaId");
                    table.ForeignKey(
                        name: "FK_DadosSimplesSalas_Usuarios_CriadorId",
                        column: x => x.CriadorId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "DadoCompostoModVar",
                columns: table => new
                {
                    ConectDadoVarId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ModificadorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadoCompostoModVar", x => x.ConectDadoVarId);
                    table.ForeignKey(
                        name: "FK_DadoCompostoModVar_DadosCompostos_DadoId",
                        column: x => x.DadoId,
                        principalTable: "DadosCompostos",
                        principalColumn: "DadoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DadoCompostoModVar_ModificadoresVariaveis_ModificadorId",
                        column: x => x.ModificadorId,
                        principalTable: "ModificadoresVariaveis",
                        principalColumn: "ModificadorVariavelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DadoCompostoSalaModFixo",
                columns: table => new
                {
                    ConectDadoFixoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ModificadorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadoCompostoSalaModFixo", x => x.ConectDadoFixoId);
                    table.ForeignKey(
                        name: "FK_DadoCompostoSalaModFixo_DadosCompostosSalas_DadoId",
                        column: x => x.DadoId,
                        principalTable: "DadosCompostosSalas",
                        principalColumn: "DadoCompostoSalaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DadoCompostoSalaModFixo_ModificadoresFixos_ModificadorId",
                        column: x => x.ModificadorId,
                        principalTable: "ModificadoresFixos",
                        principalColumn: "ModificadorFixoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DadoCompostoSalaModVar",
                columns: table => new
                {
                    ConectDadoVarId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ModificadorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadoCompostoSalaModVar", x => x.ConectDadoVarId);
                    table.ForeignKey(
                        name: "FK_DadoCompostoSalaModVar_DadosCompostosSalas_DadoId",
                        column: x => x.DadoId,
                        principalTable: "DadosCompostosSalas",
                        principalColumn: "DadoCompostoSalaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DadoCompostoSalaModVar_ModificadoresVariaveis_DadoId",
                        column: x => x.DadoId,
                        principalTable: "ModificadoresVariaveis",
                        principalColumn: "ModificadorVariavelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolagensDadosSalas",
                columns: table => new
                {
                    RolagemDadoSalaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoladoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UsuarioUsername = table.Column<string>(type: "TEXT", nullable: false),
                    Resultados = table.Column<string>(type: "TEXT", nullable: false),
                    TipoRolagem = table.Column<string>(type: "TEXT", nullable: false),
                    DadoId = table.Column<int>(type: "INTEGER", nullable: true),
                    DadoCompostoSalaId = table.Column<int>(type: "INTEGER", nullable: false),
                    SalaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolagensDadosSalas", x => x.RolagemDadoSalaId);
                    table.ForeignKey(
                        name: "FK_RolagensDadosSalas_DadosCompostosSalas_DadoCompostoSalaId",
                        column: x => x.DadoCompostoSalaId,
                        principalTable: "DadosCompostosSalas",
                        principalColumn: "DadoCompostoSalaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolagensDadosSalas_Salas_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Salas",
                        principalColumn: "SalaId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DadoCompostoModFixo_ModificadorId",
                table: "DadoCompostoModFixo",
                column: "ModificadorId");

            migrationBuilder.CreateIndex(
                name: "IX_DadoCompostoModVar_DadoId",
                table: "DadoCompostoModVar",
                column: "DadoId");

            migrationBuilder.CreateIndex(
                name: "IX_DadoCompostoModVar_ModificadorId",
                table: "DadoCompostoModVar",
                column: "ModificadorId");

            migrationBuilder.CreateIndex(
                name: "IX_DadoCompostoSalaModFixo_DadoId",
                table: "DadoCompostoSalaModFixo",
                column: "DadoId");

            migrationBuilder.CreateIndex(
                name: "IX_DadoCompostoSalaModFixo_ModificadorId",
                table: "DadoCompostoSalaModFixo",
                column: "ModificadorId");

            migrationBuilder.CreateIndex(
                name: "IX_DadoCompostoSalaModVar_DadoId",
                table: "DadoCompostoSalaModVar",
                column: "DadoId");

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
                name: "IX_DadosSimplesSalas_SalaId",
                table: "DadosSimplesSalas",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificadoresVariaveis_DadoSimplesId",
                table: "ModificadoresVariaveis",
                column: "DadoSimplesId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolagensDadosSalas_DadoCompostoSalaId",
                table: "RolagensDadosSalas",
                column: "DadoCompostoSalaId");

            migrationBuilder.CreateIndex(
                name: "IX_RolagensDadosSalas_SalaId",
                table: "RolagensDadosSalas",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Salas_UsuarioMestreId",
                table: "Salas",
                column: "UsuarioMestreId");

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
                name: "DadoCompostoModFixo");

            migrationBuilder.DropTable(
                name: "DadoCompostoModVar");

            migrationBuilder.DropTable(
                name: "DadoCompostoSalaModFixo");

            migrationBuilder.DropTable(
                name: "DadoCompostoSalaModVar");

            migrationBuilder.DropTable(
                name: "DadosSimplesSalas");

            migrationBuilder.DropTable(
                name: "RolagensDadosSalas");

            migrationBuilder.DropTable(
                name: "SalaUsuario");

            migrationBuilder.DropTable(
                name: "DadosCompostos");

            migrationBuilder.DropTable(
                name: "ModificadoresFixos");

            migrationBuilder.DropTable(
                name: "ModificadoresVariaveis");

            migrationBuilder.DropTable(
                name: "DadosCompostosSalas");

            migrationBuilder.DropTable(
                name: "DadosSimples");

            migrationBuilder.DropTable(
                name: "Salas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
