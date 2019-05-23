using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class FinalSprint230 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itinerarios_AspNetUsers_UsuarioID",
                table: "Itinerarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Viajes_Ciudades_CiudadID",
                table: "Viajes");

            migrationBuilder.DropTable(
                name: "Ciudades");

            migrationBuilder.DropIndex(
                name: "IX_Viajes_CiudadID",
                table: "Viajes");

            migrationBuilder.DropColumn(
                name: "CiudadID",
                table: "Viajes");

            migrationBuilder.RenameColumn(
                name: "UsuarioID",
                table: "Itinerarios",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Itinerarios_UsuarioID",
                table: "Itinerarios",
                newName: "IX_Itinerarios_UsuarioId");

            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "Viajes",
                nullable: true);

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

            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "Viajes");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Itinerarios",
                newName: "UsuarioID");

            migrationBuilder.RenameIndex(
                name: "IX_Itinerarios_UsuarioId",
                table: "Itinerarios",
                newName: "IX_Itinerarios_UsuarioID");

            migrationBuilder.AddColumn<int>(
                name: "CiudadID",
                table: "Viajes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ciudades",
                columns: table => new
                {
                    CiudadID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    PaisID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudades", x => x.CiudadID);
                    table.ForeignKey(
                        name: "FK_Ciudades_Paises_PaisID",
                        column: x => x.PaisID,
                        principalTable: "Paises",
                        principalColumn: "PaisID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Viajes_CiudadID",
                table: "Viajes",
                column: "CiudadID");

            migrationBuilder.CreateIndex(
                name: "IX_Ciudades_PaisID",
                table: "Ciudades",
                column: "PaisID");

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerarios_AspNetUsers_UsuarioID",
                table: "Itinerarios",
                column: "UsuarioID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Viajes_Ciudades_CiudadID",
                table: "Viajes",
                column: "CiudadID",
                principalTable: "Ciudades",
                principalColumn: "CiudadID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
