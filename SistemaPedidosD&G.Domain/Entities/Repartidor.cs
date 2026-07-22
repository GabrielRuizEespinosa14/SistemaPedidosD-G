using SistemaPedidosD_G.Domain.Enums;
using SistemaPedidosD_G.Domain.ValueObjects;

namespace SistemaPedidosD_G.Domain.Entities
{
    public class Repartidor
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;
        public Telefono Telefono { get; private set; } = null!;
        public EstadoRepartidor Estado { get; private set; }
        public DateTime FechaRegistro { get; private set; }

        public Repartidor(string nombre, string telefono)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            Telefono = new Telefono(telefono);
            Estado = EstadoRepartidor.Disponible;
            FechaRegistro = DateTime.UtcNow;
        }

        public void Activar() => Estado = EstadoRepartidor.Disponible;
        public void Desactivar() => Estado = EstadoRepartidor.Inactivo;
        public void MarcarOcupado() => Estado = EstadoRepartidor.Ocupado;
        public void MarcarDisponible() => Estado = EstadoRepartidor.Disponible;
        public bool EstaDisponible() => Estado == EstadoRepartidor.Disponible;
    }
}