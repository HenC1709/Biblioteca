using System;
using BibliotecaV1.Logic;
using BibliotecaV1.Models;
using BibliotecaV1.Services;
class Program

{
    static void Main(string[] args)
    {

        LoginService login = new LoginService();
        Usuario? usuarioActual = null;
        while (usuarioActual == null)
        {
            usuarioActual = login.IniciarSesion();
        }

        // Guardamos Usuario Actual
        Auth.Login(usuarioActual);
        Console.WriteLine($"Sesión iniciada como: {usuarioActual.nombre}");
        // Creamos los objetos principales
       
        Menu miMenu = new Menu();
        Biblioteca miDepo = new Biblioteca();
        BusquedaDeLibros buscador = new BusquedaDeLibros();
        

        // Bucle principal para que no se cierre
        bool ejecutando = true;
        while (ejecutando)
        {
            miMenu.Mostrar(usuarioActual);
            string opc = Console.ReadLine()!;

            switch (opc)
            {
                    case "1":
                    buscador.Buscar(miDepo);
                    break;
                   
                    case "2":
                    if (usuarioActual.rol == Rol.Admin)
                    {
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
                    }
                    else
                    {
                        Console.WriteLine("No tienes permiso xd");
                    }
                    break;
                    
                    case "3":
                    if (usuarioActual.rol == Rol.Admin)
                    {
                        Lista.MostrarLista();
                    }
                    else
                    {
                        Console.WriteLine("No tienes permiso xd");
                    }
                    break;

                    
                    case "0":
                    ejecutando = false;
                    break;
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}