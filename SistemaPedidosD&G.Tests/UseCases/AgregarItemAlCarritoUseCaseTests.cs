using System;
using System.Threading.Tasks;
using Moq;
using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Application.Services;
using SistemaPedidosD_G.Application.UseCases;
using SistemaPedidosD_G.Domain.Entities;
using SistemaPedidosD_G.Domain.Exceptions;

namespace SistemaPedidosD_G.Tests.UseCases
{
    [TestClass]
    public class AgregarItemAlCarritoUseCaseTests
    {
        private static Producto CrearProducto(int stock, decimal precio = 100m)
        {
            return new Producto("Arroz", "Arroz importado", precio, stock, "http://img");
        }

        [TestMethod]
        public async Task EjecutarAsync_CantidadDentroDelStock_AgregaElItem()
        {
            var producto = CrearProducto(stock: 10);
            var repoMock = new Mock<IRepositorio>();
            repoMock.Setup(r => r.ObtenerPorIdAsync(producto.Id)).ReturnsAsync(producto);

            var carritoService = new CarritoService();
            var useCase = new AgregarItemAlCarritoUseCase(carritoService, repoMock.Object);

            var request = new AgregarItemRequest { ClienteId = "cliente-1", ProductoId = producto.Id, Cantidad = 4 };

            await useCase.EjecutarAsync(request);

            var carrito = carritoService.ObtenerCarrito("cliente-1");
            Assert.AreEqual(4, carrito.Items[0].Cantidad);
        }

        [TestMethod]
        public async Task EjecutarAsync_CantidadSuperaStockDisponible_LanzaExcepcion()
        {
            var producto = CrearProducto(stock: 3);
            var repoMock = new Mock<IRepositorio>();
            repoMock.Setup(r => r.ObtenerPorIdAsync(producto.Id)).ReturnsAsync(producto);

            var carritoService = new CarritoService();
            var useCase = new AgregarItemAlCarritoUseCase(carritoService, repoMock.Object);

            var request = new AgregarItemRequest { ClienteId = "cliente-1", ProductoId = producto.Id, Cantidad = 5 };

            await Assert.ThrowsExceptionAsync<StockInsuficienteException>(() => useCase.EjecutarAsync(request));
        }

        [TestMethod]
        public async Task EjecutarAsync_YaTieneElProductoEnElCarrito_ValidaLaCantidadAcumulada()
        {
            var producto = CrearProducto(stock: 5);
            var repoMock = new Mock<IRepositorio>();
            repoMock.Setup(r => r.ObtenerPorIdAsync(producto.Id)).ReturnsAsync(producto);

            var carritoService = new CarritoService();
            var useCase = new AgregarItemAlCarritoUseCase(carritoService, repoMock.Object);

            await useCase.EjecutarAsync(new AgregarItemRequest { ClienteId = "cliente-1", ProductoId = producto.Id, Cantidad = 3 });

            await Assert.ThrowsExceptionAsync<StockInsuficienteException>(() =>
    useCase.EjecutarAsync(new AgregarItemRequest { ClienteId = "cliente-1", ProductoId = producto.Id, Cantidad = 3 }));
        }

        [TestMethod]
        public async Task EjecutarAsync_ProductoInactivo_LanzaExcepcion()
        {
            var producto = CrearProducto(stock: 10);
            producto.Desactivar();

            var repoMock = new Mock<IRepositorio>();
            repoMock.Setup(r => r.ObtenerPorIdAsync(producto.Id)).ReturnsAsync(producto);

            var carritoService = new CarritoService();
            var useCase = new AgregarItemAlCarritoUseCase(carritoService, repoMock.Object);

            var request = new AgregarItemRequest { ClienteId = "cliente-1", ProductoId = producto.Id, Cantidad = 1 };

            await Assert.ThrowsExceptionAsync<ProductoInactivoException>(() => useCase.EjecutarAsync(request));
        }

        [TestMethod]
        public async Task EjecutarAsync_CantidadCero_LanzaExcepcion()
        {
            var producto = CrearProducto(stock: 10);
            var repoMock = new Mock<IRepositorio>();
            repoMock.Setup(r => r.ObtenerPorIdAsync(producto.Id)).ReturnsAsync(producto);

            var carritoService = new CarritoService();
            var useCase = new AgregarItemAlCarritoUseCase(carritoService, repoMock.Object);

            var request = new AgregarItemRequest { ClienteId = "cliente-1", ProductoId = producto.Id, Cantidad = 0 };

            await Assert.ThrowsExceptionAsync<ArgumentException>(() => useCase.EjecutarAsync(request));
        }
    }
}