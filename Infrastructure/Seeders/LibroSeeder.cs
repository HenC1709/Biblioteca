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
            if (_repo.ObtenerTodos().Count >= 50) return;

            var libros = new List<Libro>
            {
                new Libro(1,  "El nombre de la rosa",                     "Umberto Eco",                  4),
                new Libro(2,  "1984",                                     "George Orwell",                5),
                new Libro(3,  "Clean Code",                               "Robert C. Martin",             2),
                new Libro(4,  "Cien años de soledad",                     "Gabriel García Márquez",       3),
                new Libro(5,  "El principito",                            "Antoine de Saint-Exupéry",     6),
                new Libro(6,  "Don Quijote de la Mancha",                 "Miguel de Cervantes",          2),
                new Libro(7,  "Harry Potter y la piedra filosofal",       "J.K. Rowling",                 4),
                new Libro(8,  "El código Da Vinci",                       "Dan Brown",                    3),
                new Libro(9,  "Sapiens",                                  "Yuval Noah Harari",            3),
                new Libro(10, "The Pragmatic Programmer",                 "Andrew Hunt & David Thomas",   2),
                new Libro(11, "Designing Data-Intensive Applications",    "Martin Kleppmann",             3),
                new Libro(12, "Clean Architecture",                       "Robert C. Martin",             4),
                new Libro(13, "Domain-Driven Design",                     "Eric Evans",                   2),
                new Libro(14, "Refactoring",                              "Martin Fowler",                3),
                new Libro(15, "Design Patterns",                          "Erich Gamma et al.",           2),
                new Libro(16, "Crónica de una muerte anunciada",          "Gabriel García Márquez",       5),
                new Libro(17, "Rayuela",                                  "Julio Cortázar",               3),
                new Libro(18, "Ficciones",                                "Jorge Luis Borges",            4),
                new Libro(19, "El Aleph",                                 "Jorge Luis Borges",            3),
                new Libro(20, "Pedro Páramo",                             "Juan Rulfo",                   4),
                new Libro(21, "La sombra del viento",                     "Carlos Ruiz Zafón",            5),
                new Libro(22, "El juego del ángel",                       "Carlos Ruiz Zafón",            3),
                new Libro(23, "Fahrenheit 451",                           "Ray Bradbury",                 4),
                new Libro(24, "Un mundo feliz",                           "Aldous Huxley",                4),
                new Libro(25, "Rebelión en la granja",                    "George Orwell",                6),
                new Libro(26, "El Señor de los Anillos: La Comunidad",    "J.R.R. Tolkien",               5),
                new Libro(27, "El Señor de los Anillos: Las Dos Torres",  "J.R.R. Tolkien",               4),
                new Libro(28, "El Señor de los Anillos: El Retorno",      "J.R.R. Tolkien",               4),
                new Libro(29, "El Hobbit",                                "J.R.R. Tolkien",               6),
                new Libro(30, "Dune",                                     "Frank Herbert",                5),
                new Libro(31, "Fundación",                                "Isaac Asimov",                 4),
                new Libro(32, "Yo, Robot",                                "Isaac Asimov",                 5),
                new Libro(33, "Neuromante",                               "William Gibson",               3),
                new Libro(34, "Crimen y castigo",                         "Fiódor Dostoyevski",           3),
                new Libro(35, "Los hermanos Karamazov",                   "Fiódor Dostoyevski",           2),
                new Libro(36, "Guerra y paz",                             "León Tolstói",                 2),
                new Libro(37, "Anna Karénina",                            "León Tolstói",                 3),
                new Libro(38, "Orgullo y prejuicio",                      "Jane Austen",                  5),
                new Libro(39, "Frankenstein",                             "Mary Shelley",                 4),
                new Libro(40, "Drácula",                                  "Bram Stoker",                  4),
                new Libro(41, "El retrato de Dorian Gray",                "Oscar Wilde",                  5),
                new Libro(42, "La metamorfosis",                          "Franz Kafka",                  6),
                new Libro(43, "El proceso",                               "Franz Kafka",                  3),
                new Libro(44, "El extranjero",                            "Albert Camus",                 4),
                new Libro(45, "La peste",                                 "Albert Camus",                 3),
                new Libro(46, "Homo Deus",                                "Yuval Noah Harari",            4),
                new Libro(47, "Hábitos atómicos",                         "James Clear",                  6),
                new Libro(48, "Pensar rápido, pensar despacio",           "Daniel Kahneman",              3),
                new Libro(49, "El arte de la guerra",                     "Sun Tzu",                      5),
                new Libro(50, "Meditaciones",                             "Marco Aurelio",                4)
            };

            _repo.Guardar(libros);
        }
    }
}
