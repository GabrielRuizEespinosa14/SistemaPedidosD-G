using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Application.DTO;
using SistemaPedidosD_G.Application.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.UseCases
{
    public class ObtenerProductosUseCase : IObtenerProductosUseCase
    {
        private readonly IRepositorio _productoRepositorio;

        public ObtenerProductosUseCase(IRepositorio productoRepositorio)
        {
            _productoRepositorio = productoRepositorio;
        }

        public async Task<IEnumerable<ProductoDTO>> EjecutarAsync(bool soloActivos = true)
        {
            var productos = soloActivos
                ? await _productoRepositorio.ObtenerActivosAsync()
                : await _productoRepositorio.ObtenerTodosAsync();

            return productos.Select(producto => producto.ToDTO());
        }
    }
}