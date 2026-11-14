namespace BibliotecaV2.Application.DTOs
{
    public class LoginRequest
    {
        public string Nombre { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class RegistroRequest
    {
        public string Nombre { get; set; } = "";
        public string Password { get; set; } = "";
    }
}