using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaParcial2_JoselineRuiz.Migrations
{
    /// <inheritdoc />
    public partial class cuartaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TiempoEstimado",
                table: "Tarea");

            migrationBuilder.AddColumn<decimal>(
                name: "TiempoEstimadoHoras",
                table: "Tarea",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TiempoEstimadoHoras",
                table: "Tarea");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "TiempoEstimado",
                table: "Tarea",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }
    }
}
