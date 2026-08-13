using SistemaPedidosD_G.Domain.Entities;

namespace SistemaPedidosD_G.Application.DTO
{
    public class ProductoDTO
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public string ImagenUrl { get; set; } = null!;

        public bool Activo { get; set; }

        /// <summary>
        /// "Disponible", "UltimasUnidades" o "Agotado"
        /// </summary>
        public string Disponibilidad { get; set; } = null!;

        public bool UltimasUnidades { get; set; }

        public bool PuedeAgregarAlCarrito { get; set; }
    }
}