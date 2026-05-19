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
            ConsoleHelper.Tiltte(" 🌸 BIBLIOTECA LOGIN 🌸 ");
            Console.WriteLine("1. Iniciar Sesión");
            Console.WriteLine("2. Registro");
            Console.Write("\nSeleccione una opción: ");
            string respuesta = Console.ReadLine()!.ToUpper();
            var lista = UsuarioServicio.Cargar();

            // 🔹 REGISTRO
            if (respuesta == "2")
            {
               
                Usuario nuevo = new Usuario();

                Console.Write("Nombre: ");
                nuevo.nombre = Console.ReadLine()!;

                Console.Write("ID (4 números): ");
                string id = Console.ReadLine()!;

                while (!SoloNumeros(id))
                {
                    ConsoleHelper.Error("ID Invalida (4 numeros)");
                    id = Console.ReadLine()!;
                }
                nuevo.id = id;

                lista.Add(nuevo);
                UsuarioServicio.Guardar(lista);

                ConsoleHelper.Success("Usuario registrado correctamente!");
                Console.ReadKey();
                return null;
            }

            // 🔹 LOGIN
            Console.Clear();
            ConsoleHelper.Tiltte("LOGIN");

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine()!;

            Console.Write("ID: ");
            string idlogin = Console.ReadLine()!;

            var listaUsuarios = UsuarioServicio.Cargar();

            var user = listaUsuarios
                .FirstOrDefault(u => u.nombre.ToLower() == nombre.ToLower() && u.id == idlogin);

            if (user != null)
            {
                ConsoleHelper.Success($"Bienvenido {user.nombre} ROL: {user.rol} 😎 ");
                Console.ReadKey();
                return user;
            }

            ConsoleHelper.Error("Usuario o ID incorrecto");
            Console.ReadKey();
            return null;
        }

        private bool SoloNumeros(string texto)
        {
            return texto.Length == 4 && texto.All(char.IsDigit);
        }
    }
}

  