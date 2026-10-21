using BibliotecaV2.Core.Enums;

namespace BibliotecaV2.Core.Entities
{
    public class Usuario
    {
        public string Nombre { get; set; } = "";

        // BCrypt hash — nunca se guarda la contraseña en texto plano
        public string PasswordHash { get; set; } = "";

        public Rol Rol { get; set; } = Rol.Usuario;

        public DateTime FechaRegistro { get; set; }

        // Constructor vacío requerido por JsonSerializer
        public Usuario() { }

        public Usuario(string nombre, string passwordHash, Rol rol = Rol.Usuario)
        {
            Nombre = nombre;
            PasswordHash = passwordHash;
            Rol = rol;
            FechaRegistro = DateTime.Now;
        }
    }
}
