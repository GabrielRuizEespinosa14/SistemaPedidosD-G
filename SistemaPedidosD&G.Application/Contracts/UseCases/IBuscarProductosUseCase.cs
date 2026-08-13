using SistemaPedidosD_G.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.Contracts.UseCases
{
    public interface IBuscarProductosUseCase
    {
        Task<IEnumerable<ProductoDTO>> Ejecutar(string? nombre = null);
    }
}