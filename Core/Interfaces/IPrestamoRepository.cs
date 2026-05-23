using BibliotecaV2.Core.Entities;
using BibliotecaV2.Core.Enums;

namespace BibliotecaV2.Core.Interfaces
{
    public interface IPrestamoRepository
    {
        List<Prestamo> ObtenerTodos();
        List<Prestamo> ObtenerPorUsuario(string usuarioNombre);
        List<Prestamo> ObtenerPorEstado(EstadoPrestamo estado);
        void Guardar(List<Prestamo> prestamos);
    }
}
