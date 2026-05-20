using System;
using BibliotecaV1.Helpers;
using BibliotecaV1.Models;
using System.Linq;
using BibliotecaV1.Services;

namespace BibliotecaV1.Logic
{
    public class Menu
    {
        public static void MostrarMenu(Usuario usuario, Biblioteca miDepo)
        {
          string opcion;
          BusquedaDeLibros buscador = new BusquedaDeLibros();
          do
            {
                Console.Clear();
                ConsoleHelper.Title("SISTEMA BIBLIOTECA");
                Console.WriteLine($"Usuario: {usuario.Nombre}");
                Console.WriteLine($"Rol: {usuario.Rol}");

                Console.WriteLine("1. Buscar Libro");
                Console.WriteLine("2. Ver Catalogo");
                Console.WriteLine("3. Prestar Libro");
                Console.WriteLine("4. Devolver Libro");
                //solo para los admin
                if (usuario.Rol == Rol.Admin)
                {
                    Console.WriteLine("5. Agregar Libro");
                }
                Console.WriteLine("0. Cerrar Sesión");
                opcion = InputHelper.LeerTexto("\nSeleccione una opcion: ");
                switch (opcion)
                {
                    case "1":
                    buscador.Buscar(miDepo);
                    break;

                    case "2":
                    miDepo.MostrarCatalogo();
                    break;

                    case "3":
                    ManejarPrestamo(usuario, miDepo);
                    break;

                    case "4":
                    ManejarDevolucion(usuario, miDepo);
                    break;

                    case "5":
                    if (usuario.Rol == Rol.Admin)
                        {
                            ManejarAgregarLibro(miDepo);
                        }
                    else
                        {
                            ConsoleHelper.Warning("Opcion Invalida.");
                        }    
                    break;

                    case "0":
                    Auth.Logout();
                    break;
                    default:
                    ConsoleHelper.Warning("opcion invalida");
                    break;    
                }
                if (opcion != "0")
                {
                    ConsoleHelper.Pause();
                }
            }while(opcion != "0");
        }   
        private static void ManejarPrestamo(Usuario usuario, Biblioteca miDepo)
        {
            Console.Clear();
            int IdPrestamo = InputHelper.LeerEntero("Ingrese ID del Libro: ");

            string resultado = miDepo.PrestarLibro(IdPrestamo, usuario.Nombre);
            if (resultado.Contains("exitoso"))
            {
                ConsoleHelper.Success(resultado);
            }
            else
            {
                ConsoleHelper.Warning(resultado);
            }
        }
       private static void ManejarDevolucion(Usuario usuario, Biblioteca miDepo)
        {
            Console.Clear();
            int IdDevolucion = InputHelper.LeerEntero("Ingrese ID del Libro: ");

            string resultado = miDepo.DevolverLibro(IdDevolucion, usuario.Nombre);
            if (resultado.Contains("exitoso"))
            {
                ConsoleHelper.Success(resultado);
            }
            else
            {
                ConsoleHelper.Warning(resultado);
            }
        }
        private static void ManejarAgregarLibro(Biblioteca miDepo)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== AGREGAR NUEVO LIBRO ===");
            Console.ResetColor();
            string titulo = InputHelper.LeerTexto("Titulo: ");
            string autor = InputHelper.LeerTexto("Autor: ");
            int unidades = InputHelper.LeerEntero("Unidades disponibles: ");
            if (unidades < 0)
            {
                ConsoleHelper.Error("Las unidades no pueden ser negativas.");
                return;
            }
            miDepo.RegistrarNuevoLibro(titulo, autor, unidades);
            ConsoleHelper.Success("Libro agregado correctamente.");

        }
    }
}