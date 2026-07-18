using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.Contracts.UseCases
{
    public interface ICrearProductoUseCase
    {
        Task EjecutarAsync(CrearProductoRequest request);
    }

    public class CrearProductoRequest
    {
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string ImagenUrl { get; set; } = null!;
    }
}