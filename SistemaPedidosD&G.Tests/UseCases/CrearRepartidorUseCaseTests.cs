using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Application.UseCases;
using SistemaPedidosD_G.Domain.Entities;
using SistemaPedidosD_G.Domain.Exceptions;

namespace SistemaPedidosD_G.Tests.UseCases;

[TestClass]
public class CrearRepartidorUseCaseTests
{
    [TestMethod]
    public async Task EjecutarAsync_TelefonoNuevo_CreaRepartidorDisponible()
    {
        var repositorio = new Mock<IRepartidorRepositorio>();

        repositorio
            .Setup(r => r.ExistePorTelefonoAsync("8095551234", null))
            .ReturnsAsync(false);

        repositorio
            .Setup(r => r.AgregarAsync(It.IsAny<Repartidor>()))
            .Returns(Task.CompletedTask);

        var useCase = new CrearRepartidorUseCase(repositorio.Object);

        var id = await useCase.EjecutarAsync(new CrearRepartidorRequest
        {
            Nombre = "Manuel Pérez",
            Telefono = "8095551234"
        });

        Assert.AreNotEqual(Guid.Empty, id);

        repositorio.Verify(
            r => r.AgregarAsync(It.Is<Repartidor>(repartidor =>
                repartidor.Nombre == "Manuel Pérez" &&
                repartidor.Telefono.Valor == "8095551234" &&
                repartidor.EstaDisponible())),
            Times.Once);
    }

    [TestMethod]
    public async Task EjecutarAsync_TelefonoExistente_LanzaExcepcionYNoCreaRepartidor()
    {
        var repositorio = new Mock<IRepartidorRepositorio>();

        repositorio
            .Setup(r => r.ExistePorTelefonoAsync("8095551234", null))
            .ReturnsAsync(true);

        var useCase = new CrearRepartidorUseCase(repositorio.Object);

        await Assert.ThrowsExceptionAsync<TelefonoRepartidorDuplicadoException>(
            () => useCase.EjecutarAsync(new CrearRepartidorRequest
            {
                Nombre = "Manuel Pérez",
                Telefono = "8095551234"
            }));

        repositorio.Verify(
            r => r.AgregarAsync(It.IsAny<Repartidor>()),
            Times.Never);
    }
}