using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<IEnumerable<Producto>> Ejecutar(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return await _repositorio.ObtenerActivosAsync();
            }

            return await _repositorio.BuscarPorNombreAsync(nombre);
        }
    }
}

