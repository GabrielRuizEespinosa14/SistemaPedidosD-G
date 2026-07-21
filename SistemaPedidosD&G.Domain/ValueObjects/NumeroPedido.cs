namespace SistemaPedidosD_G.Domain.ValueObjects
{
    public class NumeroPedido
    {
        public string Valor { get; private set; }

        public NumeroPedido(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException("El número de pedido no puede estar vacío.");

            if (valor.Length > 20)
                throw new ArgumentException("El número de pedido no puede superar 20 caracteres.");

            Valor = valor.ToUpper().Trim();
        }

        public static NumeroPedido Generar()
        {
            var fecha = DateTime.UtcNow.ToString("yyyyMMdd");
            var aleatorio = new Random().Next(1000, 9999);
            return new NumeroPedido($"PED-{fecha}-{aleatorio}");
        }

        public override string ToString() => Valor;

        public override bool Equals(object? obj)
        {
            if (obj is not NumeroPedido otro) return false;
            return Valor == otro.Valor;
        }

        public override int GetHashCode() => HashCode.Combine(Valor);
    }
}