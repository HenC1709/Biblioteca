using System;
using BibliotecaV1.Logic;
using BibliotecaV1.Models;
using BibliotecaV1.Services;

namespace BibliotecaV1.Services
{
    class BusquedaDeLibros
{
    public void Buscar(Biblioteca miBiblioteca)
    {
        Libro.CargarDatos();
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Introduce el nombre del libro a buscar, por favor");
        string buscar = Console.ReadLine()!;
        

        var resultado = Lista.listaLibros.Find(l => l.Titulo.Contains(buscar));

        if (resultado != null)
            Console.WriteLine($"Encontrado: {resultado.Titulo} de {resultado.Autor} Unidades: {resultado.Unidades} ingresado: {resultado.FechaDeIngreso.ToShortDateString()}");
        else
            Console.WriteLine("ese libro no se encuentra xd");

    }

}
}

