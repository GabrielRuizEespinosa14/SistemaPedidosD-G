using Microsoft.EntityFrameworkCore;
using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Domain.Entities;

namespace SistemaPedidosD_G.Infrastructure.Persistence.Repositories
{
    public class ProductoRepositorio : IRepositorio
    {
        private readonly SistemaPedidosDGDbContext _context;

        public ProductoRepositorio(SistemaPedidosDGDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
        {
            return await _context.Productos.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Producto>> ObtenerActivosAsync()
        {
            return await _context.Productos
                .AsNoTracking()
                .Where(p => p.Activo)
                .ToListAsync();
        }

        public async Task<Producto?> ObtenerPorIdAsync(Guid id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task AgregarAsync(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Producto producto)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(Guid id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
        }
    }
}