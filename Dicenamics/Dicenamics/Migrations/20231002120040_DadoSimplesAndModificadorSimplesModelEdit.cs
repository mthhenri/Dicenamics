using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dicenamics.Migrations
{
    public partial class DadoSimplesAndModificadorSimplesModelEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Modificadores");

            migrationBuilder.AddColumn<string>(
                name: "Condicao",
                table: "DadosSimples",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "DadosSimples",
                type: "TEXT",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModificadoresFixos");

            migrationBuilder.DropColumn(
                name: "Condicao",
                table: "DadosSimples");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "DadosSimples");

            migrationBuilder.CreateTable(
                name: "Modificadores",
                columns: table => new
                {
                    ModificadorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Valor = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modificadores", x => x.ModificadorId);
                });
        }
    }
}
