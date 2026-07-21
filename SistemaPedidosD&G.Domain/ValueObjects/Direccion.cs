namespace SistemaPedidosD_G.Domain.ValueObjects
{
    public class Direccion
    {
        public string Calle { get; private set; }
        public string Sector { get; private set; }
        public string Referencia { get; private set; }

        public Direccion(string calle, string sector, string referencia = "")
        {
            if (string.IsNullOrWhiteSpace(calle))
                throw new ArgumentException("La calle no puede estar vacía.");

            if (string.IsNullOrWhiteSpace(sector))
                throw new ArgumentException("El sector no puede estar vacío.");

            Calle = calle.Trim();
            Sector = sector.Trim();
            Referencia = referencia.Trim();
        }

        public override string ToString() =>
            string.IsNullOrEmpty(Referencia)
                ? $"{Calle}, {Sector}"
                : $"{Calle}, {Sector} ({Referencia})";

        public override bool Equals(object? obj)
        {
            if (obj is not Direccion otro) return false;
            return Calle == otro.Calle &&
                   Sector == otro.Sector &&
                   Referencia == otro.Referencia;
        }

        public override int GetHashCode() =>
            HashCode.Combine(Calle, Sector, Referencia);
    }
}
