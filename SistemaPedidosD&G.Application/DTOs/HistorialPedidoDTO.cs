using SistemaPedidosD_G.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.DTOs
{
    public class HistorialPedidoDTO
    {
        public Guid Id { get; set; }
        public Guid PedidoId { get; set; }
        public EstadoPedido Estado { get; set; }
        public string Observacion { get; set; } = null!;
        public DateTime Fecha { get; set; }
    }
}
