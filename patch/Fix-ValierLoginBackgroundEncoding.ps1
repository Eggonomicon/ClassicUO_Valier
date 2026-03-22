param(
    [Parameter(Mandatory = $true)]
    [string]$RepoRoot
)

$repoRootPath = (Resolve-Path $RepoRoot).Path
$source = Join-Path $PSScriptRoot "..\overlay\src\ClassicUO.Client\Game\UI\Gumps\Login\LoginBackground.cs"
$dest = Join-Path $repoRootPath "src\ClassicUO.Client\Game\UI\Gumps\Login\LoginBackground.cs"

if (-not (Test-Path $source)) {
    throw "Missing source file: $source"
}

$destDir = Split-Path -Parent $dest
if (-not (Test-Path $destDir)) {
    throw "Destination directory not found: $destDir"
}

Copy-Item $source $dest -Force
Write-Host "Replaced LoginBackground.cs with UTF-8 clean copy."
Write-Host "Now rebuild the client."
