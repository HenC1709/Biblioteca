namespace BibliotecaV2.Core.Exceptions
{
    public class CredencialesInvalidasException : Exception
    {
        public CredencialesInvalidasException()
            : base("Nombre o contraseña incorrectos.")
        {
        }
    }
}
