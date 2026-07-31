using Microsoft.AspNetCore.Mvc;
using SistemaPedidosD_G.Application.Contracts.UseCases;

namespace SistemaPedidosD_G.Api.Controllers
{
    public class PedidosController : Controller
    {
        private readonly ICancelarPedidoUseCase _cancelarPedidoUseCase;

        public PedidosController(
            ICancelarPedidoUseCase cancelarPedidoUseCase)
        {
            _cancelarPedidoUseCase = cancelarPedidoUseCase;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPut("{id}/cancelar")]
        public async Task<IActionResult> Cancelar(Guid id)
        {
            try
            {
                await _cancelarPedidoUseCase.EjecutarAsync(id);

                return Ok(new
                {
                    success = true,
                    message = "Pedido cancelado y stock restaurado correctamente."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}
