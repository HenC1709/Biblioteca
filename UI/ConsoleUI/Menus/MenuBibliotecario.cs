using BibliotecaV2.Application.Services;
using BibliotecaV2.Core.Entities;
using BibliotecaV2.UI.ConsoleUI.Helpers;

namespace BibliotecaV2.UI.ConsoleUI.Menus
{
    public class MenuBibliotecario : MenuUsuario
    {
        public MenuBibliotecario(LibroService libroService, PrestamoService prestamoService, Usuario usuario)
            : base(libroService, prestamoService, usuario) { }

        protected override void MostrarOpciones()
        {
            base.MostrarOpciones();
            Console.WriteLine("6. Agregar libro");
            Console.WriteLine("7. Ver todos los préstamos activos");
        }

        protected override void ManejarOpcion(string opcion)
        {
            switch (opcion)
            {
                case "6": ManejarAgregarLibro(); break;
                case "7": ManejarPrestamosActivos(); break;
                default: base.ManejarOpcion(opcion); break;
            }
        }

        private void ManejarAgregarLibro()
        {
            Console.Clear();
            ConsoleHelper.Title("AGREGAR LIBRO");

            string titulo = InputHelper.LeerTexto("Título: ");
            string autor = InputHelper.LeerTexto("Autor: ");
            int unidades = InputHelper.LeerEnteroPositivo("Unidades disponibles: ");

            var libro = _libroService.Agregar(titulo, autor, unidades);
            ConsoleHelper.Success($"Libro agregado con ID {libro.Id}: {libro.Titulo}");
        }

        private void ManejarPrestamosActivos()
        {
            Console.Clear();
            ConsoleHelper.Title("PRÉSTAMOS ACTIVOS");

            var activos = _prestamoService.ObtenerActivos();

            if (!activos.Any())
            {
                ConsoleHelper.Info("No hay préstamos activos en este momento.");
                return;
            }

            ConsoleHelper.Info($"{activos.Count} préstamo(s) activo(s):\n");

            foreach (var p in activos)
            {
                string linea = $"Usuario: {p.UsuarioNombre} | Libro ID: {p.LibroId} | Límite: {p.FechaLimite:dd/MM/yyyy}";

                if (p.EstaVencido())
                    ConsoleHelper.Error($"{linea} [VENCIDO - {p.DiasDeAtraso()} días]");
                else
                    ConsoleHelper.Success(linea);
            }
        }
    }
}