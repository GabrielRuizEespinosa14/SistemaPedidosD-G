using System;
using Microsoft.EntityFrameworkCore.Migrations;
using SistemaPedidosD_G.Domain.Aggregates;


#nullable disable

namespace SistemaPedidosD_G.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarFechaDesactivacionProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaDesactivacion",
                table: "Productos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCancelacion",
                table: "Pedidos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HistorialPedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PedidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialPedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorialPedidos_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistorialPedidos_PedidoId",
                table: "HistorialPedidos",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistorialPedidos");

            migrationBuilder.DropColumn(
                name: "FechaDesactivacion",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "FechaCancelacion",
                table: "Pedidos");
        }
    }
}
