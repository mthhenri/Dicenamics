using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dicenamics.Migrations
{
    public partial class DadoSalaTableMTM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DadoCompostoModFixo_DadosCompostosSalas_DadoCompostoSalaId",
                table: "DadoCompostoModFixo");

            migrationBuilder.DropForeignKey(
                name: "FK_DadoCompostoModVar_DadosCompostosSalas_DadoCompostoSalaId",
                table: "DadoCompostoModVar");

            migrationBuilder.DropForeignKey(
                name: "FK_DadosSimplesSalas_ModificadoresVariaveis_ModificadorVariavelId",
                table: "DadosSimplesSalas");

            migrationBuilder.DropIndex(
                name: "IX_DadosSimplesSalas_ModificadorVariavelId",
                table: "DadosSimplesSalas");

            migrationBuilder.DropIndex(
                name: "IX_DadoCompostoModVar_DadoCompostoSalaId",
                table: "DadoCompostoModVar");

            migrationBuilder.DropIndex(
                name: "IX_DadoCompostoModFixo_DadoCompostoSalaId",
                table: "DadoCompostoModFixo");

            migrationBuilder.DropColumn(
                name: "ModificadorVariavelId",
                table: "DadosSimplesSalas");

            migrationBuilder.DropColumn(
                name: "DadoCompostoSalaId",
                table: "DadoCompostoModVar");

            migrationBuilder.DropColumn(
                name: "DadoCompostoSalaId",
                table: "DadoCompostoModFixo");

            migrationBuilder.CreateTable(
                name: "DadoCompostoSalaModFixo",
                columns: table => new
                {
                    ConectDadoVarId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ModificadorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadoCompostoSalaModFixo", x => x.ConectDadoVarId);
                    table.ForeignKey(
                        name: "FK_DadoCompostoSalaModFixo_DadosCompostosSalas_ModificadorId",
                        column: x => x.ModificadorId,
                        principalTable: "DadosCompostosSalas",
                        principalColumn: "DadoCompostoSalaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DadoCompostoSalaModFixo_ModificadoresFixos_DadoId",
                        column: x => x.DadoId,
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DadoCompostoSalaModFixo");

            migrationBuilder.DropTable(
                name: "DadoCompostoSalaModVar");

            migrationBuilder.AddColumn<int>(
                name: "ModificadorVariavelId",
                table: "DadosSimplesSalas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DadoCompostoSalaId",
                table: "DadoCompostoModVar",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DadoCompostoSalaId",
                table: "DadoCompostoModFixo",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DadosSimplesSalas_ModificadorVariavelId",
                table: "DadosSimplesSalas",
                column: "ModificadorVariavelId");

            migrationBuilder.CreateIndex(
                name: "IX_DadoCompostoModVar_DadoCompostoSalaId",
                table: "DadoCompostoModVar",
                column: "DadoCompostoSalaId");

            migrationBuilder.CreateIndex(
                name: "IX_DadoCompostoModFixo_DadoCompostoSalaId",
                table: "DadoCompostoModFixo",
                column: "DadoCompostoSalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DadoCompostoModFixo_DadosCompostosSalas_DadoCompostoSalaId",
                table: "DadoCompostoModFixo",
                column: "DadoCompostoSalaId",
                principalTable: "DadosCompostosSalas",
                principalColumn: "DadoCompostoSalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DadoCompostoModVar_DadosCompostosSalas_DadoCompostoSalaId",
                table: "DadoCompostoModVar",
                column: "DadoCompostoSalaId",
                principalTable: "DadosCompostosSalas",
                principalColumn: "DadoCompostoSalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DadosSimplesSalas_ModificadoresVariaveis_ModificadorVariavelId",
                table: "DadosSimplesSalas",
                column: "ModificadorVariavelId",
                principalTable: "ModificadoresVariaveis",
                principalColumn: "ModificadorVariavelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
