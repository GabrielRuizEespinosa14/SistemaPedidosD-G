using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Application.UseCases;
using SistemaPedidosD_G.Domain.Entities;
using SistemaPedidosD_G.Domain.Exceptions;

namespace SistemaPedidosD_G.Tests.UseCases;

[TestClass]
public class ActualizarRepartidorUseCaseTests
{
    [TestMethod]
    public async Task EjecutarAsync_DatosValidos_ActualizaRepartidor()
    {
        var repartidor = new Repartidor(
            "Manuel Pérez",
            "8095551234");

        var repositorio = new Mock<IRepartidorRepositorio>();

        repositorio
            .Setup(r => r.ObtenerPorIdAsync(repartidor.Id))
            .ReturnsAsync(repartidor);

        repositorio
            .Setup(r => r.ExistePorTelefonoAsync(
                "8295554321",
                repartidor.Id))
            .ReturnsAsync(false);

        repositorio
            .Setup(r => r.ActualizarAsync(repartidor))
            .Returns(Task.CompletedTask);

        var useCase = new ActualizarRepartidorUseCase(repositorio.Object);

        await useCase.EjecutarAsync(
            repartidor.Id,
            new ActualizarRepartidorRequest
            {
                Nombre = "Manuel Rodríguez",
                Telefono = "8295554321"
            });

        Assert.AreEqual("Manuel Rodríguez", repartidor.Nombre);
        Assert.AreEqual("8295554321", repartidor.Telefono.Valor);

        repositorio.Verify(
            r => r.ActualizarAsync(repartidor),
            Times.Once);
    }

    [TestMethod]
    public async Task EjecutarAsync_TelefonoDeOtroRepartidor_LanzaExcepcion()
    {
        var repartidor = new Repartidor(
            "Manuel Pérez",
            "8095551234");

        var repositorio = new Mock<IRepartidorRepositorio>();

        repositorio
            .Setup(r => r.ObtenerPorIdAsync(repartidor.Id))
            .ReturnsAsync(repartidor);

        repositorio
            .Setup(r => r.ExistePorTelefonoAsync(
                "8295554321",
                repartidor.Id))
            .ReturnsAsync(true);

        var useCase = new ActualizarRepartidorUseCase(repositorio.Object);

        await Assert.ThrowsExceptionAsync<TelefonoRepartidorDuplicadoException>(
            () => useCase.EjecutarAsync(
                repartidor.Id,
                new ActualizarRepartidorRequest
                {
                    Nombre = "Manuel Rodríguez",
                    Telefono = "8295554321"
                }));

        repositorio.Verify(
            r => r.ActualizarAsync(It.IsAny<Repartidor>()),
            Times.Never);
    }
}