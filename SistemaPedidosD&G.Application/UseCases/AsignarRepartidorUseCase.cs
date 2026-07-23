using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Domain.Exceptions;

namespace SistemaPedidosD_G.Application.UseCases
{
    public class AsignarRepartidorUseCase : IAsignarRepartidorUseCase
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IRepartidorRepositorio _repartidorRepositorio;

        public AsignarRepartidorUseCase(
            IPedidoRepositorio pedidoRepositorio,
            IRepartidorRepositorio repartidorRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _repartidorRepositorio = repartidorRepositorio;
        }

        public async Task EjecutarAsync(Guid pedidoId, Guid repartidorId)
        {
            // 1. Obtener el pedido
            var pedido = await _pedidoRepositorio.ObtenerPorIdAsync(pedidoId);
            if (pedido == null)
                throw new PedidoNoEncontradoException(pedidoId);

            // 2. Obtener el repartidor
            var repartidor = await _repartidorRepositorio.ObtenerPorIdAsync(repartidorId);
            if (repartidor == null)
                throw new RepartidorNoDisponibleException(repartidorId);

            // 3. Verificar que el repartidor esté disponible
            if (!repartidor.EstaDisponible())
                throw new RepartidorNoDisponibleException(repartidorId);

            // 4. Asignar el repartidor al pedido
            pedido.AsignarRepartidor(repartidorId);

            // 5. Marcar el repartidor como ocupado
            repartidor.MarcarOcupado();

            // 6. Guardar los cambios
            await _pedidoRepositorio.ActualizarAsync(pedido);
            await _repartidorRepositorio.ActualizarAsync(repartidor);
        }
    }
}
