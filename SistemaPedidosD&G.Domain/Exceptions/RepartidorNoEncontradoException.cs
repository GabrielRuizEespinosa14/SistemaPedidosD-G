namespace SistemaPedidosD_G.Domain.Exceptions;

public sealed class RepartidorNoEncontradoException : DomainException
{
    public RepartidorNoEncontradoException(Guid repartidorId)
        : base($"No existe un repartidor con el ID {repartidorId}.")
    {
    }
}