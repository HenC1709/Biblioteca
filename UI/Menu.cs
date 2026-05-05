using System;
using BibliotecaV1.Models;

class Menu
{ 
   public void Mostrar(Usuario usuario)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("==== SISTEMA DE BIBLIOTECA V1 ====");

    Console.WriteLine("1. Buscar Libro");
    Console.WriteLine("3. Prestamos. ");

    if (usuario.rol == Rol.Admin)
    {
        Console.WriteLine("2. Agregar Libro");
        Console.WriteLine("4. Lista");
        Console.WriteLine("0. Salir");
    }
    else
    {
        Console.WriteLine("0. Salir");
    }

    Console.ResetColor();
    Console.Write("\nSelecciona una opción: ");
            
  }


}