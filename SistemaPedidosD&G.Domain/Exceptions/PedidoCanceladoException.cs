namespace SistemaPedidosD_G.Domain.Exceptions
{
    public class PedidoCanceladoException : DomainException
    {
        public PedidoCanceladoException(Guid pedidoId)
            : base($"El pedido con ID {pedidoId} ya fue cancelado y no puede modificarse.") { }
    }
}
