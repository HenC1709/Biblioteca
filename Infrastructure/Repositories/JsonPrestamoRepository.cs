using System.Text.Json;
using BibliotecaV2.Core.Entities;
using BibliotecaV2.Core.Enums;
using BibliotecaV2.Core.Interfaces;

namespace BibliotecaV2.Infrastructure.Repositories
{
    public class JsonPrestamoRepository : IPrestamoRepository
    {
        private readonly string _ruta = Path.Combine("Data", "prestamos.json");

        public List<Prestamo> ObtenerTodos()
        {
            if (!File.Exists(_ruta)) return new List<Prestamo>();
            string json = File.ReadAllText(_ruta);
            return JsonSerializer.Deserialize<List<Prestamo>>(json) ?? new List<Prestamo>();
        }

        public List<Prestamo> ObtenerPorUsuario(string usuarioNombre)
        {
            return ObtenerTodos()
                .Where(p => p.UsuarioNombre.ToLower() == usuarioNombre.ToLower())
                .ToList();
        }

        public List<Prestamo> ObtenerPorEstado(EstadoPrestamo estado)
        {
            return ObtenerTodos()
                .Where(p => p.Estado == estado)
                .ToList();
        }

        public void Guardar(List<Prestamo> prestamos)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_ruta)!);
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_ruta, JsonSerializer.Serialize(prestamos, opciones));
        }
    }
}