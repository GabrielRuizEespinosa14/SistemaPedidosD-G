using Moq;
using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Services;
using SistemaPedidosD_G.Application.UseCases;
using SistemaPedidosD_G.Domain.Entities;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Tests.UseCases
{
    [TestClass]
    public class ObtenerCarritoUseCaseTests
    {
        [TestMethod]
        public async Task EjecutarAsync_ConItems_DevuelveElTotalCalculadoDinamicamente()
        {
            var producto = new Producto("Arroz", "desc", 50m, 10, "http://img");
            var repoMock = new Mock<IRepositorio>();
            repoMock.Setup(r => r.ObtenerPorIdAsync(producto.Id)).ReturnsAsync(producto);

            var carritoService = new CarritoService();
            carritoService.AgregarItem("cliente-1", producto.Id, producto.Nombre, 3, producto.Precio);

            var useCase = new ObtenerCarritoUseCase(carritoService, repoMock.Object);

            var dto = await useCase.EjecutarAsync("cliente-1");

            Assert.AreEqual(150m, dto.Total);
            Assert.IsTrue(dto.EsValido);
        }

        [TestMethod]
        public async Task EjecutarAsync_StockBajoDespuesDeAgregarAlCarrito_MarcaElItemComoInvalido()
        {
            var producto = new Producto("Arroz", "desc", 50m, 5, "http://img");

            var carritoService = new CarritoService();
            carritoService.AgregarItem("cliente-1", producto.Id, producto.Nombre, 5, producto.Precio);

            producto.Actualizar(producto.Nombre, producto.Descripcion, producto.Precio, 2, producto.ImagenUrl);

            var repoMock = new Mock<IRepositorio>();
            repoMock.Setup(r => r.ObtenerPorIdAsync(producto.Id)).ReturnsAsync(producto);

            var useCase = new ObtenerCarritoUseCase(carritoService, repoMock.Object);

            var dto = await useCase.EjecutarAsync("cliente-1");

            Assert.IsFalse(dto.EsValido);
            Assert.IsTrue(dto.Items[0].ExcedeStock);
            Assert.AreEqual(2, dto.Items[0].StockDisponible);
        }

        [TestMethod]
        public async Task EjecutarAsync_ProductoDesactivadoDespuesDeAgregarAlCarrito_MarcaComoNoDisponible()
        {
            var producto = new Producto("Arroz", "desc", 50m, 5, "http://img");

            var carritoService = new CarritoService();
            carritoService.AgregarItem("cliente-1", producto.Id, producto.Nombre, 2, producto.Precio);

            producto.Desactivar();

            var repoMock = new Mock<IRepositorio>();
            repoMock.Setup(r => r.ObtenerPorIdAsync(producto.Id)).ReturnsAsync(producto);

            var useCase = new ObtenerCarritoUseCase(carritoService, repoMock.Object);

            var dto = await useCase.EjecutarAsync("cliente-1");

            Assert.IsFalse(dto.EsValido);
            Assert.IsTrue(dto.Items[0].ProductoNoDisponible);
        }

        [TestMethod]
        public async Task EjecutarAsync_CarritoVacio_DevuelveTotalCeroYValido()
        {
            var repoMock = new Mock<IRepositorio>();
            var carritoService = new CarritoService();
            var useCase = new ObtenerCarritoUseCase(carritoService, repoMock.Object);

            var dto = await useCase.EjecutarAsync("cliente-nuevo");

            Assert.AreEqual(0m, dto.Total);
            Assert.IsTrue(dto.EsValido);
            Assert.AreEqual(0, dto.Items.Count);
        }
    }
}