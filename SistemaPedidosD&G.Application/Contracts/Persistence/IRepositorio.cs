using SistemaPedidosD_G.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.Contracts.Persistence
{
    public interface IRepositorio
    {
        Task<IEnumerable<Producto>> ObtenerTodosAsync();
        Task<IEnumerable<Producto>> ObtenerActivosAsync();
        Task<Producto?> ObtenerPorIdAsync(Guid id);
        Task AgregarAsync(Producto producto);
        Task ActualizarAsync(Producto producto);
        Task EliminarAsync(Guid id);
    }
}