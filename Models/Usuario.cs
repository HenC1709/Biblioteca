
namespace BibliotecaV1.Models
{
    public class Usuario
{
    public string Nombre {get; set;} = "";
    public string Id {get; set;} = "";
    public Rol Rol { get; set; } = Rol.Usuario;
}
    public enum Rol
{
    Usuario,
    Admin
}

}

