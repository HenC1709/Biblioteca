namespace BibliotecaV2.Core.Entities
{
    public class Session
    {
        public Usuario? UsuarioActual { get; set; }
        public bool EstaAutenticado => UsuarioActual != null;
    }
}
