using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaPedidosD_G.Domain.Entities;

namespace SistemaPedidosD_G.Infrastructure.Persistence.Configurations
{
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("Productos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Descripcion)
                .HasMaxLength(1000);

            builder.Property(p => p.Precio)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.ImagenUrl)
                .HasMaxLength(500);

            builder.Property(p => p.Activo)
                .IsRequired();
            builder.Property(p => p.FechaDesactivacion)
    .IsRequired(false);
            builder.HasIndex(p => p.Nombre);
            builder.HasIndex(p => p.Activo);
        }
    }
}