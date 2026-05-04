using System;
using BibliotecaV1.Logic;

namespace BibliotecaV1.Services
{
    class BusquedaDeLibros
{
    public void Buscar(Biblioteca miBiblioteca)
    {
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Introduce el nombre del libro a buscar, por favor");
        string buscar = Console.ReadLine()!;

        var resultado = Lista.listaLibros.Find(l => l.Titulo.Contains(buscar));

        if (resultado != null)
            Console.WriteLine($"Encontrado: {resultado.Titulo} de {resultado.Autor}");
        else
            Console.WriteLine("ese libro no se encuentra xd");
        
    }
}
}

