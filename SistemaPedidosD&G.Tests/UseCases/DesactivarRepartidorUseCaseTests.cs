using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.UseCases;
using SistemaPedidosD_G.Domain.Entities;
using SistemaPedidosD_G.Domain.Enums;

namespace SistemaPedidosD_G.Tests.UseCases;

[TestClass]
public class DesactivarRepartidorUseCaseTests
{
    [TestMethod]
    public async Task EjecutarAsync_RepartidorExistente_LoDesactivaSinEliminarlo()
    {
        var repartidor = new Repartidor(
            "Manuel Pérez",
            "8095551234");

        var repositorio = new Mock<IRepartidorRepositorio>();

        repositorio
            .Setup(r => r.ObtenerPorIdAsync(repartidor.Id))
            .ReturnsAsync(repartidor);

        repositorio
            .Setup(r => r.ActualizarAsync(repartidor))
            .Returns(Task.CompletedTask);

        var useCase = new DesactivarRepartidorUseCase(repositorio.Object);

        await useCase.EjecutarAsync(repartidor.Id);

        Assert.AreEqual(EstadoRepartidor.Inactivo, repartidor.Estado);

        repositorio.Verify(
            r => r.ActualizarAsync(repartidor),
            Times.Once);
    }
}