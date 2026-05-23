namespace BibliotecaV2.Core.Entities
{
    public class Multa
    {
        public Guid PrestamoId { get; set; }
        public int DiasDeAtraso { get; set; }
        public decimal Monto { get; set; }
        public bool Pagada { get; set; }
        public DateTime FechaGeneracion { get; set; }

        // Constructor vacío requerido por JsonSerializer
        public Multa() { }

        public Multa(Guid prestamoId, int diasDeAtraso, decimal tarifaPorDia)
        {
            PrestamoId = prestamoId;
            DiasDeAtraso = diasDeAtraso;
            Monto = diasDeAtraso * tarifaPorDia;
            Pagada = false;
            FechaGeneracion = DateTime.Now;
        }
    }
}
