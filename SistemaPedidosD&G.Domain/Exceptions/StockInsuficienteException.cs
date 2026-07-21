namespace SistemaPedidosD_G.Domain.Exceptions
{
    public class StockInsuficienteException : DomainException
    {
        public StockInsuficienteException(Guid productoId, int cantidadSolicitada, int stockDisponible)
            : base($"Stock insuficiente para el producto {productoId}. " +
                   $"Solicitado: {cantidadSolicitada}, Disponible: {stockDisponible}")
        { }
    }
}