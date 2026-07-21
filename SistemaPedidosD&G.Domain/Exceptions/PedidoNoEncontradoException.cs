namespace SistemaPedidosD_G.Domain.Exceptions
{
    public class PedidoNoEncontradoException : DomainException
    {
        public PedidoNoEncontradoException(Guid pedidoId)
            : base($"El pedido con ID {pedidoId} no fue encontrado.") { }
    }
}