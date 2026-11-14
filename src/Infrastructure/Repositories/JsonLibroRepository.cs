using System.Text.Json;
using BibliotecaV2.Core.Entities;
using BibliotecaV2.Core.Interfaces;

namespace BibliotecaV2.Infrastructure.Repositories
{
    public class JsonLibroRepository : ILibroRepository
    {
        private readonly string _ruta = Path.Combine("Data", "libros.json");

        public List<Libro> ObtenerTodos()
        {
            if (!File.Exists(_ruta)) return new List<Libro>();
            string json = File.ReadAllText(_ruta);
            return JsonSerializer.Deserialize<List<Libro>>(json) ?? new List<Libro>();
        }

        public Libro? ObtenerPorId(int id)
        {
            return ObtenerTodos().FirstOrDefault(l => l.Id == id);
        }

        public void Guardar(List<Libro> libros)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_ruta)!);
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_ruta, JsonSerializer.Serialize(libros, opciones));
        }
    }
}