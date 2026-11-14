# 📚 BibliotecaV2

## 🧠 Descripción

**BibliotecaV2** es una evolución arquitectónica del proyecto BibliotecaV1.

El sistema fue refactorizado para dejar atrás una estructura monolítica básica e implementar una arquitectura organizada, desacoplada y escalable en `src/`, inspirada en principios de **Clean Architecture** y separación por capas.

Actualmente el proyecto funciona en consola utilizando **persistencia relacional en MySQL** mediante `MySqlConnector` (manteniendo además soporte desacoplado de repositorios JSON a través de interfaces), y está preparado para futuras expansiones hacia:

* APIs REST
* Interfaces gráficas (WPF)
* Pruebas unitarias automatizadas (`tests/`)
* Arquitecturas empresariales más avanzadas

---

# 🚀 Características Principales

## 👤 Sistema de Usuarios y Autenticación

* Registro de usuarios con contraseñas hasheadas mediante **BCrypt (`BCrypt.Net-Next`)**
* Inicio de sesión con sanitización de caracteres de control en consola
* Validación de credenciales y prevención de usuarios duplicados
* Gestión de 3 roles de acceso jerárquicos:
  * `Usuario`
  * `Bibliotecario`
  * `Admin` (con gestión y cambio validado de roles de usuario)
* Control de sesión activa mediante `SessionService`

---

## 📚 Gestión de Biblioteca

* Catálogo inicial automatizado con **50 libros** (`LibroSeeder` y script `seed_libros.sql`)
* Registro de nuevos libros (rol `Bibliotecario` y `Admin`)
* Consulta de catálogo completo con indicadores de disponibilidad
* Control automático de stock en préstamos y devoluciones
* Búsqueda de libros por título
* Persistencia transaccional en **MySQL**

---

## 🔄 Sistema de Préstamos y Multas

* Solicitud de préstamos y devolución de libros
* Validación de stock disponible y límite máximo de préstamos activos por usuario (3)
* Cálculo automático de multas por días de atraso (`MultaService`)
* Seguimiento de préstamos activos y vencidos
* Generación automática de tickets físicos en `.txt` (`Prestamo`, `Devolucion`, `Multa`)

---

## ⚠️ Sistema de Excepciones Personalizadas y Manejo Global

El proyecto implementa excepciones de dominio específicas para blindar las reglas de negocio, además de un bloque global de captura de errores críticos en el punto de entrada.

### Excepciones de dominio actuales

* `CredencialesInvalidasException`
* `LibroNoEncontradoException`
* `LimitePrestamosException`
* `PrestamoNoActivoException`
* `StockInsuficienteException`
* `UsuarioYaExisteException`

---

# 🧱 Arquitectura del Proyecto

```plaintext
BibliotecaV2/
│
├── src/                                # Código fuente principal de la aplicación
│   ├── Application/                    # Casos de uso y lógica de aplicación
│   │   ├── DTOs/
│   │   └── Services/
│   │
│   ├── Core/                           # Núcleo del dominio (sin dependencias externas)
│   │   ├── Entities/
│   │   ├── Enums/
│   │   ├── Exceptions/
│   │   └── Interfaces/
│   │
│   ├── Infrastructure/                 # Persistencia (MySQL / JSON), seeders y tickets
│   │   ├── Data/
│   │   │   └── seed_libros.sql
│   │   ├── Repositories/
│   │   │   ├── MySqlLibroRepository.cs
│   │   │   ├── MySqlPrestamoRepository.cs
│   │   │   ├── MySqlUsuarioRepository.cs
│   │   │   ├── JsonLibroRepository.cs
│   │   │   ├── JsonPrestamoRepository.cs
│   │   │   └── JsonUsuarioRepository.cs
│   │   ├── Seeders/
│   │   │   ├── LibroSeeder.cs
│   │   │   └── UsuarioSeeder.cs
│   │   └── Tickets/
│   │       └── TicketFileWriter.cs
│   │
│   ├── UI/                             # Interfaz de usuario en consola
│   │   └── ConsoleUI/
│   │       ├── Helpers/
│   │       └── Menus/
│   │
│   ├── Program.cs
│   └── BibliotecaV1.csproj
│
├── docs/                               # Documentación técnica y auditorías
│   ├── persistencia-mysql.md
│   ├── revision-inicial.md
│   └── solucion-severidad-media.md
│
├── BibliotecaV1.sln
└── README.md
```

