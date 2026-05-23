using BibliotecaV2.Core.Entities;
using BibliotecaV2.Core.Exceptions;
using BibliotecaV2.Core.Interfaces;

namespace BibliotecaV2.Application.Services
{
    public class LibroService
    {
        private readonly ILibroRepository _repo;

        public LibroService(ILibroRepository repo)
        {
            _repo = repo;
        }

        public List<Libro> ObtenerTodos()
        {
            return _repo.ObtenerTodos();
        }

        public Libro ObtenerPorId(int id)
        {
            return _repo.ObtenerPorId(id)
                ?? throw new LibroNoEncontradoException(id);
        }

        public List<Libro> BuscarPorTitulo(string query)
        {
            return _repo.ObtenerTodos()
                .Where(l => l.Titulo.ToLower().Contains(query.ToLower()))
                .ToList();
        }

        public Libro Agregar(string titulo, string autor, int unidades)
        {
            var libros = _repo.ObtenerTodos();
            int nuevoId = libros.Any() ? libros.Max(l => l.Id) + 1 : 1;
            var libro = new Libro(nuevoId, titulo, autor, unidades);
            libros.Add(libro);
            _repo.Guardar(libros);
            return libro;
        }
    }
}