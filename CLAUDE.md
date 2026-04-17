# AH Reporting System — Guía para Claude Code

## Descripción General del Proyecto

**AH Reporting System** es una aplicación web ASP.NET MVC 5 (.NET Framework 4.8) desarrollada en VB.NET para **Alternative Holdings (AH)**, un consorcio de múltiples empresas.

El sistema permite cargar datos contables mensuales desde archivos Excel (General Ledger u otros reportes similares) de cada empresa del grupo, mapear sus cuentas contables a un catálogo propio centralizado y generar reportes consolidados por empresa individual, por selección de empresas o del grupo completo.

---

## Stack Tecnológico

| Componente        | Tecnología                        |
|-------------------|-----------------------------------|
| Framework         | ASP.NET MVC 5 (.NET Framework 4.8)|
| Lenguaje          | VB.NET                            |
| ORM               | Entity Framework 6                |
| Autenticación     | ASP.NET Identity 2.x              |
| UI Framework      | AdminLTE 3.2 + Bootstrap 3.4      |
| Tablas dinámicas  | DataTables (con integración AdminLTE) |
| Base de datos     | SQL Server 2022                   |
| IDE               | Visual Studio Community 2022+     |
| Deployment        | FTP                               |
| Repositorio       | GitHub                            |

---

## Identidad Visual

- **Logo:** `~/Content/img/ah-logo.png` (descargado de Wix, incluido en el proyecto)
- **Color primario:** `#008bcc`
- **UI Base:** AdminLTE 3.2 con el color primario sobreescrito mediante CSS personalizado
- **Skin AdminLTE:** `skin-blue` con primary color ajustado a `#008bcc`

### Sobrescritura del color primario (custom.css):
```css
.bg-primary, .btn-primary, .sidebar-dark-primary .nav-sidebar > .nav-item > .nav-link.active {
    background-color: #008bcc !important;
}
.btn-primary {
    border-color: #007aad !important;
}
a { color: #008bcc; }
```

---

## Arquitectura y Estructura de Carpetas

```
AHReportingSystem/               ← Carpeta raíz del repositorio
├── CLAUDE.md                    ← Este archivo
├── .gitignore
├── AHReportingSystem.sln
└── AHReportingSystem/           ← Proyecto web
    ├── App_Start/
    │   ├── BundleConfig.vb
    │   ├── FilterConfig.vb
    │   ├── RouteConfig.vb
    │   ├── IdentityConfig.vb
    │   └── Startup.Auth.vb
    ├── Controllers/
    ├── Models/
    │   ├── IdentityModels.vb    ← Usuario, Rol con Identity
    │   ├── AppDbContext.vb      ← DbContext principal
    │   └── ViewModels/
    ├── Views/
    │   ├── Shared/
    │   │   ├── _Layout.vbhtml   ← Layout AdminLTE
    │   │   └── _LockScreen.vbhtml
    │   └── ...
    ├── Content/
    │   ├── AdminLTE/            ← Archivos AdminLTE 3.2
    │   ├── img/
    │   │   └── ah-logo.png
    │   └── custom.css
    ├── Scripts/
    ├── packages.config
    ├── Web.config
    └── Global.asax
```

---

## Modelo de Datos

### Fase 1 — Catálogo de Cuentas Propio del Sistema

```sql
-- Tabla principal de cuentas del sistema AH
CREATE TABLE SystemAccounts (
    IdCuenta    INT PRIMARY KEY,       -- Ej: 617000
    Cuenta      NVARCHAR(200) NOT NULL, -- Ej: Employer Match Expense
    Categoria   NVARCHAR(100) NOT NULL, -- Ej: Expense
    SubCategoria NVARCHAR(100) NOT NULL,-- Ej: ADMINEXPENSES
    Agrupacion  NVARCHAR(100) NOT NULL, -- Ej: EMPLOYEE EXPENSES
    Activo      BIT NOT NULL DEFAULT 1,
    CreadoPor   NVARCHAR(256),
    CreadoEn    DATETIME2,
    ModificadoPor NVARCHAR(256),
    ModificadoEn  DATETIME2
)
```

### Fase 2 — Empresas y Mapeo

