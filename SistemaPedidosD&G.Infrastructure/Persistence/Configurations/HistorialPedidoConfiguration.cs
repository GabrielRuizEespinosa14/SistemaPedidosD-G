using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaPedidosD_G.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Infrastructure.Persistence.Configurations
{
    public class HistorialPedidoConfiguration : IEntityTypeConfiguration<HistorialPedido>
    {
        public void Configure(EntityTypeBuilder<HistorialPedido> builder)
        {
            builder.ToTable("HistorialPedidos");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.Observacion)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(h => h.Estado)
                .IsRequired();

            builder.Property(h => h.Fecha)
                .IsRequired();

            builder.HasOne(h => h.Pedido)
                .WithMany(p => p.Historial)
                .HasForeignKey(h => h.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
