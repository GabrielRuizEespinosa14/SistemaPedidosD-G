using SistemaPedidosD_G.Application.DTO;
using SistemaPedidosD_G.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.Extensions
{
    public static class ProductoMappingExtensions
    {
        public static ProductoDTO ToDTO(this Producto producto)
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

                Disponibilidad = producto.ObtenerDisponibilidad().ToString(),
                UltimasUnidades = producto.TieneStockBajo(),
                PuedeAgregarAlCarrito = producto.Activo && !producto.EstaAgotado()
            };
        }

        public static Producto ToEntity(this ProductoDTO dto)
        {
            var producto = new Producto(
                dto.Nombre,
                dto.Descripcion,
                dto.Precio,
                dto.Stock,
                dto.ImagenUrl
            );

            if (!dto.Activo)
            {
                producto.Desactivar();
            }

            return producto;
        }
    }
}

