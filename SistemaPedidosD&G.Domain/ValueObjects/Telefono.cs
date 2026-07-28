using System.Text.RegularExpressions;

namespace SistemaPedidosD_G.Domain.ValueObjects
{
    public class Telefono
    {
        public string Valor { get; private set; }

        private Telefono() { Valor = string.Empty; } // Requerido por EF Core

        public Telefono(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException("El teléfono no puede estar vacío.");

            var soloNumeros = Regex.Replace(valor, @"\D", "");

            if (soloNumeros.Length < 10)
                throw new ArgumentException("El teléfono debe tener al menos 10 dígitos.");

            Valor = soloNumeros;
        }

        public override string ToString() => Valor;

        public override bool Equals(object? obj)
        {
            if (obj is not Telefono otro) return false;
            return Valor == otro.Valor;
        }

        public override int GetHashCode() => HashCode.Combine(Valor);

    }
}
