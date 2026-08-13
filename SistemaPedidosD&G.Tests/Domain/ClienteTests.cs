using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaPedidosD_G.Domain.Entities;

namespace SistemaPedidosD_G.Tests.Domain;

[TestClass]
public class ClienteTests
{
    [TestMethod]
    public void CrearCliente_ConDatosValidos_CreaClienteCorrectamente()
    {
        var cliente = new Cliente(
            "Manuel Arquimedes",
            "manuel@email.com",
            "8095551234");

        Assert.AreNotEqual(Guid.Empty, cliente.Id);
        Assert.AreEqual("Manuel Arquimedes", cliente.Nombre);
        Assert.AreEqual("manuel@email.com", cliente.Correo);
        Assert.AreEqual("8095551234", cliente.Telefono.Valor);
    }

    [TestMethod]
    public void CrearCliente_CorreoInvalido_LanzaExcepcion()
    {
        Assert.ThrowsException<ArgumentException>(() =>
            new Cliente("Manuel Arquimedes", "correo-invalido", "8095551234"));
    }
}
