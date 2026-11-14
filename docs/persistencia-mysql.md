# Guía de Persistencia con MySQL (`biblioteca_v2`)

Este documento detalla la estructura de la base de datos MySQL, los pasos de configuración y los archivos creados/modificados para migrar la persistencia desde archivos JSON locales hacia MySQL manteniendo intactas las capas `Core`, `Application` y `UI`.

---

## 1. Estructura de la Base de Datos (Esquema SQL)

Asegúrate de que las tablas creadas en tu servidor MySQL coincidan con esta definición de columnas y tipos:

```sql
CREATE DATABASE IF NOT EXISTS biblioteca_v2
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE biblioteca_v2;

CREATE TABLE IF NOT EXISTS libros (
    id INT AUTO_INCREMENT PRIMARY KEY,
    titulo VARCHAR(255) NOT NULL,
    autor VARCHAR(255) NOT NULL,
    unidades INT NOT NULL DEFAULT 0,
    fecha_ingreso DATETIME NOT NULL
);

CREATE TABLE IF NOT EXISTS usuarios (
    nombre VARCHAR(100) PRIMARY KEY,
    password_hash VARCHAR(255) NOT NULL,
    rol INT NOT NULL DEFAULT 0,
    fecha_registro DATETIME NOT NULL
);

CREATE TABLE IF NOT EXISTS prestamos (
    id CHAR(36) PRIMARY KEY,
    libro_id INT NOT NULL,
    usuario_nombre VARCHAR(100) NOT NULL,
    fecha_prestamo DATETIME NOT NULL,
    fecha_limite DATETIME NOT NULL,
    fecha_devolucion DATETIME NULL,
    estado INT NOT NULL DEFAULT 0,
    CONSTRAINT fk_prestamo_libro FOREIGN KEY (libro_id) REFERENCES libros(id),
    CONSTRAINT fk_prestamo_usuario FOREIGN KEY (usuario_nombre) REFERENCES usuarios(nombre)
);
```

### Correspondencia entre Entidades C# y Tablas MySQL

| Entidad (`Core/Entities`) | Tabla MySQL | Mapeo de Propiedades a Columnas |
| :--- | :--- | :--- |
| `Libro` | `libros` | `Id` -> `id` (`INT`), `Titulo` -> `titulo` (`VARCHAR`), `Autor` -> `autor` (`VARCHAR`), `Unidades` -> `unidades` (`INT`), `FechaIngreso` -> `fecha_ingreso` (`DATETIME`) |
| `Usuario` | `usuarios` | `Nombre` -> `nombre` (`VARCHAR PK`), `PasswordHash` -> `password_hash` (`VARCHAR`), `Rol` -> `rol` (`INT` enum), `FechaRegistro` -> `fecha_registro` (`DATETIME`) |
| `Prestamo` | `prestamos` | `Id` -> `id` (`CHAR(36)` Guid), `LibroId` -> `libro_id` (`INT FK`), `UsuarioNombre` -> `usuario_nombre` (`VARCHAR FK`), `FechaPrestamo` -> `fecha_prestamo` (`DATETIME`), `FechaLimite` -> `fecha_limite` (`DATETIME`), `FechaDevolucion` -> `fecha_devolucion` (`DATETIME NULL`), `Estado` -> `estado` (`INT` enum) |

---

## 2. Configuración de la Cadena de Conexión

En `Program.cs`, el sistema busca primero la variable de entorno `BIBLIOTECA_DB_CONN`. Si no está definida, utiliza la cadena local por defecto:

```csharp
string connectionString = Environment.GetEnvironmentVariable("BIBLIOTECA_DB_CONN")
    ?? "Server=localhost;Port=3306;Database=biblioteca_v2;User=root;Password=;";
```

### Cómo configurar tu contraseña en PowerShell (Recomendado para no subir credenciales a Git):
```powershell
$env:BIBLIOTECA_DB_CONN = "Server=localhost;Port=3306;Database=biblioteca_v2;User=root;Password=TU_PASSWORD;"
dotnet run
```

---

## 3. Archivos Creados y Modificados

### Archivos Nuevos (`Infrastructure/Repositories/`)
1. **`Infrastructure/Repositories/MySqlLibroRepository.cs`**:
   - Implementa `ILibroRepository`.
   - Realiza consultas parametrizadas (`SELECT`) y persistencia transaccional con `INSERT ... ON DUPLICATE KEY UPDATE` sobre la tabla `libros`.
2. **`Infrastructure/Repositories/MySqlUsuarioRepository.cs`**:
   - Implementa `IUsuarioRepository`.
   - Soporta búsqueda insensible a mayúsculas/minúsculas (`LOWER(nombre) = LOWER(@nombre)`) y actualización de roles/contraseñas mediante transacciones en la tabla `usuarios`.
3. **`Infrastructure/Repositories/MySqlPrestamoRepository.cs`**:
   - Implementa `IPrestamoRepository`.
   - Convierte `Guid` a `CHAR(36)`, maneja `FechaDevolucion` anulable (`DBNull.Value`) y sincroniza el estado del préstamo en la tabla `prestamos`.

### Archivos Modificados
1. **`BibliotecaV1.csproj`**:
   - Se incorporó la referencia al paquete `MySqlConnector` (`Version="3.0.0"`).
2. **`Program.cs`**:
   - Se reemplazaron `JsonLibroRepository`, `JsonUsuarioRepository` y `JsonPrestamoRepository` por sus equivalentes `MySqlLibroRepository`, `MySqlUsuarioRepository` y `MySqlPrestamoRepository`.
   - Se movió la ejecución de `LibroSeeder` y `UsuarioSeeder` dentro del bloque `try-catch` global para capturar de forma controlada cualquier fallo de conexión con el servidor MySQL al iniciar la aplicación.
