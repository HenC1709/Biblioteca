
namespace BibliotecaV1.Services
{
    class BusquedaDeLibros
{
    public void Buscar(Biblioteca miBiblioteca)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== BUSCADOR DE LIBROS ===");
        Console.ResetColor();
        
        Console.WriteLine("\nIntroduce el nombre del libro a buscar, por favor");
        string buscar = Console.ReadLine()?.ToLower() ?? "";

          // AQUÍ ESTABA EL ERROR: ENTRE LA PC Y LA SILLA ESTA EL JODIDO ERROR 
         // 1. Debes llamar a miBiblioteca.Libros
         // 2. Usar .FirstOrDefault para que te devuelva UN libro
         var resultado = miBiblioteca
    .ObtenerLibros()
    .FirstOrDefault(l => l.Titulo.ToLower().Contains(buscar));

        if (resultado != null)    
            {
                Console.ForegroundColor = ConsoleColor.Green;
                // Usamos el ToString() que ya limpiamos en el modelo Libro
                Console.WriteLine($"\n¡Encontrado!\n{resultado}");
            }
        else
            {
                Console.ForegroundColor = ConsoleColor.Red;
         Console.WriteLine("ese libro no se encuentra xd");
            }
     Console.ResetColor();
          Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();

    }

}
}

