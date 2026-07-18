using System;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.Contracts.UseCases
{
    public interface IModificarCantidadItemUseCase
    {
        Task EjecutarAsync(ModificarCantidadRequest request);
    }

    public class ModificarCantidadRequest
    {
        public string ClienteId { get; set; } = null!;
        public Guid ProductoId { get; set; }  
        public int NuevaCantidad { get; set; }
    }
}
