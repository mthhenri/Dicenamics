using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dicenamics.Migrations
{
    public partial class DadoSimplesAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DadosSimples",
                columns: table => new
                {
                    DadoSimplesId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Faces = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosSimples", x => x.DadoSimplesId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DadosSimples");
        }
    }
}
