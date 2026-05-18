# BibliotecaV1 (Bibiotecla nuevo)

Resumen rápido

Proyecto consola en C# que gestiona un catálogo de libros, préstamos y devoluciones, con generación de tickets en archivos de texto y persistencia JSON en la carpeta Data.

Estructura (archivos principales)

- Bibiotecla nuevo.sln
- Bibiotecla nuevo.csproj
- Program.cs
- UI/Menu.cs
- Logic/TicketManager.cs
- Logic/ (otros archivos)
- Services/
  - biblioteca.cs
  - LoginService.cs
  - UsuarioServicio.cs
  - BusquedaDeLibros.cs
  - Auth.cs
- Models/
  - Usuario.cs
  - Libro.cs
- Data/
  - LibroRepository.cs
- Ticket_Biblioteca/  (generado en tiempo de ejecución)

Requisitos

- .NET SDK (versión compatible con TargetFramework en el .csproj). El archivo usa net10.0; instalar el SDK adecuado o cambiar TargetFramework a net8.0/net7.0 según tu SDK.

Cómo compilar y ejecutar

1. Abrir terminal en la carpeta del proyecto.
2. dotnet build
3. dotnet run --project "Bibiotecla nuevo.csproj"

Notas y hallazgos (cosas que faltan o conviene arreglar)

1. Historial Git: no se incluyó el historial en este README (se omitió por configuración). Si quieres que añada los archivos movidos/renombrados, pega la salida de:
   - git status --porcelain
   - git --no-pager log --name-status --pretty=format:"%h %ad %an %s" --date=short -n 200
   o permite ejecutar comandos aquí.

2. Nombres y namespaces:
   - El proyecto y solución usan el nombre "Bibiotecla nuevo" (typo). Los namespaces del código usan BibliotecaV1. Recomiendo unificar (renombrar proyecto/archivo .sln o cambiar los namespaces) para evitar confusión.

3. Carpetas/Data:
   - UsuarioServicio y LibroRepository escriben en Data\Usuarios.json y Data\LibrosGuardados.json. Asegurarse de que la carpeta Data exista antes de guardar o modificar el código para crearla automáticamente (Directory.CreateDirectory(Path.GetDirectoryName(ruta))).

4. Roles/Admin:
   - No hay un flujo claro para crear un usuario Admin desde la app. El enum Rol existe; para tener admins, ajustar el registro o editar el JSON manualmente.

5. Identificadores y estilo:
   - En UI/Menu.cs aparecen identificadores con acentos ("opción") y también "opcion" — mejor usar nombres ASCII consistentes (ej. opcion).

6. Tickets:
   - TicketManager genera/abre siempre los mismos archivos "Prestamo.txt" y "Devolucion.txt" y los va append-eando. Si prefieres un fichero por ticket, ajustar a nombres únicos (incluyendo ticketId en el nombre).

Sugerencias de cambios rápidos (ejecutables):
- En UsuarioServicio.Guardar y LibroRepository.Guardar: asegurar existencia de carpeta Data antes de File.WriteAllText.
- Considerar renombrar el proyecto a "BibliotecaV1" para coincidir con namespaces.
- Añadir creación de un usuario admin en la opción de registro o un script de seed.

Comandos útiles para revisar Git (copia/pega en Git Bash):
- git status --porcelain
- git --no-pager log --name-status --pretty=format:"%h %ad %an %s" --date=short -n 200
- git --no-pager log --diff-filter=R --summary -n 200   (muestra renombrados)

Si quieres, puedo:
- Crear un commit con el README en el repo.
- Aplicar los cambios sugeridos (crear directorios, ajustar namespaces, añadir creación de Admin).
- Añadir el historial Git al README si pegas/suministras la salida.

---
Generado por: Copilot CLI runtime en VS Code (asistente AI).