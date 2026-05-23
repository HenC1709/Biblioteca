using BibliotecaV2.Application.Services;
using BibliotecaV2.Core.Entities;
using BibliotecaV2.Core.Exceptions;
using BibliotecaV2.UI.ConsoleUI.Helpers;

namespace BibliotecaV2.UI.ConsoleUI.Menus
{
    public class MenuUsuario
    {
        protected readonly LibroService _libroService;
        protected readonly PrestamoService _prestamoService;
        protected readonly Usuario _usuario;

        public MenuUsuario(LibroService libroService, PrestamoService prestamoService, Usuario usuario)
        {
            _libroService = libroService;
            _prestamoService = prestamoService;
            _usuario = usuario;
        }

        public virtual void Mostrar()
        {
            string opcion;
            do
            {
                Console.Clear();
                ConsoleHelper.Title("BIBLIOTECA V2");
                Console.WriteLine($"Usuario : {_usuario.Nombre}  |  Rol: {_usuario.Rol}\n");

                MostrarOpciones();

                opcion = InputHelper.LeerTexto("\nSeleccione una opción: ");
                ManejarOpcion(opcion);

                if (opcion != "0") ConsoleHelper.Pause();
            }
            while (opcion != "0");
        }

        protected virtual void MostrarOpciones()
        {
            Console.WriteLine("1. Buscar libro");
            Console.WriteLine("2. Ver catálogo");
            Console.WriteLine("3. Pedir préstamo");
            Console.WriteLine("4. Devolver libro");
            Console.WriteLine("5. Mis préstamos");
            Console.WriteLine("0. Cerrar sesión");
        }

        protected virtual void ManejarOpcion(string opcion)
        {
            switch (opcion)
            {
                case "1": ManejarBusqueda(); break;
                case "2": ManejarCatalogo(); break;
                case "3": ManejarPrestamo(); break;
                case "4": ManejarDevolucion(); break;
                case "5": ManejarMisPrestamos(); break;
                case "0": break;
                default: ConsoleHelper.Warning("Opción inválida."); break;
            }
        }

        protected void ManejarBusqueda()
        {
            Console.Clear();
            ConsoleHelper.Title("BUSCAR LIBRO");
            string query = InputHelper.LeerTexto("Título a buscar: ");
            var resultados = _libroService.BuscarPorTitulo(query);

            if (!resultados.Any())
            {
                ConsoleHelper.Warning("No se encontraron libros con ese título.");
                return;
            }

            ConsoleHelper.Info($"\n{resultados.Count} resultado(s):\n");
            foreach (var libro in resultados)
            {
                if (libro.Unidades > 0)
                    ConsoleHelper.Success(libro.ToString());
                else
                    ConsoleHelper.Error($"{libro} [SIN STOCK]");
            }
        }

        protected void ManejarCatalogo()
        {
            Console.Clear();
            ConsoleHelper.Title("CATÁLOGO DE LIBROS");
            var libros = _libroService.ObtenerTodos();

            if (!libros.Any())
            {
                ConsoleHelper.Warning("No hay libros registrados.");
                return;
            }

            foreach (var libro in libros)
            {
                if (libro.Unidades > 0)
                    ConsoleHelper.Success(libro.ToString());
                else
                    ConsoleHelper.Error($"{libro} [SIN STOCK]");
            }
        }

        protected void ManejarPrestamo()
        {
            Console.Clear();
            ConsoleHelper.Title("PEDIR PRÉSTAMO");
            int id = InputHelper.LeerEntero("ID del libro: ");

            try
            {
                var prestamo = _prestamoService.Prestar(id, _usuario.Nombre);
                ConsoleHelper.Success($"Préstamo exitoso. Devolver antes del {prestamo.FechaLimite:dd/MM/yyyy}.");
            }
            catch (Exception ex) when (
                ex is LibroNoEncontradoException ||
                ex is StockInsuficienteException ||
                ex is LimitePrestamosException)
            {
                ConsoleHelper.Error(ex.Message);
            }
        }

        protected void ManejarDevolucion()
        {
            Console.Clear();
            ConsoleHelper.Title("DEVOLVER LIBRO");
            int id = InputHelper.LeerEntero("ID del libro a devolver: ");

            try
            {
                var (prestamo, multa) = _prestamoService.Devolver(id, _usuario.Nombre);

                if (multa != null)
                    ConsoleHelper.Warning($"Devolución con multa: ${multa.Monto:F2} por {multa.DiasDeAtraso} días de atraso.");
                else
                    ConsoleHelper.Success("Devolución exitosa. ¡Gracias!");
            }
            catch (Exception ex) when (
                ex is LibroNoEncontradoException ||
                ex is PrestamoNoActivoException)
            {
                ConsoleHelper.Error(ex.Message);
            }
        }

        protected void ManejarMisPrestamos()
        {
            Console.Clear();
            ConsoleHelper.Title("MIS PRÉSTAMOS");
            var prestamos = _prestamoService.ObtenerPorUsuario(_usuario.Nombre);

            if (!prestamos.Any())
            {
                ConsoleHelper.Info("No tienes préstamos registrados.");
                return;
            }

            foreach (var p in prestamos)
            {
                string estado = p.EstaVencido() ? "[VENCIDO]" : $"[{p.Estado}]";
                string linea = $"Libro ID: {p.LibroId} | Prestado: {p.FechaPrestamo:dd/MM/yy} | Límite: {p.FechaLimite:dd/MM/yy} {estado}";

                if (p.EstaVencido())
                    ConsoleHelper.Error(linea);
                else if (p.Estado == Core.Enums.EstadoPrestamo.Activo)
                    ConsoleHelper.Success(linea);
                else
                    ConsoleHelper.Info(linea);
            }
        }
    }
}