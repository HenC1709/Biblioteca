using BibliotecaV2.Core.Entities;
using BibliotecaV2.Core.Enums;
using BibliotecaV2.Core.Interfaces;

namespace BibliotecaV2.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repo;

        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public List<Usuario> ObtenerTodos()
        {
            return _repo.ObtenerTodos();
        }

        public void CambiarRol(string nombre, Rol nuevoRol)
        {
            var usuarios = _repo.ObtenerTodos();
            var usuario = usuarios.FirstOrDefault(u => u.Nombre.ToLower() == nombre.ToLower());

            if (usuario == null) return;

            usuario.Rol = nuevoRol;
            _repo.Guardar(usuarios);
        }
    }
}