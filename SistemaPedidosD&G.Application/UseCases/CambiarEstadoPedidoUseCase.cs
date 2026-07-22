using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Domain.Enums;
using SistemaPedidosD_G.Domain.Exceptions;

namespace SistemaPedidosD_G.Application.UseCases
{
    public class CambiarEstadoPedidoUseCase : ICambiarEstadoPedidoUseCase
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;

        public CambiarEstadoPedidoUseCase(IPedidoRepositorio pedidoRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
        }

        public async Task EjecutarAsync(Guid pedidoId, EstadoPedido nuevoEstado)
        {
            // 1. Obtener el pedido
            var pedido = await _pedidoRepositorio.ObtenerPorIdAsync(pedidoId);
            if (pedido == null)
                throw new PedidoNoEncontradoException(pedidoId);

            // 2. Cambiar el estado usando la lógica de la entidad
            pedido.CambiarEstado(nuevoEstado);

            // 3. Guardar los cambios
            await _pedidoRepositorio.ActualizarAsync(pedido);
        }
    }
}