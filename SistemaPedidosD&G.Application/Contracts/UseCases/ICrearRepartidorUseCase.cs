namespace SistemaPedidosD_G.Application.Contracts.UseCases;

public interface ICrearRepartidorUseCase
{
    Task<Guid> EjecutarAsync(CrearRepartidorRequest request);
}

public sealed class CrearRepartidorRequest
{
    public string Nombre { get; set; } = null!;
    public string Telefono { get; set; } = null!;
}