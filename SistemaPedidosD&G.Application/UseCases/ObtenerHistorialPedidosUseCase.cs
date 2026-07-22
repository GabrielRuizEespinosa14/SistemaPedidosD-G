using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Application.DTO;
using SistemaPedidosD_G.Domain.Exceptions;

namespace SistemaPedidosD_G.Application.UseCases
{
    public class ObtenerHistorialPedidosUseCase : IObtenerHistorialPedidosUseCase
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;

        public ObtenerHistorialPedidosUseCase(IPedidoRepositorio pedidoRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
        }

        public async Task<IEnumerable<PedidoDTO>> EjecutarAsync(string clienteId)
        {
            // 1. Validar que el clienteId no esté vacío
            if (string.IsNullOrWhiteSpace(clienteId))
                throw new ArgumentException("El ID del cliente es obligatorio.");

            // 2. Obtener los pedidos del cliente
            var pedidos = await _pedidoRepositorio.ObtenerPorClienteIdAsync(clienteId);

            // 3. Verificar que el cliente tenga pedidos
            if (!pedidos.Any())
                return Enumerable.Empty<PedidoDTO>();

            // 4. Mapear y retornar ordenado por fecha descendente
            return pedidos
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
