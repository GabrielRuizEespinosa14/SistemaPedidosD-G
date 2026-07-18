using SistemaPedidosD_G.Application.Contracts.Services;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Domain.Entities;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.UseCases
{
    public class ObtenerCarritoUseCase : IObtenerCarritoUseCase
    {
        private readonly ICarritoService _carritoService;

        public ObtenerCarritoUseCase(ICarritoService carritoService)
        {
            _carritoService = carritoService;
        }

        public Task<Carrito> EjecutarAsync(string clienteId)
        {
            var carrito = _carritoService.ObtenerCarrito(clienteId);
            return Task.FromResult(carrito);
        }
    }
}