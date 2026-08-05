using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.UseCases
{
    public class BuscarProductosUseCase
    {
        private readonly IRepositorio _repositorio;

        public BuscarProductosUseCase(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<ProductoDTO>> Ejecutar(string? nombre = null)
        {
            var productos = string.IsNullOrWhiteSpace(nombre)
                ? await _repositorio.ObtenerActivosAsync()
                : await _repositorio.BuscarPorNombreAsync(nombre);


            return productos.Select(ProductoDTO.DesdeEntidad);
        }
    }
}