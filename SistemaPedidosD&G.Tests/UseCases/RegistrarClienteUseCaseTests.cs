using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Application.UseCases;
using SistemaPedidosD_G.Domain.Entities;
using SistemaPedidosD_G.Domain.Exceptions;

namespace SistemaPedidosD_G.Tests.UseCases;

[TestClass]
public class RegistrarClienteUseCaseTests
{
    [TestMethod]
    public async Task EjecutarAsync_DatosUnicos_RegistraCliente()
    {
        var repositorio = new Mock<IClienteRepositorio>();
        repositorio.Setup(r => r.ExistePorCorreoAsync("manuel@email.com"))
            .ReturnsAsync(false);
        repositorio.Setup(r => r.ExistePorTelefonoAsync("8095551234"))
            .ReturnsAsync(false);
        repositorio.Setup(r => r.AgregarAsync(It.IsAny<Cliente>()))
            .Returns(Task.CompletedTask);

        var useCase = new RegistrarClienteUseCase(repositorio.Object);

        var id = await useCase.EjecutarAsync(new RegistrarClienteRequest
        {
            Nombre = "Manuel Arquimedes",
            Correo = "MANUEL@EMAIL.COM",
            Telefono = "8095551234"
        });

        Assert.AreNotEqual(Guid.Empty, id);
        repositorio.Verify(r => r.AgregarAsync(It.Is<Cliente>(cliente =>
            cliente.Nombre == "Manuel Arquimedes" &&
            cliente.Correo == "manuel@email.com" &&
            cliente.Telefono.Valor == "8095551234")), Times.Once);
    }

    [TestMethod]
    public async Task EjecutarAsync_CorreoExistente_NoRegistraCliente()
    {
        var repositorio = new Mock<IClienteRepositorio>();
        repositorio.Setup(r => r.ExistePorCorreoAsync("manuel@email.com"))
            .ReturnsAsync(true);

        var useCase = new RegistrarClienteUseCase(repositorio.Object);

        await Assert.ThrowsExceptionAsync<ClienteDuplicadoException>(() =>
            useCase.EjecutarAsync(new RegistrarClienteRequest
            {
                Nombre = "Manuel Arquimedes",
                Correo = "manuel@email.com",
                Telefono = "8095551234"
            }));

        repositorio.Verify(r => r.AgregarAsync(It.IsAny<Cliente>()), Times.Never);
    }

    [TestMethod]
    public async Task EjecutarAsync_TelefonoExistente_NoRegistraCliente()
    {
        var repositorio = new Mock<IClienteRepositorio>();
        repositorio.Setup(r => r.ExistePorCorreoAsync("manuel@email.com"))
            .ReturnsAsync(false);
        repositorio.Setup(r => r.ExistePorTelefonoAsync("8095551234"))
            .ReturnsAsync(true);

        var useCase = new RegistrarClienteUseCase(repositorio.Object);

        await Assert.ThrowsExceptionAsync<ClienteDuplicadoException>(() =>
            useCase.EjecutarAsync(new RegistrarClienteRequest
            {
                Nombre = "Manuel Arquimedes",
                Correo = "manuel@email.com",
                Telefono = "8095551234"
            }));

        repositorio.Verify(r => r.AgregarAsync(It.IsAny<Cliente>()), Times.Never);
    }
}
