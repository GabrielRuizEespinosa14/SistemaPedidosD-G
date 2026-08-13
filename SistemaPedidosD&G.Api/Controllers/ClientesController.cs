using Microsoft.AspNetCore.Mvc;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Domain.Exceptions;

namespace SistemaPedidosD_G.Api.Controllers;

[ApiController]
[Route("api/clientes")]
public class ClientesController : ControllerBase
{
    private readonly IRegistrarClienteUseCase _registrarClienteUseCase;

    public ClientesController(IRegistrarClienteUseCase registrarClienteUseCase)
    {
        _registrarClienteUseCase = registrarClienteUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Registrar(
        [FromBody] RegistrarClienteRequest request)
    {
        try
        {
            var clienteId = await _registrarClienteUseCase.EjecutarAsync(request);

            return StatusCode(StatusCodes.Status201Created, new
            {
                id = clienteId,
                mensaje = "Cliente registrado correctamente."
            });
        }
        catch (ClienteDuplicadoException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}
