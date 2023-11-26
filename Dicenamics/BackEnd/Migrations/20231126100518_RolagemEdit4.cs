using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    public partial class RolagemEdit4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolagensDadosSalas_Salas_SalaId",
                table: "RolagensDadosSalas");

            migrationBuilder.AlterColumn<int>(
                name: "SalaId",
                table: "RolagensDadosSalas",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "DadoId",
                table: "RolagensDadosSalas",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_RolagensDadosSalas_Salas_SalaId",
                table: "RolagensDadosSalas",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "SalaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolagensDadosSalas_Salas_SalaId",
                table: "RolagensDadosSalas");

            migrationBuilder.AlterColumn<int>(
                name: "SalaId",
                table: "RolagensDadosSalas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DadoId",
                table: "RolagensDadosSalas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RolagensDadosSalas_Salas_SalaId",
                table: "RolagensDadosSalas",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "SalaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
