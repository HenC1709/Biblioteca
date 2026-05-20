using BibliotecaV1.Data;

namespace BibliotecaV1.Services
{
  public class UsuarioServicio
    {
    private readonly UsuarioRepository _repo = new UsuarioRepository();
        public bool ExisteUsuario(string nombre)
        {
            var usuarios = _repo.LeerUsuarios();
            return usuarios.Any(u => u.Nombre.ToLower() == nombre.ToLower());
        }
    }
}