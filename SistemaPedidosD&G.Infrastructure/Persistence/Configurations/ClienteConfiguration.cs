using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaPedidosD_G.Domain.Entities;

namespace SistemaPedidosD_G.Infrastructure.Persistence.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nombre)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.Correo)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(c => c.Correo)
            .IsUnique();

        builder.Property(c => c.FechaRegistro)
            .IsRequired();

        builder.OwnsOne(c => c.Telefono, telefono =>
        {
            telefono.Property(t => t.Valor)
                .HasColumnName("Telefono")
                .IsRequired()
                .HasMaxLength(20);

            telefono.HasIndex(t => t.Valor)
                .IsUnique();
        });
    }
}
