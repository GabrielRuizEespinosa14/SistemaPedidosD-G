using System;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.Contracts.UseCases
{
    public interface IActivarProductoUseCase
    {
        Task Ejecutar(Guid id);
    }
}