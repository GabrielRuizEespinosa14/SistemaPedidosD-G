using System;
using System.Threading.Tasks;
using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.Services;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Domain.Exceptions;

namespace SistemaPedidosD_G.Application.UseCases
{
    public class ModificarCantidadItemUseCase : IModificarCantidadItemUseCase
    {
        private readonly ICarritoService _carritoService;
        private readonly IRepositorio _productoRepositorio;

        public ModificarCantidadItemUseCase(
            ICarritoService carritoService,
            IRepositorio productoRepositorio)
        {
            _carritoService = carritoService;
            _productoRepositorio = productoRepositorio;
        }

        public async Task EjecutarAsync(ModificarCantidadRequest request)
        {
            // 1. Obtener el carrito en memoria
            var carrito = _carritoService.ObtenerCarrito(request.ClienteId);

            // 2. Verificar que el item existe en el carrito
            var item = carrito.Items.Find(i => i.ProductoId == request.ProductoId);
            if (item == null)
                throw new ItemNoEncontradoEnCarritoException(request.ProductoId);

            // 3. Verificar stock disponible
            var producto = await _productoRepositorio.ObtenerPorIdAsync(request.ProductoId);
            if (producto == null)
                throw new ProductoNoEncontradoException(request.ProductoId);

            if (producto.Stock < request.NuevaCantidad)
                throw new StockInsuficienteException(request.ProductoId, request.NuevaCantidad, producto.Stock);

            // 4. Modificar la cantidad
            _carritoService.ModificarCantidad(request.ClienteId, request.ProductoId, request.NuevaCantidad);
        }
    }
}
