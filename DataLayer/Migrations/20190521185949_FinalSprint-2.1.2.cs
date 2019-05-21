using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class FinalSprint212 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itinerarios_AspNetUsers_UsuarioId",
                table: "Itinerarios");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Itinerarios",
                newName: "UsuarioID");

            migrationBuilder.RenameIndex(
                name: "IX_Itinerarios_UsuarioId",
                table: "Itinerarios",
                newName: "IX_Itinerarios_UsuarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerarios_AspNetUsers_UsuarioID",
                table: "Itinerarios",
                column: "UsuarioID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itinerarios_AspNetUsers_UsuarioID",
                table: "Itinerarios");

            migrationBuilder.RenameColumn(
                name: "UsuarioID",
                table: "Itinerarios",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Itinerarios_UsuarioID",
                table: "Itinerarios",
                newName: "IX_Itinerarios_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerarios_AspNetUsers_UsuarioId",
                table: "Itinerarios",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
