using System;
using System.Collections.Generic;

namespace SistemaPedidosD_G.Application.DTOs
{
    
    public class ItemCarritoDto
    {
        public Guid ProductoId { get; set; }
        public string NombreProducto { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }


        public int StockDisponible { get; set; }

        public bool ExcedeStock { get; set; }

     
        public bool ProductoNoDisponible { get; set; }
    }


    public class CarritoDto
    {
        public string ClienteId { get; set; } = null!;
        public List<ItemCarritoDto> Items { get; set; } = new();

      
        public decimal Total { get; set; }


        public bool EsValido { get; set; }
    }
}