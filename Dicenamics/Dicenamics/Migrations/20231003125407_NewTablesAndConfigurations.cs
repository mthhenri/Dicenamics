using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dicenamics.Migrations
{
    public partial class NewTablesAndConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DadosSimples",
                table: "DadosSimples");

            migrationBuilder.RenameTable(
                name: "DadosSimples",
                newName: "DadoBasico");

            migrationBuilder.AlterColumn<int>(
                name: "Quantidade",
                table: "DadoBasico",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "DadoSimplesId",
                table: "DadoBasico",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "DadoBasico",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SalaId",
                table: "DadoBasico",
                type: "INTEGER",
                nullable: true);

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
                name: "IX_DadoBasico_SalaId",
                table: "DadoBasico",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Salas_UsuarioMestreId",
                table: "Salas",
                column: "UsuarioMestreId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_SalaId",
                table: "Usuarios",
                column: "SalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DadoBasico_Salas_SalaId",
                table: "DadoBasico",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "SalaId");

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
                name: "FK_DadoBasico_Salas_SalaId",
                table: "DadoBasico");

            migrationBuilder.DropForeignKey(
                name: "FK_Salas_Usuarios_UsuarioMestreId",
                table: "Salas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Salas");

            migrationBuilder.DropIndex(
                name: "IX_DadoBasico_SalaId",
                table: "DadoBasico");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "DadoBasico");

            migrationBuilder.DropColumn(
                name: "SalaId",
                table: "DadoBasico");

            migrationBuilder.RenameTable(
                name: "DadoBasico",
                newName: "DadosSimples");

            migrationBuilder.AlterColumn<int>(
                name: "Quantidade",
                table: "DadosSimples",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DadoSimplesId",
                table: "DadosSimples",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DadosSimples",
                table: "DadosSimples",
                column: "DadoSimplesId");
        }
    }
}
