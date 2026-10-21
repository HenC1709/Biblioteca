namespace BibliotecaV2.Core.Exceptions
{
    public class StockInsuficienteException : Exception
    {
        public string TituloLibro { get; }

        public StockInsuficienteException(string tituloLibro)
            : base($"No hay unidades disponibles de '{tituloLibro}'.")
        {
            TituloLibro = tituloLibro;
        }
    }
}
