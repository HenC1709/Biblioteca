using BibliotecaV2.Core.Entities;

namespace BibliotecaV2.Core.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> ObtenerTodos();
        Usuario? ObtenerPorNombre(string nombre);
        void Guardar(List<Usuario> usuarios);
    }
}
