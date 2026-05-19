# BibliotecaV1 📚

Sistema de gestión de biblioteca desarrollado en C# con persistencia JSON.

Permite:
- Gestión de catálogo de libros
- Registro de usuarios
- Sistema de préstamos y devoluciones
- Generación de tickets TXT
- Login con roles (Usuario/Admin)
- Persistencia local usando JSON

---

# Tecnologías usadas

- C#
- .NET
- JSON Serialization
- Programación Orientada a Objetos (POO)

---

# Características principales

## Usuarios
- Registro de usuarios
- Inicio de sesión
- Roles:
  - Usuario
  - Admin

## Libros
- Agregar libros
- Buscar libros
- Ver catálogo
- Control de stock

## Préstamos
- Prestar libros
- Devolver libros
- Actualización automática del stock

## Tickets
Generación automática de:
- Tickets de préstamo
- Tickets de devolución 

## Ejecutar
- dotnet build
- dotnet run

Guardados en:

```plaintext
Ticket_Biblioteca/

***
BibliotecaV1/
│
├── Data/
│   └── LibroRepository.cs
│
├── Helpers/
│   ├── ConsoleHelper.cs
│   └── InputHelper.cs
│
├── Logic/
│   ├── Menu.cs
│   └── TicketManager.cs
│
├── Models/
│   ├── Libro.cs
│   └── Usuario.cs
│
├── Services/
│   ├── Auth.cs
│   ├── Biblioteca.cs
│   ├── BusquedaDeLibros.cs
│   ├── LoginService.cs
│   └── UsuarioServicio.cs
│
├── Ticket_Biblioteca/
│
├── Program.cs
└── BibliotecaV1.csproj

