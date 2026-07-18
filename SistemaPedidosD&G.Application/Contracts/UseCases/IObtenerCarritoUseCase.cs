using SistemaPedidosD_G.Domain.Entities;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.Contracts.UseCases
{
    public interface IObtenerCarritoUseCase
    {
        Task<Carrito> EjecutarAsync(string clienteId);
    }
}