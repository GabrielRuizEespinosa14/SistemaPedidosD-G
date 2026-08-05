using Microsoft.Extensions.DependencyInjection;
using SistemaPedidosD_G.Application.Contracts.Services;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Application.Services;
using SistemaPedidosD_G.Application.UseCases;

namespace SistemaPedidosD_G.Application.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Servicios
            services.AddSingleton<ICarritoService, CarritoService>();

            // Casos de uso - Carrito
            services.AddScoped<IAgregarItemAlCarritoUseCase, AgregarItemAlCarritoUseCase>();
            services.AddScoped<IEliminarItemDelCarritoUseCase, EliminarItemDelCarritoUseCase>();
            services.AddScoped<IModificarCantidadItemUseCase, ModificarCantidadItemUseCase>();
            services.AddScoped<IObtenerCarritoUseCase, ObtenerCarritoUseCase>();
            services.AddScoped<IVaciarCarritoUseCase, VaciarCarritoUseCase>();
            services.AddScoped<IProcesarCheckoutUseCase, ProcesarCheckoutUseCase>();

            // Casos de uso - Productos
            services.AddScoped<ICrearProductoUseCase, CrearProductoUseCase>();
            services.AddScoped<IObtenerProductosUseCase, ObtenerProductosUseCase>();

            // Casos de uso - Pedidos
            services.AddScoped<ICambiarEstadoPedidoUseCase, CambiarEstadoPedidoUseCase>();
            services.AddScoped<ICancelarPedidoUseCase, CancelarPedidoUseCase>();
            services.AddScoped<IConfirmarPedidoUseCase, ConfirmarPedidoUseCase>();
            services.AddScoped<IObtenerHistorialPedidosUseCase, ObtenerHistorialPedidosUseCase>();
            services.AddScoped<IObtenerPedidosPendientesUseCase, ObtenerPedidosPendientesUseCase>();
            services.AddScoped<IObtenerPedidosUseCase, ObtenerPedidosUseCase>();

            // Casos de uso - Repartidores
            services.AddScoped<IAsignarRepartidorUseCase, AsignarRepartidorUseCase>();
            services.AddScoped<ICrearRepartidorUseCase, CrearRepartidorUseCase>();
            services.AddScoped<IActualizarRepartidorUseCase, ActualizarRepartidorUseCase>();
            services.AddScoped<IDesactivarRepartidorUseCase, DesactivarRepartidorUseCase>();
            services.AddScoped<IObtenerRepartidoresUseCase, ObtenerRepartidoresUseCase>();
            services.AddScoped<IObtenerRepartidoresDisponiblesUseCase, ObtenerRepartidoresDisponiblesUseCase>();

            return services;
        }
    }
}
