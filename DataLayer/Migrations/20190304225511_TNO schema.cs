using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class TNOschema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(nullable: true),
                    FirstLastName = table.Column<string>(nullable: true),
                    SecondLastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Permission = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioID);
                });

            migrationBuilder.CreateTable(
                name: "Visa",
                columns: table => new
                {
                    VisaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visa", x => x.VisaID);
                });

            migrationBuilder.CreateTable(
                name: "Pasaporte",
                columns: table => new
                {
                    PasaporteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UsuarioCI = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaVencimiento = table.Column<DateTime>(nullable: false),
                    Actualizaciones = table.Column<int>(nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    UsuarioID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pasaporte", x => x.PasaporteID);
                    table.ForeignKey(
                        name: "FK_Pasaporte_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Viaje",
                columns: table => new
                {
                    ViajeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MotivoViaje = table.Column<int>(nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: false),
                    Costo = table.Column<int>(nullable: false),
                    UsuarioID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viaje", x => x.ViajeID);
                    table.ForeignKey(
                        name: "FK_Viaje_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pasaporte_Visa",
                columns: table => new
                {
                    Pasaporte_VisaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PasaporteID = table.Column<int>(nullable: true),
                    VisaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pasaporte_Visa", x => x.Pasaporte_VisaID);
                    table.ForeignKey(
                        name: "FK_Pasaporte_Visa_Pasaporte_PasaporteID",
                        column: x => x.PasaporteID,
                        principalTable: "Pasaporte",
                        principalColumn: "PasaporteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pasaporte_Visa_Visa_VisaID",
                        column: x => x.VisaID,
                        principalTable: "Visa",
                        principalColumn: "VisaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Instituciones",
                columns: table => new
                {
                    InstitucionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    ViajeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituciones", x => x.InstitucionID);
                    table.ForeignKey(
                        name: "FK_Instituciones_Viaje_ViajeID",
                        column: x => x.ViajeID,
                        principalTable: "Viaje",
                        principalColumn: "ViajeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    PaisID = table.Column<string>(nullable: false),
                    ViajeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.PaisID);
                    table.ForeignKey(
                        name: "FK_Pais_Viaje_ViajeID",
                        column: x => x.ViajeID,
                        principalTable: "Viaje",
                        principalColumn: "ViajeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ciudades",
                columns: table => new
                {
                    CiudadID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    PaisID = table.Column<string>(nullable: true),
                    ViajeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudades", x => x.CiudadID);
                    table.ForeignKey(
                        name: "FK_Ciudades_Pais_PaisID",
                        column: x => x.PaisID,
                        principalTable: "Pais",
                        principalColumn: "PaisID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ciudades_Viaje_ViajeID",
                        column: x => x.ViajeID,
                        principalTable: "Viaje",
                        principalColumn: "ViajeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pais_Visa",
                columns: table => new
                {
                    Pais_VisaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PaisID = table.Column<string>(nullable: true),
                    ViajeID = table.Column<int>(nullable: true),
                    VisaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais_Visa", x => x.Pais_VisaID);
                    table.ForeignKey(
                        name: "FK_Pais_Visa_Pais_PaisID",
                        column: x => x.PaisID,
                        principalTable: "Pais",
                        principalColumn: "PaisID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pais_Visa_Viaje_ViajeID",
                        column: x => x.ViajeID,
                        principalTable: "Viaje",
                        principalColumn: "ViajeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pais_Visa_Visa_VisaID",
                        column: x => x.VisaID,
                        principalTable: "Visa",
                        principalColumn: "VisaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ciudades_PaisID",
                table: "Ciudades",
                column: "PaisID");

            migrationBuilder.CreateIndex(
                name: "IX_Ciudades_ViajeID",
                table: "Ciudades",
                column: "ViajeID");

            migrationBuilder.CreateIndex(
                name: "IX_Instituciones_ViajeID",
                table: "Instituciones",
                column: "ViajeID");

            migrationBuilder.CreateIndex(
                name: "IX_Pais_ViajeID",
                table: "Pais",
                column: "ViajeID");

            migrationBuilder.CreateIndex(
                name: "IX_Pais_Visa_PaisID",
                table: "Pais_Visa",
                column: "PaisID");

            migrationBuilder.CreateIndex(
                name: "IX_Pais_Visa_ViajeID",
                table: "Pais_Visa",
                column: "ViajeID");

            migrationBuilder.CreateIndex(
                name: "IX_Pais_Visa_VisaID",
                table: "Pais_Visa",
                column: "VisaID");

            migrationBuilder.CreateIndex(
                name: "IX_Pasaporte_UsuarioID",
                table: "Pasaporte",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Pasaporte_Visa_PasaporteID",
                table: "Pasaporte_Visa",
                column: "PasaporteID");

            migrationBuilder.CreateIndex(
                name: "IX_Pasaporte_Visa_VisaID",
                table: "Pasaporte_Visa",
                column: "VisaID");

            migrationBuilder.CreateIndex(
                name: "IX_Viaje_UsuarioID",
                table: "Viaje",
                column: "UsuarioID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ciudades");

            migrationBuilder.DropTable(
                name: "Instituciones");

            migrationBuilder.DropTable(
                name: "Pais_Visa");

            migrationBuilder.DropTable(
                name: "Pasaporte_Visa");

            migrationBuilder.DropTable(
                name: "Pais");

            migrationBuilder.DropTable(
                name: "Pasaporte");

            migrationBuilder.DropTable(
                name: "Visa");

            migrationBuilder.DropTable(
                name: "Viaje");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
