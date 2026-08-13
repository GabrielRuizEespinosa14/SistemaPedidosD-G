using SistemaPedidosD_G.Domain.Aggregates.AggregatesPedido;
using SistemaPedidosD_G.Domain.Aggregates;
namespace SistemaPedidosD_G.Application.Contracts.Persistence
{
    public interface IPedidoRepositorio
    {
        Task<Pedido?> ObtenerPorIdAsync(Guid id);
        Task<Pedido?> ObtenerPorNumeroAsync(string numeroPedido);
        Task<IEnumerable<Pedido>> ObtenerPorClienteIdAsync(string clienteId);
        Task<IEnumerable<Pedido>> ObtenerTodosAsync();
        Task AgregarAsync(Pedido pedido);
        Task ActualizarAsync(Pedido pedido);
    }
}
