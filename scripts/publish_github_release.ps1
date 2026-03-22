[CmdletBinding()]
param(
    [string]$RepoRoot = '',
    [Parameter(Mandatory = $true)]
    [string]$Tag,
    [string]$Title = '',
    [switch]$PreRelease
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

if (-not (Get-Command gh -ErrorAction SilentlyContinue)) {
    throw 'GitHub CLI (gh) is required to publish a release from PowerShell.'
}

$zip = Join-Path $RepoRoot 'release/ValierClassicUOClient-win-x64.zip'
$manifest = Join-Path $RepoRoot 'release/patch-manifest.json'

foreach ($required in @($zip, $manifest)) {
    if (-not (Test-Path $required)) {
        throw "Missing release artifact: $required"
    }
}

if ([string]::IsNullOrWhiteSpace($Title)) {
    $Title = $Tag
}

$preArg = @()
if ($PreRelease) { $preArg += '--prerelease' }

& gh release create $Tag $zip $manifest --title $Title --notes "Valier ClassicUO automated release." @preArg
