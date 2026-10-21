namespace BibliotecaV2.Core.Exceptions
{
    public class LimitePrestamosException : Exception
    {
        public int Limite { get; }

        public LimitePrestamosException(int limite)
            : base($"Se alcanzó el límite de {limite} préstamos activos por usuario.")
        {
            Limite = limite;
        }
    }
}
