using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Data.Migrations
{
    public partial class MigracaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarteirasTrabalho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PisPasep = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarteirasTrabalho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarteirasTrabalho_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataAgendamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodigoTuss = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exames_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vacinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NumeroDoses = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ClienteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacinas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClientesVacinas",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    VacinaId = table.Column<int>(type: "int", nullable: false),
                    DataAplicacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientesVacinas", x => new { x.ClienteId, x.VacinaId });
                    table.ForeignKey(
                        name: "FK_ClientesVacinas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientesVacinas_Vacinas_VacinaId",
                        column: x => x.VacinaId,
                        principalTable: "Vacinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarteirasTrabalho_ClienteId",
                table: "CarteirasTrabalho",
                column: "ClienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientesVacinas_VacinaId",
                table: "ClientesVacinas",
                column: "VacinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Exames_ClienteId",
                table: "Exames",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacinas_ClienteId",
                table: "Vacinas",
                column: "ClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarteirasTrabalho");

            migrationBuilder.DropTable(
                name: "ClientesVacinas");

            migrationBuilder.DropTable(
                name: "Exames");

            migrationBuilder.DropTable(
                name: "Vacinas");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
