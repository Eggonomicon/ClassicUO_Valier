param(
    [string]$RepoRoot = "."
)

$ErrorActionPreference = "Stop"

$repoRoot = (Resolve-Path $RepoRoot).Path
$brandingDir = Join-Path $repoRoot "branding"
$resourcesDir = Join-Path $repoRoot "src\ClassicUO.Client\Resources"

if (-not (Test-Path $resourcesDir)) {
    throw "Missing resources directory: $resourcesDir"
}

$backgroundCandidates = @(
    (Join-Path $brandingDir "ValierGameBackground.png"),
    (Join-Path $brandingDir "ValierLauncherBackground_2048w.png"),
    (Join-Path $brandingDir "ValierLauncherBackground_2048_squarepad.png")
)

$logoCandidates = @(
    (Join-Path $brandingDir "ValierLogo.png")
)

$backgroundSource = $backgroundCandidates | Where-Object { Test-Path $_ } | Select-Object -First 1
$logoSource = $logoCandidates | Where-Object { Test-Path $_ } | Select-Object -First 1

if ($backgroundSource) {
    Copy-Item $backgroundSource (Join-Path $resourcesDir "game-background.png") -Force
    Write-Host "Client background synced from $backgroundSource"
}
else {
    Write-Warning "No Valier background image found in branding\"
}

if ($logoSource) {
    Copy-Item $logoSource (Join-Path $resourcesDir "cuologo.png") -Force
    Write-Host "Client logo synced from $logoSource"
}
else {
    Write-Warning "No Valier logo image found in branding\"
}
