using BibliotecaV2.Core.Entities;
using BibliotecaV2.Core.Interfaces;
using MySqlConnector;

namespace BibliotecaV2.Infrastructure.Repositories
{
    public class MySqlLibroRepository : ILibroRepository
    {
        private readonly string _connectionString;

        public MySqlLibroRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Libro> ObtenerTodos()
        {
            var libros = new List<Libro>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            using var cmd = new MySqlCommand(
                "SELECT id, titulo, autor, unidades, fecha_ingreso FROM libros ORDER BY id", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                libros.Add(new Libro
                {
                    Id = reader.GetInt32("id"),
                    Titulo = reader.GetString("titulo"),
                    Autor = reader.GetString("autor"),
                    Unidades = reader.GetInt32("unidades"),
                    FechaIngreso = reader.GetDateTime("fecha_ingreso")
                });
            }
            return libros;
        }

        public Libro? ObtenerPorId(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            using var cmd = new MySqlCommand(
                "SELECT id, titulo, autor, unidades, fecha_ingreso FROM libros WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;

            return new Libro
            {
                Id = reader.GetInt32("id"),
                Titulo = reader.GetString("titulo"),
                Autor = reader.GetString("autor"),
                Unidades = reader.GetInt32("unidades"),
                FechaIngreso = reader.GetDateTime("fecha_ingreso")
            };
        }

        public void Guardar(List<Libro> libros)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            using var tx = conn.BeginTransaction();

            foreach (var libro in libros)
            {
                using var cmd = new MySqlCommand(@"
                    INSERT INTO libros (id, titulo, autor, unidades, fecha_ingreso)
                    VALUES (@id, @titulo, @autor, @unidades, @fecha)
                    ON DUPLICATE KEY UPDATE
                        titulo = VALUES(titulo),
                        autor = VALUES(autor),
                        unidades = VALUES(unidades);", conn, tx);

                cmd.Parameters.AddWithValue("@id", libro.Id);
                cmd.Parameters.AddWithValue("@titulo", libro.Titulo);
                cmd.Parameters.AddWithValue("@autor", libro.Autor);
                cmd.Parameters.AddWithValue("@unidades", libro.Unidades);
                cmd.Parameters.AddWithValue("@fecha", libro.FechaIngreso);
                cmd.ExecuteNonQuery();
            }

            tx.Commit();
        }
    }
}
