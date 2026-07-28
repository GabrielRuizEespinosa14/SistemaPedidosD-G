using Microsoft.EntityFrameworkCore;
using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Domain.Entities;

namespace SistemaPedidosD_G.Infrastructure.Persistence.Repositories
{
    public class CarritoRepositorio : ICarritoRepositorio
    {
        private readonly SistemaPedidosDGDbContext _context;

        public CarritoRepositorio(SistemaPedidosDGDbContext context)
        {
            _context = context;
        }

        public async Task<Carrito?> ObtenerPorClienteIdAsync(string clienteId)
        {
            return await _context.Carritos
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.ClienteId == clienteId);
        }

        public async Task<Carrito?> ObtenerPorIdAsync(Guid id)
        {
            // Nota: Carrito usa ClienteId como clave natural (ver CarritoConfiguration).
            // Ver comentario de diseño más abajo: esta interfaz no encaja del todo bien.
            return await _context.Carritos
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.ClienteId == id.ToString());
        }

        public async Task AgregarAsync(Carrito carrito)
        {
            await _context.Carritos.AddAsync(carrito);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Carrito carrito)
        {
            _context.Carritos.Update(carrito);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(Guid id)
        {
            var carrito = await _context.Carritos
                .FirstOrDefaultAsync(c => c.ClienteId == id.ToString());

            if (carrito != null)
            {
                _context.Carritos.Remove(carrito);
                await _context.SaveChangesAsync();
            }
        }
    }
}
