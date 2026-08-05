using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Domain.Entities;
using SistemaPedidosD_G.Domain.Exceptions;
using SistemaPedidosD_G.Domain.ValueObjects;

namespace SistemaPedidosD_G.Application.UseCases;

public class CrearRepartidorUseCase : ICrearRepartidorUseCase
{
    private readonly IRepartidorRepositorio _repartidorRepositorio;

    public CrearRepartidorUseCase(
        IRepartidorRepositorio repartidorRepositorio)
    {
        _repartidorRepositorio = repartidorRepositorio;
    }

    public async Task<Guid> EjecutarAsync(CrearRepartidorRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var telefono = new Telefono(request.Telefono);

        var existeTelefono = await _repartidorRepositorio
            .ExistePorTelefonoAsync(telefono.Valor);

        if (existeTelefono)
            throw new TelefonoRepartidorDuplicadoException(telefono.Valor);

        var repartidor = new Repartidor(
            request.Nombre,
            telefono.Valor);

        await _repartidorRepositorio.AgregarAsync(repartidor);

        return repartidor.Id;
    }
}