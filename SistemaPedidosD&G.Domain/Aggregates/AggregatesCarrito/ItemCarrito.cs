using System;

namespace SistemaPedidosD_G.Domain.Aggregates.AggregatesCarrito
{
    public class ItemCarrito
    {
        public Guid ProductoId { get; private set; }
        public string NombreProducto { get; private set; } = null!;
        public int Cantidad { get; private set; }
        public decimal PrecioUnitario { get; private set; }


        private ItemCarrito() { } // Requerido por EF Core

        public ItemCarrito(Guid productoId, string nombreProducto, int cantidad, decimal precioUnitario)
        {
            ProductoId = productoId;
            NombreProducto = nombreProducto;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }

        public void ModificarCantidad(int nuevaCantidad)
        {
            if (nuevaCantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero.");
            Cantidad = nuevaCantidad;
        }

        public decimal ObtenerSubtotal() => Cantidad * PrecioUnitario;

    }
}
