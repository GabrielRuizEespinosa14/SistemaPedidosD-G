using Microsoft.AspNetCore.Mvc;

namespace SistemaPedidosD_G.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SaludController : ControllerBase
{
    [HttpGet]
    public IActionResult Obtener()
    {
        return Ok(new
        {
            mensaje = "La API de SistemaPedidosD-G está funcionando."
        });
    }
}