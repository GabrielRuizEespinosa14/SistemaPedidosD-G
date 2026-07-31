using SistemaPedidosD_G.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.UseCases
{
    public class ActivarProductoUseCase
    {
        private readonly IRepositorio _repositorio;


        public ActivarProductoUseCase(
            IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }


        public async Task Ejecutar(Guid id)
        {
            var producto = await _repositorio.ObtenerPorIdAsync(id);


            if (producto == null)
                throw new Exception("El producto no existe");


            producto.Activar();


            await _repositorio.ActualizarAsync(producto);
        }
    }
}
