using SistemaPedidosD_G.Domain.Aggregates.AggregatesCarrito;
using SistemaPedidosD_G.Domain.Aggregates;
using System;

namespace SistemaPedidosD_G.Application.Contracts.Services
{
    public interface ICarritoService
    {
        Carrito ObtenerCarrito(string clienteId);
        void AgregarItem(string clienteId, Guid productoId, string nombreProducto, int cantidad, decimal precioUnitario);
        void ModificarCantidad(string clienteId, Guid productoId, int nuevaCantidad);
        void EliminarItem(string clienteId, Guid productoId);
        void Vaciar(string clienteId);
    }
}
