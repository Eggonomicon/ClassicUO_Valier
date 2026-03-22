[CmdletBinding()]
param(
    [string]$RepoRoot = '',
    [string]$ProductName = 'Valier ClassicUO Client',
    [string]$ExeName = 'ValierClassicUO',
    [string]$CompanyName = 'ValierUO',
    [string]$Description = 'Valier ClassicUO Client',
    [string]$Version = '1.0.0.0'
)

$ErrorActionPreference = 'Stop'

# Resolve repo root safely after param binding.
if ([string]::IsNullOrWhiteSpace($RepoRoot)) {
    $scriptRoot = if ($PSScriptRoot) { $PSScriptRoot } else { Split-Path -Parent $MyInvocation.MyCommand.Path }
    if ([string]::IsNullOrWhiteSpace($scriptRoot)) {
        throw 'Could not resolve script root.'
    }
    $RepoRoot = Split-Path -Parent $scriptRoot
}

function Set-XmlNodeValue {
    param(
        [xml]$Xml,
        [System.Xml.XmlNode]$Parent,
        [string]$Name,
        [string]$Value
    )

    $node = $Parent.SelectSingleNode($Name)
    if (-not $node) {
        $node = $Xml.CreateElement($Name)
        [void]$Parent.AppendChild($node)
    }
    $node.InnerText = $Value
}

$brandingDir = Join-Path $RepoRoot 'branding'
$bootstrapProject = Join-Path $RepoRoot 'src/ClassicUO.Bootstrap/src/ClassicUO.Bootstrap.csproj'
$clientProject = Join-Path $RepoRoot 'src/ClassicUO.Client/ClassicUO.Client.csproj'
$bootstrapIconTarget = Join-Path $RepoRoot 'src/ClassicUO.Bootstrap/src/ValierIcon.ico'
$clientIconTarget = Join-Path $RepoRoot 'src/ClassicUO.Client/ValierIcon.ico'
$clientBrandingDir = Join-Path $RepoRoot 'src/ClassicUO.Client/Assets/Branding'
$iconSource = Join-Path $brandingDir 'ValierIcon.ico'
$logoSource = Join-Path $brandingDir 'ValierLogo.png'
$backgroundSource = Join-Path $brandingDir 'ValierGameBackground.png'

foreach ($required in @($bootstrapProject, $clientProject)) {
    if (-not (Test-Path $required)) {
        throw "Missing required file: $required"
    }
}

$hasCustomIcon = Test-Path $iconSource

New-Item -ItemType Directory -Force -Path (Split-Path -Parent $bootstrapIconTarget) | Out-Null
New-Item -ItemType Directory -Force -Path (Split-Path -Parent $clientIconTarget) | Out-Null
New-Item -ItemType Directory -Force -Path $clientBrandingDir | Out-Null

if ($hasCustomIcon) {
    Copy-Item -Force $iconSource $bootstrapIconTarget
    Copy-Item -Force $iconSource $clientIconTarget
} else {
    Write-Warning "branding/ValierIcon.ico not found. Using the repository's existing ClassicUO icon."
}
if (Test-Path $logoSource) { Copy-Item -Force $logoSource (Join-Path $clientBrandingDir 'ValierLogo.png') }
if (Test-Path $backgroundSource) { Copy-Item -Force $backgroundSource (Join-Path $clientBrandingDir 'ValierGameBackground.png') }

# Patch bootstrap project: rename the EXE and set icon + metadata.
[xml]$bootstrapXml = Get-Content -Raw -Path $bootstrapProject
$bootstrapPg = $bootstrapXml.Project.PropertyGroup | Select-Object -First 1
if (-not $bootstrapPg) { throw "Could not locate PropertyGroup in $bootstrapProject" }
Set-XmlNodeValue -Xml $bootstrapXml -Parent $bootstrapPg -Name 'AssemblyName' -Value $ExeName
if ($hasCustomIcon) {
    Set-XmlNodeValue -Xml $bootstrapXml -Parent $bootstrapPg -Name 'ApplicationIcon' -Value 'ValierIcon.ico'
}
Set-XmlNodeValue -Xml $bootstrapXml -Parent $bootstrapPg -Name 'Product' -Value $ProductName
Set-XmlNodeValue -Xml $bootstrapXml -Parent $bootstrapPg -Name 'Company' -Value $CompanyName
Set-XmlNodeValue -Xml $bootstrapXml -Parent $bootstrapPg -Name 'Description' -Value $Description
Set-XmlNodeValue -Xml $bootstrapXml -Parent $bootstrapPg -Name 'FileVersion' -Value $Version
Set-XmlNodeValue -Xml $bootstrapXml -Parent $bootstrapPg -Name 'AssemblyVersion' -Value $Version
$bootstrapXml.Save($bootstrapProject)

# Patch client project: keep AssemblyName as cuo so bootstrap loading stays compatible.
[xml]$clientXml = Get-Content -Raw -Path $clientProject
$clientPg = $clientXml.Project.PropertyGroup | Select-Object -First 1
if (-not $clientPg) { throw "Could not locate PropertyGroup in $clientProject" }
if ($hasCustomIcon) {
    Set-XmlNodeValue -Xml $clientXml -Parent $clientPg -Name 'ApplicationIcon' -Value 'ValierIcon.ico'
}
Set-XmlNodeValue -Xml $clientXml -Parent $clientPg -Name 'Product' -Value $ProductName
Set-XmlNodeValue -Xml $clientXml -Parent $clientPg -Name 'Company' -Value $CompanyName
Set-XmlNodeValue -Xml $clientXml -Parent $clientPg -Name 'Description' -Value $Description
Set-XmlNodeValue -Xml $clientXml -Parent $clientPg -Name 'FileVersion' -Value $Version
Set-XmlNodeValue -Xml $clientXml -Parent $clientPg -Name 'AssemblyVersion' -Value $Version
$clientXml.Save($clientProject)

$brandReadme = @"
Valier branding assets staged here by patch_bootstrap_branding.ps1.

These files are ready for UI-layer wiring in a later phase:
- ValierLogo.png
- ValierGameBackground.png

This script intentionally does NOT claim that the current ClassicUO UI already consumes them.
"@
Set-Content -Path (Join-Path $clientBrandingDir 'README.txt') -Value $brandReadme -Encoding UTF8

Write-Host "Branding patch complete."
if ($hasCustomIcon) {
    Write-Host "Custom icon applied: $iconSource"
} else {
    Write-Host "Custom icon not present; default repository icon retained."
}
Write-Host "Bootstrap EXE target: $ExeName.exe"
Write-Host "Client assembly name preserved as cuo for compatibility."
