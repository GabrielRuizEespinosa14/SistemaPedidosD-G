using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaPedidosD_G.Domain.Entities;
using SistemaPedidosD_G.Domain.Enums;

namespace SistemaPedidosD_G.Tests.Domain;

[TestClass]
public class RepartidorTests
{
    [TestMethod]
    public void CrearRepartidor_ConDatosValidos_EmpiezaDisponible()
    {
        var repartidor = new Repartidor(
            "Manuel Pérez",
            "8095551234");

        Assert.AreEqual("Manuel Pérez", repartidor.Nombre);
        Assert.AreEqual("8095551234", repartidor.Telefono.ToString());
        Assert.AreEqual(EstadoRepartidor.Disponible, repartidor.Estado);
    }

    [TestMethod]
    public void ActualizarDatos_ConDatosValidos_ActualizaNombreYTelefono()
    {
        var repartidor = new Repartidor(
            "Manuel Pérez",
            "8095551234");

        repartidor.ActualizarDatos(
            "Manuel Rodríguez",
            "8295554321");

        Assert.AreEqual("Manuel Rodríguez", repartidor.Nombre);
        Assert.AreEqual("8295554321", repartidor.Telefono.ToString());
    }

    [TestMethod]
    public void Desactivar_RepartidorDisponible_CambiaEstadoAInactivo()
    {
        var repartidor = new Repartidor(
            "Manuel Pérez",
            "8095551234");

        repartidor.Desactivar();

        Assert.AreEqual(EstadoRepartidor.Inactivo, repartidor.Estado);
        Assert.IsFalse(repartidor.EstaDisponible());
    }
}