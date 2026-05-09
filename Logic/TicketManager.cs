using System;
using System.Data;
using System.IO;
using BibliotecaV1.Models;

namespace BibliotecaV1.Logic
{
    public class TicketManager
    {
         private string carpeta = "Ticket_Biblioteca";

         public TicketManager()
        {
            if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
        }

        public void GenerarTicketPrestamo(Libro libro, string usuario)
        {
            string nombreArchivo = Path.Combine(carpeta, $"Prestamo_{DateTime.Now:yyyyMMdd_HHmmss}.txt" );

            string contenido = $@"

 ___________________________________________
|                                           |
|   SISTEMA DE BIBLIOTECA - PRESTAMO [OK]   |
|___________________________________________|
|                                           |
|  FECHA: {DateTime.Now:dd/MM/yyyy HH:mm:ss}               |
|  USUARIO: {usuario.PadRight(20)}            |
|___________________________________________|
|                                           |
|  DETALLES DEL LIBRO:                      |
|  TITULO: {libro.Titulo.PadRight(25)}        |
|  AUTOR:  {libro.Autor.PadRight(25)}        |
|___________________________________________|
|                                           |
|   *** POR FAVOR DEVOLVER A TIEMPO ***     |
|___________________________________________|";

File.WriteAllText(nombreArchivo, contenido);
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"\n[TICKET] Comprobante de prestamo creado: {nombreArchivo} ");
Console.ResetColor();
        }

        public void GenerarTicketDevolucion(Libro libro, string usuario)
        {
            string nombreArchivo = Path.Combine(carpeta, $"Devolucion_{DateTime.Now:yyyyMMdd_HHmmss}.txt");

            string contenido = $@" 

 ___________________________________________
|                                           |
|   SISTEMA DE BIBLIOTECA - DEVOLUCION      |
|___________________________________________|
|                                           |
|  FECHA: {DateTime.Now:dd/MM/yyyy HH:mm:ss}               |
|  USUARIO: {usuario.PadRight(20)}            |
|___________________________________________|
|                                           |
|  ESTADO: DEVUELTO EXITOSAMENTE            |
|  LIBRO: {libro.Titulo.PadRight(26)}        |
|___________________________________________|
|                                           |
|     GRACIAS POR USAR NUESTRO SERVICIO     |
|___________________________________________|";

            File.WriteAllText(nombreArchivo, contenido);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[TICKET] Comprobante de devolución creado.");
            Console.ResetColor();
        }
    }

}