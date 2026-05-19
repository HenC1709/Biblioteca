using System;
using System.Data;
using System.IO;
using BibliotecaV1.Helpers;
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
            Random random = new Random();
            int numero = random.Next(1000, 9999);
            string ticketId= $"PRE-{numero}";
  string nombreArchivo = Path.Combine(carpeta, $"Prestamo.txt" );

  
string contenido = $@"
==========================================
        NUEVO PRESTAMO DETECTADO
==========================================
TICKET: {ticketId}
FECHA: {DateTime.Now:dd/MM/yyyy HH:mm:ss}
USUARIO: {usuario}

lIBRO: {libro.Titulo.PadRight(25)}
AUTOR: {libro.Autor.PadRight(25)}

STOCK ACTUAL: {libro.Unidades}
------------------------------------------
 *** POR FAVOR DEVOLVER A TIEMPO ***


";


File.AppendAllText(nombreArchivo, contenido);
Console.ForegroundColor = ConsoleColor.Green;
ConsoleHelper.Success($"\n[TICKET] Comprobante de prestamo creado: {nombreArchivo} ");
Console.ResetColor();
        }

        public void GenerarTicketDevolucion(Libro libro, string usuario)
        {
            Random random = new Random();
            int numero = random.Next(1000, 9999);
            string ticketId= $"DEV-{numero}";
  string nombreArchivo = Path.Combine(carpeta, $"Devolucion.txt");

  string contenido = $@" 

==========================================
         NUEVA DEVOLUCIÓN 
==========================================
TICKET: {ticketId}
FECHA: {DateTime.Now:dd/MM/yyyy HH:mm:ss}
USUARIO: {usuario}

lIBRO: {libro.Titulo.PadRight(25)}
AUTOR: {libro.Autor.PadRight(25)}

NUEVO STOCK: {libro.Unidades}
------------------------------------------
 ***GRACIAS POR USAR NUESTRO SERVICIO*** <3


";

  File.AppendAllText(nombreArchivo, contenido);
  Console.ForegroundColor = ConsoleColor.Green;
  ConsoleHelper.Success($"\n[TICKET] Comprobante de devolución creado.");
  Console.ResetColor();
        }
    }

}