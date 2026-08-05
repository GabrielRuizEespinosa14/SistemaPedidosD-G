using SistemaPedidosD_G.Domain.Enums;

namespace SistemaPedidosD_G.Application.DTO;

public sealed class RepartidorDTO
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public EstadoRepartidor Estado { get; set; }
    public DateTime FechaRegistro { get; set; }
    public int PedidosAsignadosActuales { get; set; }
}