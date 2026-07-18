using System;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.Contracts.UseCases
{
    public interface IAgregarItemAlCarritoUseCase
    {
        Task EjecutarAsync(AgregarItemRequest request);
    }

    public class AgregarItemRequest
    {
        public string ClienteId { get; set; } = null!;
        public Guid ProductoId { get; set; }
        public int Cantidad { get; set; }
    }
}
