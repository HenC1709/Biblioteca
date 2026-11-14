using BibliotecaV2.Core.Entities;
using BibliotecaV2.Core.Enums;
using BibliotecaV2.Core.Interfaces;
using MySqlConnector;

namespace BibliotecaV2.Infrastructure.Repositories
{
    public class MySqlPrestamoRepository : IPrestamoRepository
    {
        private readonly string _connectionString;

        public MySqlPrestamoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Prestamo> ObtenerTodos()
        {
            var prestamos = new List<Prestamo>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            using var cmd = new MySqlCommand(
                "SELECT id, libro_id, usuario_nombre, fecha_prestamo, fecha_limite, fecha_devolucion, estado FROM prestamos", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                prestamos.Add(MapearPrestamo(reader));
            }
            return prestamos;
        }

        public List<Prestamo> ObtenerPorUsuario(string usuarioNombre)
        {
            var prestamos = new List<Prestamo>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            using var cmd = new MySqlCommand(@"
                SELECT id, libro_id, usuario_nombre, fecha_prestamo, fecha_limite, fecha_devolucion, estado
                FROM prestamos
                WHERE LOWER(usuario_nombre) = LOWER(@usuario)", conn);
            cmd.Parameters.AddWithValue("@usuario", usuarioNombre);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                prestamos.Add(MapearPrestamo(reader));
            }
            return prestamos;
        }

        public List<Prestamo> ObtenerPorEstado(EstadoPrestamo estado)
        {
            var prestamos = new List<Prestamo>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            using var cmd = new MySqlCommand(@"
                SELECT id, libro_id, usuario_nombre, fecha_prestamo, fecha_limite, fecha_devolucion, estado
                FROM prestamos
                WHERE estado = @estado", conn);
            cmd.Parameters.AddWithValue("@estado", (int)estado);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                prestamos.Add(MapearPrestamo(reader));
            }
            return prestamos;
        }

        public void Guardar(List<Prestamo> prestamos)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            using var tx = conn.BeginTransaction();

            foreach (var p in prestamos)
            {
                using var cmd = new MySqlCommand(@"
                    INSERT INTO prestamos (id, libro_id, usuario_nombre, fecha_prestamo, fecha_limite, fecha_devolucion, estado)
                    VALUES (@id, @libroId, @usuario, @fechaPrestamo, @fechaLimite, @fechaDevolucion, @estado)
                    ON DUPLICATE KEY UPDATE
                        fecha_devolucion = VALUES(fecha_devolucion),
                        estado = VALUES(estado);", conn, tx);

                cmd.Parameters.AddWithValue("@id", p.Id.ToString());
                cmd.Parameters.AddWithValue("@libroId", p.LibroId);
                cmd.Parameters.AddWithValue("@usuario", p.UsuarioNombre);
                cmd.Parameters.AddWithValue("@fechaPrestamo", p.FechaPrestamo);
                cmd.Parameters.AddWithValue("@fechaLimite", p.FechaLimite);
                cmd.Parameters.AddWithValue("@fechaDevolucion", (object?)p.FechaDevolucion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@estado", (int)p.Estado);
                cmd.ExecuteNonQuery();
            }

            tx.Commit();
        }

        private static Prestamo MapearPrestamo(MySqlDataReader reader)
        {
            int colDev = reader.GetOrdinal("fecha_devolucion");
            return new Prestamo
            {
                Id = Guid.Parse(reader.GetString("id")),
                LibroId = reader.GetInt32("libro_id"),
                UsuarioNombre = reader.GetString("usuario_nombre"),
                FechaPrestamo = reader.GetDateTime("fecha_prestamo"),
                FechaLimite = reader.GetDateTime("fecha_limite"),
                FechaDevolucion = reader.IsDBNull(colDev) ? null : reader.GetDateTime(colDev),
                Estado = (EstadoPrestamo)reader.GetInt32("estado")
            };
        }
    }
}
