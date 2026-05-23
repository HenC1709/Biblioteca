using BibliotecaV2.UI.ConsoleUI.Helpers;

namespace BibliotecaV2.UI.ConsoleUI.Helpers
{
    public static class InputHelper
    {
        public static string LeerTexto(string mensaje)
        {
            string texto;
            do
            {
                Console.Write(mensaje);
                texto = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrEmpty(texto))
                    ConsoleHelper.Warning("El campo no puede estar vacío.");
            }
            while (string.IsNullOrEmpty(texto));
            return texto;
        }

        public static string LeerPassword(string mensaje)
        {
            Console.Write(mensaje);
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password[..^1];
                    Console.Write("\b \b");
                }
                else if (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            }
            while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }

        public static int LeerEntero(string mensaje)
        {
            int numero;
            Console.Write(mensaje);
            while (!int.TryParse(Console.ReadLine(), out numero))
            {
                ConsoleHelper.Error("Número inválido.");
                Console.Write(mensaje);
            }
            return numero;
        }

        public static int LeerEnteroPositivo(string mensaje)
        {
            int numero;
            do
            {
                numero = LeerEntero(mensaje);
                if (numero < 0)
                    ConsoleHelper.Warning("El número no puede ser negativo.");
            }
            while (numero < 0);
            return numero;
        }

        // Bug de V1 corregido: comparar con "s" minúscula después de ToLower()
        public static bool LeerConfirmacion(string mensaje)
        {
            Console.Write($"{mensaje} (S/N): ");
            string respuesta = Console.ReadLine()?.Trim().ToLower() ?? "";
            return respuesta == "s";
        }
    }
}