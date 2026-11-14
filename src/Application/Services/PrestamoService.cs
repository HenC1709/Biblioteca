using BibliotecaV2.Core.Entities;
using BibliotecaV2.Core.Enums;
using BibliotecaV2.Core.Exceptions;
using BibliotecaV2.Core.Interfaces;

namespace BibliotecaV2.Application.Services
{
    public class PrestamoService
    {
        private const int LimitePrestamosPorUsuario = 3;

        private readonly IPrestamoRepository _prestamoRepo;
        private readonly ILibroRepository _libroRepo;
        private readonly ITicketWriter _ticketWriter;
        private readonly MultaService _multaService;

        public PrestamoService(
            IPrestamoRepository prestamoRepo,
            ILibroRepository libroRepo,
            ITicketWriter ticketWriter,
            MultaService multaService)
        {
            _prestamoRepo = prestamoRepo;
            _libroRepo = libroRepo;
            _ticketWriter = ticketWriter;
            _multaService = multaService;
        }

        public Prestamo Prestar(int libroId, string usuarioNombre)
        {
            // 1. El libro existe
            var libro = _libroRepo.ObtenerPorId(libroId)
                ?? throw new LibroNoEncontradoException(libroId);

            // 2. Hay stock
            if (libro.Unidades <= 0)
                throw new StockInsuficienteException(libro.Titulo);

            // 3. El usuario no llegó al límite
            var prestamosActivos = _prestamoRepo
                .ObtenerPorUsuario(usuarioNombre)
                .Count(p => p.Estado == EstadoPrestamo.Activo);

            if (prestamosActivos >= LimitePrestamosPorUsuario)
                throw new LimitePrestamosException(LimitePrestamosPorUsuario);

            // 4. Registrar préstamo y descontar stock
            var prestamo = new Prestamo(libroId, usuarioNombre);
            libro.Unidades--;

            var prestamos = _prestamoRepo.ObtenerTodos();
            prestamos.Add(prestamo);

            var libros = _libroRepo.ObtenerTodos();
            var libroEnLista = libros.First(l => l.Id == libroId);
            libroEnLista.Unidades = libro.Unidades;

            _prestamoRepo.Guardar(prestamos);
            _libroRepo.Guardar(libros);

            _ticketWriter.GenerarTicket(TipoTicket.Prestamo, libro, usuarioNombre);

            return prestamo;
        }

        public (Prestamo prestamo, Multa? multa) Devolver(int libroId, string usuarioNombre)
        {
            // 1. El libro existe
            var libro = _libroRepo.ObtenerPorId(libroId)
                ?? throw new LibroNoEncontradoException(libroId);

            // 2. El usuario tiene un préstamo activo de ese libro
            var prestamos = _prestamoRepo.ObtenerTodos();
            var prestamo = prestamos
                .FirstOrDefault(p =>
                    p.LibroId == libroId &&
                    p.UsuarioNombre.ToLower() == usuarioNombre.ToLower() &&
                    p.Estado == EstadoPrestamo.Activo)
                ?? throw new PrestamoNoActivoException(libroId, usuarioNombre);

            // 3. Calcular multa si hay atraso
            Multa? multa = _multaService.CalcularSiHayAtraso(prestamo);

            // 4. Actualizar estado del préstamo
            prestamo.FechaDevolucion = DateTime.Now;
            prestamo.Estado = multa != null ? EstadoPrestamo.ConMulta : EstadoPrestamo.Devuelto;

            // 5. Devolver unidad al stock
            var libros = _libroRepo.ObtenerTodos();
            var libroEnLista = libros.First(l => l.Id == libroId);
            libroEnLista.Unidades++;

            _prestamoRepo.Guardar(prestamos);
            _libroRepo.Guardar(libros);

            TipoTicket tipoTicket = multa != null ? TipoTicket.Multa : TipoTicket.Devolucion;
            _ticketWriter.GenerarTicket(tipoTicket, libro, usuarioNombre, multa);

            return (prestamo, multa);
        }

        public List<Prestamo> ObtenerActivos()
        {
            return _prestamoRepo.ObtenerPorEstado(EstadoPrestamo.Activo);
        }

        public List<Prestamo> ObtenerPorUsuario(string usuarioNombre)
        {
            return _prestamoRepo.ObtenerPorUsuario(usuarioNombre);
        }
    }
}