using System;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using BibliotecaV1.Helpers;
using BibliotecaV1.Models;

namespace BibliotecaV1.Logic
{
    public class TicketManager
    {
         private readonly string _carpeta = "Ticket_Biblioteca";

         public TicketManager()
        {
  if (!Directory.Exists(_carpeta)) Directory.CreateDirectory(_carpeta);
        }

public void GenerarTicketPrestamo(Libro libro, string usuario)
{ 
string ticketId = CrearTicketId("PRE");
string contenido = $@"
==========================================
        NUEVO PRESTAMO DETECTADO
==========================================
TICKET: {ticketId}
FECHA: {DateTime.Now:dd/MM/yyyy HH:mm:ss}
USUARIO: {usuario}

LIBRO: {libro.Titulo.PadRight(25)}
AUTOR: {libro.Autor.PadRight(25)}

STOCK ACTUAL: {libro.Unidades}
------------------------------------------
 *** POR FAVOR DEVOLVER A TIEMPO ***


";
GuardarTicket("Prestamo", ticketId, contenido);
}

public void GenerarTicketDevolucion(Libro libro, string usuario)
{
string ticketId = CrearTicketId("DEV");
string contenido = $@" 
==========================================
         NUEVA DEVOLUCIÓN 
==========================================
TICKET: {ticketId}
FECHA: {DateTime.Now:dd/MM/yyyy HH:mm:ss}
USUARIO: {usuario}

LIBRO: {libro.Titulo.PadRight(25)}
AUTOR: {libro.Autor.PadRight(25)}

NUEVO STOCK: {libro.Unidades}
------------------------------------------
 ***GRACIAS POR USAR NUESTRO SERVICIO*** <3


";
GuardarTicket("Devolucion", ticketId, contenido);
}
    private readonly Random _random = new Random();
    private string CrearTicketId(string prefijo)
        {
           
           int numero = _random.Next(1000, 9999);
           return $"{prefijo}-{numero}"; 
        }
    private void GuardarTicket(string nombreArchivo, string ticketId, string contenido)
        {
            string archiovFinal= $"{nombreArchivo}_{ticketId}";

            string ruta = Path.Combine(_carpeta, archiovFinal);
            File.WriteAllText(ruta, contenido);
            ConsoleHelper.Success($"\n[TICKET] Ticket generado: {archiovFinal}");
        }
    }
}