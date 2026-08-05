using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Application.DTO;

namespace SistemaPedidosD_G.Application.UseCases;

public class ObtenerRepartidoresUseCase : IObtenerRepartidoresUseCase
{
    private readonly IRepartidorRepositorio _repartidorRepositorio;

    public ObtenerRepartidoresUseCase(
        IRepartidorRepositorio repartidorRepositorio)
    {
        _repartidorRepositorio = repartidorRepositorio;
    }

    public async Task<IEnumerable<RepartidorDTO>> EjecutarAsync()
    {
        var repartidores = await _repartidorRepositorio.ObtenerTodosAsync();

        var resultado = new List<RepartidorDTO>();

        foreach (var repartidor in repartidores)
        {
            var pedidosAsignados = await _repartidorRepositorio
                .ContarPedidosAsignadosAsync(repartidor.Id);

            resultado.Add(new RepartidorDTO
            {
                Id = repartidor.Id,
                Nombre = repartidor.Nombre,
                Telefono = repartidor.Telefono.Valor,
                Estado = repartidor.Estado,
                FechaRegistro = repartidor.FechaRegistro,
                PedidosAsignadosActuales = pedidosAsignados
            });
        }

        return resultado;
    }
}