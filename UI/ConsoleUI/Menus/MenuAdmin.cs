using BibliotecaV2.Application.Services;
using BibliotecaV2.Core.Entities;
using BibliotecaV2.Core.Enums;
using BibliotecaV2.UI.ConsoleUI.Helpers;

namespace BibliotecaV2.UI.ConsoleUI.Menus
{
    public class MenuAdmin : MenuBibliotecario
    {
        private readonly UsuarioService _usuarioService;

        public MenuAdmin(
            LibroService libroService,
            PrestamoService prestamoService,
            UsuarioService usuarioService,
            Usuario usuario)
            : base(libroService, prestamoService, usuario)
        {
            _usuarioService = usuarioService;
        }

        protected override void MostrarOpciones()
        {
            base.MostrarOpciones();
            Console.WriteLine("8. Gestionar usuarios");
        }

        protected override void ManejarOpcion(string opcion)
        {
            if (opcion == "8")
                ManejarGestionUsuarios();
            else
                base.ManejarOpcion(opcion);
        }

        private void ManejarGestionUsuarios()
        {
            Console.Clear();
            ConsoleHelper.Title("GESTIÓN DE USUARIOS");

            var usuarios = _usuarioService.ObtenerTodos();

            if (!usuarios.Any())
            {
                ConsoleHelper.Info("No hay usuarios registrados.");
                ConsoleHelper.Pause();
                return;
            }

            ConsoleHelper.Info("Usuarios registrados:\n");
            foreach (var u in usuarios)
                Console.WriteLine($"  {u.Nombre,-20} [{u.Rol}]  —  desde {u.FechaRegistro:dd/MM/yyyy}");

            Console.WriteLine();
            string nombre = InputHelper.LeerTexto("Nombre del usuario a modificar (0 para cancelar): ");
            if (nombre == "0") return;

            Console.WriteLine("\nRoles disponibles:");
            Console.WriteLine("  1. Usuario");
            Console.WriteLine("  2. Bibliotecario");
            Console.WriteLine("  3. Admin");

            string rolOpcion = InputHelper.LeerTexto("Nuevo rol: ");
            Rol nuevoRol = rolOpcion switch
            {
                "1" => Rol.Usuario,
                "2" => Rol.Bibliotecario,
                "3" => Rol.Admin,
                _ => Rol.Usuario
            };

            _usuarioService.CambiarRol(nombre, nuevoRol);
            ConsoleHelper.Success($"Rol de '{nombre}' actualizado a {nuevoRol}.");
        }
    }
}