namespace SistemaPedidosD_G.Application.Contracts.UseCases;

public interface IActualizarRepartidorUseCase
{
    Task EjecutarAsync(
        Guid repartidorId,
        ActualizarRepartidorRequest request);
}

public sealed class ActualizarRepartidorRequest
{
    public string Nombre { get; set; } = null!;
    public string Telefono { get; set; } = null!;
}