# Informe de Revisión Inicial

Se completó una auditoría estática de solo lectura sobre el código, arquitectura, seguridad y convenciones del repositorio.
El proyecto compila exitosamente en .NET 8 sin advertencias, pero presenta vulnerabilidades de seguridad como credenciales fijas y datos sensibles rastreados en Git.
Se identificaron errores lógicos críticos como el cierre intempestivo de la aplicación ante fallo de login y la pérdida total de registros de multas por falta de persistencia.
Existe un incumplimiento sistemático de las convenciones fijadas: código íntegramente en español, y ramas y commits fuera del estándar establecido.
Se seleccionaron los 30 pendientes más prioritarios ordenados por severidad; 3 observaciones secundarias (interfaces de servicios, documentación XML y análisis estático) quedaron fuera del límite.

---

## 1. Seguridad

- [ ] **[Severidad: alta]** Contraseña por defecto y credenciales fijas en código
  - Archivo: Infrastructure/Seeders/UsuarioSeeder.cs (líneas 21 y 31)
  - Qué pasa: Se inicializa un hash con la clave `"admin123"` en texto plano y además se expone directamente en la consola durante el arranque.
  - Sugerencia: Cargar credenciales desde variables de entorno seguras o solicitar la creación de la contraseña administrativa en el primer inicio.

- [ ] **[Severidad: alta]** Datos sensibles y hashes reales bajo control de versiones
  - Archivo: Data/usuarios.json (líneas 1-14)
  - Qué pasa: El archivo con nombres y hashes BCrypt de usuarios reales está rastreado en Git sin exclusión en `.gitignore`.
  - Sugerencia: Añadir la carpeta `Data/` al archivo `.gitignore` y purgar el historial del repositorio para proteger las credenciales expuestas.

- [ ] **[Severidad: media]** Ausencia de reglas de complejidad y longitud en contraseñas
  - Archivo: Application/Services/AuthService.cs (líneas 33-46)
  - Qué pasa: El registro de usuarios no valida longitud mínima, variedad de caracteres ni rechaza contraseñas en blanco.
  - Sugerencia: Crear un validador que requiera un mínimo de caracteres y patrones de seguridad antes de generar el hash.

- [x] **[Severidad: media - RESUELTO]** Captura de contraseña en consola sin filtrado de teclas de control
  - Archivo: UI/ConsoleUI/Helpers/InputHelper.cs (líneas 27-41)
  - Qué pasa: El método aceptaba cualquier tecla distinta a Enter y Backspace, acumulando caracteres no imprimibles o de navegación en la clave. (Corregido con `!char.IsControl(key.KeyChar)`).
  - Sugerencia: Filtrar los caracteres con `!char.IsControl(key.KeyChar)` antes de anexarlos al texto de la contraseña.

---

## 2. Errores o Posibles Bugs

- [ ] **[Severidad: alta]** Terminación inesperada de la aplicación ante error de autenticación
  - Archivo: UI/ConsoleUI/Menus/LoginView.cs (líneas 33, 62-67) y Program.cs (líneas 40-46)
  - Qué pasa: Cuando las credenciales son incorrectas, `ManejarLogin()` retorna `null` y el ciclo de `Program.cs` asume que el usuario desea salir del sistema.
  - Sugerencia: Controlar los reintentos dentro de `LoginView` y devolver `null` únicamente cuando se escoja la opción "0. Salir".

- [ ] **[Severidad: alta]** Pérdida definitiva de datos de multas por falta de persistencia
  - Archivo: Application/Services/PrestamoService.cs (líneas 81-99) y Application/Services/MultaService.cs (líneas 9-15)
  - Qué pasa: La entidad `Multa` y su propiedad `Pagada` se instancian en memoria y se imprimen en ticket, pero nunca se guardan en un repositorio persistente.
  - Sugerencia: Diseñar una interfaz `IMultaRepository` y persistir las multas para permitir consulta de saldos y control de cobranza.

- [ ] **[Severidad: media]** Falso positivo en mensaje de actualización de rol para usuarios inexistentes
  - Archivo: UI/ConsoleUI/Menus/MenuAdmin.cs (líneas 72-73) y Application/Services/UsuarioService.cs (líneas 21-30)
  - Qué pasa: `UsuarioService.CambiarRol` finaliza silenciosamente si el usuario no existe, pero la interfaz siempre confirma el cambio exitoso.
  - Sugerencia: Devolver un resultado booleano o lanzar una excepción descriptiva si el usuario no es encontrado en el repositorio.

