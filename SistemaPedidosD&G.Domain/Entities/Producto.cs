using System;

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

        public void Activar() => Activo = true;
        public void Desactivar() => Activo = false;
        public bool EstaAgotado() => Stock <= 0;
    }
}