using BibliotecaV2.Application.Services;
using BibliotecaV2.Core.Enums;
using BibliotecaV2.Infrastructure.Repositories;
using BibliotecaV2.Infrastructure.Seeders;
using BibliotecaV2.Infrastructure.Tickets;
using BibliotecaV2.UI.ConsoleUI.Helpers;
using BibliotecaV2.UI.ConsoleUI.Menus;


// INFRAESTRUCTURA — repositorios concretos (MySQL)

string connectionString = Environment.GetEnvironmentVariable("BIBLIOTECA_DB_CONN")
    ?? "Server=localhost;Port=3306;Database=biblioteca_v2;User=root;Password=;";

var libroRepo    = new MySqlLibroRepository(connectionString);
var usuarioRepo  = new MySqlUsuarioRepository(connectionString);
var prestamoRepo = new MySqlPrestamoRepository(connectionString);
var ticketWriter = new TicketFileWriter();


// APPLICATION — servicios con dependencias inyectadas

var session        = new SessionService();
var authService    = new AuthService(usuarioRepo, session);
var libroService   = new LibroService(libroRepo);
var multaService   = new MultaService();
var prestamoService = new PrestamoService(prestamoRepo, libroRepo, ticketWriter, multaService);
var usuarioService  = new UsuarioService(usuarioRepo);


// UI — login y despacho al menú correcto

var loginView = new LoginView(authService);

try
{
    // SEEDERS — datos de ejemplo al primer arranque en BD
    new LibroSeeder(libroRepo).Seed();
    new UsuarioSeeder(usuarioRepo).Seed();

    while (true)
    {
        var usuario = loginView.Mostrar();

        if (usuario == null)
        {
            ConsoleHelper.Info("Hasta pronto.");
            break;
        }

        // Despachar al menú según el rol del usuario
        var menu = usuario.Rol switch
        {
            Rol.Admin          => (MenuUsuario)new MenuAdmin(libroService, prestamoService, usuarioService, usuario),
            Rol.Bibliotecario  => new MenuBibliotecario(libroService, prestamoService, usuario),
            _                  => new MenuUsuario(libroService, prestamoService, usuario)
        };

        menu.Mostrar();
        authService.Logout();
    }
}
catch (Exception ex)
{
    ConsoleHelper.Error($"[ERROR CRÍTICO] Ocurrió un error inesperado en la aplicación: {ex.Message}");
    ConsoleHelper.Pause();
}