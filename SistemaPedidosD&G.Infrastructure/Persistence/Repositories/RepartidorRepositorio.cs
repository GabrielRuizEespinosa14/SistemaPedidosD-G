using Microsoft.EntityFrameworkCore;
using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Domain.Entities;
using SistemaPedidosD_G.Domain.Enums;

namespace SistemaPedidosD_G.Infrastructure.Persistence.Repositories
{
    public class RepartidorRepositorio : IRepartidorRepositorio
    {
        private readonly SistemaPedidosDGDbContext _context;

        public RepartidorRepositorio(SistemaPedidosDGDbContext context)
        {
            _context = context;
        }

        public async Task<Repartidor?> ObtenerPorIdAsync(Guid id)
        {
            return await _context.Repartidores.FindAsync(id);
        }

        public async Task<IEnumerable<Repartidor>> ObtenerDisponiblesAsync()
        {
            return await _context.Repartidores
                .AsNoTracking()
                .Where(r => r.Estado == EstadoRepartidor.Disponible)
                .ToListAsync();
        }

        public async Task<IEnumerable<Repartidor>> ObtenerTodosAsync()
        {
            return await _context.Repartidores.AsNoTracking().ToListAsync();
        }
        public async Task<bool> ExistePorTelefonoAsync(
    string telefono,
    Guid? repartidorIdExcluir = null)
        {
            return await _context.Repartidores.AnyAsync(r =>
                r.Telefono.Valor == telefono &&
                (!repartidorIdExcluir.HasValue ||
                 r.Id != repartidorIdExcluir.Value));
        }
        public async Task<int> ContarPedidosAsignadosAsync(Guid repartidorId)
        {
            return await _context.Pedidos.CountAsync(p =>
                p.RepartidorId == repartidorId &&
                p.Estado != EstadoPedido.Completado &&
                p.Estado != EstadoPedido.Cancelado);
        }
        public async Task AgregarAsync(Repartidor repartidor)
        {
            await _context.Repartidores.AddAsync(repartidor);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Repartidor repartidor)
        {
            _context.Repartidores.Update(repartidor);
            await _context.SaveChangesAsync();
        }
    }
}