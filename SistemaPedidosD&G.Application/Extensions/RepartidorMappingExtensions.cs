using SistemaPedidosD_G.Application.DTO;
using SistemaPedidosD_G.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.Extensions
{
    public static class RepartidorMappingExtensions
    {
        public static RepartidorDTO ToDTO(this Repartidor repartidor)
        {
            return new RepartidorDTO
            {
                Id = repartidor.Id,
                Nombre = repartidor.Nombre,
                Telefono = repartidor.Telefono.Valor,
                Estado = repartidor.Estado,
                FechaRegistro = repartidor.FechaRegistro
            };
        }
    }
}
