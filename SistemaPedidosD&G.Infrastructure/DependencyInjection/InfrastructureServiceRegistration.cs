using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Infrastructure.Persistence;
using SistemaPedidosD_G.Infrastructure.Persistence.Repositories;

namespace SistemaPedidosD_G.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<SistemaPedidosDGDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsAssembly(typeof(SistemaPedidosDGDbContext).Assembly.FullName)));

            services.AddScoped<IRepositorio, ProductoRepositorio>();
            services.AddScoped<IPedidoRepositorio, PedidoRepositorio>();
            services.AddScoped<IRepartidorRepositorio, RepartidorRepositorio>();
            services.AddScoped<ICarritoRepositorio, CarritoRepositorio>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}