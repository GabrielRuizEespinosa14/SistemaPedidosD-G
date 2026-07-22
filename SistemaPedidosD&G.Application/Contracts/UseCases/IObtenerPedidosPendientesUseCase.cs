using SistemaPedidosD_G.Application.DTO;

namespace SistemaPedidosD_G.Application.Contracts.UseCases
{
    public interface IObtenerPedidosPendientesUseCase
    {
        Task<IEnumerable<PedidoDTO>> EjecutarAsync();
    }
}