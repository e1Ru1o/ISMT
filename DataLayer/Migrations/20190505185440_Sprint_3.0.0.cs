using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class Sprint_300 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(nullable: true),
                    FirstLastName = table.Column<string>(nullable: true),
                    SecondLastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadosViaje",
                columns: table => new
                {
                    EstadoViajeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosViaje", x => x.EstadoViajeID);
                });

            migrationBuilder.CreateTable(
                name: "Responsabilidades",
                columns: table => new
                {
                    ResponsabilidadID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsabilidades", x => x.ResponsabilidadID);
                });

            migrationBuilder.CreateTable(
                name: "Visas",
                columns: table => new
                {
                    VisaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visas", x => x.VisaID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pasaportes",
                columns: table => new
                {
                    PasaporteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CodigoPasaporte = table.Column<string>(nullable: true),
                    UsuarioCI = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaVencimiento = table.Column<DateTime>(nullable: false),
                    Actualizaciones = table.Column<int>(nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pasaportes", x => x.PasaporteID);
                    table.ForeignKey(
                        name: "FK_Pasaportes_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Viajes",
                columns: table => new
                {
                    ViajeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MotivoViaje = table.Column<int>(nullable: true),
                    FechaInicio = table.Column<DateTime>(nullable: true),
                    FechaFin = table.Column<DateTime>(nullable: true),
                    Costo = table.Column<int>(nullable: true),
                    EstadoViajeID = table.Column<int>(nullable: true),
                    UsuarioId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viajes", x => x.ViajeID);
                    table.ForeignKey(
                        name: "FK_Viajes_EstadosViaje_EstadoViajeID",
                        column: x => x.EstadoViajeID,
                        principalTable: "EstadosViaje",
                        principalColumn: "EstadoViajeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Viajes_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios_Responsabilidades",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<string>(nullable: false),
                    ResponsabilidadID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios_Responsabilidades", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Usuarios_Responsabilidades_Responsabilidades_ResponsabilidadID",
                        column: x => x.ResponsabilidadID,
                        principalTable: "Responsabilidades",
                        principalColumn: "ResponsabilidadID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_Responsabilidades_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workflow",
                columns: table => new
                {
                    WorkflowID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EstadoViajeOrigenEstadoViajeID = table.Column<int>(nullable: true),
                    EstadoViajeDestinoEstadoViajeID = table.Column<int>(nullable: true),
                    ResponsabilidadID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workflow", x => x.WorkflowID);
                    table.ForeignKey(
                        name: "FK_Workflow_EstadosViaje_EstadoViajeDestinoEstadoViajeID",
                        column: x => x.EstadoViajeDestinoEstadoViajeID,
                        principalTable: "EstadosViaje",
                        principalColumn: "EstadoViajeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Workflow_EstadosViaje_EstadoViajeOrigenEstadoViajeID",
                        column: x => x.EstadoViajeOrigenEstadoViajeID,
                        principalTable: "EstadosViaje",
                        principalColumn: "EstadoViajeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Workflow_Responsabilidades_ResponsabilidadID",
                        column: x => x.ResponsabilidadID,
                        principalTable: "Responsabilidades",
                        principalColumn: "ResponsabilidadID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pasaportes_Visas",
                columns: table => new
                {
                    Pasaporte_VisaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PasaporteID = table.Column<int>(nullable: true),
                    VisaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pasaportes_Visas", x => x.Pasaporte_VisaID);
                    table.ForeignKey(
                        name: "FK_Pasaportes_Visas_Pasaportes_PasaporteID",
                        column: x => x.PasaporteID,
                        principalTable: "Pasaportes",
                        principalColumn: "PasaporteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pasaportes_Visas_Visas_VisaID",
                        column: x => x.VisaID,
                        principalTable: "Visas",
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
                        name: "FK_Instituciones_Viajes_ViajeID",
                        column: x => x.ViajeID,
                        principalTable: "Viajes",
                        principalColumn: "ViajeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    PaisID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    ViajeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.PaisID);
                    table.ForeignKey(
                        name: "FK_Paises_Viajes_ViajeID",
                        column: x => x.ViajeID,
                        principalTable: "Viajes",
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
                    PaisID = table.Column<int>(nullable: true),
                    ViajeID = table.Column<int>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Ciudades_Viajes_ViajeID",
                        column: x => x.ViajeID,
                        principalTable: "Viajes",
                        principalColumn: "ViajeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Paises_Visas",
                columns: table => new
                {
                    Pais_VisaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PaisID = table.Column<int>(nullable: false),
                    VisaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises_Visas", x => x.Pais_VisaID);
                    table.ForeignKey(
                        name: "FK_Paises_Visas_Paises_PaisID",
                        column: x => x.PaisID,
                        principalTable: "Paises",
                        principalColumn: "PaisID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Paises_Visas_Visas_VisaID",
                        column: x => x.VisaID,
                        principalTable: "Visas",
                        principalColumn: "VisaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "IX_Paises_ViajeID",
                table: "Paises",
                column: "ViajeID");

            migrationBuilder.CreateIndex(
                name: "IX_Paises_Visas_PaisID",
                table: "Paises_Visas",
                column: "PaisID");

            migrationBuilder.CreateIndex(
                name: "IX_Paises_Visas_VisaID",
                table: "Paises_Visas",
                column: "VisaID");

            migrationBuilder.CreateIndex(
                name: "IX_Pasaportes_UsuarioId",
                table: "Pasaportes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Pasaportes_Visas_PasaporteID",
                table: "Pasaportes_Visas",
                column: "PasaporteID");

            migrationBuilder.CreateIndex(
                name: "IX_Pasaportes_Visas_VisaID",
                table: "Pasaportes_Visas",
                column: "VisaID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Responsabilidades_ResponsabilidadID",
                table: "Usuarios_Responsabilidades",
                column: "ResponsabilidadID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Responsabilidades_UsuarioId",
                table: "Usuarios_Responsabilidades",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Viajes_EstadoViajeID",
                table: "Viajes",
                column: "EstadoViajeID");

            migrationBuilder.CreateIndex(
                name: "IX_Viajes_UsuarioId",
                table: "Viajes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Workflow_EstadoViajeDestinoEstadoViajeID",
                table: "Workflow",
                column: "EstadoViajeDestinoEstadoViajeID");

            migrationBuilder.CreateIndex(
                name: "IX_Workflow_EstadoViajeOrigenEstadoViajeID",
                table: "Workflow",
                column: "EstadoViajeOrigenEstadoViajeID");

            migrationBuilder.CreateIndex(
                name: "IX_Workflow_ResponsabilidadID",
                table: "Workflow",
                column: "ResponsabilidadID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Ciudades");

            migrationBuilder.DropTable(
                name: "Instituciones");

            migrationBuilder.DropTable(
                name: "Paises_Visas");

            migrationBuilder.DropTable(
                name: "Pasaportes_Visas");

            migrationBuilder.DropTable(
                name: "Usuarios_Responsabilidades");

            migrationBuilder.DropTable(
                name: "Workflow");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Paises");

            migrationBuilder.DropTable(
                name: "Pasaportes");

            migrationBuilder.DropTable(
                name: "Visas");

            migrationBuilder.DropTable(
                name: "Responsabilidades");

            migrationBuilder.DropTable(
                name: "Viajes");

            migrationBuilder.DropTable(
                name: "EstadosViaje");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
