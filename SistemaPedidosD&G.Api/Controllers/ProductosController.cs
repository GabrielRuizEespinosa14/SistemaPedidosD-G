using Microsoft.AspNetCore.Mvc;
using SistemaPedidosD_G.Application.UseCases;

namespace SistemaPedidosD_G.Api.Controllers
{

    public class ProductosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        
            private readonly ActivarProductoUseCase _activarProducto;
            private readonly DesactivarProductoUseCase _desactivarProducto;
        private readonly BuscarProductosUseCase _buscarProductos;

        public ProductosController(
                ActivarProductoUseCase activarProducto,
                DesactivarProductoUseCase desactivarProducto, BuscarProductosUseCase buscarProductos)
            {
                _activarProducto = activarProducto;
                _desactivarProducto = desactivarProducto;
            _buscarProductos = buscarProductos;
        }
       


        [HttpPut("{id}/activar")]
            public async Task<IActionResult> Activar(Guid id)
            {
                await _activarProducto.Ejecutar(id);

                return Ok(new
                {
                    mensaje = "Producto activado correctamente"
                });
            }



            [HttpPut("{id}/desactivar")]
            public async Task<IActionResult> Desactivar(Guid id)
            {
                await _desactivarProducto.Ejecutar(id);

                return Ok(new
                {
                    mensaje = "Producto desactivado correctamente"
                });
            
             }


        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar([FromQuery] string nombre)
        {
            var productos = await _buscarProductos.Ejecutar(nombre);

            if (!productos.Any())
            {
                return NotFound(new
                {
                    Mensaje = "No hay productos que coincidan con la búsqueda."
                });
            }

            return Ok(productos);
        }
    }
}
