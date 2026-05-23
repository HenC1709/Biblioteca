using BibliotecaV2.Core.Entities;

namespace BibliotecaV2.Application.Services
{
    public class MultaService
    {
        private const decimal TarifaPorDia = 500m; // pesos por día de atraso

        public Multa? CalcularSiHayAtraso(Prestamo prestamo)
        {
            if (!prestamo.EstaVencido()) return null;

            int dias = prestamo.DiasDeAtraso();
            return new Multa(prestamo.Id, dias, TarifaPorDia);
        }
    }
}