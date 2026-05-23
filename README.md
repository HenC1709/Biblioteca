# 📚 BibliotecaV2

## 🧠 Descripción

**BibliotecaV2** es una evolución arquitectónica del proyecto BibliotecaV1.

El sistema fue refactorizado para dejar atrás una estructura monolítica básica y comenzar a implementar una arquitectura más organizada, desacoplada y escalable, inspirada en principios de Clean Architecture y separación por capas.

Actualmente el proyecto funciona completamente en consola usando persistencia local en archivos JSON, pero la estructura fue preparada para facilitar futuras migraciones hacia:

* Bases de datos SQL
* APIs REST
* Interfaces gráficas (WPF)
* Arquitecturas empresariales más avanzadas

---

# 🚀 Características Principales

## 👤 Sistema de Usuarios

* Registro de usuarios
* Inicio de sesión
* Validación de credenciales
* Roles de usuario:

  * Usuario
  * Administrador
* Prevención de usuarios duplicados
* Persistencia automática de sesiones

---

## 📚 Gestión de Biblioteca

* Registro de libros
* Modificación de libros
* Eliminación de libros
* Control de stock
* Búsqueda de libros
* Persistencia automática en JSON

---

## 🔄 Sistema de Préstamos

* Solicitud de préstamos
* Devolución de libros
* Validación de disponibilidad
* Control de límite de préstamos
* Estado de préstamos mediante enums
* Generación automática de tickets

---

## ⚠️ Sistema de Excepciones Personalizadas

El proyecto ahora implementa excepciones específicas para blindar reglas de negocio.

### Exceptions actuales

* `CredencialesInvalidasException`
* `LibroNoEncontradoException`
* `LimitePrestamosException`
* `PrestamoNoActivoException`
* `StockInsuficienteException`
* `UsuarioYaExisteException`

Esto permite:

* código más limpio
* errores más controlados
* mejor mantenimiento
* lógica desacoplada de la interfaz

---

# 🧱 Arquitectura del Proyecto

```plaintext
BibliotecaV2/
│
├── Application/                    # Casos de uso y lógica de aplicación
│   ├── DTOs/
│   └── Services/
│
├── Core/                           # Núcleo del dominio
│   ├── Entities/
│   ├── Enums/
│   ├── Exceptions/
│   └── Interfaces/
│
├── Infrastructure/                 # Persistencia e implementaciones
│   ├── Data/
│   ├── Repositories/
│   ├── Seeders/
│   └── Tickets/
│
├── UI/                             # Interfaz de consola
│   └── ConsoleUI/
│
├── Data/                           # Datos persistidos localmente
│
├── Program.cs
├── BibliotecaV1.csproj
└── README.md
```

---

# 🧩 Explicación de Capas

## 🟦 Core

Contiene las reglas más importantes del sistema.

### Incluye:

* Entities
* Enums
* Interfaces
* Exceptions

Esta capa no depende de ninguna otra.

---

## 🟨 Application

Contiene la lógica de negocio y coordinación del sistema.

### Servicios actuales

* `AuthService`
* `LibroService`
* `MultaService`
* `PrestamoService`
* `SessionService`
* `UsuarioService`

También incluye DTOs para desacoplar entradas y salidas.

---

## 🟩 Infrastructure

Contiene implementaciones técnicas.

### Incluye:

* Repositorios JSON
* Seeders automáticos
* Generación de tickets
* Persistencia local

Actualmente el proyecto usa:

```csharp
System.Text.Json
```

para serialización.

---

## 🟥 UI

Capa responsable únicamente de interacción con consola.

La lógica fuerte ya no vive aquí.

---

# 🧬 Principios Aplicados

* Separación por responsabilidades
* Bajo acoplamiento
* Uso de interfaces
* Encapsulamiento
* Persistencia desacoplada
* Arquitectura preparada para escalar
* Dominio separado de infraestructura
* Manejo profesional de errores

---

# 🛠️ Tecnologías Utilizadas

* C#
* .NET 8
* Consola
* System.Text.Json
* Programación Orientada a Objetos

---

# 📦 Persistencia

Actualmente el proyecto usa persistencia basada en JSON.

Archivos utilizados:

```plaintext
Infrastructure/Data/
├── libros.json
├── prestamos.json
└── usuarios.json
```

Esto permite:

* guardar datos sin bases de datos
* practicar lógica backend
* preparar migración futura hacia SQL

---

# 🎫 Tickets Automáticos

El sistema genera tickets automáticamente mediante:

```plaintext
Infrastructure/Tickets/
```

Esto desacopla completamente la escritura de tickets del resto del sistema.

---

# 🧪 Seeders

El proyecto implementa seeders automáticos para generar datos iniciales:

* `LibroSeeder`
* `UsuarioSeeder`

---

# ▶️ Cómo Ejecutar

## Requisitos

* .NET 8 SDK instalado

---

## Ejecutar proyecto

```bash
dotnet run
```

---

# 📈 Evolución del Proyecto

## BibliotecaV1

* Arquitectura básica
* Servicios mezclados
* Persistencia simple
* Menor separación de responsabilidades

---

## BibliotecaV2

* Refactorización arquitectónica
* Separación por capas
* Exceptions personalizadas
* Interfaces desacopladas
* DTOs
* Repositorios JSON
* Dominio organizado
* Preparación para SQL/API/WPF

---

# 🔮 Futuro del Proyecto

Próximas metas planeadas:

* Migración hacia SQL
* Repository Pattern más avanzado
* Unit Testing
* APIs REST
* JWT Authentication
* Logging
* Dependency Injection
* Entity Framework
* WPF
* Clean Architecture más avanzada

---

# 📚 Objetivo Educativo

Este proyecto fue desarrollado principalmente como práctica avanzada de:

* C#
* Arquitectura de software
* Programación Orientada a Objetos
* Persistencia de datos
* Backend Development
* Separación por capas
* Escalabilidad de proyectos

---

# 👨‍💻 Autor

Desarrollado por Henry Cuellar.

Repositorio:

* urlGitHub - BibliotecaV2[https://github.com/HenC1709/Biblioteca/t](https://github.com/HenC1709/Biblioteca/t)
