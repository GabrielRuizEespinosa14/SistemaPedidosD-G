using SistemaPedidosD_G.Domain.ValueObjects;
using System.Net.Mail;

namespace SistemaPedidosD_G.Domain.Entities;

public class Cliente
{
    public Guid Id { get; private set; }
    public string Nombre { get; private set; } = null!;
    public string Correo { get; private set; } = null!;
    public Telefono Telefono { get; private set; } = null!;
    public DateTime FechaRegistro { get; private set; }

    private Cliente()
    {
        // Requerido por Entity Framework Core.
    }

    public Cliente(string nombre, string correo, string telefono)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del cliente es obligatorio.");

        if (string.IsNullOrWhiteSpace(correo))
            throw new ArgumentException("El correo del cliente es obligatorio.");

        try
        {
            _ = new MailAddress(correo);
        }
        catch (FormatException)
        {
            throw new ArgumentException(
                "El correo electrónico no tiene un formato válido.");
        }

        Id = Guid.NewGuid();
        Nombre = nombre.Trim();
        Correo = correo.Trim().ToLowerInvariant();
        Telefono = new Telefono(telefono);
        FechaRegistro = DateTime.UtcNow;
    }
}
