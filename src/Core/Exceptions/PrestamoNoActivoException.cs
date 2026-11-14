namespace BibliotecaV2.Core.Exceptions
{
    public class PrestamoNoActivoException : Exception
    {
        public PrestamoNoActivoException(int libroId, string usuarioNombre)
            : base($"El usuario '{usuarioNombre}' no tiene un préstamo activo del libro ID {libroId}.")
        {
        }
    }
}