```sql
CREATE TABLE Companies (
    CompanyId   INT IDENTITY PRIMARY KEY,
    Name        NVARCHAR(200) NOT NULL,
    Code        NVARCHAR(20) NOT NULL,
    Active      BIT NOT NULL DEFAULT 1
)

CREATE TABLE CompanyAccounts (
    CompanyAccountId INT IDENTITY PRIMARY KEY,
    CompanyId        INT NOT NULL REFERENCES Companies(CompanyId),
    ExternalAccountId NVARCHAR(50) NOT NULL,  -- Cuenta en sistema externo
    ExternalAccountName NVARCHAR(200) NOT NULL,
    SystemAccountIdCuenta INT REFERENCES SystemAccounts(IdCuenta), -- Mapeo
    IsMapped     BIT NOT NULL DEFAULT 0
)
```

### Fase 3 — Carga de GL y Reportes

```sql
CREATE TABLE GLEntries (
    EntryId         INT IDENTITY PRIMARY KEY,
    CompanyId       INT NOT NULL REFERENCES Companies(CompanyId),
    Period          DATE NOT NULL,        -- Mes/Año del GL
    CompanyAccountId INT NOT NULL REFERENCES CompanyAccounts(CompanyAccountId),
    Debit           DECIMAL(18,2) NOT NULL DEFAULT 0,
    Credit          DECIMAL(18,2) NOT NULL DEFAULT 0,
    Balance         DECIMAL(18,2) NOT NULL DEFAULT 0,
    LoadedBy        NVARCHAR(256),
    LoadedAt        DATETIME2
)
```

---

## Sistema de Usuarios

Usa **ASP.NET Identity 2.x** con Entity Framework.

### Roles definidos:
- `Admin` — Acceso total al sistema
- `Manager` — Puede cargar GL, ver reportes, gestionar mapeos
- `Viewer` — Solo lectura de reportes

### Clase ApplicationUser:
```vb
Public Class ApplicationUser
    Inherits IdentityUser
    Public Property FullName As String
    Public Property LastLogin As DateTime?
    Public Property IsActive As Boolean
End Class
```

---

## LockScreen (Pantalla de Bloqueo)

La pantalla de bloqueo se activa automáticamente cuando el usuario no interactúa con el sistema durante el tiempo configurado en `Web.config`.

### Configuración en Web.config:
```xml
<appSettings>
    <add key="LockScreenTimeoutMinutes" value="15" />
</appSettings>
```

### Implementación:
- JavaScript en `_Layout.vbhtml` detecta inactividad (mousemove, keypress, click, scroll)
- Al expirar el tiempo, redirige a `/Account/LockScreen`
- La pantalla de bloqueo muestra el logo de AH y solicita contraseña
- No hace logout completo; solo bloquea la sesión
- Al desbloquear exitosamente, redirige a la URL previa (guardada en sesión)

---

## Convenciones de Código

### VB.NET
- Usar `Option Strict On` en todos los archivos
- Nombres de clases: PascalCase
- Nombres de variables y propiedades: camelCase en variables locales, PascalCase en propiedades
- Prefijo `_` para campos privados: `Private _nombre As String`
- Comentarios en **inglés** (código) pero mensajes de UI en **inglés** también (el sistema es para AH que opera en inglés)

### Controllers
- Heredar de `BaseController` (crear una clase base con funcionalidades comunes)
- Toda acción que requiere autenticación lleva `<Authorize>` attribute
- Usar ViewModels separados para cada vista

### Vistas (.vbhtml)
- Todas las vistas extienden `_Layout.vbhtml`
- Usar `@Html.AntiForgeryToken()` en todos los formularios POST
- DataTables para cualquier listado de más de 20 registros potenciales
- Mensajes de éxito/error mediante TempData["SuccessMessage"] / TempData["ErrorMessage"]

### Entity Framework
- Code First con Migrations
- El DbContext principal es `ApplicationDbContext` en `Models/AppDbContext.vb`
- Las entidades llevan anotaciones de datos y configuración Fluent en `OnModelCreating`

---

## AdminLTE 3.2 — Componentes Disponibles

AdminLTE 3.2 está incluido en `~/Content/AdminLTE/`. Los componentes principales a usar:

- **Sidebar:** Menú lateral con ícono de AH logo en la parte superior
- **Cards:** Para envolver formularios y tablas (`card`, `card-header`, `card-body`)
- **DataTables:** Integrado con el plugin `datatables.net-bs4`
- **Select2:** Para dropdowns con búsqueda
- **SweetAlert2:** Para confirmaciones y alertas elegantes
- **Toastr:** Para notificaciones toast

### CDN o local:
Usar archivos locales en producción. Para AdminLTE, los archivos están en `~/Content/AdminLTE/`.

---

## Fase 1 — Alcance Detallado (IMPLEMENTAR PRIMERO)

