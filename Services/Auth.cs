using BibliotecaV1.Models;

namespace BibliotecaV1.Services
{
    public static class Auth
    {
        public static Usuario? UsuarioActual { get; private set; }

        public static void Login(Usuario user)
        {
            UsuarioActual = user;
        }

        public static void Logout()
        {
            UsuarioActual = null;
        }
    }
}