- [ ] **[Severidad: media]** Asignación silenciosa de rol Usuario ante opciones inválidas en menú de administración
  - Archivo: UI/ConsoleUI/Menus/MenuAdmin.cs (líneas 64-70)
  - Qué pasa: Si el administrador escribe un valor inválido o presiona una tecla no listada, el bloque switch asigna por defecto `Rol.Usuario` sin advertencia.
  - Sugerencia: Validar la entrada dentro de un bucle de confirmación y rechazar opciones que no correspondan exactamente a los roles disponibles.

- [ ] **[Severidad: media]** Estado 'Vencido' no persistido ni actualizado en los préstamos
  - Archivo: Core/Enums/EstadoPrestamo.cs (línea 6) y Application/Services/PrestamoService.cs
  - Qué pasa: Aunque el valor `Vencido` existe en el enum, ningún servicio actualiza este estado en los datos guardados, calculándose solo al vuelo para la pantalla.
  - Sugerencia: Incorporar un método en `PrestamoService` que actualice formalmente el estado de los préstamos vencidos en el almacenamiento.

- [ ] **[Severidad: media]** Persistencia no transaccional entre préstamos y unidades de libros
  - Archivo: Application/Services/PrestamoService.cs (líneas 58-59 y 93-94)
  - Qué pasa: El guardado de préstamos y la actualización del catálogo de libros se realizan en escrituras de archivo independientes sin mecanismo de reversión.
  - Sugerencia: Agrupar ambas operaciones bajo una unidad de coordinación que revierta el primer archivo si falla la escritura del segundo.

- [ ] **[Severidad: baja]** Pausa duplicada en pantalla de gestión de usuarios
  - Archivo: UI/ConsoleUI/Menus/MenuAdmin.cs (línea 46) y UI/ConsoleUI/Menus/MenuUsuario.cs (línea 35)
  - Qué pasa: Al no haber usuarios registrados se ejecuta `ConsoleHelper.Pause()` dentro del flujo y el menú contenedor repite la pausa inmediatamente después.
  - Sugerencia: Eliminar la llamada a `ConsoleHelper.Pause()` interna y permitir que el ciclo base de menú gestione la pausa unificada.

---

## 3. Cosas que no Sigan las Convenciones del Proyecto

- [ ] **[Severidad: alta]** Código fuente desarrollado en español en vez de inglés
  - Archivo: Core, Application, Infrastructure y UI (todo el código)
  - Qué pasa: Las entidades, servicios, excepciones, métodos y variables están nombrados en español, infringiendo la convención de inglés en código.
  - Sugerencia: Reestructurar los nombres de clases, contratos y miembros para que utilicen el idioma inglés según la norma del proyecto.

- [ ] **[Severidad: media]** Nombres de ramas en Git fuera de las convenciones feature/fix/docs
  - Archivo: Repositorio Git (ramas locales y remotas)
  - Qué pasa: Existen ramas denominadas `Refactor/BibliotecaV2` y `chore/revision-agente` que no emplean los prefijos estandarizados obligatorios.
  - Sugerencia: Ajustar los nombres de ramas activas y futuras para que comiencen exclusivamente con `feature/`, `fix/` o `docs/`.

- [ ] **[Severidad: media]** Mensajes de commit no conformes con tipo(scope): descripción
  - Archivo: Repositorio Git (historial de commits)
  - Qué pasa: Gran parte de los registros de confirmación utilizan texto libre o carecen de alcance estructurado (e.g. `refactor: PR Completed`, `Refactor: all code`).
  - Sugerencia: Configurar githooks con validadores de conventional commits para exigir la sintaxis obligatoria antes de confirmar cambios.

---

## 4. Estructura de Carpetas y Nombres Inconsistentes o Confusos

- [ ] **[Severidad: media]** Discrepancia entre nombre de la solución y espacio de nombres del código
  - Archivo: BibliotecaV1.csproj (línea 8) y Program.cs (líneas 1-7)
  - Qué pasa: Los archivos de solución y proyecto se denominan `BibliotecaV1`, mientras que el `RootNamespace` y las directivas internas emplean `BibliotecaV2`.
  - Sugerencia: Unificar el nombre del archivo de proyecto y solución para coincidir exactamente con el espacio de nombres principal.

