using System;
using System.Threading.Tasks;
using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.Services;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Domain.Exceptions;

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
            if (request.Cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero.");


            var producto = await _productoRepository.ObtenerPorIdAsync(request.ProductoId);
            if (producto == null)
                throw new ProductoNoEncontradoException(request.ProductoId);

            if (!producto.Activo)
                throw new ProductoInactivoException(request.ProductoId);


            var carrito = _carritoService.ObtenerCarrito(request.ClienteId);
            var itemExistente = carrito.Items.Find(i => i.ProductoId == request.ProductoId);
            var cantidadTotalEnCarrito = (itemExistente?.Cantidad ?? 0) + request.Cantidad;

            if (producto.Stock < cantidadTotalEnCarrito)
                throw new StockInsuficienteException(request.ProductoId, cantidadTotalEnCarrito, producto.Stock);


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