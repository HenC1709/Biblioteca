# Análisis y Solución de Errores de Severidad Media (Gestión de Roles)

Este documento detalla los dos problemas de **severidad media** priorizados y resueltos en el módulo de administración de usuarios (`MenuAdmin` y `UsuarioService`), seleccionados por su impacto directo en la integridad de permisos y por requerir la modificación de únicamente dos archivos del proyecto.

---

## 1. Falso positivo en actualización de rol para usuarios inexistentes

- **Archivos involucrados:** `Application/Services/UsuarioService.cs` y `UI/ConsoleUI/Menus/MenuAdmin.cs`

### ¿Por qué sucedía?
El método `UsuarioService.CambiarRol(string nombre, Rol nuevoRol)` tenía un tipo de retorno `void`. Cuando buscaba el usuario en la lista mediante `FirstOrDefault(...)` y no encontraba coincidencias (`usuario == null`), ejecutaba un `return;` silencioso sin lanzar ninguna excepción ni devolver un valor booleano que indicara el fallo. Por su parte, `MenuAdmin` invocaba `CambiarRol` y en la línea inmediatamente siguiente imprimía el mensaje de éxito sin verificar si el cambio realmente ocurrió.

### ¿Qué hacía?
Si el administrador cometía un error de tipeo al escribir el nombre del usuario (por ejemplo, escribiendo `"Orlandoo"` en lugar de `"Orlando"`), el sistema igualmente le pedía el nuevo rol y mostraba en verde:
> `Rol de 'Orlandoo' actualizado a Admin.`

Sin embargo, en el archivo `Data/usuarios.json` no se realizaba ningún cambio, engañando al administrador al hacerle creer que los permisos habían sido otorgados o revocados.

### ¿Por qué era de Severidad Media?
- **No era Baja** porque no se trata de un detalle estético; altera la confiabilidad de una operación administrativa de control de acceso y produce desincronización entre lo que el administrador cree que configuró y el estado real de los permisos.
- **No era Alta** porque no corrompe el archivo JSON ni detiene la ejecución del programa, sino que falla de forma silenciosa omitiendo la escritura.

### Solución aplicada
Se cambió la firma de `UsuarioService.CambiarRol` para que retorne `bool` (`false` cuando `usuario == null` y `true` tras persistir el cambio). En `MenuAdmin.cs`, se evalúa este valor y, si es `false`, se informa con un mensaje de error claro (`No se encontró ningún usuario con el nombre '...'`).

---

## 2. Asignación silenciosa de rol `Usuario` ante opciones inválidas en menú de administración

- **Archivo involucrado:** `UI/ConsoleUI/Menus/MenuAdmin.cs`

### ¿Por qué sucedía?
Al solicitar el nuevo rol por consola (`1. Usuario`, `2. Bibliotecario`, `3. Admin`), la expresión `switch` en `MenuAdmin.cs` utilizaba el patrón de descarte (`_ => Rol.Usuario`) como caso por defecto para cualquier entrada que no fuera `"1"`, `"2"` o `"3"`.

### ¿Qué hacía?
Si el administrador ingresaba por accidente un número fuera de rango (como `"4"`), escribía el nombre del rol en texto (como `"Admin"`) o cometía un error al teclear, el sistema no mostraba ninguna advertencia: degradaba silenciosamente la cuenta seleccionada al nivel más bajo (`Rol.Usuario`), guardaba el cambio en disco y mostraba un mensaje de éxito. Esto podía provocar la pérdida accidental de privilegios de otros administradores o bibliotecarios.

### ¿Por qué era de Severidad Media?
- **No era Baja** porque modifica datos persistidos sin consentimiento explícito ante una entrada inválida, pudiendo revocar privilegios críticos de cuentas administrativas o de bibliotecarios por un simple error de teclado.
- **No era Alta** porque solo puede ocurrir dentro de una sesión autenticada de administrador y requiere una interacción manual errónea en ese submenú.

### Solución aplicada
Se modificó la expresión `switch` para evaluar hacia un tipo anulable `Rol?`, devolviendo `null` en el caso por defecto (`_ => null`). Si el resultado es `null`, se cancela inmediatamente la operación mostrando una advertencia (`Opción de rol inválida. No se realizaron cambios.`) sin tocar los permisos del usuario.
