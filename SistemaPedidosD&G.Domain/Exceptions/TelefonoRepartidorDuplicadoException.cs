namespace SistemaPedidosD_G.Domain.Exceptions;

public sealed class TelefonoRepartidorDuplicadoException : DomainException
{
    public TelefonoRepartidorDuplicadoException(string telefono)
        : base($"Ya existe un repartidor registrado con el teléfono {telefono}.")
    {
    }
}