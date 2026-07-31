using SistemaPedidosD_G.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Domain.Entities
{
    public class HistorialPedido
    {
        public Guid Id { get; private set; }

        public Guid PedidoId { get; private set; }

        public EstadoPedido Estado { get; private set; }

        public string Observacion { get; private set; } = null!;

        public DateTime Fecha { get; private set; }

        // Navegación para EF Core
        public Pedido Pedido { get; private set; } = null!;

        private HistorialPedido() { }

        public HistorialPedido(Guid pedidoId, EstadoPedido estado, string observacion)
        {
            Id = Guid.NewGuid();
            PedidoId = pedidoId;
            Estado = estado;
            Observacion = observacion;
            Fecha = DateTime.UtcNow;
        }
    }
}

