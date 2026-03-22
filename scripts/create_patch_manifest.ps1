[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [string]$InputDirectory,
    [Parameter(Mandatory = $true)]
    [string]$OutputFile,
    [string]$Version = '',
    [string]$Channel = 'stable',
    [string]$BootstrapExe = 'ValierClassicUO.exe'
)

$ErrorActionPreference = 'Stop'

if (-not (Test-Path $InputDirectory)) {
    throw "Input directory not found: $InputDirectory"
}

$root = (Resolve-Path $InputDirectory).Path
$files = Get-ChildItem -Path $root -Recurse -File | Sort-Object FullName

if ([string]::IsNullOrWhiteSpace($Version)) {
    $Version = (Get-Date).ToString('yyyy.MM.dd.HHmmss')
}

$entries = foreach ($file in $files) {
    $hash = Get-FileHash -Path $file.FullName -Algorithm SHA256
    $relativePath = $file.FullName.Substring($root.Length)
    $relativePath = $relativePath -replace '^[\\/]+', ''
    $relativePath = $relativePath -replace '\\', '/'

    [pscustomobject]@{
        path   = $relativePath
        size   = $file.Length
        sha256 = $hash.Hash.ToLowerInvariant()
    }
}

$manifest = [pscustomobject]@{
    version       = $Version
    channel       = $Channel
    published_utc = (Get-Date).ToUniversalTime().ToString('o')
    bootstrap_exe = $BootstrapExe
    files         = $entries
}

New-Item -ItemType Directory -Force -Path (Split-Path -Parent $OutputFile) | Out-Null
$manifest | ConvertTo-Json -Depth 8 | Set-Content -Path $OutputFile -Encoding UTF8
Write-Host "Wrote patch manifest: $OutputFile"
