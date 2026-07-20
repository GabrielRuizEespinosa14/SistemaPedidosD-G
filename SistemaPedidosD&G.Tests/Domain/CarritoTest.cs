using SistemaPedidosD_G.Domain.Entities;
using System;

namespace SistemaPedidosD_G.Tests.Domain
{
    [TestClass]
    public class CarritoTests
    {
        [TestMethod]
        public void ObtenerTotal_ConMultiplesItems_SumaDinamicamenteLosSubtotales()
        {
            var carrito = new Carrito("cliente-1");
            carrito.AgregarItem(Guid.NewGuid(), "Arroz", 2, 50m);
            carrito.AgregarItem(Guid.NewGuid(), "Habichuela", 3, 40m);

            var total = carrito.ObtenerTotal();

            Assert.AreEqual(220m, total);
        }

        [TestMethod]
        public void ObtenerTotal_AlModificarCantidad_RecalculaElTotal()
        {
            var productoId = Guid.NewGuid();
            var carrito = new Carrito("cliente-1");
            carrito.AgregarItem(productoId, "Arroz", 2, 50m);

            carrito.ModificarCantidadItem(productoId, 5);

            Assert.AreEqual(250m, carrito.ObtenerTotal());
        }

        [TestMethod]
        public void ObtenerTotal_AlAgregarElMismoProductoDosVeces_AcumulaCantidadYTotal()
        {
            var productoId = Guid.NewGuid();
            var carrito = new Carrito("cliente-1");

            carrito.AgregarItem(productoId, "Arroz", 2, 50m);
            carrito.AgregarItem(productoId, "Arroz", 3, 50m);

            Assert.AreEqual(1, carrito.Items.Count);
            Assert.AreEqual(5, carrito.Items[0].Cantidad);
            Assert.AreEqual(250m, carrito.ObtenerTotal());
        }

        [TestMethod]
        public void ObtenerTotal_CarritoVacio_EsCero()
        {
            var carrito = new Carrito("cliente-1");

            Assert.AreEqual(0m, carrito.ObtenerTotal());
            Assert.IsTrue(carrito.EstaVacio());
        }
    }
}