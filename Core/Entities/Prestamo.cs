using BibliotecaV2.Core.Enums;

namespace BibliotecaV2.Core.Entities
{
    public class Prestamo
    {
        public Guid Id { get; set; }
        public int LibroId { get; set; }
        public string UsuarioNombre { get; set; } = "";
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaLimite { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public EstadoPrestamo Estado { get; set; }

        // Constructor vacío requerido por JsonSerializer
        public Prestamo() { }

        public Prestamo(int libroId, string usuarioNombre, int diasLimite = 7)
        {
            Id = Guid.NewGuid();
            LibroId = libroId;
            UsuarioNombre = usuarioNombre;
            FechaPrestamo = DateTime.Now;
            FechaLimite = DateTime.Now.AddDays(diasLimite);
            FechaDevolucion = null;
            Estado = EstadoPrestamo.Activo;
        }

        public bool EstaVencido()
        {
            return Estado == EstadoPrestamo.Activo && DateTime.Now > FechaLimite;
        }

        public int DiasDeAtraso()
        {
            if (!EstaVencido()) return 0;
            return (int)(DateTime.Now - FechaLimite).TotalDays;
        }
    }
}
