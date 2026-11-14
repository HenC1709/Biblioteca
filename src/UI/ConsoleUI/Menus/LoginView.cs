using BibliotecaV2.Application.Services;
using BibliotecaV2.Core.Entities;
using BibliotecaV2.Core.Exceptions;
using BibliotecaV2.UI.ConsoleUI.Helpers;

namespace BibliotecaV2.UI.ConsoleUI.Menus
{
    public class LoginView
    {
        private readonly AuthService _authService;

        public LoginView(AuthService authService)
        {
            _authService = authService;
        }

        // Retorna el usuario autenticado o null si el usuario cancela
        public Usuario? Mostrar()
        {
            while (true)
            {
                Console.Clear();
                ConsoleHelper.Title("BIBLIOTECA V2");
                Console.WriteLine("1. Iniciar sesión");
                Console.WriteLine("2. Registrarse");
                Console.WriteLine("0. Salir");

                string opcion = InputHelper.LeerTexto("\nSeleccione una opción: ");

                switch (opcion)
                {
                    case "1":
                        return ManejarLogin();
                    case "2":
                        ManejarRegistro();
                        break;
                    case "0":
                        return null;
                    default:
                        ConsoleHelper.Warning("Opción inválida.");
                        ConsoleHelper.Pause();
                        break;
                }
            }
        }

        private Usuario? ManejarLogin()
        {
            Console.Clear();
            ConsoleHelper.Title("INICIAR SESIÓN");

            string nombre = InputHelper.LeerTexto("Usuario: ");
            string password = InputHelper.LeerPassword("Contraseña: ");

            try
            {
                var usuario = _authService.Login(nombre, password);
                ConsoleHelper.Success($"\nBienvenido, {usuario.Nombre} [{usuario.Rol}]");
                ConsoleHelper.Pause();
                return usuario;
            }
            catch (CredencialesInvalidasException ex)
            {
                ConsoleHelper.Error(ex.Message);
                ConsoleHelper.Pause();
                return null;
            }
        }

        private void ManejarRegistro()
        {
            Console.Clear();
            ConsoleHelper.Title("REGISTRO");

            string nombre = InputHelper.LeerTexto("Nombre de usuario: ");
            string password = InputHelper.LeerPassword("Contraseña: ");
            string confirmacion = InputHelper.LeerPassword("Confirmar contraseña: ");

            if (password != confirmacion)
            {
                ConsoleHelper.Error("Las contraseñas no coinciden.");
                ConsoleHelper.Pause();
                return;
            }

            try
            {
                _authService.Registrar(nombre, password);
                ConsoleHelper.Success("Usuario registrado correctamente. Ya puedes iniciar sesión.");
            }
            catch (UsuarioYaExisteException ex)
            {
                ConsoleHelper.Error(ex.Message);
            }

            ConsoleHelper.Pause();
        }
    }
}