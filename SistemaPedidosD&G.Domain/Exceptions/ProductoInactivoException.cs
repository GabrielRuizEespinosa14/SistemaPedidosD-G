namespace SistemaPedidosD_G.Domain.Exceptions
{
    public class ProductoInactivoException : DomainException
    {
        public ProductoInactivoException(Guid productoId)
            : base($"El producto con ID {productoId} no está disponible en el catálogo.") { }
    }
}