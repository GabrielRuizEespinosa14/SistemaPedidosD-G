using SistemaPedidosD_G.Domain.Enums;

namespace SistemaPedidosD_G.Application.DTO
{
    public class CheckoutDTO
    {
        public string ClienteId { get; set; } = null!;
        public string NombreCliente { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public MetodoEntrega MetodoEntrega { get; set; }

        //Solo requerido si MetodoEntrega es Delivery
        public string? Calle { get; set; }
        public string? Sector { get; set; }
        public string? Referencia { get; set; }
    }
}