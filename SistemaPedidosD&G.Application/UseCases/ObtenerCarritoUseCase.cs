using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.Services;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Application.DTOs;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.UseCases
{
    public class ObtenerCarritoUseCase : IObtenerCarritoUseCase
    {
        private readonly ICarritoService _carritoService;
        private readonly IRepositorio _productoRepositorio;

        public ObtenerCarritoUseCase(
            ICarritoService carritoService,
            IRepositorio productoRepositorio)
        {
            _carritoService = carritoService;
            _productoRepositorio = productoRepositorio;
        }

        public async Task<CarritoDto> EjecutarAsync(string clienteId)
        {
            var carrito = _carritoService.ObtenerCarrito(clienteId);

            var dto = new CarritoDto
            {
                ClienteId = carrito.ClienteId,
         
                Total = carrito.ObtenerTotal(),
                EsValido = true
            };

            foreach (var item in carrito.Items)
            {
           
                var producto = await _productoRepositorio.ObtenerPorIdAsync(item.ProductoId);

                var productoNoDisponible = producto == null || !producto.Activo;
                var stockDisponible = producto?.Stock ?? 0;
                var excedeStock = !productoNoDisponible && item.Cantidad > stockDisponible;

                if (productoNoDisponible || excedeStock)
                    dto.EsValido = false;

                dto.Items.Add(new ItemCarritoDto
                {
                    ProductoId = item.ProductoId,
                    NombreProducto = item.NombreProducto,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = item.PrecioUnitario,
                    Subtotal = item.ObtenerSubtotal(),
                    StockDisponible = stockDisponible,
                    ExcedeStock = excedeStock,
                    ProductoNoDisponible = productoNoDisponible
                });
            }

            return dto;
        }
    }
}