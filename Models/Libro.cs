using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using BibliotecaV1.Logic;

namespace BibliotecaV1.Models
{
   class Libro
{

   [JsonPropertyName("ID")] public int ID { get; set; }
    public string Titulo { get; set; }

    public string Autor { get; set; }

    [JsonPropertyName("Unidades")] public int Unidades {get; set;}

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

    public Libro(int id, string titulo, string autor, int unidades)
    {
        this.ID = id;

        this.Titulo = titulo;

        this.Autor = autor;

        this.Unidades = unidades;

        this.FechaDeIngreso = DateTime.Now;
    }


public override string ToString()
    {
       Console.ForegroundColor = ConsoleColor.Green;
       return $"[ID: {ID}] -{Titulo} (Autor: {Autor}) - Unidades: {Unidades} - Ingresado el: {FechaDeIngreso.ToShortDateString()}";
    }

} 
}

