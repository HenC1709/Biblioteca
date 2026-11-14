namespace BibliotecaV2.Core.Exceptions
{
    public class LibroNoEncontradoException : Exception
    {
        public int LibroId { get; }

        public LibroNoEncontradoException(int libroId)
            : base($"No se encontró un libro con ID {libroId}.")
        {
            LibroId = libroId;
        }
    }
}
