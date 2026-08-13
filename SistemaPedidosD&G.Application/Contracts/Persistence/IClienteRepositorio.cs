using SistemaPedidosD_G.Domain.Entities;

namespace SistemaPedidosD_G.Application.Contracts.Persistence;

public interface IClienteRepositorio
{
    Task<bool> ExistePorCorreoAsync(string correo);
    Task<bool> ExistePorTelefonoAsync(string telefono);
    Task AgregarAsync(Cliente cliente);
}