### 1.1 Modelo de Datos Base
- [x] ApplicationUser con Identity 2.x
- [x] ApplicationRole
- [x] ApplicationDbContext
- [ ] Migración inicial con tablas de Identity
- [ ] Tabla SystemAccounts

### 1.2 Sistema de Autenticación
- [ ] Login (`/Account/Login`)
- [ ] Logout
- [ ] LockScreen (`/Account/LockScreen`) con JS inactivity timer
- [ ] Gestión de perfil básico

### 1.3 Gestión de Usuarios (solo Admin)
- [ ] CRUD de usuarios (`/Admin/Users`)
- [ ] Asignación de roles
- [ ] Activar/Desactivar usuarios

### 1.4 Catálogo de Cuentas del Sistema
- [ ] CRUD de SystemAccounts (`/Accounts/`)
- [ ] Listado con DataTables (búsqueda, ordenamiento, paginación)
- [ ] Crear cuenta nueva (formulario con validación)
- [ ] Editar cuenta
- [ ] Activar/Desactivar cuenta (soft delete)
- [ ] Exportar listado a Excel

### 1.5 Catálogos de Soporte
- [ ] CRUD de Categorías
- [ ] CRUD de SubCategorías (relacionada a Categoría)
- [ ] CRUD de Agrupaciones

---

## Fases Futuras (Solo para Contexto)

### Fase 2 — Empresas y Mapeo de Cuentas
- CRUD de Companies
- Carga de cuentas externas desde Excel
- Interfaz de mapeo cuenta externa → cuenta del sistema
- Validación de mapeo completo

### Fase 3 — Carga de General Ledger
- Upload de archivo Excel por empresa y período
- Procesamiento y validación del GL
- Verificación de mapeos (alertar cuentas no mapeadas)
- Almacenamiento en GLEntries

### Fase 4 — Reportes
- Balance Sheet consolidado
- Income Statement consolidado
- Filtros por empresa(s) y período
- Exportación a Excel y PDF

---

## Configuración de Base de Datos

### Desarrollo (`Web.config`)
Servidor: `ALEX-PC\SQLEXPRESS` — Autenticación de Windows (Integrated Security=True).
```xml
<add name="AHReportingContext"
     connectionString="Data Source=ALEX-PC\SQLEXPRESS;Initial Catalog=AHReportingSystem;Integrated Security=True;MultipleActiveResultSets=True;App=AHReportingSystem"
     providerName="System.Data.SqlClient" />
```

### Producción (`Web.Release.config`)
Autenticación SQL (User Id / Password). Los valores reales **no se guardan en Git**.
Antes del deploy FTP, reemplazar en `Web.Release.config`:
- `PROD_SERVER` → nombre/IP del servidor SQL de producción
- `ah_app_user` → usuario SQL de producción
- `CHANGE_ME` → contraseña del usuario SQL de producción

### Crear base de datos:
```
PM> Enable-Migrations
PM> Add-Migration InitialCreate
PM> Update-Database
```

---

## Configuración de NuGet Packages

Todos los paquetes están en `packages.config`. Para restaurar:

1. Abrir la solución en Visual Studio
2. Click derecho en la solución → "Restore NuGet Packages"
3. O en Package Manager Console: `Update-Package -reinstall`

### Paquetes clave:
- `Microsoft.AspNet.Mvc` 5.2.9
- `EntityFramework` 6.4.4
- `Microsoft.AspNet.Identity.*` 2.2.3
- `Microsoft.Owin.*` 4.2.2
- `EPPlus` 5.8.5 (Excel processing — para Fase 2+)
- `Newtonsoft.Json` 13.0.3

---

## AdminLTE — Instalación Manual

AdminLTE 3.2 no está en NuGet. Debes:

1. Descargar AdminLTE 3.2.0 desde: https://github.com/ColorlibHQ/AdminLTE/releases/tag/v3.2.0
2. Copiar el contenido de la carpeta `dist/` a `~/Content/AdminLTE/`
3. La estructura esperada es:
   ```
   Content/AdminLTE/
   ├── css/
   │   ├── adminlte.min.css
   │   └── adminlte.css
   ├── js/
   │   ├── adminlte.min.js
   │   └── adminlte.js
   └── img/
   ```

Alternativamente, usar CDN para desarrollo:
```html
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/admin-lte@3.2/dist/css/adminlte.min.css">
<script src="https://cdn.jsdelivr.net/npm/admin-lte@3.2/dist/js/adminlte.min.js"></script>
```

---

## Gestión de Git

### Repositorio:
`https://github.com/OnTechGit/AHReportingSystem.git`

