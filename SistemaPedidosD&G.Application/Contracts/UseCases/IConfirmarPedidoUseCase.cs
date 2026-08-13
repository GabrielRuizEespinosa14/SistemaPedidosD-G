using SistemaPedidosD_G.Application.DTO;
using SistemaPedidosD_G.Domain.Aggregates;

namespace SistemaPedidosD_G.Application.Contracts.UseCases
{
    public interface IConfirmarPedidoUseCase
    {
        Task<PedidoDTO> EjecutarAsync(CheckoutDTO checkout);
    }
}
