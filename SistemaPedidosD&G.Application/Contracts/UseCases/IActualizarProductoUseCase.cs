using System;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.Contracts.UseCases
{
    public interface IActualizarProductoUseCase
    {
        Task EjecutarAsync(Guid id, ActualizarProductoRequest request);
    }

    public class ActualizarProductoRequest
    {
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string ImagenUrl { get; set; } = null!;
    }
}