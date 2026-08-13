using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.Services;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Application.DTO;
using SistemaPedidosD_G.Domain.Aggregates.AggregatesPedido;
using SistemaPedidosD_G.Domain.Enums;
using SistemaPedidosD_G.Domain.Exceptions;
using SistemaPedidosD_G.Domain.ValueObjects;

namespace SistemaPedidosD_G.Application.UseCases
{
    public class ConfirmarPedidoUseCase : IConfirmarPedidoUseCase
    {
        private readonly ICarritoService _carritoService;
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IRepositorio _productoRepositorio;

        public ConfirmarPedidoUseCase(
            ICarritoService carritoService,
            IPedidoRepositorio pedidoRepositorio,
            IRepositorio productoRepositorio)
        {
            _carritoService = carritoService;
            _pedidoRepositorio = pedidoRepositorio;
            _productoRepositorio = productoRepositorio;
        }

        public async Task<PedidoDTO> EjecutarAsync(CheckoutDTO checkout)
        {
            // 1. Verificar que el carrito no esté vacío
            var carrito = _carritoService.ObtenerCarrito(checkout.ClienteId);
            if (carrito.EstaVacio())
                throw new CarritoVacioException();

            // 2. Construir dirección si es delivery
            Direccion? direccion = null;
            if (checkout.MetodoEntrega == MetodoEntrega.Delivery)
            {
                if (string.IsNullOrWhiteSpace(checkout.Calle) ||
                    string.IsNullOrWhiteSpace(checkout.Sector))
                    throw new ArgumentException(
                        "La calle y el sector son obligatorios para delivery.");

                direccion = new Direccion(
                    checkout.Calle!,
                    checkout.Sector!,
                    checkout.Referencia ?? string.Empty
                );
            }

            // 3. Crear el pedido
            var pedido = new Pedido(
                checkout.ClienteId,
                checkout.NombreCliente,
                checkout.Telefono,
                checkout.MetodoEntrega,
                direccion
            );

            // 4. Agregar detalles del carrito al pedido
            // y verificar stock de cada producto
            foreach (var item in carrito.Items)
            {
                var producto = await _productoRepositorio
                    .ObtenerPorIdAsync(item.ProductoId);

                if (producto == null)
                    throw new ProductoNoEncontradoException(item.ProductoId);

                if (producto.Stock < item.Cantidad)
                    throw new StockInsuficienteException(
                        item.ProductoId, item.Cantidad, producto.Stock);

                pedido.AgregarDetalle(
                    item.ProductoId,
                    item.NombreProducto,
                    item.Cantidad,
                    item.PrecioUnitario
                );
                producto.ReservarStock(item.Cantidad);

                await _productoRepositorio.ActualizarAsync(producto);
            }

            // 5. Guardar el pedido en la BD
            await _pedidoRepositorio.AgregarAsync(pedido);

            // 6. Vaciar el carrito
            _carritoService.Vaciar(checkout.ClienteId);

            // 7. Retornar el DTO del pedido
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
                Detalles = pedido.Detalles.Select(d => new DetallePedidoDTO
                {
                    ProductoId = d.ProductoId,
                    NombreProducto = d.NombreProducto,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario.Valor,
                    Subtotal = d.ObtenerSubtotal().Valor
                }).ToList()
            };
        }
    }
}
