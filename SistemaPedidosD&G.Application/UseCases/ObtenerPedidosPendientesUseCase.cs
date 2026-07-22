using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Application.DTO;
using SistemaPedidosD_G.Domain.Enums;

namespace SistemaPedidosD_G.Application.UseCases
{
    public class ObtenerPedidosPendientesUseCase : IObtenerPedidosPendientesUseCase
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;

        public ObtenerPedidosPendientesUseCase(IPedidoRepositorio pedidoRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
        }

        public async Task<IEnumerable<PedidoDTO>> EjecutarAsync()
        {
            var pedidos = await _pedidoRepositorio.ObtenerTodosAsync();

            return pedidos
                .Where(p => p.Estado == EstadoPedido.Pendiente)
                .OrderByDescending(p => p.FechaCreacion)
                .Select(p => new PedidoDTO
                {
                    Id = p.Id,
                    NumeroPedido = p.NumeroPedido.ToString(),
                    NombreCliente = p.NombreCliente,
                    Telefono = p.Telefono.ToString(),
                    MetodoEntrega = p.MetodoEntrega,
                    Direccion = p.Direccion?.ToString(),
                    Estado = p.Estado,
                    FechaCreacion = p.FechaCreacion,
                    Total = p.ObtenerTotal().Valor,
                    Detalles = p.Detalles.Select(d => new DetallePedidoDTO
                    {
                        ProductoId = d.ProductoId,
                        NombreProducto = d.NombreProducto,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario.Valor,
                        Subtotal = d.ObtenerSubtotal().Valor
                    }).ToList()
                });
        }
    }
}