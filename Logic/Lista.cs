using System;
using System.Xml;
using BibliotecaV1.Models;

namespace BibliotecaV1.Logic
{
    class Lista
{
     public static List<Libro> listaLibros = new List<Libro>();

     public static void MostrarLista()
    {
        Libro.CargarDatos();
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine(" === Lista de Libros === ");
        Console.ResetColor();

        if (listaLibros.Count == 0)
        {
            Console.WriteLine("Epa mi loco, aca no hay nada xd");
        }
        else
        {
            foreach (var item in listaLibros)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("-" + item.ToString());
            }
        }
    }
    
}
}

