using BibliotecaV2.Core.Entities;
using BibliotecaV2.Core.Enums;
using BibliotecaV2.Core.Interfaces;
using MySqlConnector;

namespace BibliotecaV2.Infrastructure.Repositories
{
    public class MySqlUsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public MySqlUsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Usuario> ObtenerTodos()
        {
            var usuarios = new List<Usuario>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            using var cmd = new MySqlCommand(
                "SELECT nombre, password_hash, rol, fecha_registro FROM usuarios", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                usuarios.Add(new Usuario
                {
                    Nombre = reader.GetString("nombre"),
                    PasswordHash = reader.GetString("password_hash"),
                    Rol = (Rol)reader.GetInt32("rol"),
                    FechaRegistro = reader.GetDateTime("fecha_registro")
                });
            }
            return usuarios;
        }

        public Usuario? ObtenerPorNombre(string nombre)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            using var cmd = new MySqlCommand(
                "SELECT nombre, password_hash, rol, fecha_registro FROM usuarios WHERE LOWER(nombre) = LOWER(@nombre)", conn);
            cmd.Parameters.AddWithValue("@nombre", nombre);

            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;

            return new Usuario
            {
                Nombre = reader.GetString("nombre"),
                PasswordHash = reader.GetString("password_hash"),
                Rol = (Rol)reader.GetInt32("rol"),
                FechaRegistro = reader.GetDateTime("fecha_registro")
            };
        }

        public void Guardar(List<Usuario> usuarios)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            using var tx = conn.BeginTransaction();

            foreach (var u in usuarios)
            {
                using var cmd = new MySqlCommand(@"
                    INSERT INTO usuarios (nombre, password_hash, rol, fecha_registro)
                    VALUES (@nombre, @hash, @rol, @fecha)
                    ON DUPLICATE KEY UPDATE
                        password_hash = VALUES(password_hash),
                        rol = VALUES(rol);", conn, tx);

                cmd.Parameters.AddWithValue("@nombre", u.Nombre);
                cmd.Parameters.AddWithValue("@hash", u.PasswordHash);
                cmd.Parameters.AddWithValue("@rol", (int)u.Rol);
                cmd.Parameters.AddWithValue("@fecha", u.FechaRegistro);
                cmd.ExecuteNonQuery();
            }

            tx.Commit();
        }
    }
}
