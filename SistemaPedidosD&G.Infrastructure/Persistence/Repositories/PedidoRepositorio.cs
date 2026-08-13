using Microsoft.EntityFrameworkCore;
using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Domain.Aggregates.AggregatesPedido;
using SistemaPedidosD_G.Domain.Aggregates;

namespace SistemaPedidosD_G.Infrastructure.Persistence.Repositories
{
    public class PedidoRepositorio : IPedidoRepositorio
    {
        private readonly SistemaPedidosDGDbContext _context;

        public PedidoRepositorio(SistemaPedidosDGDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido?> ObtenerPorIdAsync(Guid id)
        {
            return await _context.Pedidos
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pedido?> ObtenerPorNumeroAsync(string numeroPedido)
        {
            return await _context.Pedidos
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.NumeroPedido.Valor == numeroPedido.ToUpper().Trim());
        }

        public async Task<IEnumerable<Pedido>> ObtenerPorClienteIdAsync(string clienteId)
        {
            return await _context.Pedidos
                .Include(p => p.Detalles)
                .Where(p => p.ClienteId == clienteId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> ObtenerTodosAsync()
        {
            return await _context.Pedidos
                .Include(p => p.Detalles)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AgregarAsync(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }
    }
}
