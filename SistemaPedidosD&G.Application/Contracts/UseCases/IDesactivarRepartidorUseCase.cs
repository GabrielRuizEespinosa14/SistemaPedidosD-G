namespace SistemaPedidosD_G.Application.Contracts.UseCases;

public interface IDesactivarRepartidorUseCase
{
    Task EjecutarAsync(Guid repartidorId);
}