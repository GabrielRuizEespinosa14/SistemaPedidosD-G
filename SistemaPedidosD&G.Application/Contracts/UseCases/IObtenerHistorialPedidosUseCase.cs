using SistemaPedidosD_G.Application.DTO;

namespace SistemaPedidosD_G.Application.Contracts.UseCases
{
    public interface IObtenerHistorialPedidosUseCase
    {
        Task<IEnumerable<PedidoDTO>> EjecutarAsync(string clienteId);
    }
}