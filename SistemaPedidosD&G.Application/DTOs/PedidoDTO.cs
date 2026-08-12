using SistemaPedidosD_G.Application.DTOs;
using SistemaPedidosD_G.Domain.Enums;

namespace SistemaPedidosD_G.Application.DTO
{
    public class PedidoDTO
    {
        public Guid Id { get; set; }
        public string NumeroPedido { get; set; } = null!;
        public string NombreCliente { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public MetodoEntrega MetodoEntrega { get; set; }
        public string? Direccion { get; set; }
        public EstadoPedido Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal Total { get; set; }
        public List<DetallePedidoDTO> Detalles { get; set; } = new();
        public List<HistorialPedidoDTO> Historial { get; set; } = new();
    }

    public class DetallePedidoDTO
    {
        public Guid ProductoId { get; set; }
        public string NombreProducto { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