---

# 🧩 Explicación de Capas

## 🟦 Core

Contiene las reglas y contratos fundamentales del dominio:

* `Entities`: `Libro`, `Usuario`, `Prestamo`, `Multa`, `Session`
* `Enums`: `Rol`, `EstadoPrestamo`, `TipoTicket`
* `Interfaces`: `ILibroRepository`, `IUsuarioRepository`, `IPrestamoRepository`, `ISessionService`, `ITicketWriter`
* `Exceptions`: Excepciones de negocio

Esta capa no depende de ninguna otra.

---

## 🟨 Application

Contiene la lógica de negocio y coordinación de casos de uso:

* `AuthService`
* `LibroService`
* `MultaService`
* `PrestamoService`
* `SessionService`
* `UsuarioService`

---

## 🟩 Infrastructure

Contiene las implementaciones técnicas de acceso a datos y sistema de archivos:

* **Repositorios MySQL (`MySqlConnector`)**: `MySqlLibroRepository`, `MySqlUsuarioRepository`, `MySqlPrestamoRepository`
* **Repositorios JSON (`System.Text.Json`)**: `JsonLibroRepository`, `JsonUsuarioRepository`, `JsonPrestamoRepository`
* **Seeders automáticos**: `LibroSeeder` (50 libros) y `UsuarioSeeder` (admin inicial)
* **Generación de tickets**: `TicketFileWriter`

---

## 🟥 UI

Capa responsable exclusivamente de la interacción por consola:

* Menús jerárquicos polimórficos: `MenuUsuario` -> `MenuBibliotecario` -> `MenuAdmin`
* Vista de acceso: `LoginView`
* Utilidades de entrada y formato: `InputHelper`, `ConsoleHelper`

---

# 🛠️ Tecnologías Utilizadas

* **C# / .NET 8**
* **MySQL** (`MySqlConnector` 3.0.0)
* **BCrypt.Net-Next** (4.2.0) para hashing seguro de contraseñas
* **System.Text.Json**
* **Programación Orientada a Objetos y Clean Architecture**

---

# 🗄️ Persistencia en Base de Datos (MySQL)

El sistema utiliza la base de datos **`biblioteca_v2`** con tres tablas principales:

* `libros`
* `usuarios`
* `prestamos`

Puedes consultar el esquema SQL completo y los detalles de mapeo en [`docs/persistencia-mysql.md`](docs/persistencia-mysql.md), así como el script de 50 libros en [`src/Infrastructure/Data/seed_libros.sql`](src/Infrastructure/Data/seed_libros.sql).

---

# ▶️ Cómo Ejecutar

## 1. Requisitos

* **.NET 8 SDK** instalado
* Servidor **MySQL** activo con la base de datos `biblioteca_v2` creada (ver [`docs/persistencia-mysql.md`](docs/persistencia-mysql.md))

## 2. Configurar conexión a MySQL (PowerShell)

Puedes definir la contraseña de tu usuario `root` o la cadena completa en variables de entorno:

```powershell
$env:BIBLIOTECA_DB_PASS = 'tu_password_mysql'
```

*(Opcionalmente mediante `$env:BIBLIOTECA_DB_CONN = "Server=localhost;Port=3306;Database=biblioteca_v2;User=root;Password=tu_password_mysql;"`).*

## 3. Compilar y ejecutar desde la raíz

```bash
dotnet build
dotnet run --project src
```

---

# 📈 Evolución del Proyecto

## BibliotecaV1
* Arquitectura monolítica básica
* Servicios acoplados a la interfaz
* Persistencia local simple

## BibliotecaV2
* Separación por capas en directorio `src/`
* Persistencia relacional con **MySQL** e interfaces desacopladas
* Hashing de contraseñas con **BCrypt**
* Excepciones personalizadas y manejo global de errores
* Menús por rol (`Usuario`, `Bibliotecario`, `Admin`)
* Seeders automáticos con catálogo de 50 libros
* Documentación técnica en `docs/`

---

# 🔮 Próximos Pasos Planeados

* Proyecto de **Unit Testing** (`tests/`) con xUnit
* Persistencia en base de datos para el historial de `Multa`
* Operaciones de edición y baja lógica de libros
* API REST y cliente gráfico (WPF)

---

# 👨‍💻 Autor

Desarrollado por **Henry Cuellar**.

* [GitHub - Biblioteca](https://github.com/HenC1709/Biblioteca)
