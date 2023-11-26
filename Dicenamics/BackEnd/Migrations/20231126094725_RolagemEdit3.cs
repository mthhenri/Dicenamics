using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    public partial class RolagemEdit3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RolagemDadoSalaId",
                table: "RolagensDadosSalas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "Resultados",
                table: "RolagensDadosSalas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolagensDadosSalas",
                table: "RolagensDadosSalas",
                column: "RolagemDadoSalaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RolagensDadosSalas",
                table: "RolagensDadosSalas");

            migrationBuilder.DropColumn(
                name: "RolagemDadoSalaId",
                table: "RolagensDadosSalas");

            migrationBuilder.DropColumn(
                name: "Resultados",
                table: "RolagensDadosSalas");
        }
    }
}
