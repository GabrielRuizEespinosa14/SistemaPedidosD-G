using System;
using System.Threading.Tasks;
using SistemaPedidosD_G.Application.Contracts.Services;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Domain.Exceptions;

namespace SistemaPedidosD_G.Application.UseCases
{
    public class EliminarItemDelCarritoUseCase : IEliminarItemDelCarritoUseCase
    {
        private readonly ICarritoService _carritoService;

        public EliminarItemDelCarritoUseCase(ICarritoService carritoService)
        {
            _carritoService = carritoService;
        }

        public Task EjecutarAsync(string clienteId, Guid productoId)
        {
            var carrito = _carritoService.ObtenerCarrito(clienteId);
            if (carrito.EstaVacio())
                throw new CarritoVacioException();

            _carritoService.EliminarItem(clienteId, productoId);
            return Task.CompletedTask;
        }
    }
}