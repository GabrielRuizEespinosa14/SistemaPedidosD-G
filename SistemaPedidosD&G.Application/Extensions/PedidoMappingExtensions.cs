using SistemaPedidosD_G.Application.DTO;
using SistemaPedidosD_G.Application.DTOs;
using SistemaPedidosD_G.Domain.Aggregates.AggregatesPedido;
using SistemaPedidosD_G.Domain.Aggregates;
using SistemaPedidosD_G.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.Extensions
{
    public static class PedidoMappingExtensions
    {
        public static PedidoDTO ToDTO(this Pedido pedido)
        {
            return new PedidoDTO
            {
                Id = pedido.Id,
                NumeroPedido = pedido.NumeroPedido.ToString(),
                NombreCliente = pedido.NombreCliente,
                Telefono = pedido.Telefono.ToString(),
                MetodoEntrega = pedido.MetodoEntrega,
                Direccion = pedido.Direccion?.ToString(),
                Estado = pedido.Estado,
                FechaCreacion = pedido.FechaCreacion,
                Total = pedido.ObtenerTotal().Valor,
                Detalles = pedido.Detalles
                    .Select(detalle => detalle.ToDTO())
                    .ToList(),

                Historial = pedido.Historial
    .Select(historial => historial.ToDTO())
    .ToList()
            };
        }

        public static DetallePedidoDTO ToDTO(this DetallePedido detalle)
        {
            return new DetallePedidoDTO
            {
                ProductoId = detalle.ProductoId,
                NombreProducto = detalle.NombreProducto,
                Cantidad = detalle.Cantidad,
                PrecioUnitario = detalle.PrecioUnitario.Valor,
                Subtotal = detalle.ObtenerSubtotal().Valor
            };
        }

        public static HistorialPedidoDTO ToDTO(this HistorialPedido historial)
        {
            return new HistorialPedidoDTO
            {
                Id = historial.Id,
                PedidoId = historial.PedidoId,
                Estado = historial.Estado,
                Observacion = historial.Observacion,
                Fecha = historial.Fecha
            };
        }
    }
}