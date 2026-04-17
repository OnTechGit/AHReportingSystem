#Requires -Version 5.1
<#
.SYNOPSIS
    Descarga todos los assets de UI del AH Reporting System.
    Ejecutar UNA SOLA VEZ después de clonar el repositorio.

.DESCRIPTION
    Descarga localmente:
      - AdminLTE 3.2.0
      - Bootstrap 4.6.2
      - jQuery 3.7.1
      - DataTables 1.13.8 + Buttons 2.4.2 + JSZip
      - Select2 4.1.0
      - Font Awesome 5.15.4 (CSS + webfonts)
      - SweetAlert2 11
      - Logo de Alternative Holdings

.USAGE
    Abrir PowerShell en la carpeta raíz del repositorio y ejecutar:
        .\Setup-Assets.ps1

    Si hay error de política de ejecución:
        Set-ExecutionPolicy -Scope CurrentUser -ExecutionPolicy RemoteSigned
#>

[CmdletBinding()]
param()

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

# ─── Configuración ────────────────────────────────────────────────────────────
$ProjectRoot = Join-Path $PSScriptRoot "AHReportingSystem"
$Content     = Join-Path $ProjectRoot "Content"
$Scripts     = Join-Path $ProjectRoot "Scripts"

# Progreso
$downloaded = 0
$failed     = 0

function Get-Asset {
    param(
        [string]$Url,
        [string]$Destination,
        [string]$Label
    )
    $dir = Split-Path $Destination -Parent
    if (-not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir -Force | Out-Null }

    try {
        $wc = New-Object System.Net.WebClient
        $wc.Headers.Add("User-Agent", "AHReporting-Setup/1.0")
        $wc.DownloadFile($Url, $Destination)
        Write-Host "  [OK] $Label" -ForegroundColor Green
        $script:downloaded++
    }
    catch {
        Write-Host "  [FAIL] $Label — $($_.Exception.Message)" -ForegroundColor Red
        $script:failed++
    }
}

Write-Host ""
Write-Host "==================================================" -ForegroundColor Cyan
Write-Host "  AH Reporting System — Setup de Assets UI" -ForegroundColor Cyan
Write-Host "==================================================" -ForegroundColor Cyan
Write-Host "  Destino: $ProjectRoot" -ForegroundColor Gray
Write-Host ""

# ─── AdminLTE 3.2.0 ───────────────────────────────────────────────────────────
Write-Host "AdminLTE 3.2.0" -ForegroundColor Yellow
Get-Asset `
    "https://cdn.jsdelivr.net/npm/admin-lte@3.2.0/dist/css/adminlte.min.css" `
    "$Content\AdminLTE\css\adminlte.min.css" `
    "adminlte.min.css"
Get-Asset `
    "https://cdn.jsdelivr.net/npm/admin-lte@3.2.0/dist/css/adminlte.css" `
    "$Content\AdminLTE\css\adminlte.css" `
    "adminlte.css"
Get-Asset `
    "https://cdn.jsdelivr.net/npm/admin-lte@3.2.0/dist/js/adminlte.min.js" `
    "$Content\AdminLTE\js\adminlte.min.js" `
    "adminlte.min.js"
Get-Asset `
    "https://cdn.jsdelivr.net/npm/admin-lte@3.2.0/dist/js/adminlte.js" `
    "$Content\AdminLTE\js\adminlte.js" `
    "adminlte.js"

# ─── Bootstrap 4.6.2 ──────────────────────────────────────────────────────────
Write-Host ""
Write-Host "Bootstrap 4.6.2" -ForegroundColor Yellow
Get-Asset `
    "https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" `
    "$Content\bootstrap\css\bootstrap.min.css" `
    "bootstrap.min.css"
Get-Asset `
    "https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js" `
    "$Content\bootstrap\js\bootstrap.bundle.min.js" `
    "bootstrap.bundle.min.js (con Popper)"

# ─── jQuery 3.7.1 ─────────────────────────────────────────────────────────────
Write-Host ""
Write-Host "jQuery 3.7.1" -ForegroundColor Yellow
Get-Asset `
    "https://cdn.jsdelivr.net/npm/jquery@3.7.1/dist/jquery.min.js" `
    "$Scripts\jquery-3.7.1.min.js" `
    "jquery-3.7.1.min.js"
Get-Asset `
    "https://cdn.jsdelivr.net/npm/jquery@3.7.1/dist/jquery.js" `
    "$Scripts\jquery-3.7.1.js" `
    "jquery-3.7.1.js"

# ─── DataTables 1.13.8 ────────────────────────────────────────────────────────
Write-Host ""
Write-Host "DataTables 1.13.8" -ForegroundColor Yellow
Get-Asset `
    "https://cdn.datatables.net/1.13.8/css/dataTables.bootstrap4.min.css" `
    "$Content\datatables\dataTables.bootstrap4.min.css" `
    "dataTables.bootstrap4.min.css"
Get-Asset `
    "https://cdn.datatables.net/1.13.8/js/jquery.dataTables.min.js" `
    "$Scripts\datatables\jquery.dataTables.min.js" `
    "jquery.dataTables.min.js"
Get-Asset `
    "https://cdn.datatables.net/1.13.8/js/dataTables.bootstrap4.min.js" `
    "$Scripts\datatables\dataTables.bootstrap4.min.js" `
    "dataTables.bootstrap4.min.js"

# ─── DataTables Buttons 2.4.2 ─────────────────────────────────────────────────
Write-Host ""
Write-Host "DataTables Buttons 2.4.2" -ForegroundColor Yellow
Get-Asset `
    "https://cdn.datatables.net/buttons/2.4.2/css/buttons.bootstrap4.min.css" `
    "$Content\datatables\buttons.bootstrap4.min.css" `
    "buttons.bootstrap4.min.css"
