using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dicenamics.Migrations
{
    public partial class NewModelsDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AcessoSalaDadosAcessoSalaId",
                table: "DadosSimples",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AcessoUsuarioDadosAcessoUsuarioId",
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

            migrationBuilder.CreateTable(
                name: "AcessosSalasDados",
                columns: table => new
                {
                    AcessoSalaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SalaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcessosSalasDados", x => x.AcessoSalaId);
                    table.ForeignKey(
                        name: "FK_AcessosSalasDados_Salas_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Salas",
                        principalColumn: "SalaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcessosUsuariosDados",
                columns: table => new
                {
                    AcessoUsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcessosUsuariosDados", x => x.AcessoUsuarioId);
                    table.ForeignKey(
                        name: "FK_AcessosUsuariosDados_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DadosCompostos",
                columns: table => new
                {
                    DadoCompostoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AcessoUsuarioDadosAcessoUsuarioId = table.Column<int>(type: "INTEGER", nullable: true),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    DadoCompostoSalaId = table.Column<int>(type: "INTEGER", nullable: true),
                    AcessoSalaDadosAcessoSalaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosCompostos", x => x.DadoCompostoId);
                    table.ForeignKey(
                        name: "FK_DadosCompostos_AcessosSalasDados_AcessoSalaDadosAcessoSalaId",
                        column: x => x.AcessoSalaDadosAcessoSalaId,
                        principalTable: "AcessosSalasDados",
                        principalColumn: "AcessoSalaId");
                    table.ForeignKey(
                        name: "FK_DadosCompostos_AcessosUsuariosDados_AcessoUsuarioDadosAcessoUsuarioId",
                        column: x => x.AcessoUsuarioDadosAcessoUsuarioId,
                        principalTable: "AcessosUsuariosDados",
                        principalColumn: "AcessoUsuarioId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DadosSimples_AcessoSalaDadosAcessoSalaId",
                table: "DadosSimples",
                column: "AcessoSalaDadosAcessoSalaId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosSimples_AcessoUsuarioDadosAcessoUsuarioId",
                table: "DadosSimples",
                column: "AcessoUsuarioDadosAcessoUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_AcessosSalasDados_SalaId",
                table: "AcessosSalasDados",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_AcessosUsuariosDados_UsuarioId",
                table: "AcessosUsuariosDados",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosCompostos_AcessoSalaDadosAcessoSalaId",
                table: "DadosCompostos",
                column: "AcessoSalaDadosAcessoSalaId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosCompostos_AcessoUsuarioDadosAcessoUsuarioId",
                table: "DadosCompostos",
                column: "AcessoUsuarioDadosAcessoUsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_DadosSimples_AcessosSalasDados_AcessoSalaDadosAcessoSalaId",
                table: "DadosSimples",
                column: "AcessoSalaDadosAcessoSalaId",
                principalTable: "AcessosSalasDados",
                principalColumn: "AcessoSalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DadosSimples_AcessosUsuariosDados_AcessoUsuarioDadosAcessoUsuarioId",
                table: "DadosSimples",
                column: "AcessoUsuarioDadosAcessoUsuarioId",
                principalTable: "AcessosUsuariosDados",
                principalColumn: "AcessoUsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DadosSimples_AcessosSalasDados_AcessoSalaDadosAcessoSalaId",
                table: "DadosSimples");

            migrationBuilder.DropForeignKey(
                name: "FK_DadosSimples_AcessosUsuariosDados_AcessoUsuarioDadosAcessoUsuarioId",
                table: "DadosSimples");

            migrationBuilder.DropTable(
                name: "DadosCompostos");

            migrationBuilder.DropTable(
                name: "AcessosSalasDados");

            migrationBuilder.DropTable(
                name: "AcessosUsuariosDados");

            migrationBuilder.DropIndex(
                name: "IX_DadosSimples_AcessoSalaDadosAcessoSalaId",
                table: "DadosSimples");

            migrationBuilder.DropIndex(
                name: "IX_DadosSimples_AcessoUsuarioDadosAcessoUsuarioId",
                table: "DadosSimples");

            migrationBuilder.DropColumn(
                name: "AcessoSalaDadosAcessoSalaId",
                table: "DadosSimples");

            migrationBuilder.DropColumn(
                name: "AcessoUsuarioDadosAcessoUsuarioId",
                table: "DadosSimples");

            migrationBuilder.DropColumn(
                name: "DadoSimplesSalaId",
                table: "DadosSimples");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "DadosSimples");
        }
    }
}
