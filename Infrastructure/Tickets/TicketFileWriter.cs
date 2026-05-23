using BibliotecaV2.Core.Entities;
using BibliotecaV2.Core.Enums;
using BibliotecaV2.Core.Interfaces;

namespace BibliotecaV2.Infrastructure.Tickets
{
    public class TicketFileWriter : ITicketWriter
    {
        private readonly string _carpeta = "Tickets";

        public TicketFileWriter()
        {
            Directory.CreateDirectory(_carpeta);
        }

        public void GenerarTicket(TipoTicket tipo, Libro libro, string usuarioNombre, Multa? multa = null)
        {
            string ticketId = $"{tipo.ToString().ToUpper()[..3]}-{Guid.NewGuid().ToString()[..8].ToUpper()}";
            string contenido = ConstruirContenido(tipo, ticketId, libro, usuarioNombre, multa);
            string nombreArchivo = $"{tipo}_{ticketId}.txt";
            string ruta = Path.Combine(_carpeta, nombreArchivo);

            File.WriteAllText(ruta, contenido);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"\n[TICKET] Generado: {nombreArchivo}");
            Console.ResetColor();
        }

        private string ConstruirContenido(TipoTicket tipo, string ticketId, Libro libro, string usuarioNombre, Multa? multa)
        {
            string titulo = tipo switch
            {
                TipoTicket.Prestamo    => "NUEVO PRÉSTAMO",
                TipoTicket.Devolucion  => "DEVOLUCIÓN REGISTRADA",
                TipoTicket.Multa       => "DEVOLUCIÓN CON MULTA",
                _                      => "TRANSACCIÓN"
            };

            string mensaje = tipo switch
            {
                TipoTicket.Prestamo    => "*** POR FAVOR DEVOLVER A TIEMPO ***",
                TipoTicket.Devolucion  => "*** GRACIAS POR USAR NUESTRO SERVICIO ***",
                TipoTicket.Multa       => $"*** MULTA: ${multa?.Monto:F2} por {multa?.DiasDeAtraso} días de atraso ***",
                _                      => ""
            };

            return $"""
==========================================
{titulo}
==========================================
TICKET : {ticketId}
FECHA  : {DateTime.Now:dd/MM/yyyy HH:mm:ss}
USUARIO: {usuarioNombre}

LIBRO  : {libro.Titulo}
AUTOR  : {libro.Autor}
STOCK  : {libro.Unidades} unidades

------------------------------------------
{mensaje}

""";
        }
    }
}