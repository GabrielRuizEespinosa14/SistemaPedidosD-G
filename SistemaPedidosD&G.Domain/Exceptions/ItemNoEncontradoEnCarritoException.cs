namespace SistemaPedidosD_G.Domain.Exceptions
{
    public class ItemNoEncontradoEnCarritoException : DomainException
    {
        public ItemNoEncontradoEnCarritoException(Guid productoId)
            : base($"El producto con ID {productoId} no está en el carrito.") { }
    }
}