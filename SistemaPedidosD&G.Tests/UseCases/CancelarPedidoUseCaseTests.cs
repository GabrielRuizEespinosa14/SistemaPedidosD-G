using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.UseCases;
using SistemaPedidosD_G.Domain.Aggregates;
using SistemaPedidosD_G.Domain.Enums;
using SistemaPedidosD_G.Domain.ValueObjects;
using SistemaPedidosD_G.Domain.Exceptions;
using SistemaPedidosD_G.Domain.Aggregates.AggregatesPedido;
using SistemaPedidosD_G.Domain.Entities;

namespace SistemaPedidosD_G.Tests.UseCases;

[TestClass]
public class CancelarPedidoUseCaseTests
{
    [TestMethod]
    public async Task EjecutarAsync_PedidoPendiente_DevuelveStockYActualizaProducto()
    {
        // Arrange
        var producto = new Producto(
            "Arroz",
            "Arroz premium",
            100m,
            10,
            "imagen.jpg");

        // Simula que HU-10 ya reservó 3 unidades al confirmar.
        producto.ReservarStock(3);

        var pedido = new Pedido(
            "cliente-1",
            "Gabriel Ruiz",
            "8095551234",
            MetodoEntrega.Delivery,
            new Direccion("Calle Principal", "Centro", "Casa azul"));

        pedido.AgregarDetalle(
            producto.Id,
            producto.Nombre,
            3,
            producto.Precio);

        var pedidoRepositorio = new Mock<IPedidoRepositorio>();
        pedidoRepositorio
            .Setup(r => r.ObtenerPorIdAsync(pedido.Id))
            .ReturnsAsync(pedido);

        pedidoRepositorio
            .Setup(r => r.ActualizarAsync(pedido))
            .Returns(Task.CompletedTask);

        var productoRepositorio = new Mock<IRepositorio>();
        productoRepositorio
            .Setup(r => r.ObtenerPorIdAsync(producto.Id))
            .ReturnsAsync(producto);

        productoRepositorio
            .Setup(r => r.ActualizarAsync(producto))
            .Returns(Task.CompletedTask);

        var unitOfWork = new Mock<IUnitOfWork>();

        var useCase = new CancelarPedidoUseCase(
            pedidoRepositorio.Object,
            productoRepositorio.Object,
            unitOfWork.Object);

        // Act
        await useCase.EjecutarAsync(pedido.Id);

        // Assert
        Assert.AreEqual(10, producto.Stock);
        Assert.AreEqual(EstadoPedido.Cancelado, pedido.Estado);

        productoRepositorio.Verify(
            r => r.ActualizarAsync(producto),
            Times.Once);

        pedidoRepositorio.Verify(
            r => r.ActualizarAsync(pedido),
            Times.Once);
    }
    [TestMethod]
    public async Task EjecutarAsync_PedidoDespachado_NoPermiteCancelarNiDevuelveStock()
    {
        // Arrange
        var producto = new Producto(
            "Arroz",
            "Arroz premium",
            100m,
            10,
            "imagen.jpg");

        producto.ReservarStock(3);

        var pedido = new Pedido(
            "cliente-1",
            "Gabriel Ruiz",
            "8095551234",
            MetodoEntrega.Delivery,
            new Direccion("Calle Principal", "Centro", "Casa azul"));

        pedido.AgregarDetalle(
            producto.Id,
            producto.Nombre,
            3,
            producto.Precio);

        pedido.CambiarEstado(EstadoPedido.Despachado);

        var pedidoRepositorio = new Mock<IPedidoRepositorio>();
        pedidoRepositorio
            .Setup(r => r.ObtenerPorIdAsync(pedido.Id))
            .ReturnsAsync(pedido);

        var productoRepositorio = new Mock<IRepositorio>();
        productoRepositorio
            .Setup(r => r.ObtenerPorIdAsync(producto.Id))
            .ReturnsAsync(producto);

        var unitOfWork = new Mock<IUnitOfWork>();

        var useCase = new CancelarPedidoUseCase(
            pedidoRepositorio.Object,
            productoRepositorio.Object,
            unitOfWork.Object);

        // Act y Assert
        await Assert.ThrowsExceptionAsync<CambioEstadoInvalidoException>(
            () => useCase.EjecutarAsync(pedido.Id));

        Assert.AreEqual(7, producto.Stock);
        Assert.AreEqual(EstadoPedido.Despachado, pedido.Estado);

        productoRepositorio.Verify(
            r => r.ActualizarAsync(It.IsAny<Producto>()),
            Times.Never);

        pedidoRepositorio.Verify(
            r => r.ActualizarAsync(It.IsAny<Pedido>()),
            Times.Never);
    }
}