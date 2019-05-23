using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class FinalSprint211 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itinerarios_AspNetUsers_UsuarioId",
                table: "Itinerarios");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Itinerarios",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerarios_AspNetUsers_UsuarioId",
                table: "Itinerarios",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itinerarios_AspNetUsers_UsuarioId",
                table: "Itinerarios");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Itinerarios",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerarios_AspNetUsers_UsuarioId",
                table: "Itinerarios",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
