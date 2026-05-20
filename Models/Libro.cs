namespace BibliotecaV1.Models
{
   public class Libro
{
    
   public int Id { get; set; }
   
    public string Titulo { get; set; }

    public string Autor { get; set; }

   public int Unidades {get; set;}

    public DateTime FechaDeIngreso { get; set; }

    // constructor para crear un libro de golpe 

    public Libro(int id, string titulo, string autor, int unidades)
    {
        this.Id = id;

        this.Titulo = titulo;

        this.Autor = autor;

        this.Unidades = unidades;

        this.FechaDeIngreso = DateTime.Now;
    }


public override string ToString()
    {
       return $"[ID: {Id}] -{Titulo} (Autor: {Autor}) - Unidades: {Unidades} - Ingresado el: {FechaDeIngreso.ToShortDateString()}";
    }

} 
}

