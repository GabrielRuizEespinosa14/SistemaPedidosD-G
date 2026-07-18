using SistemaPedidosD_G.Application.Contracts.Services;
using SistemaPedidosD_G.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SistemaPedidosD_G.Application.Services
{
    public class CarritoService : ICarritoService
    {
        // Diccionario en memoria: ClienteId → Carrito
        private readonly Dictionary<string, Carrito> _carritos = new();

        public Carrito ObtenerCarrito(string clienteId)
        {
            if (!_carritos.ContainsKey(clienteId))
                _carritos[clienteId] = new Carrito(clienteId);

            return _carritos[clienteId];
        }

        public void AgregarItem(string clienteId, Guid productoId, string nombreProducto, int cantidad, decimal precioUnitario)
        {
            var carrito = ObtenerCarrito(clienteId);
            carrito.AgregarItem(productoId, nombreProducto, cantidad, precioUnitario);
        }

        public void ModificarCantidad(string clienteId, Guid productoId, int nuevaCantidad)
        {
            var carrito = ObtenerCarrito(clienteId);
            carrito.ModificarCantidadItem(productoId, nuevaCantidad);
        }

        public void EliminarItem(string clienteId, Guid productoId)
        {
            var carrito = ObtenerCarrito(clienteId);
            carrito.EliminarItem(productoId);
        }

        public void Vaciar(string clienteId)
        {
            if (_carritos.ContainsKey(clienteId))
                _carritos[clienteId].Vaciar();
        }
    }
}