using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class PermissionRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario_Permiso");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.AddColumn<int>(
                name: "Permission",
                table: "Usuarios",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permission",
                table: "Usuarios");

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    PermisoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.PermisoID);
                });

            migrationBuilder.CreateTable(
                name: "Usuario_Permiso",
                columns: table => new
                {
                    Usuario_PermisoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PermisoID = table.Column<int>(nullable: false),
                    UsuarioID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario_Permiso", x => x.Usuario_PermisoID);
                    table.ForeignKey(
                        name: "FK_Usuario_Permiso_Permisos_PermisoID",
                        column: x => x.PermisoID,
                        principalTable: "Permisos",
                        principalColumn: "PermisoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuario_Permiso_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Permiso_PermisoID",
                table: "Usuario_Permiso",
                column: "PermisoID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Permiso_UsuarioID",
                table: "Usuario_Permiso",
                column: "UsuarioID");
        }
    }
}
