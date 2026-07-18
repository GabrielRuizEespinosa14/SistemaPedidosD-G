using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Domain.Entities;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.UseCases
{
    public class CrearProductoUseCase : ICrearProductoUseCase
    {
        private readonly IRepositorio _productoRepositorio;

        public CrearProductoUseCase(IRepositorio productoRepositorio)
        {
            _productoRepositorio = productoRepositorio;
        }

        public async Task EjecutarAsync(CrearProductoRequest request)
        {
            var producto = new Producto(
                request.Nombre,
                request.Descripcion,
                request.Precio,
                request.Stock,
                request.ImagenUrl
            );

            await _productoRepositorio.AgregarAsync(producto);
        }
    }
}