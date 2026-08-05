using Microsoft.AspNetCore.Mvc;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Domain.Exceptions;

namespace SistemaPedidosD_G.Api.Controllers;

[ApiController]
[Route("api/repartidores")]
public class RepartidoresController : ControllerBase
{
    private readonly ICrearRepartidorUseCase _crearRepartidorUseCase;
    private readonly IActualizarRepartidorUseCase _actualizarRepartidorUseCase;
    private readonly IDesactivarRepartidorUseCase _desactivarRepartidorUseCase;
    private readonly IObtenerRepartidoresUseCase _obtenerRepartidoresUseCase;
    private readonly IObtenerRepartidoresDisponiblesUseCase
        _obtenerRepartidoresDisponiblesUseCase;

    public RepartidoresController(
        ICrearRepartidorUseCase crearRepartidorUseCase,
        IActualizarRepartidorUseCase actualizarRepartidorUseCase,
        IDesactivarRepartidorUseCase desactivarRepartidorUseCase,
        IObtenerRepartidoresUseCase obtenerRepartidoresUseCase,
        IObtenerRepartidoresDisponiblesUseCase
            obtenerRepartidoresDisponiblesUseCase)
    {
        _crearRepartidorUseCase = crearRepartidorUseCase;
        _actualizarRepartidorUseCase = actualizarRepartidorUseCase;
        _desactivarRepartidorUseCase = desactivarRepartidorUseCase;
        _obtenerRepartidoresUseCase = obtenerRepartidoresUseCase;
        _obtenerRepartidoresDisponiblesUseCase =
            obtenerRepartidoresDisponiblesUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Crear(
        [FromBody] CrearRepartidorRequest request)
    {
        try
        {
            var repartidorId = await _crearRepartidorUseCase
                .EjecutarAsync(request);

            return StatusCode(StatusCodes.Status201Created, new
            {
                id = repartidorId,
                mensaje = "Repartidor registrado correctamente."
            });
        }
        catch (TelefonoRepartidorDuplicadoException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Actualizar(
        Guid id,
        [FromBody] ActualizarRepartidorRequest request)
    {
        try
        {
            await _actualizarRepartidorUseCase.EjecutarAsync(id, request);

            return Ok(new
            {
                mensaje = "Repartidor actualizado correctamente."
            });
        }
        catch (RepartidorNoEncontradoException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
        catch (TelefonoRepartidorDuplicadoException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPatch("{id:guid}/desactivar")]
    public async Task<IActionResult> Desactivar(Guid id)
    {
        try
        {
            await _desactivarRepartidorUseCase.EjecutarAsync(id);

            return Ok(new
            {
                mensaje = "Repartidor desactivado correctamente."
            });
        }
        catch (RepartidorNoEncontradoException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var repartidores = await _obtenerRepartidoresUseCase
            .EjecutarAsync();

        return Ok(repartidores);
    }

    [HttpGet("disponibles")]
    public async Task<IActionResult> ObtenerDisponibles()
    {
        var repartidores = await _obtenerRepartidoresDisponiblesUseCase
            .EjecutarAsync();

        return Ok(repartidores);
    }
}