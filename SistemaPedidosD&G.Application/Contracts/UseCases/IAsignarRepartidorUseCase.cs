namespace SistemaPedidosD_G.Application.Contracts.UseCases
{
    public interface IAsignarRepartidorUseCase
    {
        Task EjecutarAsync(Guid pedidoId, Guid repartidorId);
    }
}

