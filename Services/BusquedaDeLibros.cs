using BibliotecaV1.Helpers;
namespace BibliotecaV1.Services
{
    class BusquedaDeLibros
   {
    public void Buscar(Biblioteca miBiblioteca)
       {
        Console.Clear();
        ConsoleHelper.Tiltte("BUSCADOR DE LIBROS");
        Console.ResetColor();
        
        ConsoleHelper.Info("\nIntroduce el nombre del libro a buscar, por favor");
        string buscar = Console.ReadLine()?.ToLower() ?? "";

          // AQUÍ ESTABA EL ERROR: ENTRE LA PC Y LA SILLA ESTA EL JODIDO ERROR 
         // 1. Debes llamar a miBiblioteca.Libros
         // 2. Usar .FirstOrDefault para que te devuelva UN libro
         var resultado = miBiblioteca
    .ObtenerLibros()
    .FirstOrDefault(l => l.Titulo.ToLower().Contains(buscar));

        if (resultado != null)    
            {        
           // Usamos el ToString() que ya limpiamos en el modelo Libro
          ConsoleHelper.Success($"\n¡Encontrado!\n{resultado}");
            }
        else
            {
        ConsoleHelper.Error("ese libro no se encuentra xd");
            }
       }
    }
}

