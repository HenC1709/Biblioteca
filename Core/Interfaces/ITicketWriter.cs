using BibliotecaV2.Core.Entities;
using BibliotecaV2.Core.Enums;

namespace BibliotecaV2.Core.Interfaces
{
    public interface ITicketWriter
    {
        void GenerarTicket(TipoTicket tipo, Libro libro, string usuarioNombre, Multa? multa = null);
    }
}
