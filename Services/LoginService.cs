using BibliotecaV1.Models;
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
            Console.WriteLine(" ==== ¿Tienes cuenta? =======");
            Console.Write("(SI/NO) ¿?");
            string respuesta = Console.ReadLine()!.ToUpper();
            var lista = UsuarioServicio.Cargar();

            // 🔹 REGISTRO
            if (respuesta == "N")
            {
               
                Usuario nuevo = new Usuario();

                Console.Write("Nombre: ");
                nuevo.nombre = Console.ReadLine()!;

                Console.Write("ID (4 números): ");
                string id = Console.ReadLine()!;

                while (!SoloNumeros(id))
                {
                    Console.WriteLine("ID Invalida (4 numeros)");
                    id = Console.ReadLine()!;
                }
                nuevo.id = id;

                lista.Add(nuevo);
                UsuarioServicio.Guardar(lista);

                Console.WriteLine("Usuario registrado correctamente!");
                Console.ReadKey();
                return null;
            }

            // 🔹 LOGIN
            Console.Clear();
            Console.WriteLine("=== LOGIN ===");

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine()!;

            Console.Write("ID: ");
            string idlogin = Console.ReadLine()!;

            var listaUsuarios = UsuarioServicio.Cargar();

            var user = listaUsuarios
                .FirstOrDefault(u => u.nombre.ToLower() == nombre.ToLower());

            if (user != null)
            {
                Console.WriteLine($"Bienvenido {user.nombre} 😎");
                Console.ReadKey();
                return user;
            }

            Console.WriteLine("Usuario o ID incorrecto");
            Console.ReadKey();
            return null;
        }

        private bool SoloNumeros(string texto)
        {
            return texto.Length == 4 && texto.All(char.IsDigit);
        }
    }
}

  