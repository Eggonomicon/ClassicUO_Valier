param(
    [string]$RepoRoot = "."
)

$root = (Resolve-Path $RepoRoot).Path
$runtimeRoot = Join-Path $root "Data\Client\ValierUI"

$folders = @(
    "Chat",
    "Inventory",
    "Spellbook",
    "Hotbar",
    "Buttons",
    "Frames",
    "Icons"
)

foreach ($folder in $folders) {
    $path = Join-Path $runtimeRoot $folder
    New-Item -ItemType Directory -Force -Path $path | Out-Null

    $readme = Join-Path $path "README.txt"
    if (-not (Test-Path $readme)) {
        Set-Content -Path $readme -Encoding UTF8 -Value "Place Valier UI runtime art for $folder here."
    }
}

Write-Host "Created runtime ValierUI folders under: $runtimeRoot"
