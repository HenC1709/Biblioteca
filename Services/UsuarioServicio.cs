using System.Text.Json;
using BibliotecaV1.Models;

namespace BibliotecaV1.Services
{
  public static class UsuarioServicio
    {
        private static string ruta = Path.Combine("Data", "Usuarios.json");
        public static List<Usuario> Cargar()
        {
            if (!File.Exists(ruta))
            return new List<Usuario>();

            string contenido = File.ReadAllText(ruta);
            return JsonSerializer.Deserialize<List<Usuario>>(contenido) ?? new List<Usuario>();

        }

        public static void Guardar(List<Usuario> usuarios)
        {
            var json = JsonSerializer.Serialize(usuarios, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(ruta, json);
        }

        public static bool ExisteUsuario(List<Usuario> lista, string nombre)
        {
            return lista.Any(u => u.nombre.ToLower() == nombre.ToLower());
        }
    }
}