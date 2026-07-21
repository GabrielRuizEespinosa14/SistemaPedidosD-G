namespace SistemaPedidosD_G.Domain.Exceptions
{
    public class CarritoVacioException : DomainException
    {
        public CarritoVacioException()
            : base("El carrito está vacío.") { }
    }
}