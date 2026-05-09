using System;
using BibliotecaV1.Logic;
using BibliotecaV1.Models;
using BibliotecaV1.Services;
class Program

{
    static void Main(string[] args)
    {
        Biblioteca miDepo = new Biblioteca(); 
        LoginService login = new LoginService();
        Usuario? usuarioActual = null;
        while (usuarioActual == null)
        {
            usuarioActual = login.IniciarSesion();
        }

        // Guardamos Usuario Actual
        Auth.Login(usuarioActual);
        Console.WriteLine($"Sesión iniciada como: {usuarioActual.nombre}");
        System.Threading.Thread.Sleep(1000);
     //Molde de nuevo Menu!

     if (usuarioActual.rol == Rol.Admin)
        {
            Menu.MenuAdmin(usuarioActual, miDepo);
        }
    else
        {
            Menu.MenuUsuario(usuarioActual, miDepo);
        }

        Auth.Logout();

        Console.WriteLine("Sesión cerrada correctamente");
    }     
}