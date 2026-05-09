using BibliotecaV1.Data;
using BibliotecaV1.Models;
using BibliotecaV1.Logic;

namespace BibliotecaV1.Services
{
    public class Biblioteca
    {
        private readonly LibroRepository _repo = new LibroRepository();
        private readonly TicketManager _ticket = new TicketManager();

        private List<Libro> _libros;

        public Biblioteca()
        {
            _libros = _repo.LeerLibros();
        }

        // =========================
        // OBTENER LIBROS
        // =========================

        public List<Libro> ObtenerLibros()
        {
            return _libros;
        }
         // =========================
        // AGREGAR LIBRO
        // =========================

        public void AgregarLibro(Libro nuevoLibro)
        {
            _libros.Add(nuevoLibro);
            _repo.GuardarLibros(_libros);
        }

        public void RegistrarNuevoLibro(string titulo, string autor, int unidades)
        {
            int nuevoId = 1;

            if (_libros.Count > 0)
            {
                nuevoId = _libros.Max(l => l.ID) + 1;
            }

            Libro nuevoLibro = new Libro(nuevoId, titulo, autor, unidades);

            AgregarLibro(nuevoLibro);
        }

        // =========================
        // PRESTAR LIBRO
        // =========================

        public string PrestarLibro(int idLibro, string usuarioNombre)
        {
            var libro = _libros.FirstOrDefault(l => l.ID == idLibro);

            if (libro == null)
            {
                return "Libro no encontrado.";
            }

            if (libro.Unidades <= 0)
            {
                return "No hay stock disponible.";
            }

            libro.Unidades--;

            _repo.GuardarLibros(_libros);

            _ticket.GenerarTicketPrestamo(libro, usuarioNombre);

            return $"Préstamo exitoso: {libro.Titulo}";
        }

        // =========================
        // DEVOLVER LIBRO
        // =========================

        public string DevolverLibro(int idLibro, string usuarioNombre)
        {
            var libro = _libros.FirstOrDefault(l => l.ID == idLibro);

            if (libro == null)
            {
                return "Libro no encontrado.";
            }

            libro.Unidades++;

            _repo.GuardarLibros(_libros);

            _ticket.GenerarTicketDevolucion(libro, usuarioNombre);

            return $"Devolución exitosa: {libro.Titulo}";
        }

        // =========================
        // MOSTRAR CATÁLOGO
        // =========================

        public void MostrarCatalogo()
        {
            Console.Clear();
            Console.WriteLine("=== CATALOGO DE LIBROS ===");

            if (_libros.Count == 0)
            {
                Console.WriteLine("EPA mi loco acá no hay nada.");
                return;
            }

            foreach (var libro in _libros)
            {
                if (libro.Unidades > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(libro);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{libro} [SIN STOCK]");
                }
            }

        }
    }
}