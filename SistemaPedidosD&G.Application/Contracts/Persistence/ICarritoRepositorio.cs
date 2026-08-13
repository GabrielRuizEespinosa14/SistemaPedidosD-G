using SistemaPedidosD_G.Domain.Aggregates.AggregatesCarrito;
using SistemaPedidosD_G.Domain.Aggregates;
using System;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.Contracts.Persistence
{
    public interface ICarritoRepositorio
    {
        Task<Carrito?> ObtenerPorClienteIdAsync(string clienteId);
        Task<Carrito?> ObtenerPorIdAsync(Guid id);
        Task AgregarAsync(Carrito carrito);
        Task ActualizarAsync(Carrito carrito);
        Task EliminarAsync(Guid id);
    }
}