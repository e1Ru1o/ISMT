using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class FinalSprint220 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visas_AspNetUsers_UsuarioId",
                table: "Visas");

            migrationBuilder.DropIndex(
                name: "IX_Visas_UsuarioId",
                table: "Visas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Visas");

            migrationBuilder.CreateTable(
                name: "Usuario_Visa",
                columns: table => new
                {
                    Usuario_VisaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<string>(nullable: true),
                    VisaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario_Visa", x => x.Usuario_VisaID);
                    table.ForeignKey(
                        name: "FK_Usuario_Visa_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuario_Visa_Visas_VisaID",
                        column: x => x.VisaID,
                        principalTable: "Visas",
                        principalColumn: "VisaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Visa_UsuarioId",
                table: "Usuario_Visa",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Visa_VisaID",
                table: "Usuario_Visa",
                column: "VisaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario_Visa");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Visas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visas_UsuarioId",
                table: "Visas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visas_AspNetUsers_UsuarioId",
                table: "Visas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
