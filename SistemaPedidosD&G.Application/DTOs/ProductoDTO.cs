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

      
        /// "Disponible", "UltimasUnidades" o "Agotado"
       
        public string Disponibilidad { get; set; } = null!;

        public bool UltimasUnidades { get; set; }
       
        public bool PuedeAgregarAlCarrito { get; set; }

        public static ProductoDTO DesdeEntidad(Producto producto)
        {
            return new ProductoDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                ImagenUrl = producto.ImagenUrl,
                Activo = producto.Activo,

                // Usa el método de dominio 
                Disponibilidad = producto.ObtenerDisponibilidad().ToString(),
                UltimasUnidades = producto.TieneStockBajo(),
                PuedeAgregarAlCarrito = producto.Activo && !producto.EstaAgotado()
            };
        }
    }
}