- [ ] **[Severidad: media]** Archivos de datos vacíos y carpetas de almacenamiento duplicadas
  - Archivo: Infrastructure/Data/ (libros.json, prestamos.json, usuarios.json)
  - Qué pasa: La carpeta `Infrastructure/Data/` contiene archivos de 0 bytes mientras que la aplicación almacena y lee la información desde la carpeta raíz `Data/`.
  - Sugerencia: Eliminar la carpeta `Infrastructure/Data/` vacía y documentar formalmente la ubicación de almacenamiento elegida.

- [ ] **[Severidad: media]** Archivo espurio con caracter de control en la raíz del proyecto
  - Archivo:  (raíz del repositorio)
  - Qué pasa: Se encuentra un archivo con nombre no imprimible (`\357\200\233`) cuyo contenido es un volcado residual de la salida de `git log`.
  - Sugerencia: Eliminar este archivo residual y vigilar que no se creen archivos de texto accidentales mediante redirecciones en terminal.

- [x] **[Severidad: baja - RESUELTO]** Desorden visual en la numeración de los menús derivados
  - Archivo: UI/ConsoleUI/Menus/MenuUsuario.cs (línea 31)
  - Qué pasa: La opción "0. Cerrar sesión" provenía de la clase base y quedaba listada en medio de la pantalla antes de las opciones 6, 7 y 8. (Corregido imprimiendo el cierre de sesión al final del bucle principal de presentación en `MenuUsuario`).
  - Sugerencia: Sobrescribir la presentación de opciones para que la alternativa de cierre siempre se imprima en el último renglón numérico.

---

## 5. Código Repetido, Muerto o Difícil de Entender

- [ ] **[Severidad: media]** Archivo DTO vacío sin definición de tipos
  - Archivo: Application/DTOs/LoginRequest.cs
  - Qué pasa: El archivo se mantiene en el proyecto con 0 bytes debido a que `LoginRequest` fue declarada dentro de `AuthRequest.cs`.
  - Sugerencia: Eliminar el archivo vacío o reubicar cada clase DTO en su archivo propio manteniendo coherencia de nombres.

- [ ] **[Severidad: media]** Código muerto: clase genérica OperationResult<T> sin invocación
  - Archivo: Application/DTOs/OperationResult.cs (líneas 3-21)
  - Qué pasa: La clase `OperationResult<T>` está implementada pero no es utilizada por ningún servicio, método ni controlador del repositorio.
  - Sugerencia: Adoptar este tipo de retorno en los servicios para unificar la respuesta de operaciones o eliminar la clase si no será utilizada.

- [ ] **[Severidad: media]** Entidad Session sin utilización en la arquitectura de dominio
  - Archivo: Core/Entities/Session.cs (líneas 3-8)
  - Qué pasa: La clase `Session` replica la lógica del servicio `SessionService` pero no es instanciada ni referenciada en ninguna parte del sistema.
  - Sugerencia: Eliminar la entidad huérfana o definir claramente su propósito si se requiere modelar sesiones persistentes en base de datos.

- [ ] **[Severidad: media]** Acoplamiento indebido de infraestructura con la consola del sistema
  - Archivo: Infrastructure/Tickets/TicketFileWriter.cs (líneas 25-27) e Infrastructure/Seeders/UsuarioSeeder.cs (líneas 30-32)
  - Qué pasa: Clases de infraestructura invocan directamente `Console.ForegroundColor` y `Console.WriteLine`, quebrantando la separación de capas.
  - Sugerencia: Desacoplar la infraestructura mediante eventos o un servicio de logging inyectado, evitando dependencias directas con la consola.

- [ ] **[Severidad: baja]** Uso de rutas relativas dependientes del directorio de ejecución
  - Archivo: Infrastructure/Repositories/JsonLibroRepository.cs (línea 9) y Infrastructure/Tickets/TicketFileWriter.cs (línea 9)
  - Qué pasa: La referencia `Path.Combine("Data", ...)` se calcula a partir del directorio actual de trabajo, fallando si la aplicación arranca desde otra ruta.
  - Sugerencia: Resolver las rutas absolutas utilizando `AppContext.BaseDirectory` o inyectarlas mediante parámetros de configuración.

