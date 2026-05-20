# 📚 BibliotecaV1

## 📝 Resumen

**BibliotecaV1** es un proyecto de consola desarrollado en **C#** enfocado en la gestión básica y eficiente de una biblioteca. 

El sistema está diseñado para ofrecer una experiencia intuitiva a través de la terminal, dividiendo las capacidades según el rol del usuario y asegurando que la información no se pierda al cerrar el programa.

Actualmente, el proyecto se encuentra en una etapa de refactorización y mejora arquitectónica para preparar futuras versiones más avanzadas.

---

## 🚀 Funcionalidades Actuales

### 👥 Gestión de Usuarios y Autenticación
- **Registro de usuarios:** Creación de nuevas cuentas en el sistema.
- **Inicio de sesión seguro:** Validación de credenciales para acceder.
- **Control de Roles:**
  - **Usuario:** Acceso a búsquedas, préstamos y devoluciones.
  - **Administrador (Admin):** Control total sobre la gestión de libros y reportes.
- **Robustez del sistema:** 
  - Validación estricta de ID numérica.
  - Prevención de usuarios duplicados para evitar conflictos en la base de datos.

### 📖 Gestión de la Biblioteca
- **Control de Libros:** Alta, baja y modificación del catálogo (exclusivo de Admin).
- **Préstamos y Devoluciones:** Flujo completo para que los usuarios soliciten y entreguen libros.
- **Generación Automática de Tickets:** Emisión de un comprobante físico/digital en texto cada vez que se realiza una transacción.
- **Persistencia de Datos:** Almacenamiento local automático en formato **JSON** para conservar el estado de los libros y usuarios.

---

## 🛠️ Tecnologías Usadas

- **Lenguaje:** C#
- **Ecosistema:** .NET (Consola)
- **Serialización:** `System.Text.Json` para la persistencia de datos.

---

## 📂 Estructura del Proyecto

```plaintext
BibliotecaV1/
│
├── Data/                          # Capa de almacenamiento y persistencia
│   ├── LibroRepository.cs
│   ├── UsuarioRepository.cs
│   ├── LibrosGuardados.json       # Base de datos local de libros
│   └── Usuarios.json              # Base de datos local de usuarios
│
├── Helpers/                       # Herramientas de utilidad general
│   ├── ConsoleHelper.cs
│   └── InputHelper.cs
│
├── Logic/                         # Flujo principal de la aplicación
│   ├── Menu.cs
│   └── TicketManager.cs
│
├── Models/                        # Clases de entidad (Modelos de datos)
│   ├── Libro.cs
│   ├── Usuario.cs
│   └── Prestamo.cs                # (En preparación para futuras actualizaciones)
│
├── Services/                      # Lógica de negocio y servicios del sistema
│   ├── Auth.cs
│   ├── Biblioteca.cs
│   ├── BusquedaDeLibros.cs
│   ├── LoginService.cs
│   └── UsuarioServicio.cs
│
├── Ticket_Biblioteca/             # Carpeta de salida
│   └── (Tickets generados automáticamente en .txt)
│
├── .gitignore
├── Program.cs                     # Punto de entrada de la aplicación
└── BibliotecaV1.csproj