using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Domain.Exceptions;
using SistemaPedidosD_G.Domain.ValueObjects;

namespace SistemaPedidosD_G.Application.UseCases;

public class ActualizarRepartidorUseCase : IActualizarRepartidorUseCase
{
    private readonly IRepartidorRepositorio _repartidorRepositorio;

    public ActualizarRepartidorUseCase(
        IRepartidorRepositorio repartidorRepositorio)
    {
        _repartidorRepositorio = repartidorRepositorio;
    }

    public async Task EjecutarAsync(
        Guid repartidorId,
        ActualizarRepartidorRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var repartidor = await _repartidorRepositorio
            .ObtenerPorIdAsync(repartidorId);

        if (repartidor is null)
            throw new RepartidorNoEncontradoException(repartidorId);

        var telefono = new Telefono(request.Telefono);

        var existeTelefono = await _repartidorRepositorio
            .ExistePorTelefonoAsync(telefono.Valor, repartidorId);

        if (existeTelefono)
            throw new TelefonoRepartidorDuplicadoException(telefono.Valor);

        repartidor.ActualizarDatos(request.Nombre, telefono.Valor);

        await _repartidorRepositorio.ActualizarAsync(repartidor);
    }
}