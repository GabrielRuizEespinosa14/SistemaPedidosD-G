using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.DTO;
using SistemaPedidosD_G.Application.Services;
using SistemaPedidosD_G.Application.UseCases;
using SistemaPedidosD_G.Domain.Entities;
using SistemaPedidosD_G.Domain.Enums;
using SistemaPedidosD_G.Domain.Exceptions;

namespace SistemaPedidosD_G.Tests.UseCases;

[TestClass]
public class ConfirmarPedidoUseCaseTests
{
    [TestMethod]
    public async Task EjecutarAsync_StockDisponible_ReservaStockYGuardaProducto()
    {
        // Arrange
        var producto = new Producto(
            "Arroz",
            "Arroz premium",
            100m,
            10,
            "imagen.jpg");

        var productoRepositorio = new Mock<IRepositorio>();
        productoRepositorio
            .Setup(r => r.ObtenerPorIdAsync(producto.Id))
            .ReturnsAsync(producto);

        productoRepositorio
            .Setup(r => r.ActualizarAsync(producto))
            .Returns(Task.CompletedTask);

        var pedidoRepositorio = new Mock<IPedidoRepositorio>();
        pedidoRepositorio
            .Setup(r => r.AgregarAsync(It.IsAny<Pedido>()))
            .Returns(Task.CompletedTask);

        var carritoService = new CarritoService();
        carritoService.AgregarItem(
            "cliente-1",
            producto.Id,
            producto.Nombre,
            3,
            producto.Precio);

        var useCase = new ConfirmarPedidoUseCase(
            carritoService,
            pedidoRepositorio.Object,
            productoRepositorio.Object);

        var checkout = new CheckoutDTO
        {
            ClienteId = "cliente-1",
            NombreCliente = "Gabriel Ruiz",
            Telefono = "8095551234",
            MetodoEntrega = MetodoEntrega.Delivery,
            Calle = "Calle Principal",
            Sector = "Centro",
            Referencia = "Casa azul"
        };

        // Act
        var pedido = await useCase.EjecutarAsync(checkout);

        // Assert
        Assert.AreEqual(7, producto.Stock);
        Assert.AreEqual(300m, pedido.Total);

        Assert.IsFalse(string.IsNullOrWhiteSpace(pedido.NumeroPedido));
        Assert.AreEqual(EstadoPedido.Pendiente, pedido.Estado);
        Assert.IsTrue(pedido.FechaCreacion <= DateTime.UtcNow);

        productoRepositorio.Verify(
            r => r.ActualizarAsync(producto),
            Times.Once);

        pedidoRepositorio.Verify(
            r => r.AgregarAsync(It.IsAny<Pedido>()),
            Times.Once);
    }
    [TestMethod]
    public async Task EjecutarAsync_StockInsuficiente_RechazaPedidoSinReservarStock()
    {
        // Arrange
        var producto = new Producto(
            "Arroz",
            "Arroz premium",
            100m,
            2,
            "imagen.jpg");

        var productoRepositorio = new Mock<IRepositorio>();
        productoRepositorio
            .Setup(r => r.ObtenerPorIdAsync(producto.Id))
            .ReturnsAsync(producto);

        var pedidoRepositorio = new Mock<IPedidoRepositorio>();

        var carritoService = new CarritoService();
        carritoService.AgregarItem(
            "cliente-1",
            producto.Id,
            producto.Nombre,
            3,
            producto.Precio);

        var useCase = new ConfirmarPedidoUseCase(
            carritoService,
            pedidoRepositorio.Object,
            productoRepositorio.Object);

        var checkout = new CheckoutDTO
        {
            ClienteId = "cliente-1",
            NombreCliente = "Gabriel Ruiz",
            Telefono = "8095551234",
            MetodoEntrega = MetodoEntrega.Delivery,
            Calle = "Calle Principal",
            Sector = "Centro"
        };

        // Act y Assert
        await Assert.ThrowsExceptionAsync<StockInsuficienteException>(
            () => useCase.EjecutarAsync(checkout));

        Assert.AreEqual(2, producto.Stock);

        productoRepositorio.Verify(
            r => r.ActualizarAsync(It.IsAny<Producto>()),
            Times.Never);

        pedidoRepositorio.Verify(
            r => r.AgregarAsync(It.IsAny<Pedido>()),
            Times.Never);
    }
}