using System;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.Contracts.UseCases
{
    public interface IEliminarItemDelCarritoUseCase
    {
        Task EjecutarAsync(string clienteId, Guid productoId);
    }
}