using System;
using BibliotecaV1.Helpers;
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
        ConsoleHelper.Info($"Sesión iniciada como: {usuarioActual.Nombre}");
        System.Threading.Thread.Sleep(100);
     //Molde de nuevo Menu!
       Menu.MostrarMenu(usuarioActual, miDepo);
         

        Auth.Logout();

        ConsoleHelper.Success("Sesión cerrada correctamente");
    }     
}