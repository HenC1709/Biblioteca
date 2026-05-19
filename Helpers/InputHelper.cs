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
                ConsoleHelper.Error("Numero Invalido"
                );
                Console.Write(mensaje);
            }
            return numero;
        }
        public static string LeerTexto(string mensaje)
        {
            string texto;
            do
            {
             Console.Write(mensaje);
            texto = Console.ReadLine()?.Trim() ?? "";
           
           if (string.IsNullOrEmpty(texto))
                {
                    ConsoleHelper.Warning("El campo no puede estar vacío.");
                }
            }while(string.IsNullOrEmpty(texto));
            return texto;
        }
        public static bool LeerConfirmación(string mensaje)
        {
            Console.Write($"{mensaje} (S/N): ");
            string respuesta = Console.ReadLine()?.Trim().ToLower() ?? "";
            return respuesta == "S";
        }
        public static int LeerNumeroEntero(string mensaje)
        {
            int numero;
            do
            {
                numero = LeerEntero(mensaje);
                if (numero < 0)
                {
                    ConsoleHelper.Warning("El número no puede ser negativo.");
                }
            }while (numero < 0);
            return numero;
        }
    }
}