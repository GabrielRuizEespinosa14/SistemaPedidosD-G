using SistemaPedidosD_G.Application.DTO;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.Contracts.UseCases
{
    public interface IProcesarCheckoutUseCase
    {
        Task EjecutarAsync(CheckoutDTO checkout);
    }
}