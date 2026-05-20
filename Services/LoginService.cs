using BibliotecaV1.Models;
using BibliotecaV1.Helpers;
using System.Linq;
using System.Data.Common;


namespace BibliotecaV1.Services
{
    public class LoginService
    {
        // La ruta hacia tu carpeta de datos protegida  public class LoginService
        public Usuario? IniciarSesion()
        {
            Console.Clear();
            ConsoleHelper.Title("LOGIN");
            Console.WriteLine("1. Iniciar Sesión");
            Console.WriteLine("2. Registro");
            string respuesta = InputHelper.LeerTexto("\nSeleccione una opción: ").ToUpper();
            var lista = UsuarioServicio.Cargar();

            // 🔹 REGISTRO
            if (respuesta == "2")
            {
               
                Usuario nuevo = new Usuario();

                nuevo.Nombre = InputHelper.LeerTexto("Nombre: ");

                string id = InputHelper.LeerTexto("ID (4 numeros): ");

                while (!SoloNumeros(id))
                {
                    ConsoleHelper.Warning("ID Invalida (4 numeros): ");
                    id = InputHelper.LeerTexto("ID (4 numeros): ");
                }
                if (UsuarioServicio.ExisteUsuario(lista, nuevo.Nombre))
                {
                    ConsoleHelper.Warning("Ese usuario ya existe.");
                    ConsoleHelper.Pause();
                    return null;
                }
                nuevo.Id = id;

                lista.Add(nuevo);
                UsuarioServicio.Guardar(lista);

                ConsoleHelper.Success("Usuario registrado correctamente!");
                ConsoleHelper.Pause();
                return null;
            }

            // 🔹 LOGIN
            Console.Clear();
            ConsoleHelper.Title("LOGIN");

            string nombre = InputHelper.LeerTexto("Nombre: ");

            string idlogin = InputHelper.LeerTexto("ID: ");

            var listaUsuarios = UsuarioServicio.Cargar();

            var user = listaUsuarios
                .FirstOrDefault(u => u.Nombre.ToLower() == nombre.ToLower() && u.Id == idlogin);

            if (user != null)
            {
                ConsoleHelper.Success($"Bienvenido {user.Nombre} ROL: {user.Rol} 😎 ");
                ConsoleHelper.Pause();
                return user;
            }

            ConsoleHelper.Error("Usuario o ID incorrecto");
            ConsoleHelper.Pause();
            return null;
        }

        private bool SoloNumeros(string texto)
        {
            return texto.Length == 4 && texto.All(char.IsDigit);
        }
    }
}

  