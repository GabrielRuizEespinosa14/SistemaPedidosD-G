using Microsoft.EntityFrameworkCore;
using SistemaPedidosD_G.Domain.Entities;

namespace SistemaPedidosD_G.Infrastructure.Persistence
{
    public class SistemaPedidosDGDbContext : DbContext
    {
        public SistemaPedidosDGDbContext(DbContextOptions<SistemaPedidosDGDbContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; } = null!;
        public DbSet<Pedido> Pedidos { get; set; } = null!;
        public DbSet<Repartidor> Repartidores { get; set; } = null!;
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Carrito> Carritos { get; set; } = null!;

        public DbSet<HistorialPedido> HistorialPedidos => Set<HistorialPedido>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SistemaPedidosDGDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
