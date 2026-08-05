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


        private Repartidor() { } // Requerido por EF Core

        public Repartidor(string nombre, string telefono)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del repartidor es obligatorio.");

            Id = Guid.NewGuid();
            Nombre = nombre.Trim();
            Telefono = new Telefono(telefono);
            Estado = EstadoRepartidor.Disponible;
            FechaRegistro = DateTime.UtcNow;
        }
        public void ActualizarDatos(string nombre, string telefono)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del repartidor es obligatorio.");

            Nombre = nombre.Trim();
            Telefono = new Telefono(telefono);
        }
        public void Activar() => Estado = EstadoRepartidor.Disponible;
        public void Desactivar() => Estado = EstadoRepartidor.Inactivo;
        public void MarcarOcupado() => Estado = EstadoRepartidor.Ocupado;
        public void MarcarDisponible() => Estado = EstadoRepartidor.Disponible;
        public bool EstaDisponible() => Estado == EstadoRepartidor.Disponible;

    }
}