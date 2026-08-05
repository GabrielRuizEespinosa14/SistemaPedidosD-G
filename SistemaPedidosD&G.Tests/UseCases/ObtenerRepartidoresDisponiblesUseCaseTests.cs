using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.UseCases;
using SistemaPedidosD_G.Domain.Entities;
using SistemaPedidosD_G.Domain.Enums;

namespace SistemaPedidosD_G.Tests.UseCases;

[TestClass]
public class ObtenerRepartidoresDisponiblesUseCaseTests
{
    [TestMethod]
    public async Task EjecutarAsync_ConRepartidorDisponible_DevuelveSoloDisponibles()
    {
        var repartidorDisponible = new Repartidor(
            "Manuel Pérez",
            "8095551234");

        var repositorio = new Mock<IRepartidorRepositorio>();

        repositorio
            .Setup(r => r.ObtenerDisponiblesAsync())
            .ReturnsAsync(new[] { repartidorDisponible });

        var useCase = new ObtenerRepartidoresDisponiblesUseCase(
            repositorio.Object);

        var resultado = (await useCase.EjecutarAsync()).ToList();

        Assert.AreEqual(1, resultado.Count);
        Assert.AreEqual("Manuel Pérez", resultado[0].Nombre);
        Assert.AreEqual(
            EstadoRepartidor.Disponible,
            resultado[0].Estado);
        Assert.AreEqual(0, resultado[0].PedidosAsignadosActuales);
    }
}