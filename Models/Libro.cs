using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using BibliotecaV1.Logic;

namespace BibliotecaV1.Models
{
   class Libro
{
    public string Titulo {get; set;}

    public string Autor {get; set;}

    public string Disponible {get; set;}

    public DateTime FechaDeIngreso { get; set; }

    public static void GuardarLibro(Libro NuevoLibro)
    {
        string ruta = Path.Combine("Data","LibrosGuardados.json");
        List<Libro> Lista = new List<Libro>();
        if (File.Exists(ruta))
        {
            string contenido = File.ReadAllText(ruta);
            if (!string.IsNullOrEmpty(contenido))
            {
                Lista = JsonSerializer.Deserialize<List<Libro>>(contenido)!;
            }
        }
        Lista.Add(NuevoLibro);
        var opciones = new JsonSerializerOptions { WriteIndented = true };
        string jsonfinal = JsonSerializer.Serialize(Lista, opciones);
        File.WriteAllText(ruta, jsonfinal);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Libro guardado con exito");
    } //Cargar datos del json

public static void CargarDatos()
    {
        string ruta = Path.Combine("Data","LibrosGuardados.json");
        if (File.Exists(ruta))
        {
            string contenido = File.ReadAllText(ruta);
            Lista.listaLibros = JsonSerializer.Deserialize<List<Libro>>(contenido) ?? new List<Libro>();
        }
    }

    // constructor para crear un libro de golpe 

    public Libro(string titulo, string autor, string disponible)
    {
        Titulo = titulo;

        Autor = autor;

        Disponible = disponible;

        FechaDeIngreso = DateTime.Now;
    }


public override string ToString()
    {
       Console.ForegroundColor = ConsoleColor.Green;
       return $"{Titulo} - {Autor} - {Disponible} Ingresado el: {FechaDeIngreso}";
    }

} 
}

