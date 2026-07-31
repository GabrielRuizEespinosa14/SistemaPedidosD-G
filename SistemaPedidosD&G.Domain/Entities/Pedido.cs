using SistemaPedidosD_G.Domain.Enums;
using SistemaPedidosD_G.Domain.Exceptions;
using SistemaPedidosD_G.Domain.ValueObjects;

namespace SistemaPedidosD_G.Domain.Entities
{
    public class Pedido
    {
        public Guid Id { get; private set; }
        public NumeroPedido NumeroPedido { get; private set; } = null!;
        public string ClienteId { get; private set; } = null!;
        public string NombreCliente { get; private set; } = null!;
        public Telefono Telefono { get; private set; } = null!;
        public MetodoEntrega MetodoEntrega { get; private set; }
        public Direccion? Direccion { get; private set; }
        public EstadoPedido Estado { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public DateTime? FechaActualizacion { get; private set; }
        public Guid? RepartidorId { get; private set; }
        public DateTime? FechaCancelacion { get; private set; }
        public List<DetallePedido> Detalles { get; private set; } = new();
        public List<HistorialPedido> Historial { get; private set; } = new();

        private Pedido() { } // Requerido por EF Core

        public Pedido(string clienteId, string nombreCliente, string telefono,
            MetodoEntrega metodoEntrega, Direccion? direccion = null)
        {
            Id = Guid.NewGuid();
            NumeroPedido = NumeroPedido.Generar();
            ClienteId = clienteId;
            NombreCliente = nombreCliente;
            Telefono = new Telefono(telefono);
            MetodoEntrega = metodoEntrega;
            Direccion = direccion;
            Estado = EstadoPedido.Pendiente;
            FechaCreacion = DateTime.UtcNow;
            Historial.Add(new HistorialPedido(
    Id,
    EstadoPedido.Pendiente,
    "Pedido creado"));
        }

        public void AgregarDetalle(Guid productoId, string nombreProducto, int cantidad, decimal precioUnitario)
        {
            var detalle = new DetallePedido(Id, productoId, nombreProducto, cantidad, precioUnitario);
            Detalles.Add(detalle);
        }

        public Dinero ObtenerTotal()
        {
            var total = new Dinero(0);
            foreach (var detalle in Detalles)
                total = total.Sumar(detalle.ObtenerSubtotal());
            return total;
        }

        public void CambiarEstado(EstadoPedido nuevoEstado)
        {
            if (Estado == EstadoPedido.Cancelado)
                throw new PedidoCanceladoException(Id);

            if (Estado == EstadoPedido.Completado)
                throw new CambioEstadoInvalidoException(Estado.ToString(), nuevoEstado.ToString());

            Estado = nuevoEstado;
            FechaActualizacion = DateTime.UtcNow;
        }

        public void Cancelar()
        {
            if (Estado == EstadoPedido.Completado)
                throw new CambioEstadoInvalidoException(
                    Estado.ToString(),
                    EstadoPedido.Cancelado.ToString());

            if (Estado == EstadoPedido.Cancelado)
                throw new PedidoCanceladoException(Id);

            if (Estado is not EstadoPedido.Pendiente
                and not EstadoPedido.EnPreparacion)
            {
                throw new CambioEstadoInvalidoException(
                    Estado.ToString(),
                    EstadoPedido.Cancelado.ToString());
            }

            Estado = EstadoPedido.Cancelado;
            FechaActualizacion = DateTime.UtcNow;

            Historial.Add(
    new HistorialPedido(
        Id,
        Estado,
        $"Estado cambiado a {Estado}"
    ));
        }

        public void AsignarRepartidor(Guid repartidorId)
        {
            if (Estado == EstadoPedido.Cancelado)
                throw new PedidoCanceladoException(Id);

            RepartidorId = repartidorId;
            Estado = EstadoPedido.EnCamino;
            FechaActualizacion = DateTime.UtcNow;
        }
    }
}

