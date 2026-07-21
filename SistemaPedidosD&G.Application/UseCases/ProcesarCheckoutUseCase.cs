using SistemaPedidosD_G.Application.Contracts.Services;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Application.DTO;
using SistemaPedidosD_G.Domain.Enums;
using SistemaPedidosD_G.Domain.Exceptions;
using SistemaPedidosD_G.Domain.ValueObjects;
using System.Threading.Tasks;

namespace SistemaPedidosD_G.Application.UseCases
{
    public class ProcesarCheckoutUseCase : IProcesarCheckoutUseCase
    {
        private readonly ICarritoService _carritoService;

        public ProcesarCheckoutUseCase(ICarritoService carritoService)
        {
            _carritoService = carritoService;
        }

        public Task EjecutarAsync(CheckoutDTO checkout)
        {
            // 1. Validar que el carrito no esté vacío
            var carrito = _carritoService.ObtenerCarrito(checkout.ClienteId);
            if (carrito.EstaVacio())
                throw new CarritoVacioException();

            // 2. Validar nombre del cliente
            if (string.IsNullOrWhiteSpace(checkout.NombreCliente))
                throw new ArgumentException("El nombre del cliente es obligatorio.");

            // 3. Validar teléfono usando Value Object
            var telefono = new Telefono(checkout.Telefono);

            // 4. Validar dirección si el método es Delivery
            if (checkout.MetodoEntrega == MetodoEntrega.Delivery)
            {
                if (string.IsNullOrWhiteSpace(checkout.Calle) ||
                    string.IsNullOrWhiteSpace(checkout.Sector))
                    throw new ArgumentException("La calle y el sector son obligatorios para delivery.");

                var direccion = new Direccion(
                    checkout.Calle,
                    checkout.Sector,
                    checkout.Referencia ?? string.Empty
                );
            }

            return Task.CompletedTask;
        }
    }
}
