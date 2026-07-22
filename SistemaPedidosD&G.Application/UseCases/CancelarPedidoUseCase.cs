using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Domain.Exceptions;

namespace SistemaPedidosD_G.Application.UseCases
{
    public class CancelarPedidoUseCase : ICancelarPedidoUseCase
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IRepositorio _productoRepositorio;

        public CancelarPedidoUseCase(
            IPedidoRepositorio pedidoRepositorio,
            IRepositorio productoRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _productoRepositorio = productoRepositorio;
        }

        public async Task EjecutarAsync(Guid pedidoId)
        {
            // 1. Obtener el pedido
            var pedido = await _pedidoRepositorio.ObtenerPorIdAsync(pedidoId);
            if (pedido == null)
                throw new PedidoNoEncontradoException(pedidoId);

            // 2. Cancelar el pedido usando la lógica de la entidad
            pedido.Cancelar();

            // 3. Retornar el stock de cada producto
            foreach (var detalle in pedido.Detalles)
            {
                var producto = await _productoRepositorio
                    .ObtenerPorIdAsync(detalle.ProductoId);

                if (producto != null)
                    producto.Actualizar(
                        producto.Nombre,
                        producto.Descripcion,
                        producto.Precio,
                        producto.Stock + detalle.Cantidad,
                        producto.ImagenUrl
                    );
            }

            // 4. Guardar los cambios del pedido
            await _pedidoRepositorio.ActualizarAsync(pedido);
        }
    }
}
