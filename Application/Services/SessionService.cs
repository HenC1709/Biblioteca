using BibliotecaV2.Core.Entities;
using BibliotecaV2.Core.Interfaces;

namespace BibliotecaV2.Application.Services
{
    public class SessionService : ISessionService
    {
        public Usuario? UsuarioActual { get; private set; }
        public bool EstaAutenticado => UsuarioActual != null;

        public void Login(Usuario usuario)
        {
            UsuarioActual = usuario;
        }

        public void Logout()
        {
            UsuarioActual = null;
        }
    }
}