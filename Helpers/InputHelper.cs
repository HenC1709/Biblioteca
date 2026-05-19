using System.Data.SqlTypes;

namespace BibliotecaV1.Helpers
{
    public static class InputHelper
    {
        public static int LeerEntero(string mensaje)
        {
            int numero;
            Console.Write(mensaje);

            while(!int.TryParse(Console.ReadLine(), out numero))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Número inválido.");
                Console.ResetColor();

                Console.Write(mensaje);
            }
            return numero;
        }
        public static string LeerTexto(string mensaje)
        {
            Console.Write(mensaje);
            return Console.ReadLine()?.Trim() ?? "";
        }
    }
}