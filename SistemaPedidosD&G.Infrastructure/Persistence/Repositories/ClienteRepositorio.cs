using Microsoft.EntityFrameworkCore;
using SistemaPedidosD_G.Application.Contracts.Persistence;
using SistemaPedidosD_G.Domain.Entities;

namespace SistemaPedidosD_G.Infrastructure.Persistence.Repositories;

public class ClienteRepositorio : IClienteRepositorio
{
    private readonly SistemaPedidosDGDbContext _context;

    public ClienteRepositorio(SistemaPedidosDGDbContext context)
    {
        _context = context;
    }

    public Task<bool> ExistePorCorreoAsync(string correo)
    {
        return _context.Clientes.AnyAsync(c => c.Correo == correo);
    }

    public Task<bool> ExistePorTelefonoAsync(string telefono)
    {
        return _context.Clientes.AnyAsync(c => c.Telefono.Valor == telefono);
    }

    public async Task AgregarAsync(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
    }
}
