using BibliotecaV2.Core.Entities;
using BibliotecaV2.Core.Interfaces;

namespace BibliotecaV2.Infrastructure.Seeders
{
    public class LibroSeeder
    {
        private readonly ILibroRepository _repo;

        public LibroSeeder(ILibroRepository repo)
        {
            _repo = repo;
        }

        public void Seed()
        {
            if (_repo.ObtenerTodos().Any()) return;

            var libros = new List<Libro>
            {
                new Libro(1,  "El nombre de la rosa",          "Umberto Eco",              4),
                new Libro(2,  "1984",                           "George Orwell",            5),
                new Libro(3,  "Clean Code",                     "Robert C. Martin",         2),
                new Libro(4,  "Cien anos de soledad",           "Gabriel García Márquez",   3),
                new Libro(5,  "El principito",                  "Antoine de Saint-Exupéry", 6),
                new Libro(6,  "Don Quijote de la Mancha",       "Miguel de Cervantes",      2),
                new Libro(7,  "Harry Potter y la piedra filosofal", "J.K. Rowling",         4),
                new Libro(8,  "El codigo Da Vinci",             "Dan Brown",                3),
                new Libro(9,  "Sapiens",                        "Yuval Noah Harari",        3),
                new Libro(10, "The Pragmatic Programmer",       "Hunt & Thomas",            2),
            };

            _repo.Guardar(libros);
        }
    }
}