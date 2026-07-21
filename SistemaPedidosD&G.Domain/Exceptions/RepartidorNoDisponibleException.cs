namespace SistemaPedidosD_G.Domain.Exceptions
{
    public class RepartidorNoDisponibleException : DomainException
    {
        public RepartidorNoDisponibleException(Guid repartidorId)
            : base($"El repartidor con ID {repartidorId} no está disponible actualmente.") { }
    }
}