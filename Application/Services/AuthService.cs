using BibliotecaV2.Core.Entities;
using BibliotecaV2.Core.Enums;
using BibliotecaV2.Core.Exceptions;
using BibliotecaV2.Core.Interfaces;
using BCrypt.Net;

namespace BibliotecaV2.Application.Services
{
    public class AuthService
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly ISessionService _session;

        public AuthService(IUsuarioRepository usuarioRepo, ISessionService session)
        {
            _usuarioRepo = usuarioRepo;
            _session = session;
        }

        // Devuelve el usuario si las credenciales son válidas, lanza excepción si no
        public Usuario Login(string nombre, string password)
        {
            var usuario = _usuarioRepo.ObtenerPorNombre(nombre)
                ?? throw new CredencialesInvalidasException();

            bool passwordValida = BCrypt.Net.BCrypt.Verify(password, usuario.PasswordHash);
            if (!passwordValida) throw new CredencialesInvalidasException();

            _session.Login(usuario);
            return usuario;
        }

        public Usuario Registrar(string nombre, string password)
        {
            if (_usuarioRepo.ObtenerPorNombre(nombre) != null)
                throw new UsuarioYaExisteException(nombre);

            string hash = BCrypt.Net.BCrypt.HashPassword(password);
            var nuevoUsuario = new Usuario(nombre, hash, Rol.Usuario);

            var lista = _usuarioRepo.ObtenerTodos();
            lista.Add(nuevoUsuario);
            _usuarioRepo.Guardar(lista);

            return nuevoUsuario;
        }

        public void Logout()
        {
            _session.Logout();
        }
    }
}