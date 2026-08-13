using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaPedidosD_G.Domain.Aggregates.AggregatesPedido;
using SistemaPedidosD_G.Domain.Aggregates;

namespace SistemaPedidosD_G.Infrastructure.Persistence.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");

            builder.HasKey(p => p.Id);

            builder.OwnsOne(p => p.NumeroPedido, np =>
            {
                np.Property(x => x.Valor)
                    .HasColumnName("NumeroPedido")
                    .IsRequired()
                    .HasMaxLength(20);

                np.HasIndex(x => x.Valor).IsUnique();
            });

            builder.Property(p => p.ClienteId)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.NombreCliente)
                .IsRequired()
                .HasMaxLength(150);

            builder.OwnsOne(p => p.Telefono, telefono =>
            {
                telefono.Property(t => t.Valor)
                    .HasColumnName("Telefono")
                    .IsRequired()
                    .HasMaxLength(20);
            });

            builder.Property(p => p.MetodoEntrega)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.OwnsOne(p => p.Direccion, direccion =>
            {
                direccion.Property(d => d.Calle).HasColumnName("Direccion_Calle").HasMaxLength(200);
                direccion.Property(d => d.Sector).HasColumnName("Direccion_Sector").HasMaxLength(150);
                direccion.Property(d => d.Referencia).HasColumnName("Direccion_Referencia").HasMaxLength(300);
            });

            builder.Property(p => p.Estado)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.FechaCreacion)
                .IsRequired();

            builder.Property(p => p.FechaActualizacion);

            builder.Property(p => p.RepartidorId);

            builder.OwnsMany(p => p.Detalles, detalle =>
            {
                detalle.ToTable("DetallesPedido");
                detalle.WithOwner().HasForeignKey(d => d.PedidoId);
                detalle.HasKey(d => d.Id);

                detalle.Property(d => d.ProductoId).IsRequired();

                detalle.Property(d => d.NombreProducto)
                    .IsRequired()
                    .HasMaxLength(150);

                detalle.Property(d => d.Cantidad).IsRequired();

                detalle.OwnsOne(d => d.PrecioUnitario, precio =>
                {
                    precio.Property(x => x.Valor)
                        .HasColumnName("PrecioUnitario")
                        .HasColumnType("decimal(18,2)")
                        .IsRequired();

                    precio.Property(x => x.Moneda)
                        .HasColumnName("Moneda")
                        .HasMaxLength(5)
                        .IsRequired();
                });
            });
        }
    }
}