using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Domain.Exceptions;

namespace SistemaPedidosD_G.Application.UseCases;

public class DesactivarRepartidorUseCase : IDesactivarRepartidorUseCase
{
    private readonly IRepartidorRepositorio _repartidorRepositorio;

    public DesactivarRepartidorUseCase(
        IRepartidorRepositorio repartidorRepositorio)
    {
        _repartidorRepositorio = repartidorRepositorio;
    }

    public async Task EjecutarAsync(Guid repartidorId)
    {
        var repartidor = await _repartidorRepositorio
            .ObtenerPorIdAsync(repartidorId);

        if (repartidor is null)
            throw new RepartidorNoEncontradoException(repartidorId);

        repartidor.Desactivar();

        await _repartidorRepositorio.ActualizarAsync(repartidor);
    }
}