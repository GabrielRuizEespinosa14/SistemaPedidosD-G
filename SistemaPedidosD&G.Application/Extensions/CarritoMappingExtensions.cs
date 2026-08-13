using SistemaPedidosD_G.Application.DTOs;
using SistemaPedidosD_G.Domain.Aggregates.AggregatesCarrito;
using SistemaPedidosD_G.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.Extensions
{
    public static class CarritoMappingExtensions
    {
        public static ItemCarritoDto ToDTO(this ItemCarrito item)
        {
            return new ItemCarritoDto
            {
                ProductoId = item.ProductoId,
                NombreProducto = item.NombreProducto,
                Cantidad = item.Cantidad,
                PrecioUnitario = item.PrecioUnitario,
                Subtotal = item.ObtenerSubtotal()
            };
        }

        public static CarritoDto ToDTO(this Carrito carrito)
        {
            return new CarritoDto
            {
                ClienteId = carrito.ClienteId,

                Items = carrito.Items
                    .Select(item => item.ToDTO())
                    .ToList(),

                Total = carrito.ObtenerTotal()
            };
        }
    }
}
