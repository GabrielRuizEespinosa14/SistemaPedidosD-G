using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Application.DTO;

namespace SistemaPedidosD_G.Application.UseCases;

public class ObtenerRepartidoresDisponiblesUseCase
    : IObtenerRepartidoresDisponiblesUseCase
{
    private readonly IRepartidorRepositorio _repartidorRepositorio;

    public ObtenerRepartidoresDisponiblesUseCase(
        IRepartidorRepositorio repartidorRepositorio)
    {
        _repartidorRepositorio = repartidorRepositorio;
    }

    public async Task<IEnumerable<RepartidorDTO>> EjecutarAsync()
    {
        var repartidores = await _repartidorRepositorio
            .ObtenerDisponiblesAsync();

        return repartidores.Select(repartidor => new RepartidorDTO
        {
            Id = repartidor.Id,
            Nombre = repartidor.Nombre,
            Telefono = repartidor.Telefono.Valor,
            Estado = repartidor.Estado,
            FechaRegistro = repartidor.FechaRegistro,
            PedidosAsignadosActuales = 0
        });
    }
}