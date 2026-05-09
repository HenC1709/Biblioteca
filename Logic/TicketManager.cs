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
  string nombreArchivo = Path.Combine(carpeta, $"Prestamo.txt" );

  
string contenido = $@"
==========================================
        NUEVO PRESTAMO DETECTADO
==========================================
FECHA: {DateTime.Now:dd/MM/yyyy HH:mm:ss}
USUARIO: {usuario}
lIBRO: {libro.Titulo.PadRight(25)}
AUTOR: {libro.Autor.PadRight(25)}
------------------------------------------
 *** POR FAVOR DEVOLVER A TIEMPO ***


";


File.AppendAllText(nombreArchivo, contenido);
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"\n[TICKET] Comprobante de prestamo creado: {nombreArchivo} ");
Console.ResetColor();
        }

        public void GenerarTicketDevolucion(Libro libro, string usuario)
        {
  string nombreArchivo = Path.Combine(carpeta, $"Devolucion.txt");

  string contenido = $@" 

==========================================
         NUEVA DEVOLUCIÓN 
==========================================
FECHA: {DateTime.Now:dd/MM/yyyy HH:mm:ss}
USUARIO: {usuario}
lIBRO: {libro.Titulo.PadRight(25)}
AUTOR: {libro.Autor.PadRight(25)}
------------------------------------------
 ***GRACIAS POR USAR NUESTRO SERVICIO*** <3

 
";

  File.AppendAllText(nombreArchivo, contenido);
  Console.ForegroundColor = ConsoleColor.Green;
  Console.WriteLine($"\n[TICKET] Comprobante de devolución creado.");
  Console.ResetColor();
        }
    }

}