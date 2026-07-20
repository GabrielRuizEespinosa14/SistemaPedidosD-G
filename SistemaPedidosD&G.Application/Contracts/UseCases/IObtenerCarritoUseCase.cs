using SistemaPedidosD_G.Application.DTOs;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.Contracts.UseCases
{
    public interface IObtenerCarritoUseCase
    {
        Task<CarritoDto> EjecutarAsync(string clienteId);
    }
}