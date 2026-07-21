namespace SistemaPedidosD_G.Domain.ValueObjects
{
    public class Dinero
    {
        public decimal Valor { get; private set; }
        public string Moneda { get; private set; }

        public Dinero(decimal valor, string moneda = "DOP")
        {
            if (valor < 0)
                throw new ArgumentException("El valor del dinero no puede ser negativo.");

            Valor = valor;
            Moneda = moneda;
        }

        public Dinero Sumar(Dinero otro)
        {
            if (Moneda != otro.Moneda)
                throw new InvalidOperationException("No se pueden sumar monedas diferentes.");
            return new Dinero(Valor + otro.Valor, Moneda);
        }

        public Dinero Multiplicar(int cantidad)
        {
            if (cantidad < 0)
                throw new ArgumentException("La cantidad no puede ser negativa.");
            return new Dinero(Valor * cantidad, Moneda);
        }

        public override string ToString() => $"{Moneda} {Valor:F2}";

        public override bool Equals(object? obj)
        {
            if (obj is not Dinero otro) return false;
            return Valor == otro.Valor && Moneda == otro.Moneda;
        }

        public override int GetHashCode() => HashCode.Combine(Valor, Moneda);
    }
}
