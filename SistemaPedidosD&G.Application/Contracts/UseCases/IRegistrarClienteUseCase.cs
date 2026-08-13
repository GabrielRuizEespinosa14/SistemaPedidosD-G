namespace SistemaPedidosD_G.Application.Contracts.UseCases;

public interface IRegistrarClienteUseCase
{
    Task<Guid> EjecutarAsync(RegistrarClienteRequest request);
}

public sealed class RegistrarClienteRequest
{
    public string Nombre { get; set; } = null!;
    public string Correo { get; set; } = null!;
    public string Telefono { get; set; } = null!;
}
