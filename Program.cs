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
        Libro.CargarDatos();
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
                    Console.Write("Disponible: "); string i = Console.ReadLine()!;
                    miDepo.AgregarLibro(new Libro(t, a, i));
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