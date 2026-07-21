namespace SistemaPedidosD_G.Domain.Exceptions
{
    public class ProductoNoEncontradoException : DomainException
    {
        public ProductoNoEncontradoException(Guid productoId)
            : base($"El producto con ID {productoId} no fue encontrado.") { }
    }
}