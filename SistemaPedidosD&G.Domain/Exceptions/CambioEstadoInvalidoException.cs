namespace SistemaPedidosD_G.Domain.Exceptions
{
    public class CambioEstadoInvalidoException : DomainException
    {
        public CambioEstadoInvalidoException(string estadoActual, string estadoNuevo)
            : base($"No se puede cambiar el estado de '{estadoActual}' a '{estadoNuevo}'.") { }
    }
}