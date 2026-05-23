namespace BibliotecaV2.Core.Entities
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = "";
        public string Autor { get; set; } = "";
        public int Unidades { get; set; }
        public DateTime FechaIngreso { get; set; }

        // Constructor vacío requerido por JsonSerializer
        public Libro() { }

        public Libro(int id, string titulo, string autor, int unidades)
        {
            Id = id;
            Titulo = titulo;
            Autor = autor;
            Unidades = unidades;
            FechaIngreso = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[ID: {Id}] {Titulo} — {Autor} | Unidades: {Unidades} | Ingresado: {FechaIngreso.ToShortDateString()}";
        }
    }
}

