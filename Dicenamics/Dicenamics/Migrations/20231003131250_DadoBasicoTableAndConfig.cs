using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dicenamics.Migrations
{
    public partial class DadoBasicoTableAndConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DadoBasico_Salas_SalaId",
                table: "DadoBasico");

            migrationBuilder.RenameTable(
                name: "DadoBasico",
                newName: "DadosBasicos");

            migrationBuilder.RenameIndex(
                name: "IX_DadoBasico_SalaId",
                table: "DadosBasicos",
                newName: "IX_DadosBasicos_SalaId");

            migrationBuilder.AlterColumn<int>(
                name: "Faces",
                table: "DadosBasicos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DadosBasicos",
                table: "DadosBasicos",
                column: "Faces");

            migrationBuilder.CreateTable(
                name: "ModificadoresVariaveis",
                columns: table => new
                {
                    ModificadorVariavelId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    DadoFaces = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModificadoresVariaveis", x => x.ModificadorVariavelId);
                    table.ForeignKey(
                        name: "FK_ModificadoresVariaveis_DadosBasicos_DadoFaces",
                        column: x => x.DadoFaces,
                        principalTable: "DadosBasicos",
                        principalColumn: "Faces");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModificadoresVariaveis_DadoFaces",
                table: "ModificadoresVariaveis",
                column: "DadoFaces");

            migrationBuilder.AddForeignKey(
                name: "FK_DadosBasicos_Salas_SalaId",
                table: "DadosBasicos",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "SalaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DadosBasicos_Salas_SalaId",
                table: "DadosBasicos");

            migrationBuilder.DropTable(
                name: "ModificadoresVariaveis");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DadosBasicos",
                table: "DadosBasicos");

            migrationBuilder.RenameTable(
                name: "DadosBasicos",
                newName: "DadoBasico");

            migrationBuilder.RenameIndex(
                name: "IX_DadosBasicos_SalaId",
                table: "DadoBasico",
                newName: "IX_DadoBasico_SalaId");

            migrationBuilder.AlterColumn<int>(
                name: "Faces",
                table: "DadoBasico",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "FK_DadoBasico_Salas_SalaId",
                table: "DadoBasico",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "SalaId");
        }
    }
}
