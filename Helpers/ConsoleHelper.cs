namespace BibliotecaV1.Helpers
{
    public static class ConsoleHelper
    {
      public static void Success(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }
        public static void Error (string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }
        public static void Warning(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }
        public static void Info(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }
        public static void Tiltte(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("========================");
            Console.WriteLine(mensaje);
            Console.WriteLine("========================");
            Console.ResetColor();
        }
        public static void Pause()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ResetColor();

            Console.ReadKey();
        }
    }
}