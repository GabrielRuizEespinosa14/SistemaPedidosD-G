using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.UseCases
{
    public class ActualizarProductoUseCase : IActualizarProductoUseCase
    {
        private readonly IRepositorio _productoRepositorio;

        public ActualizarProductoUseCase(IRepositorio productoRepositorio)
        {
            _productoRepositorio = productoRepositorio;
        }

        public async Task EjecutarAsync(Guid id, ActualizarProductoRequest request)
        {
            var producto = await _productoRepositorio.ObtenerPorIdAsync(id);

            if (producto is null)
                throw new ProductoNoEncontradoException(id);

            producto.Actualizar(
                request.Nombre,
                request.Descripcion,
                request.Precio,
                request.Stock,
                request.ImagenUrl
            );

            await _productoRepositorio.ActualizarAsync(producto);
        }
    }
}
