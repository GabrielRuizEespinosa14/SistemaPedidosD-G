using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.Services;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using System;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.UseCases
{
    public class AgregarItemAlCarritoUseCase : IAgregarItemAlCarritoUseCase
    {
        private readonly ICarritoService _carritoService;
        private readonly IRepositorio _productoRepository;

        public AgregarItemAlCarritoUseCase(
            ICarritoService carritoService,
            IRepositorio productoRepositorio)
        {
            _carritoService = carritoService;
            _productoRepository = productoRepositorio;
        }

        public async Task EjecutarAsync(AgregarItemRequest request)
        {
            // 1. Verificar que el producto existe y está activo
            var producto = await _productoRepository.ObtenerPorIdAsync(request.ProductoId);
            if (producto == null || !producto.Activo)
                throw new Exception("El producto no existe o no está disponible.");

            // 2. Verificar stock suficiente
            if (producto.Stock < request.Cantidad)
                throw new Exception("No hay suficiente stock disponible.");

            // 3. Agregar al carrito en memoria
            _carritoService.AgregarItem(
                request.ClienteId,
                request.ProductoId,
                producto.Nombre,
                request.Cantidad,
                producto.Precio
            );
        }
    }
}