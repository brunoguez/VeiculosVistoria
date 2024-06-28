using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VistoriaAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Placa = table.Column<string>(type: "TEXT", nullable: true),
                    Chassi = table.Column<string>(type: "TEXT", nullable: true),
                    Motor = table.Column<string>(type: "TEXT", nullable: true),
                    Ano_Fabricacao = table.Column<int>(type: "INTEGER", nullable: false),
                    Ano_Modelo = table.Column<int>(type: "INTEGER", nullable: false),
                    Marca = table.Column<string>(type: "TEXT", nullable: true),
                    Linha = table.Column<string>(type: "TEXT", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Potencia = table.Column<double>(type: "REAL", nullable: true),
                    Observacoes = table.Column<string>(type: "TEXT", nullable: true),
                    DataIntegracao = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_Placa_Chassi_Motor",
                table: "Veiculos",
                columns: new[] { "Placa", "Chassi", "Motor" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Veiculos");
        }
    }
}
