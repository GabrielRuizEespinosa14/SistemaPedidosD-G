using System;
using SistemaPedidosD_G.Domain.Exceptions;

namespace SistemaPedidosD_G.Domain.Entities
{
    public class Producto
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;
        public string Descripcion { get; private set; } = null!;
        public decimal Precio { get; private set; }
        public int Stock { get; private set; }
        public string ImagenUrl { get; private set; } = null!;
        public bool Activo { get; private set; }


        private Producto() { } // Requerido por EF Core

        public Producto(string nombre, string descripcion, decimal precio, int stock, string imagenUrl)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            Stock = stock;
            ImagenUrl = imagenUrl;
            Activo = true;
        }

        public void Actualizar(string nombre, string descripcion, decimal precio, int stock, string imagenUrl)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            Stock = stock;
            ImagenUrl = imagenUrl;
        }
        public void ReservarStock(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad a reservar debe ser mayor que cero.");

            if (Stock < cantidad)
                throw new StockInsuficienteException(Id, cantidad, Stock);

            Stock -= cantidad;
        }

        public void DevolverStock(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad a devolver debe ser mayor que cero.");

            Stock += cantidad;
        }
        public void Activar() => Activo = true;
        public void Desactivar() => Activo = false;
        public bool EstaAgotado() => Stock <= 0;

    }
}