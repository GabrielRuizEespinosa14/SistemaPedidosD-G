using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Application.Contracts.UseCases;
using SistemaPedidosD_G.Domain.Entities;
using SistemaPedidosD_G.Domain.Exceptions;

namespace SistemaPedidosD_G.Application.UseCases;

public class RegistrarClienteUseCase : IRegistrarClienteUseCase
{
    private readonly IClienteRepositorio _clienteRepositorio;

    public RegistrarClienteUseCase(IClienteRepositorio clienteRepositorio)
    {
        _clienteRepositorio = clienteRepositorio;
    }

    public async Task<Guid> EjecutarAsync(RegistrarClienteRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var cliente = new Cliente(
            request.Nombre,
            request.Correo,
            request.Telefono);

        if (await _clienteRepositorio.ExistePorCorreoAsync(cliente.Correo))
            throw new ClienteDuplicadoException("correo");

        if (await _clienteRepositorio.ExistePorTelefonoAsync(cliente.Telefono.Valor))
            throw new ClienteDuplicadoException("teléfono");

        await _clienteRepositorio.AgregarAsync(cliente);

        return cliente.Id;
    }
}
