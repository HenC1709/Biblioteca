using System;
using System.Collections.Generic;
using System.Text.Json;
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

    public void PrestarLibro()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("======================");
            Console.WriteLine(" SISTEMA DE PRÉSTAMOS ");
            Console.WriteLine("======================");
            Console.ResetColor();
            Libro.CargarDatos();

            Console.Write("\nIngrese el ID del articulo a pedir: ");

            // verificamos que el user escriba un numero

            if (int.TryParse(Console.ReadLine(), out int idBuscado))
            {
                var articulo = Lista.listaLibros.FirstOrDefault(l => l.ID == idBuscado);
                if (articulo != null) // si lo encuentra xd
                {
                    if (articulo.Unidades > 0) // por si algun gracioso cree que tiene libros infinitos
                    {
                        articulo.Unidades--; //para restar una unidad!
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"\n¡ÉXITO! has pedido prestado: {articulo.Titulo}");

                        // detalle visual: si queda 1 se pone en amarillo!
                        if (articulo.Unidades == 1)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Unidades en bodega son: {articulo.Unidades}");
                        Console.ResetColor();
                       
                       // no olvidar actualizar el json
                       ActualizarCatalogojson();
                 
                    }
                    else
                    {
                 Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nLo siento, no quedan unidades disponibles de este artículo.");
                Console.ResetColor();
                    }
                }
                else
                {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nNo existe ningún artículo con el ID: {idBuscado} xd");
            Console.ResetColor();
                }
            }
            else
            {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nPor favor, ingresa un ID numérico válido.");
        Console.ResetColor();
           }
        } 

        public void DevolverLibro()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("======================");
            Console.WriteLine(" SISTEMA DE DEVOLUCIÓN ");
            Console.WriteLine("======================");
            Console.ResetColor();
            Libro.CargarDatos();

            Console.Write("\nIngrese el ID del articulo a Devolver: ");

            // verificamos que el user escriba un numero

            if (int.TryParse(Console.ReadLine(), out int idBuscado))
            {
                var articulo = Lista.listaLibros.FirstOrDefault(l => l.ID == idBuscado);
                if (articulo != null) // si lo encuentra xd
                {
                    if (articulo.Unidades >= 0) // por si algun gracioso cree que tiene libros infinitos
                    {
                        articulo.Unidades++; //para sumar una unidad!
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"\n¡DEVOLUCION EXITOSA! {articulo.Titulo}");
                        Console.WriteLine($"Nuevo stock disponible: {articulo.Unidades}");
                       // no olvidar actualizar el json
                       ActualizarCatalogojson();
                 
                    }
                    else
                    {
                 Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nLo siento, no encontramos el registro con el ID {idBuscado}");
                Console.ResetColor();
                    }
                }
                else
                {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nNo existe ningún artículo con el ID: {idBuscado} xd");
            Console.ResetColor();
                }
            }
            else
            {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nPor favor, ingresa un ID numérico válido.");
        Console.ResetColor();
           }
        } 
   
   private void ActualizarCatalogojson()
        {
        string ruta = Path.Combine("Data","LibrosGuardados.json");
        // Opciones para que el JSON se vea ordenado y bonito
    var opciones = new JsonSerializerOptions { WriteIndented = true };
    
    // Convertimos TODA la lista (con las unidades ya restadas) a texto JSON
    string jsonFinal = JsonSerializer.Serialize(Lista.listaLibros, opciones);
    
    // Sobreescribimos el archivo viejo con los datos nuevos
    File.WriteAllText(ruta, jsonFinal);
        }
    }
}
