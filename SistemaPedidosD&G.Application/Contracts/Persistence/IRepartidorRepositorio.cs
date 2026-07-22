using SistemaPedidosD_G.Domain.Entities;

namespace SistemaPedidosD_G.Application.Contracts.Persistence
{
    public interface IRepartidorRepositorio
    {
        Task<Repartidor?> ObtenerPorIdAsync(Guid id);
        Task<IEnumerable<Repartidor>> ObtenerDisponiblesAsync();
        Task<IEnumerable<Repartidor>> ObtenerTodosAsync();
        Task AgregarAsync(Repartidor repartidor);
        Task ActualizarAsync(Repartidor repartidor);
    }
}
