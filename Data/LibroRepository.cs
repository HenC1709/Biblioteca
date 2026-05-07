using System.Text.Json;
using BibliotecaV1.Models;

namespace BibliotecaV1.Data
{
   public class LibroRepository
    {
        private readonly string _ruta = Path.Combine("Data", "LibrosGuardados.json");

        public List<Libro> LeerLibros()
        {
            if (!File.Exists(_ruta)) return new List<Libro>();
            string contenido = File.ReadAllText(_ruta);
            return JsonSerializer.Deserialize<List<Libro>>(contenido) ?? new List<Libro>();
        }

        public void GuardarLibros(List<Libro> libros)
        {
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(libros, opciones);
            File.WriteAllText(_ruta, json);
        }
    }  
}
