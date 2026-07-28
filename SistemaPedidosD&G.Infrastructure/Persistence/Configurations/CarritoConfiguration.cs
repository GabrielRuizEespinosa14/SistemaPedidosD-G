using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaPedidosD_G.Domain.Entities;

namespace SistemaPedidosD_G.Infrastructure.Persistence.Configurations
{
    public class CarritoConfiguration : IEntityTypeConfiguration<Carrito>
    {
        public void Configure(EntityTypeBuilder<Carrito> builder)
        {
            builder.ToTable("Carritos");

            // ClienteId es la clave natural del carrito (1 carrito activo por cliente)
            builder.HasKey(c => c.ClienteId);

            builder.Property(c => c.ClienteId)
                .HasMaxLength(100);

            builder.OwnsMany(c => c.Items, item =>
            {
                item.ToTable("ItemsCarrito");
                item.WithOwner().HasForeignKey("ClienteId");

                item.Property<int>("Id");
                item.HasKey("Id");

                item.Property(i => i.ProductoId).IsRequired();

                item.Property(i => i.NombreProducto)
                    .IsRequired()
                    .HasMaxLength(150);

                item.Property(i => i.Cantidad).IsRequired();

                item.Property(i => i.PrecioUnitario)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
            });
        }
    }
}
