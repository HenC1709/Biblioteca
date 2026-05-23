using BibliotecaV2.Core.Entities;

namespace BibliotecaV2.Core.Interfaces
{
    public interface ISessionService
    {
        Usuario? UsuarioActual { get; }
        bool EstaAutenticado { get; }
        void Login(Usuario usuario);
        void Logout();
    }
}
