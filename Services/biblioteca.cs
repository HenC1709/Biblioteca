using System;
using System.Collections.Generic;
using BibliotecaV1.Logic;
using BibliotecaV1.Models;

namespace BibliotecaV1.Services
{
    class Biblioteca
{

    public void AgregarLibro(Libro nuevoLibro)
    {
        Lista.listaLibros.Add(nuevoLibro);

       Libro.GuardarLibro(nuevoLibro);

        Console.WriteLine($"libro '{nuevoLibro.Titulo}' agregado al sistema");

    }
}
}