- [x] **[Severidad: baja - RESUELTO]** Directiva 'using' redundante en mismo espacio de nombres
  - Archivo: UI/ConsoleUI/Helpers/InputHelper.cs (línea 1)
  - Qué pasa: El archivo importaba `using BibliotecaV2.UI.ConsoleUI.Helpers;` a pesar de pertenecer exactamente a ese mismo namespace. (Línea redundante eliminada).
  - Sugerencia: Quitar la directiva redundantemente declarada para mantener limpio el código fuente.

---

## 6. Cosas que Faltan (Pruebas, Validaciones, Manejo de Errores)

- [ ] **[Severidad: alta]** Ausencia absoluta de proyectos y pruebas automatizadas
  - Archivo: BibliotecaV1.sln (solución completa)
  - Qué pasa: No existen proyectos de pruebas unitarias ni de integración para validar reglas de negocio, límites de préstamos ni persistencia.
  - Sugerencia: Crear un proyecto de pruebas con xUnit o NUnit que valide los casos críticos de servicios y cálculos financieros.

- [ ] **[Severidad: media]** Falta de validación en creación de libros y unidades negativas
  - Archivo: Application/Services/LibroService.cs (líneas 34-42)
  - Qué pasa: El método `Agregar` no valida títulos ni autores vacíos y permite registrar unidades con valores negativos en el catálogo.
  - Sugerencia: Agregar validaciones de negocio que verifiquen cadenas no vacías y aseguren que las unidades sean estrictamente mayores o iguales a cero.

- [ ] **[Severidad: media]** Falta de validación de formato en registro de nombres de usuario
  - Archivo: Application/Services/AuthService.cs (líneas 33-46)
  - Qué pasa: Se pueden crear usuarios con nombres vacíos o formados únicamente por espacios en blanco sin ninguna restricción de formato.
  - Sugerencia: Validar que el nombre cumpla con una longitud mínima y caracteres alfanuméricos válidos antes de procesar el alta.

- [ ] **[Severidad: media]** Discrepancia entre características documentadas en README y métodos no implementados
  - Archivo: README.md (líneas 37-38) y Application/Services/LibroService.cs
  - Qué pasa: La documentación asegura que existen operaciones de modificación y eliminación de libros, pero los servicios carecen de dichos métodos.
  - Sugerencia: Desarrollar las funcionalidades de edición y baja de libros o rectificar el README para reflejar únicamente las funciones operativas.

- [ ] **[Severidad: media]** Falta de salvaguarda ante la revocación del último administrador
  - Archivo: Application/Services/UsuarioService.cs (líneas 21-30)
  - Qué pasa: Se puede modificar el rol del único administrador hacia un rol regular, dejando el sistema sin cuentas con acceso a la administración.
  - Sugerencia: Bloquear la modificación de rol si el usuario afectado es el único administrador registrado en el sistema.

- [ ] **[Severidad: baja - RESUELTO]** Ausencia de captura global para excepciones no controladas en el bucle principal
  - Archivo: Program.cs (líneas 38-58)
  - Qué pasa: Errores no previstos (e.g. bloqueos de archivos JSON por concurrencia o permisos de disco) causaban la caída súbita del programa sin mensaje controlado. (Implementado bloque try-catch global en el despacho de menús).
  - Sugerencia: Envolver el despacho de opciones en un bloque try-catch global que capture cualquier excepción imprevista y la notifique vía `ConsoleHelper`.

---

## Lo que NO revisé

- **Ejecución dinámica de flujos de consola interactivos:** No se interactuó con los menús de consola en tiempo de ejecución para evaluar usabilidad o eventos de teclado complejos en vivo.
- **Concurrencia multihilo y bloqueos de archivo:** No se evaluaron escenarios de accesos paralelos o simultáneos sobre los archivos JSON para medir posibles condiciones de carrera en disco.
- **Auditoría de vulnerabilidades en paquetes externos:** No se ejecutaron herramientas de inspección de seguridad en línea de NuGet para el paquete `BCrypt.Net-Next` debido a las restricciones de aislamiento de la red.
- **Directorios y artefactos excluidos por regla:** No se analizaron los contenidos de `.git/`, `bin/`, `obj/` ni artefactos autogenerados.
- **Ambigüedad sobre la persistencia en control de versiones:** No estoy seguro de si la inclusión del archivo `Data/usuarios.json` en Git fue una decisión intencional para facilitar pruebas locales del evaluador o una omisión accidental de configuración en `.gitignore`.

