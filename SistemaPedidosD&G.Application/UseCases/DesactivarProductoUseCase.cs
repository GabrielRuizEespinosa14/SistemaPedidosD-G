using SistemaPedidosD_G.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.Services;
using SistemaPedidosD_G.Application.Contracts.UseCases;

namespace SistemaPedidosD_G.Application.UseCases
{
    public class DesactivarProductoUseCase : IDesactivarProductoUseCase
    {
        private readonly IRepositorio _repositorio;

        public DesactivarProductoUseCase(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task Ejecutar(Guid id)
        {
            var producto = await _repositorio.ObtenerPorIdAsync(id);

            if (producto == null)
                throw new Exception("El producto no existe");

            producto.Desactivar();

            await _repositorio.ActualizarAsync(producto);
        }
    }
}
