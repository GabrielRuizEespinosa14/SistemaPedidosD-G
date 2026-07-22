using SistemaPedidosD_G.Domain.Enums;

namespace SistemaPedidosD_G.Application.Contracts.UseCases
{
    public interface ICambiarEstadoPedidoUseCase
    {
        Task EjecutarAsync(Guid pedidoId, EstadoPedido nuevoEstado);
    }
}