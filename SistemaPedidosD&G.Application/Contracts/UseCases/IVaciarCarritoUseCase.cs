using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.Contracts.UseCases
{
    public interface IVaciarCarritoUseCase
    {
        Task EjecutarAsync(string clienteId);
    }
}