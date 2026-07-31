using SistemaPedidosD_G.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.Contracts.Persistence
{
    public interface IProductoRepository
    {

        Task<Producto?> ObtenerPorIdAsync(int id);


        Task ActualizarAsync(Producto producto);

    }
}
