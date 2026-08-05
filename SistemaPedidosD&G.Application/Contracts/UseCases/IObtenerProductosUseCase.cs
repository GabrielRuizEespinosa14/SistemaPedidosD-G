using SistemaPedidosD_G.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.Contracts.UseCases
{
    public interface IObtenerProductosUseCase
    {
        Task<IEnumerable<ProductoDTO>> EjecutarAsync(bool soloActivos = true);
    }
}