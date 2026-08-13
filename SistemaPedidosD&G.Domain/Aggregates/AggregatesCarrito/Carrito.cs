using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaPedidosD_G.Domain.Aggregates.AggregatesCarrito
{
    public class Carrito
    {
        public string ClienteId { get; private set; } = null!;
        public List<ItemCarrito> Items { get; private set; } = new();

        private Carrito() { } // Requerido por EF Core

        public Carrito(string clienteId)
        {
            ClienteId = clienteId;
        }

        public void AgregarItem(Guid productoId, string nombreProducto, int cantidad, decimal precioUnitario)
        {
            var itemExistente = Items.FirstOrDefault(i => i.ProductoId == productoId);

            if (itemExistente != null)
                itemExistente.ModificarCantidad(itemExistente.Cantidad + cantidad);
            else
                Items.Add(new ItemCarrito(productoId, nombreProducto, cantidad, precioUnitario));
        }

        public void ModificarCantidadItem(Guid productoId, int nuevaCantidad)
        {
            var item = Items.FirstOrDefault(i => i.ProductoId == productoId);
            if (item == null)
                throw new Exception("El producto no está en el carrito.");

            item.ModificarCantidad(nuevaCantidad);
        }

        public void EliminarItem(Guid productoId)
        {
            var item = Items.FirstOrDefault(i => i.ProductoId == productoId);
            if (item == null)
                throw new Exception("El producto no está en el carrito.");

            Items.Remove(item);
        }

        public void Vaciar() => Items.Clear();

        public decimal ObtenerTotal() => Items.Sum(i => i.ObtenerSubtotal());

        public bool EstaVacio() => !Items.Any();

    }
}
