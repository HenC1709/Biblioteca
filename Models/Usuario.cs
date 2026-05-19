
namespace BibliotecaV1.Models
{
    public class Usuario
{
    public string nombre {get; set;} = "";
    public string id {get; set;} = "";
    public Rol rol { get; set; } = Rol.Usuario;
}
    public enum Rol
{
    Usuario,
    Admin
}

}

