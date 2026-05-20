using System.Text.Json;
using BibliotecaV1.Models;

namespace BibliotecaV1.Data
{
    public class UsuarioRepository
    {
        private readonly string _ruta = Path.Combine("Data", "Usuarios.json");

        public List<Usuario> LeerUsuarios()
        {
            if (!File.Exists(_ruta))
            {
                return new List<Usuario>();
            }
            string contenido = File.ReadAllText(_ruta);
            return JsonSerializer.Deserialize<List<Usuario>>(contenido)
            ?? new List<Usuario>(); 
        }

        public void GuardarUsuario(List<Usuario> usuarios)
        {
            Directory.CreateDirectory("Data");
            var opciones = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(usuarios, opciones);
            File.WriteAllText(_ruta, json);
        }
    }
}