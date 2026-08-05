using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.UseCases;
using SistemaPedidosD_G.Domain.Entities;

namespace SistemaPedidosD_G.Tests.UseCases;

[TestClass]
public class ObtenerRepartidoresUseCaseTests
{
    [TestMethod]
    public async Task EjecutarAsync_ConRepartidores_DevuelveCantidadDePedidosAsignados()
    {
        var manuel = new Repartidor(
            "Manuel Pérez",
            "8095551234");

        var angel = new Repartidor(
            "Ángel Rodríguez",
            "8295554321");

        var repositorio = new Mock<IRepartidorRepositorio>();

        repositorio
            .Setup(r => r.ObtenerTodosAsync())
            .ReturnsAsync(new[] { manuel, angel });

        repositorio
            .Setup(r => r.ContarPedidosAsignadosAsync(manuel.Id))
            .ReturnsAsync(2);

        repositorio
            .Setup(r => r.ContarPedidosAsignadosAsync(angel.Id))
            .ReturnsAsync(0);

        var useCase = new ObtenerRepartidoresUseCase(repositorio.Object);

        var resultado = (await useCase.EjecutarAsync()).ToList();

        Assert.AreEqual(2, resultado.Count);

        Assert.AreEqual("Manuel Pérez", resultado[0].Nombre);
        Assert.AreEqual(2, resultado[0].PedidosAsignadosActuales);

        Assert.AreEqual("Ángel Rodríguez", resultado[1].Nombre);
        Assert.AreEqual(0, resultado[1].PedidosAsignadosActuales);
    }
}