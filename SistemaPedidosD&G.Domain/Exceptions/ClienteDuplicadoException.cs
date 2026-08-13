namespace SistemaPedidosD_G.Domain.Exceptions;

public sealed class ClienteDuplicadoException : DomainException
{
    public ClienteDuplicadoException(string campo)
        : base($"Ya existe un cliente registrado con ese {campo}.")
    {
    }
}