### IMPORTANTE — Política de Commits:
**Claude Code NO debe hacer commits.** Los commits los realiza el desarrollador (Alex) cuando lo considera apropiado.

Claude Code puede:
- Crear y modificar archivos
- Sugerir mensajes de commit
- Ejecutar `git status` y `git diff` para mostrar cambios

Claude Code NO debe:
- Ejecutar `git commit`
- Ejecutar `git push`
- Ejecutar `git add` sin instrucción explícita del desarrollador

### Configuración inicial del repo (ejecutar manualmente):
```bash
git init
git remote add origin https://github.com/OnTechGit/AHReportingSystem.git
git add .
git commit -m "Initial project structure: ASP.NET MVC 5 VB.NET with AdminLTE 3.2 and Identity"
git branch -M main
git push -u origin main
```

---

## Deployment vía FTP

La configuración de publicación FTP se hará mediante un perfil de publicación en Visual Studio:

1. Click derecho en el proyecto → Publish
2. Seleccionar "FTP"
3. Configurar las credenciales del servidor
4. El archivo de perfil se guardará en `Properties/PublishProfiles/`

**No incluir credenciales FTP en el repositorio.**

---

## Notas Importantes para Claude Code

1. **Siempre usar VB.NET**, nunca C#.
2. **Respetar la estructura AdminLTE** en todas las vistas — no mezclar estilos Bootstrap puro con AdminLTE.
3. **Siempre validar** tanto del lado del cliente (jQuery Validation) como del servidor (ModelState).
4. **Los mensajes de UI son en inglés** (la interfaz es para usuarios del consorcio AH).
5. **No usar Scaffold automático de Visual Studio** — escribir el código manualmente para tener control total.
6. **EF Code First con Migrations** — no usar Database First.
7. **LockScreen es obligatorio** — debe implementarse desde el inicio, en el Layout.
8. **`Option Strict On`** en todos los archivos VB.NET.
9. **No hacer commits** — solo el desarrollador decide cuándo hacer commit.
10. **Pedir confirmación** antes de eliminar o modificar archivos existentes.

---

## Prompt Inicial para Comenzar el Trabajo

Ver sección al final de este documento: **"PROMPT INICIAL"**

---

## PROMPT INICIAL

Usa este prompt para iniciar el trabajo con Claude Code:

```
Hola Claude Code. Estamos comenzando el desarrollo del AH Reporting System.
Lee el archivo CLAUDE.md completo en la raíz del repositorio para entender el proyecto.

El proyecto es una aplicación web ASP.NET MVC 5 (.NET Framework 4.8) en VB.NET
para Alternative Holdings. Ya tenemos la estructura base del proyecto creada con
los archivos .sln, .vbproj, packages.config y la configuración inicial.

TU TAREA para la Fase 1 es:

1. Revisar todos los archivos existentes en el proyecto para entender la estructura.

2. Implementar el sistema de autenticación completo:
   a. ApplicationUser con campos FullName, LastLogin, IsActive
   b. ApplicationDbContext con Entity Framework 6
   c. Login/Logout funcional con AdminLTE 3.2
   d. LockScreen con timer de inactividad (configurable en Web.config clave LockScreenTimeoutMinutes)

3. Implementar gestión de usuarios (Admin only):
   a. Listado de usuarios con DataTables
   b. Crear usuario con asignación de rol (Admin, Manager, Viewer)
   c. Editar usuario
   d. Activar/Desactivar usuario

4. Implementar el CRUD completo de SystemAccounts (Catálogo de Cuentas):
   a. Tabla con campos: IdCuenta (int, PK manual), Cuenta, Categoria, SubCategoria, Agrupacion
   b. Listado con DataTables (búsqueda, ordenamiento, exportar Excel)
   c. Crear cuenta nueva
   d. Editar cuenta
   e. Soft delete (campo Activo)

5. Implementar catálogos de soporte (Categoría, SubCategoría, Agrupación)

RESTRICCIONES IMPORTANTES:
- Todo el código en VB.NET con Option Strict On
- UI con AdminLTE 3.2 (archivos locales en ~/Content/AdminLTE/)
- Color primario: #008bcc
- Logo: ~/Content/img/ah-logo.png
- NO hacer commits de Git (solo el desarrollador hace commits)
- Antes de modificar archivos existentes, mostrar qué cambiarás y pedir confirmación

Comienza revisando el archivo CLAUDE.md y luego el estado actual del proyecto,
y dime qué encuentras y cómo procederás.
```
