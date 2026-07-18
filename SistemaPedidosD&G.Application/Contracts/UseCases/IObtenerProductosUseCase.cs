using SistemaPedidosD_G.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.Contracts.UseCases
{
    public interface IObtenerProductosUseCase
    {
        Task<IEnumerable<Producto>> EjecutarAsync(bool soloActivos = true);
    }
}