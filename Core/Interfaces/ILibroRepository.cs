using BibliotecaV2.Core.Entities;

namespace BibliotecaV2.Core.Interfaces
{
    public interface ILibroRepository
    {
        List<Libro> ObtenerTodos();
        Libro? ObtenerPorId(int id);
        void Guardar(List<Libro> libros);
    }
}
