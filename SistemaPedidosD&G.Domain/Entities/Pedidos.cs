using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Domain.Entities
{
    public class Pedidos
    {
        //Propiedades
        public Guid Id { get; private set; }
        public string CodigoPedido { get; private set; } = null!;
        public string Nombre { get; private set; } = null!;
        public string Direccion { get; private set; } = null!;
        //Propiedades de navegación
        //Constructores
        public Pedidos (string codigoPedido, string nombre, string direccion)
        {
            Id = Guid.NewGuid();
            CodigoPedido = codigoPedido;
            Nombre = nombre;
            Direccion = direccion;
        }
        //Métodosss
    }
}
