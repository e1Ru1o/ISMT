using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class FinalSprint29 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "ViajesInvitados",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViajeInvitadoID",
                table: "Historial",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Historial_ViajeInvitadoID",
                table: "Historial",
                column: "ViajeInvitadoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Historial_ViajesInvitados_ViajeInvitadoID",
                table: "Historial",
                column: "ViajeInvitadoID",
                principalTable: "ViajesInvitados",
                principalColumn: "ViajeInvitadoID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Historial_ViajesInvitados_ViajeInvitadoID",
                table: "Historial");

            migrationBuilder.DropIndex(
                name: "IX_Historial_ViajeInvitadoID",
                table: "Historial");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "ViajesInvitados");

            migrationBuilder.DropColumn(
                name: "ViajeInvitadoID",
                table: "Historial");
        }
    }
}
