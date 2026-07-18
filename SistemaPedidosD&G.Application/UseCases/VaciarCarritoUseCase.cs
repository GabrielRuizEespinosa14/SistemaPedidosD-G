using SistemaPedidosD_G.Application.Contracts.Services;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using System;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.UseCases
{
    public class VaciarCarritoUseCase : IVaciarCarritoUseCase
    {
        private readonly ICarritoService _carritoService;

        public VaciarCarritoUseCase(ICarritoService carritoService)
        {
            _carritoService = carritoService;
        }

        public Task EjecutarAsync(string clienteId)
        {
            var carrito = _carritoService.ObtenerCarrito(clienteId);
            if (carrito.EstaVacio())
                throw new Exception("El carrito ya está vacío.");

            _carritoService.Vaciar(clienteId);
            return Task.CompletedTask;
        }
    }
}