param(
    [string]$RepoRoot = "."
)

$ErrorActionPreference = "Stop"

$repoRoot = (Resolve-Path $RepoRoot).Path
$overlayRoot = (Resolve-Path (Join-Path $PSScriptRoot "..\overlay")).Path
$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"
$backupRoot = Join-Path $repoRoot "patch\backup-valier-login-phase2\$timestamp"

New-Item -ItemType Directory -Force -Path $backupRoot | Out-Null

function Backup-File {
    param([string]$Path)

    if (Test-Path $Path) {
        $relative = $Path.Substring($repoRoot.Length).TrimStart('\','/')
        $dest = Join-Path $backupRoot $relative
        $destDir = Split-Path -Parent $dest
        New-Item -ItemType Directory -Force -Path $destDir | Out-Null
        Copy-Item $Path $dest -Force
    }
}

function Copy-OverlayFile {
    param(
        [string]$RelativePath
    )

    $source = Join-Path $overlayRoot $RelativePath
    $target = Join-Path $repoRoot $RelativePath

    if (-not (Test-Path $source)) {
        throw "Missing overlay file: $source"
    }

    Backup-File -Path $target

    $targetDir = Split-Path -Parent $target
    New-Item -ItemType Directory -Force -Path $targetDir | Out-Null
    Copy-Item $source $target -Force
}

$overlayFiles = @(
    "src\ClassicUO.Client\Game\UI\Gumps\Login\LoginBackground.cs",
    "src\ClassicUO.Client\Game\UI\Gumps\Login\LoginGump.cs",
    "src\ClassicUO.Client\Game\UI\Gumps\Login\ServerSelectionGump.cs",
    "scripts\Sync-ValierClientBranding.ps1"
)

foreach ($file in $overlayFiles) {
    Copy-OverlayFile -RelativePath $file
}

# Seed branding defaults if the repo branding folder is missing the files.
$brandingDir = Join-Path $repoRoot "branding"
New-Item -ItemType Directory -Force -Path $brandingDir | Out-Null

$defaultBranding = @{
    "ValierGameBackground.png" = Join-Path $overlayRoot "branding\ValierGameBackground.png"
    "ValierLogo.png"           = Join-Path $overlayRoot "branding\ValierLogo.png"
}

foreach ($entry in $defaultBranding.GetEnumerator()) {
    $target = Join-Path $brandingDir $entry.Key

    if (-not (Test-Path $target) -and (Test-Path $entry.Value)) {
        Copy-Item $entry.Value $target -Force
    }
}

# Patch GameController window titles.
$gameControllerPath = Join-Path $repoRoot "src\ClassicUO.Client\GameController.cs"
if (-not (Test-Path $gameControllerPath)) {
    throw "Missing file: $gameControllerPath"
}

Backup-File -Path $gameControllerPath
$gameController = Get-Content $gameControllerPath -Raw
$gameController = $gameController.Replace('Window.Title = $"ClassicUO - {CUOEnviroment.Version}";', 'Window.Title = $"ValierUO - {CUOEnviroment.Version}";')
$gameController = $gameController.Replace('Window.Title = $"ClassicUO [dev] - {CUOEnviroment.Version}";', 'Window.Title = $"ValierUO [dev] - {CUOEnviroment.Version}";')
$gameController = $gameController.Replace('Window.Title = $"ClassicUO - {CUOEnviroment.Version}";', 'Window.Title = $"ValierUO - {CUOEnviroment.Version}";')
$gameController = $gameController.Replace('Window.Title = $"{title} - ClassicUO [dev] - {CUOEnviroment.Version}";', 'Window.Title = $"{title} - ValierUO [dev] - {CUOEnviroment.Version}";')
$gameController = $gameController.Replace('Window.Title = $"{title} - ClassicUO - {CUOEnviroment.Version}";', 'Window.Title = $"{title} - ValierUO - {CUOEnviroment.Version}";')
Set-Content $gameControllerPath $gameController -Encoding utf8

# Patch LoginScene so the minimum size stays but the window remains resizable.
$loginScenePath = Join-Path $repoRoot "src\ClassicUO.Client\Game\Scenes\LoginScene.cs"
if (-not (Test-Path $loginScenePath)) {
    throw "Missing file: $loginScenePath"
}

Backup-File -Path $loginScenePath
$loginScene = Get-Content $loginScenePath -Raw

$loginScene = $loginScene.Replace('Client.Game.Window.AllowUserResizing = false;', 'Client.Game.Window.AllowUserResizing = true;')

$oldBlock = @"
            int width = Client.Game.ScaleWithDpi(640);
            int height = Client.Game.ScaleWithDpi(480);
            SDL.SDL_SetWindowMinimumSize(Client.Game.Window.Handle, width, height);
            Client.Game.SetWindowSize(width, height);
"@

$newBlock = @"
            int width = Client.Game.ScaleWithDpi(640);
            int height = Client.Game.ScaleWithDpi(480);
            SDL.SDL_SetWindowMinimumSize(Client.Game.Window.Handle, width, height);

            if (Client.Game.Window.ClientBounds.Width < width || Client.Game.Window.ClientBounds.Height < height)
            {
                Client.Game.SetWindowSize
                (
                    System.Math.Max(width, Client.Game.Window.ClientBounds.Width),
                    System.Math.Max(height, Client.Game.Window.ClientBounds.Height)
                );
            }
"@

if ($loginScene.Contains($oldBlock)) {
    $loginScene = $loginScene.Replace($oldBlock, $newBlock)
}

Set-Content $loginScenePath $loginScene -Encoding utf8

# Sync the branding assets into the embedded resource filenames.
& (Join-Path $repoRoot "scripts\Sync-ValierClientBranding.ps1") -RepoRoot $repoRoot

Write-Host "Applied Valier responsive login phase 2 patch:"
Write-Host " - ValierUO title text"
Write-Host " - responsive login panel"
Write-Host " - larger shard selection panel"
Write-Host " - login background now uses embedded Valier branding"
Write-Host " - repo backups stored in $backupRoot"
