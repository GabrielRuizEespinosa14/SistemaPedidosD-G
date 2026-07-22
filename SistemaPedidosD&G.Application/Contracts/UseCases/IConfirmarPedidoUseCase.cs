using SistemaPedidosD_G.Application.DTO;

namespace SistemaPedidosD_G.Application.Contracts.UseCases
{
    public interface IConfirmarPedidoUseCase
    {
        Task<PedidoDTO> EjecutarAsync(CheckoutDTO checkout);
    }
}
