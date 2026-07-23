using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaPedidosD_G.Domain.Entities;
using SistemaPedidosD_G.Domain.Exceptions;

namespace SistemaPedidosD_G.Tests.Domain;

[TestClass]
public class ProductoStockTests
{
    [TestMethod]
    public void ReservarStock_CantidadDisponible_DescuentaElStock()
    {
        var producto = new Producto(
            "Arroz",
            "Arroz premium",
            100m,
            10,
            "imagen.jpg");

        producto.ReservarStock(3);

        Assert.AreEqual(7, producto.Stock);
    }

    [TestMethod]
    public void ReservarStock_CantidadMayorAlStock_LanzaExcepcionYNoModificaStock()
    {
        var producto = new Producto(
            "Arroz",
            "Arroz premium",
            100m,
            5,
            "imagen.jpg");

        Assert.ThrowsException<StockInsuficienteException>(
            () => producto.ReservarStock(6));

        Assert.AreEqual(5, producto.Stock);
    }

    [TestMethod]
    public void DevolverStock_CantidadValida_AumentaElStock()
    {
        var producto = new Producto(
            "Arroz",
            "Arroz premium",
            100m,
            10,
            "imagen.jpg");

        producto.ReservarStock(4);
        producto.DevolverStock(4);

        Assert.AreEqual(10, producto.Stock);
    }
}