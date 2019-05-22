using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class FinalSprint214 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visas_Regiones_RegionID",
                table: "Visas");

            migrationBuilder.DropIndex(
                name: "IX_Visas_RegionID",
                table: "Visas");

            migrationBuilder.DropColumn(
                name: "RegionID",
                table: "Visas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegionID",
                table: "Visas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visas_RegionID",
                table: "Visas",
                column: "RegionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Visas_Regiones_RegionID",
                table: "Visas",
                column: "RegionID",
                principalTable: "Regiones",
                principalColumn: "RegionID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
