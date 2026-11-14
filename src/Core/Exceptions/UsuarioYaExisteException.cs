namespace BibliotecaV2.Core.Exceptions
{
    public class UsuarioYaExisteException : Exception
    {
        public string Nombre { get; }

        public UsuarioYaExisteException(string nombre)
            : base($"Ya existe un usuario con el nombre '{nombre}'.")
        {
            Nombre = nombre;
        }
    }
}
