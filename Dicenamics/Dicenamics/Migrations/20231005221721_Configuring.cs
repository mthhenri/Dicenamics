using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dicenamics.Migrations
{
    public partial class Configuring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DadosCompostos_Salas_SalaId",
                table: "DadosCompostos");

            migrationBuilder.DropForeignKey(
                name: "FK_DadosCompostos_Usuarios_CriadorId",
                table: "DadosCompostos");

            migrationBuilder.DropForeignKey(
                name: "FK_DadosSimples_Salas_SalaId",
                table: "DadosSimples");

            migrationBuilder.DropForeignKey(
                name: "FK_DadosSimples_Usuarios_CriadorId",
                table: "DadosSimples");

            migrationBuilder.DropIndex(
                name: "IX_DadosSimples_CriadorId",
                table: "DadosSimples");

            migrationBuilder.DropIndex(
                name: "IX_DadosSimples_SalaId",
                table: "DadosSimples");

            migrationBuilder.DropIndex(
                name: "IX_DadosCompostos_CriadorId",
                table: "DadosCompostos");

            migrationBuilder.DropIndex(
                name: "IX_DadosCompostos_SalaId",
                table: "DadosCompostos");

            migrationBuilder.DropColumn(
                name: "AcessoPrivado",
                table: "DadosSimples");

            migrationBuilder.DropColumn(
                name: "CriadorId",
                table: "DadosSimples");

            migrationBuilder.DropColumn(
                name: "DadoSimplesSalaId",
                table: "DadosSimples");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "DadosSimples");

            migrationBuilder.DropColumn(
                name: "SalaId",
                table: "DadosSimples");

            migrationBuilder.DropColumn(
                name: "AcessoPrivado",
                table: "DadosCompostos");

            migrationBuilder.DropColumn(
                name: "CriadorId",
                table: "DadosCompostos");

            migrationBuilder.DropColumn(
                name: "DadoCompostoSalaId",
                table: "DadosCompostos");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "DadosCompostos");

            migrationBuilder.DropColumn(
                name: "SalaId",
                table: "DadosCompostos");

            migrationBuilder.CreateTable(
                name: "DadosCompostosSalas",
                columns: table => new
                {
                    DadoCompostoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DadoCompostoSalaId = table.Column<int>(type: "INTEGER", nullable: false),
                    AcessoPrivado = table.Column<bool>(type: "INTEGER", nullable: false),
                    CriadorId = table.Column<int>(type: "INTEGER", nullable: false),
                    SalaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosCompostosSalas", x => x.DadoCompostoId);
                    table.ForeignKey(
                        name: "FK_DadosCompostosSalas_DadosCompostos_DadoCompostoId",
                        column: x => x.DadoCompostoId,
                        principalTable: "DadosCompostos",
                        principalColumn: "DadoCompostoId",
                        onDelete: ReferentialAction.Cascade);
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
                    DadoSimplesId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DadoSimplesSalaId = table.Column<int>(type: "INTEGER", nullable: false),
                    AcessoPrivado = table.Column<bool>(type: "INTEGER", nullable: false),
                    CriadorId = table.Column<int>(type: "INTEGER", nullable: false),
                    SalaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosSimplesSalas", x => x.DadoSimplesId);
                    table.ForeignKey(
                        name: "FK_DadosSimplesSalas_DadosSimples_DadoSimplesId",
                        column: x => x.DadoSimplesId,
                        principalTable: "DadosSimples",
                        principalColumn: "DadoSimplesId",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_DadosCompostosSalas_CriadorId",
                table: "DadosCompostosSalas",
                column: "CriadorId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosCompostosSalas_SalaId",
                table: "DadosCompostosSalas",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosSimplesSalas_CriadorId",
                table: "DadosSimplesSalas",
                column: "CriadorId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosSimplesSalas_SalaId",
                table: "DadosSimplesSalas",
                column: "SalaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DadosCompostosSalas");

            migrationBuilder.DropTable(
                name: "DadosSimplesSalas");

            migrationBuilder.AddColumn<bool>(
                name: "AcessoPrivado",
                table: "DadosSimples",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CriadorId",
                table: "DadosSimples",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DadoSimplesSalaId",
                table: "DadosSimples",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "DadosSimples",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SalaId",
                table: "DadosSimples",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AcessoPrivado",
                table: "DadosCompostos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CriadorId",
                table: "DadosCompostos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DadoCompostoSalaId",
                table: "DadosCompostos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "DadosCompostos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SalaId",
                table: "DadosCompostos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DadosSimples_CriadorId",
                table: "DadosSimples",
                column: "CriadorId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosSimples_SalaId",
                table: "DadosSimples",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosCompostos_CriadorId",
                table: "DadosCompostos",
                column: "CriadorId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosCompostos_SalaId",
                table: "DadosCompostos",
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
        }
    }
}
