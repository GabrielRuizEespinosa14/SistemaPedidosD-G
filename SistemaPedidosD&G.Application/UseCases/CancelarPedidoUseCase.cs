using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Domain.Exceptions;

namespace SistemaPedidosD_G.Application.UseCases
{
    public class CancelarPedidoUseCase : ICancelarPedidoUseCase
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IRepositorio _productoRepositorio;
        private readonly IUnitOfWork _unitOfWork;
        public CancelarPedidoUseCase(
      IPedidoRepositorio pedidoRepositorio,
      IRepositorio productoRepositorio,
      IUnitOfWork unitOfWork)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _productoRepositorio = productoRepositorio;
            _unitOfWork = unitOfWork;
        }

        public async Task EjecutarAsync(Guid pedidoId)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var pedido = await _pedidoRepositorio.ObtenerPorIdAsync(pedidoId);

                if (pedido == null)
                    throw new PedidoNoEncontradoException(pedidoId);

                pedido.Cancelar();

                foreach (var detalle in pedido.Detalles)
                {
                    var producto = await _productoRepositorio.ObtenerPorIdAsync(detalle.ProductoId);

                    if (producto == null)
                        continue;

                    producto.DevolverStock(detalle.Cantidad);

                    await _productoRepositorio.ActualizarAsync(producto);
                }

                await _pedidoRepositorio.ActualizarAsync(pedido);

                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
