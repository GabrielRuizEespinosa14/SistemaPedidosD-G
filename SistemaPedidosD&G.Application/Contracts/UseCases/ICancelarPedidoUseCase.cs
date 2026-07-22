namespace SistemaPedidosD_G.Application.Contracts.UseCases
{
    public interface ICancelarPedidoUseCase
    {
        Task EjecutarAsync(Guid pedidoId);
    }
}