Get-Asset `
    "https://cdn.datatables.net/buttons/2.4.2/js/dataTables.buttons.min.js" `
    "$Scripts\datatables\dataTables.buttons.min.js" `
    "dataTables.buttons.min.js"
Get-Asset `
    "https://cdn.datatables.net/buttons/2.4.2/js/buttons.bootstrap4.min.js" `
    "$Scripts\datatables\buttons.bootstrap4.min.js" `
    "buttons.bootstrap4.min.js"
Get-Asset `
    "https://cdn.datatables.net/buttons/2.4.2/js/buttons.html5.min.js" `
    "$Scripts\datatables\buttons.html5.min.js" `
    "buttons.html5.min.js (exportar Excel/CSV)"

# ─── JSZip 3.10.1 (requerido por DataTables Buttons Excel) ───────────────────
Write-Host ""
Write-Host "JSZip 3.10.1" -ForegroundColor Yellow
Get-Asset `
    "https://cdnjs.cloudflare.com/ajax/libs/jszip/3.10.1/jszip.min.js" `
    "$Scripts\jszip.min.js" `
    "jszip.min.js"

# ─── Select2 4.1.0 ────────────────────────────────────────────────────────────
Write-Host ""
Write-Host "Select2 4.1.0" -ForegroundColor Yellow
Get-Asset `
    "https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" `
    "$Content\select2\select2.min.css" `
    "select2.min.css"
Get-Asset `
    "https://cdn.jsdelivr.net/npm/select2-bootstrap4-theme@1.0.0/dist/select2-bootstrap4.min.css" `
    "$Content\select2\select2-bootstrap4.min.css" `
    "select2-bootstrap4.min.css"
Get-Asset `
    "https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.full.min.js" `
    "$Scripts\select2\select2.full.min.js" `
    "select2.full.min.js"

# ─── SweetAlert2 11 ───────────────────────────────────────────────────────────
Write-Host ""
Write-Host "SweetAlert2 11" -ForegroundColor Yellow
Get-Asset `
    "https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css" `
    "$Content\sweetalert2\sweetalert2.min.css" `
    "sweetalert2.min.css"
Get-Asset `
    "https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.all.min.js" `
    "$Scripts\sweetalert2.all.min.js" `
    "sweetalert2.all.min.js"

# ─── Font Awesome 5.15.4 ──────────────────────────────────────────────────────
Write-Host ""
Write-Host "Font Awesome 5.15.4 (CSS + webfonts)" -ForegroundColor Yellow

# Descargar el ZIP completo de Font Awesome Free
$faZip = Join-Path $env:TEMP "fontawesome-free-5.15.4-web.zip"
$faExtract = Join-Path $env:TEMP "fontawesome-5154"

try {
    $wc = New-Object System.Net.WebClient
    $wc.Headers.Add("User-Agent", "AHReporting-Setup/1.0")
    Write-Host "  Descargando ZIP (~3 MB)..." -ForegroundColor Gray
    $wc.DownloadFile(
        "https://use.fontawesome.com/releases/v5.15.4/fontawesome-free-5.15.4-web.zip",
        $faZip
    )
    Expand-Archive -Path $faZip -DestinationPath $faExtract -Force

    $faSource = Join-Path $faExtract "fontawesome-free-5.15.4-web"
    $faDestCss  = "$Content\fontawesome\css"
    $faDestFonts = "$Content\fontawesome\webfonts"

    New-Item -ItemType Directory -Path $faDestCss   -Force | Out-Null
    New-Item -ItemType Directory -Path $faDestFonts -Force | Out-Null

    Copy-Item "$faSource\css\all.min.css" $faDestCss
    Copy-Item "$faSource\css\all.css"     $faDestCss
    Copy-Item "$faSource\webfonts\*"      $faDestFonts -Recurse
    Write-Host "  [OK] all.min.css + webfonts" -ForegroundColor Green
    $script:downloaded += 2

    Remove-Item $faZip     -Force -ErrorAction SilentlyContinue
    Remove-Item $faExtract -Recurse -Force -ErrorAction SilentlyContinue
}
catch {
    Write-Host "  [FAIL] Font Awesome ZIP — $($_.Exception.Message)" -ForegroundColor Red
    $script:failed++
}

# ─── Logo Alternative Holdings ────────────────────────────────────────────────
Write-Host ""
Write-Host "Logo Alternative Holdings" -ForegroundColor Yellow
Get-Asset `
    "https://static.wixstatic.com/media/f4dbb0_3ac6f73accf3460fb87aff9d1d8ac0a0~mv2.jpg/v1/fill/w_460,h_386,al_c,q_80,usm_0.66_1.00_0.01,enc_avif,quality_auto/AH%20LOGO%20(w_o%20font).jpg" `
    "$Content\img\ah-logo.png" `
    "ah-logo.png"

# ─── Resumen ──────────────────────────────────────────────────────────────────
Write-Host ""
Write-Host "==================================================" -ForegroundColor Cyan
Write-Host "  Resultado: $downloaded OK   $failed fallidos" -ForegroundColor $(if ($failed -eq 0) { "Green" } else { "Yellow" })
Write-Host "==================================================" -ForegroundColor Cyan

if ($failed -gt 0) {
    Write-Host ""
    Write-Host "  Algunos archivos fallaron. Verifica tu conexion a internet" -ForegroundColor Yellow
    Write-Host "  y vuelve a ejecutar el script." -ForegroundColor Yellow
}
else {
    Write-Host ""
    Write-Host "  Todos los assets descargados correctamente." -ForegroundColor Green
    Write-Host "  Ya puedes compilar y ejecutar el proyecto en Visual Studio." -ForegroundColor Green
}
Write-Host ""
