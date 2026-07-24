# SC601 - Asociacion Solidarista (Modulo Pago SINPE)

Bienvenido al repositorio del proyecto **Asociacion Solidarista**, enfocado en la gestion de transacciones y comercios mediante pagos SINPE.

## Arquitectura del Proyecto
El proyecto sigue el patron arquitectonico **MVC (Model-View-Controller)** clasico.
Para el acceso a datos, se implementa el patron **Repository** apoyado sobre **Entity Framework Code First**, centralizando las consultas y aislando la logica de acceso a datos del controlador.

## Tecnologias Utilizadas
- **Framework**: .NET Framework 4.7.2
- **Lenguaje**: C#
- **ORM**: Entity Framework 6 (Code First & Migrations)
- **Front-end**: HTML5, ASP.NET Razor (`.cshtml`), Bootstrap, Vanilla CSS.
- **Autenticacion**: ASP.NET Identity (preconfigurado).
- **Herramientas de Build**: MSBuild (Visual Studio 2022).

## Estructura de Carpetas
- `Controllers/`: Manejan las peticiones HTTP, validan el `ModelState` y se comunican con los Repositorios.
- `Models/Entities/`: Modelos de dominio mapeados directamente a la base de datos (POCOs).
- `Infrastructure/DbContexts/`: Contiene `AsociacionSolidaristaDbContext`, definido mediante Fluent API.
- `Infrastructure/Repositories/`: Implementacion del patron `IRepository<T>` y sus repositorios concretos.
- `Views/`: Vistas de Razor divididas por modulo, ademas del layout global en `Views/Shared`.
- `Migrations/`: Historico de cambios estructurales aplicados a la base de datos SQL.

## Explicacion de Cada Modulo
1. **Comercio**: Entidad raiz que representa a las empresas o entes asociados. Permite administrar su informacion de contacto y estado.
2. **CajaSINPE**: Representa terminales o numeros receptores de SINPE vinculados a un Comercio especifico.
3. **PagoSINPE**: El modulo central transaccional. Registra el flujo de dinero de un origen a un destino, siempre vinculado a una `CajaSINPE`.
4. **Bitacora**: Sistema de auditoria de solo lectura. Registra el historico de movimientos (Datos Anteriores y Datos Posteriores) y se asocia de forma opcional a los pagos.

## Flujo del Negocio
La cardinalidad y el flujo de los modulos ocurre de la siguiente manera:
Un **Comercio** puede tener registradas multiples **Cajas SINPE** (Relacion 1:N). 
Cada **Caja SINPE** recibe y procesa multiples **Pagos SINPE** (Relacion 1:N). 
Los eventos importantes que alteren el estado del sistema deben registrarse transversalmente en la **Bitacora** para mantener la trazabilidad.

## Como Restaurar Paquetes NuGet
En la raiz del proyecto, utilizando la consola:
```cmd
nuget.exe restore proyecto.asociacionsolidarista.slnx
```
*(Es necesario contar con el ejecutable `nuget.exe` local o globalmente en el sistema).*

## Como Compilar el Proyecto
Debido al uso de `.slnx` y targets especificos, se requiere MSBuild de Visual Studio 2022 (o Build Tools):
```cmd
"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" proyecto.asociacionsolidarista.slnx
```

## Como Ejecutar el Proyecto
- **Opcion 1 (Visual Studio)**: Abrir la solucion en Visual Studio 2022 y ejecutar mediante `IIS Express` (F5).
- **Opcion 2 (Local IIS)**: Configurar un sitio en el IIS local apuntando a la raiz del proyecto y asegurarse de que el Application Pool use .NET Framework 4.0 (Integrado).

## Convenciones Utilizadas
- **Caracteres Especiales**: Esta ESTRICTAMENTE PROHIBIDO el uso de tildes, eñes o caracteres especiales en el codigo fuente, nombres de archivo, variables y comentarios. Todo debe mantenerse en ASCII estandar.
- **Codificacion**: Todos los archivos creados deben guardarse en formato **UTF-8**.
- **Acceso a Datos**: No invocar a `DbContext` desde los Controladores. Todo se inyecta y pasa por los Repositorios (`_repository.GetAll()`, `_repository.Add()`, etc.).

## Decisiones de Diseño
- **Evitar Auto-Scaffolding**: Las vistas se diseñan y ajustan a mano basándose en plantillas seguras. Se descubrio que el scaffolding por defecto reintroduce caracteres que rompen la regla de convencion ASCII.
- **Claridad de UI**: En los selects/dropdowns donde existian multiples Cajas, se decidio concatenar "Nombre Caja - Nombre Comercio" para evitar ambiguedades de cara al usuario.
- **Bitacora Desacoplada**: El controlador de Bitacora es de solo lectura y evita crear dependencias circulares.

## Trabajo Pendiente
1. **Seguridad (Autenticacion y Roles)**: Implementar la inyeccion del `[Authorize]` en las rutas y desarrollar el modulo de inicio de sesion con Identity.
2. **Auditoria Activa (Bitacora)**: Modificar `ComercioController`, `CajaSINPEController` y `PagoSINPEController` para que inserten eventos en la Bitacora durante los POST (Creacion, Actualizacion y Borrado).
3. **Soft-Delete**: Actualmente `Comercio` borra datos permanentes mediante un POST en linea; se podria migrar hacia un borrado logico (`Estado = 0`) para evitar conflictos de claves foraneas en cascada.
