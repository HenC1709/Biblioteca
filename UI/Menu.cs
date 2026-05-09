using System;
using BibliotecaV1.Models;
using System.Linq;
using BibliotecaV1.Services;
using System.Diagnostics;

namespace BibliotecaV1.Logic
{
   public class Menu
{ 
   public static void MenuUsuario(Usuario usuario, Biblioteca miDepo)
   {
   string opción; //Instanciamos aquí para que el usuario tenga acceso a las herramientas
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
     Console.WriteLine("0. Cerrar sesión");
     Console.ResetColor();
     Console.Write("\nSelecciona una opcion: ");

     opción = Console.ReadLine()!;

     switch (opción)
        {
          case "1":
          buscador.Buscar(miDepo);
          break;

          case "2":
          miDepo.MostrarCatalogo();
          break;

          case "3":
        Console.Clear();
Console.Write("Ingrese ID del libro: ");

if (int.TryParse(Console.ReadLine(), out int idPrestamo))
{
    string resultado = miDepo.PrestarLibro(idPrestamo, usuario.nombre);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(resultado);
    Console.ResetColor();
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("ID inválido.");
    Console.ResetColor();
}
break;

          case "4":
Console.Clear();
Console.Write("Ingrese ID del libro a devolver: ");

if (int.TryParse(Console.ReadLine(), out int idDevolucion))
{
    string resultado = miDepo.DevolverLibro(idDevolucion, usuario.nombre);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(resultado);
    Console.ResetColor();
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("ID inválido.");
    Console.ResetColor();
}
break;
          
          case "0":
          Auth.Logout();
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
   public static void MenuAdmin(Usuario usuario, Biblioteca miDepo)
    {
            string opcion;
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
        Console.WriteLine("0. cerrar sesión");
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
               Console.Write("Autor: "); string a = Console.ReadLine()!;
               Console.Write("Unidades Disponibles: ");
               if (int.TryParse(Console.ReadLine(), out int u))
                  {
                     miDepo.RegistrarNuevoLibro(t, a, u);
                  }
                  else
                  {
                     Console.WriteLine("Error: las unidades deben ser en numero. ");
                  }
            break;

          case "3":
Console.Clear();
Console.Write("Ingrese ID del libro: ");

if (int.TryParse(Console.ReadLine(), out int idPrestamo))
{
    string resultado = miDepo.PrestarLibro(idPrestamo, usuario.nombre);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(resultado);
    Console.ResetColor();
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("ID inválido.");
    Console.ResetColor();
}
break;

            case "4":
            Console.WriteLine("Accediendo al sistema...");
            miDepo.MostrarCatalogo();
            break;

case "5":
Console.Clear();
Console.Write("Ingrese ID del libro a devolver: ");

if (int.TryParse(Console.ReadLine(), out int idDevolucion))
{
    string resultado = miDepo.DevolverLibro(idDevolucion, usuario.nombre);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(resultado);
    Console.ResetColor();
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("ID inválido.");
    Console.ResetColor();
}
break;

            case "0":
            Auth.Logout();
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