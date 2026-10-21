using System.Text.Json;
using BibliotecaV2.Core.Entities;
using BibliotecaV2.Core.Interfaces;

namespace BibliotecaV2.Infrastructure.Repositories
{
    public class JsonUsuarioRepository : IUsuarioRepository
    {
        private readonly string _ruta = Path.Combine("Data", "usuarios.json");

        public List<Usuario> ObtenerTodos()
        {
            if (!File.Exists(_ruta)) return new List<Usuario>();
            string json = File.ReadAllText(_ruta);
            return JsonSerializer.Deserialize<List<Usuario>>(json) ?? new List<Usuario>();
        }

        public Usuario? ObtenerPorNombre(string nombre)
        {
            return ObtenerTodos()
                .FirstOrDefault(u => u.Nombre.ToLower() == nombre.ToLower());
        }

        public void Guardar(List<Usuario> usuarios)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_ruta)!);
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_ruta, JsonSerializer.Serialize(usuarios, opciones));
        }
    }
}