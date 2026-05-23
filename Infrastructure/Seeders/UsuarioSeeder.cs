using BibliotecaV2.Core.Entities;
using BibliotecaV2.Core.Enums;
using BibliotecaV2.Core.Interfaces;

namespace BibliotecaV2.Infrastructure.Seeders
{
    public class UsuarioSeeder
    {
        private readonly IUsuarioRepository _repo;

        public UsuarioSeeder(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public void Seed()
        {
            if (_repo.ObtenerTodos().Any()) return;

            // Contraseña por defecto: "admin123" — se hashea aquí mismo
            string hashAdmin = BCrypt.Net.BCrypt.HashPassword("admin123");

            var usuarios = new List<Usuario>
            {
                new Usuario("admin", hashAdmin, Rol.Admin)
            };

            _repo.Guardar(usuarios);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[SEEDER] Usuario admin creado. Contraseña por defecto: admin123");
            Console.ResetColor();
        }
    }
}