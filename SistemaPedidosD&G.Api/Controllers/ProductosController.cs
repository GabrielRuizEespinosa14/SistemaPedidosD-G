using Microsoft.AspNetCore.Mvc;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Application.UseCases;

namespace SistemaPedidosD_G.Api.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductosController : ControllerBase
    {
        private readonly IObtenerProductosUseCase _obtenerProductos;
        private readonly ActivarProductoUseCase _activarProducto;
        private readonly DesactivarProductoUseCase _desactivarProducto;
        private readonly BuscarProductosUseCase _buscarProductos;

        public ProductosController(
            IObtenerProductosUseCase obtenerProductos,
            ActivarProductoUseCase activarProducto,
            DesactivarProductoUseCase desactivarProducto,
            BuscarProductosUseCase buscarProductos)
        {
            _obtenerProductos = obtenerProductos;
            _activarProducto = activarProducto;
            _desactivarProducto = desactivarProducto;
            _buscarProductos = buscarProductos;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCatalogo([FromQuery] bool soloActivos = true)
        {
            var productos = await _obtenerProductos.EjecutarAsync(soloActivos);
            return Ok(productos);
        }

        [HttpPut("{id}/activar")]
        public async Task<IActionResult> Activar(Guid id)
        {
            await _activarProducto.Ejecutar(id);
            return Ok(new { mensaje = "Producto activado correctamente" });
        }

        [HttpPut("{id}/desactivar")]
        public async Task<IActionResult> Desactivar(Guid id)
        {
            await _desactivarProducto.Ejecutar(id);
            return Ok(new { mensaje = "Producto desactivado correctamente" });
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar([FromQuery] string nombre)
        {
            var productos = await _buscarProductos.Ejecutar(nombre);

            if (!productos.Any())
            {
                return NotFound(new { mensaje = "No hay productos que coincidan con la búsqueda." });
            }

            return Ok(productos);
        }
    }
}