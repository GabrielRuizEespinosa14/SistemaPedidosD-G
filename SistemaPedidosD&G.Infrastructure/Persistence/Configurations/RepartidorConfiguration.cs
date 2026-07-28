using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaPedidosD_G.Domain.Entities;

namespace SistemaPedidosD_G.Infrastructure.Persistence.Configurations
{
    public class RepartidorConfiguration : IEntityTypeConfiguration<Repartidor>
    {
        public void Configure(EntityTypeBuilder<Repartidor> builder)
        {
            builder.ToTable("Repartidores");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Nombre)
                .IsRequired()
                .HasMaxLength(150);

            builder.OwnsOne(r => r.Telefono, telefono =>
            {
                telefono.Property(t => t.Valor)
                    .HasColumnName("Telefono")
                    .IsRequired()
                    .HasMaxLength(20);

                telefono.HasIndex(t => t.Valor).IsUnique();
            });

            builder.Property(r => r.Estado)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(r => r.FechaRegistro)
                .IsRequired();
        }
    }
}
