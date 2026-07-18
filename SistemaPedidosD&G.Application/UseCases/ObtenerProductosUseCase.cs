using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Domain.Entities;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Producto>> EjecutarAsync(bool soloActivos = true)
        {
            if (soloActivos)
                return await _productoRepositorio.ObtenerActivosAsync();

            return await _productoRepositorio.ObtenerTodosAsync();
        }
    }
}
