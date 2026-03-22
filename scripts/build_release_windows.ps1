[CmdletBinding()]
param(
    [string]$RepoRoot = '',
    [string]$Version = '',
    [string]$Runtime = 'win-x64'
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

$requiredPaths = @(
    (Join-Path $RepoRoot 'ClassicUO.sln'),
    (Join-Path $RepoRoot 'scripts/patch_bootstrap_branding.ps1'),
    (Join-Path $RepoRoot 'scripts/package_release_windows.ps1'),
    (Join-Path $RepoRoot 'launch/ValierClassicUOClient.bat'),
    (Join-Path $RepoRoot 'settings/Valier.settings.template.json'),
    (Join-Path $RepoRoot 'src/ClassicUO.Bootstrap/src/ClassicUO.Bootstrap.csproj'),
    (Join-Path $RepoRoot 'src/ClassicUO.Client/ClassicUO.Client.csproj')
)

foreach ($path in $requiredPaths) {
    if (-not (Test-Path $path)) {
        throw "Missing required path: $path"
    }
}

$optionalBranding = @(
    (Join-Path $RepoRoot 'branding/ValierIcon.ico'),
    (Join-Path $RepoRoot 'branding/ValierLogo.png'),
    (Join-Path $RepoRoot 'branding/ValierGameBackground.png')
)

foreach ($asset in $optionalBranding) {
    if (-not (Test-Path $asset)) {
        Write-Warning "Optional branding asset not found: $asset"
    }
}

if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    throw '.NET SDK was not found in PATH.'
}

$bootstrapProject = Join-Path $RepoRoot 'src/ClassicUO.Bootstrap/src/ClassicUO.Bootstrap.csproj'
$clientProject = Join-Path $RepoRoot 'src/ClassicUO.Client/ClassicUO.Client.csproj'
$distDir = Join-Path $RepoRoot 'bin/dist'

Push-Location $RepoRoot
try {
    if (Get-Command git -ErrorAction SilentlyContinue) {
        git submodule update --init --recursive
    }

    $brandingVersion = if ($Version) { $Version } else { '1.0.0.0' }
    & (Join-Path $RepoRoot 'scripts/patch_bootstrap_branding.ps1') -RepoRoot $RepoRoot -Version $brandingVersion

    dotnet restore .\ClassicUO.sln
    if ($LASTEXITCODE -ne 0) {
        throw "dotnet restore failed with exit code $LASTEXITCODE"
    }

    if (Test-Path $distDir) {
        Remove-Item -Recurse -Force $distDir
    }
    New-Item -ItemType Directory -Force -Path $distDir | Out-Null

    Write-Host "Publishing bootstrap project to $distDir"
    dotnet publish $bootstrapProject -c Release -o $distDir
    if ($LASTEXITCODE -ne 0) {
        throw "Bootstrap publish failed with exit code $LASTEXITCODE"
    }

    Write-Host "Publishing client project to $distDir for runtime $Runtime"
    dotnet publish $clientProject -c Release -p:NativeLib=Shared -p:OutputType=Library -r $Runtime -o $distDir
    if ($LASTEXITCODE -ne 0) {
        throw "Client publish failed with exit code $LASTEXITCODE"
    }

    & (Join-Path $RepoRoot 'scripts/package_release_windows.ps1') -RepoRoot $RepoRoot -Version $Version
}
finally {
    Pop-Location
}
