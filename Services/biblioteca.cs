using BibliotecaV1.Data;
using BibliotecaV1.Models;
using BibliotecaV1.Logic;

namespace BibliotecaV1.Services
{
  public class Biblioteca
    {

        private readonly LibroRepository _repo = new LibroRepository();
        public List<Libro> libros { get; private set; }

        public Biblioteca()
        {
            libros = _repo.LeerLibros();
        }

    public void AgregarLibro(Libro nuevoLibro)
    {
       libros.Add(nuevoLibro);
       _repo.GuardarLibros(libros);
    Console.WriteLine($"libro '{nuevoLibro.Titulo}' agregado al sistema");

    }

    public void PrestarLibro()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("======================");
            Console.WriteLine(" SISTEMA DE PRÉSTAMOS ");
            Console.WriteLine("======================");
            Console.ResetColor();

            Console.Write("\nIngrese el ID del articulo a pedir: ");

            // verificamos que el user escriba un numero

            if (int.TryParse(Console.ReadLine(), out int idBuscado))
            {
                var articulo =  libros.FirstOrDefault(l => l.ID == idBuscado);
                if (articulo != null) // si lo encuentra xd
                {
                    if (articulo.Unidades > 0) // por si algun gracioso cree que tiene libros infinitos
                    {
                        articulo.Unidades--; //para restar una unidad!
                        _repo.GuardarLibros(libros);
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"\n¡ÉXITO! has pedido prestado: {articulo.Titulo}");

                        // detalle visual: si queda 1 se pone en amarillo!
                        if (articulo.Unidades == 1)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Unidades en bodega son: {articulo.Unidades}");
                        Console.ResetColor();
                       
                 
                    }
                    else
                    {
                 Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nLo siento, no quedan unidades disponibles de este artículo.");
                Console.ResetColor();
                    }
                }
                else
                {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nNo existe ningún artículo con el ID: {idBuscado} xd");
            Console.ResetColor();
                }
            }
            else
            {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nPor favor, ingresa un ID numérico válido.");
        Console.ResetColor();
           }
        } 

        public void DevolverLibro()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("======================");
            Console.WriteLine(" SISTEMA DE DEVOLUCIÓN ");
            Console.WriteLine("======================");
            Console.ResetColor();

            Console.Write("\nIngrese el ID del articulo a Devolver: ");

            // verificamos que el user escriba un numero

            if (int.TryParse(Console.ReadLine(), out int idBuscado))
            {
                var articulo = libros.FirstOrDefault(l => l.ID == idBuscado);
                if (articulo != null) // si lo encuentra xd
                {
                    if (articulo.Unidades >= 0) // por si algun gracioso cree que tiene libros infinitos
                    {
                        articulo.Unidades++; //para sumar una unidad!
                        _repo.GuardarLibros(libros);
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"\n¡DEVOLUCION EXITOSA! {articulo.Titulo}");
                        Console.WriteLine($"Nuevo stock disponible: {articulo.Unidades}");
                 
                    }
                    else
                    {
                 Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nLo siento, no encontramos el registro con el ID {idBuscado}");
                Console.ResetColor();
                    }
                }
                else
                {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nNo existe ningún artículo con el ID: {idBuscado} xd");
            Console.ResetColor();
                }
            }
            else
            {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nPor favor, ingresa un ID numérico válido.");
        Console.ResetColor();
           }
        } 

    public void RegistrarNuevoLibro(string titulo, string autor, int unidades)
        {
            int nuevoId = 1;
            if (libros != null && libros.Count > 0)
            {
                nuevoId = libros.Max(l => l.ID) + 1;
            }

            Libro nuevoLibro = new Libro(nuevoId, titulo, autor, unidades);
            libros.Add(nuevoLibro);
            _repo.GuardarLibros(libros);
        }
        public void MostrarCatalogo()
        {
            Console.Clear();
            Console.WriteLine("=== CATALOGO DE LIBROS ===");

            if (libros.Count == 0)
            {
                Console.WriteLine("EPA mi loco acá no hay nada. ");
                return;
            }
            foreach (var libro in libros)
            {
                if (libro.Unidades > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(libro.ToString());
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{libro.ToString()} [SIN STOCK]");
                }
            }
            Console.ResetColor();
            Console.WriteLine("\nPresione cualquier tecla para volver...");
            Console.ReadKey();
        }
   }
}