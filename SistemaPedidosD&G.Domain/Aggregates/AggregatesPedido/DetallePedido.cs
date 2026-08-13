using SistemaPedidosD_G.Domain.ValueObjects;

namespace SistemaPedidosD_G.Domain.Aggregates.AggregatesPedido
{
    public class DetallePedido
    {
        public Guid Id { get; private set; }
        public Guid PedidoId { get; private set; }
        public Guid ProductoId { get; private set; }
        public string NombreProducto { get; private set; } = null!;
        public int Cantidad { get; private set; }
        public Dinero PrecioUnitario { get; private set; } = null!;


        private DetallePedido() { } // Requerido por EF Core

        public DetallePedido(Guid pedidoId, Guid productoId, string nombreProducto, int cantidad, decimal precioUnitario)
        {
            Id = Guid.NewGuid();
            PedidoId = pedidoId;
            ProductoId = productoId;
            NombreProducto = nombreProducto;
            Cantidad = cantidad;
            PrecioUnitario = new Dinero(precioUnitario);
        }

        public Dinero ObtenerSubtotal() => PrecioUnitario.Multiplicar(Cantidad);

    }
}
