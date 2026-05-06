using System;
using BibliotecaV1.Models;
using System.Linq;
using BibliotecaV1.Services;
using System.Diagnostics;

namespace BibliotecaV1.Logic
{
   public class Menu
{ 
   public static void MenuUsuario(Usuario usuario)
   {
   string opción; //Instanciamos aquí para que el usuario tenga acceso a las herramientas
   Biblioteca miDepo = new Biblioteca();
    BusquedaDeLibros buscador = new BusquedaDeLibros();

    do
    {
     Console.Clear();
     Console.ForegroundColor = ConsoleColor.Cyan;
     Console.WriteLine("==== SISTEMA DE BIBLIOTECA V1 ====");
     Console.WriteLine("1. Buscar Libro");
     Console.WriteLine("2. Lista");
     Console.WriteLine("3. Prestamos. ");
     Console.WriteLine("4. Devolución. ");
     Console.WriteLine("0. Salir");
     Console.ResetColor();
     Console.Write("\nSelecciona una opcion: ");

     opción = Console.ReadLine()!;

     switch (opción)
        {
          case "1":
          buscador.Buscar(miDepo);
          break;

          case "2":
          Lista.MostrarLista();
          break;

          case "3":
          miDepo.PrestarLibro();
          break;

          case "4":
          miDepo.DevolverLibro();
          break;
          
          case "0":
          break;

          default:
          Console.WriteLine("Opcion no valida! ");
          break;
                  
        }
        if (opción !="0")
                {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                Console.ReadKey(); 
                }
      } 
    while (opción != "0");
   }
   public static void MenuAdmin(Usuario usuario)
    {
            string opcion;
       Biblioteca miDepo = new Biblioteca();
      BusquedaDeLibros buscador = new BusquedaDeLibros();

      do
        {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("==== SISTEMA DE BIBLIOTECA V1 ====");
        Console.WriteLine("1. Buscar Libro");
        Console.WriteLine("2. Agregar Libro");
        Console.WriteLine("3. Prestamos. ");
        Console.WriteLine("4. Lista");
        Console.WriteLine("5. Devolución. ");
        Console.WriteLine("0. Salir");
        Console.ResetColor();
        Console.Write("\nSelecciona una opción: "); 

        opcion = Console.ReadLine()!;

        switch (opcion)
           {
               case "1":
               buscador.Buscar(miDepo);
               break;

               case "2":
              Console.ForegroundColor = ConsoleColor.Yellow;
              Console.Write("Título: "); string t = Console.ReadLine()!;
              Console.ForegroundColor = ConsoleColor.Yellow;
              Console.Write("Autor: "); string a = Console.ReadLine()!;
              Console.ForegroundColor = ConsoleColor.Yellow;
             Console.Write("Unidades Diponibles: "); //para pedir unidades disponibles para usar xd
            int u = int.Parse(Console.ReadLine()!); // Magia para generar ID de forma automatica, esto toma los libros añadidos y los idea
            Libro.CargarDatos();
           int nuevoId = 1;
            if (Lista.listaLibros != null && Lista.listaLibros.Count > 0)
            {
            nuevoId = Lista.listaLibros.Max(l => l.ID) + 1;
            } // ahora creamos el libro con su nuevo ID y unidades
            Libro nuevoLibro = new Libro(nuevoId, t, a, u);
            miDepo.AgregarLibro(nuevoLibro);
            break;

            case "3":
            Console.WriteLine("Accediendo al sistema...");
            miDepo.PrestarLibro();
            break;

            case "4":
            Console.WriteLine("Accediendo al sistema...");
            Lista.MostrarLista();
            break;

            case "5":
            Console.WriteLine("Accediendo al sistema...");
            miDepo.DevolverLibro();
            break;

            case "0":
            break;

            default:
            Console.WriteLine("Opcion no valida. ");
            break;
           }
           if (opcion != "0")
                {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                Console.ReadKey();
                }
                
        } while (opcion != "0");
    }
}

}