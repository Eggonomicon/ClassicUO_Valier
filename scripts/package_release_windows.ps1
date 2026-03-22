[CmdletBinding()]
param(
    [string]$RepoRoot = '',
    [string]$ReleaseName = 'ValierClassicUOClient',
    [string]$ExeName = 'ValierClassicUO.exe',
    [string]$Version = ''
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

$distDir = Join-Path $RepoRoot 'bin/dist'
$releaseRoot = Join-Path $RepoRoot 'release'
$releaseDir = Join-Path $releaseRoot $ReleaseName
$zipPath = Join-Path $releaseRoot "$ReleaseName-win-x64.zip"
$launchBat = Join-Path $RepoRoot 'launch/ValierClassicUOClient.bat'
$settingsTemplate = Join-Path $RepoRoot 'settings/Valier.settings.template.json'
$licenseFile = Join-Path $RepoRoot 'LICENSE.md'
$manifestOut = Join-Path $releaseRoot 'patch-manifest.json'

if (-not (Test-Path $distDir)) { throw "Missing build output: $distDir" }
if (-not (Test-Path $launchBat)) { throw "Missing launcher BAT: $launchBat" }
if (-not (Test-Path $settingsTemplate)) { throw "Missing settings template: $settingsTemplate" }

if (Test-Path $releaseDir) { Remove-Item -Recurse -Force $releaseDir }
New-Item -ItemType Directory -Force -Path $releaseDir | Out-Null

Copy-Item -Recurse -Force (Join-Path $distDir '*') $releaseDir
Copy-Item -Force $launchBat $releaseDir
Copy-Item -Force $settingsTemplate (Join-Path $releaseDir 'Valier.settings.template.json')
if (Test-Path $licenseFile) { Copy-Item -Force $licenseFile $releaseDir }

$notes = @"
Valier ClassicUO packaged release.

Main executable:
- $ExeName

First-run helper:
- ValierClassicUOClient.bat

Settings template:
- Valier.settings.template.json
"@
Set-Content -Path (Join-Path $releaseDir 'README-FIRST.txt') -Value $notes -Encoding UTF8

$manifestScript = Join-Path $RepoRoot 'scripts/create_patch_manifest.ps1'
if (-not (Test-Path $manifestScript)) {
    throw "Missing manifest script: $manifestScript"
}

& $manifestScript -InputDirectory $releaseDir -OutputFile $manifestOut -Version $Version -BootstrapExe $ExeName

if (Test-Path $zipPath) { Remove-Item -Force $zipPath }
Compress-Archive -Path (Join-Path $releaseDir '*') -DestinationPath $zipPath -CompressionLevel Optimal

Write-Host "Packaged release directory: $releaseDir"
Write-Host "Packaged zip: $zipPath"
Write-Host "Patch manifest: $manifestOut"
