param(
    [string]$RepoRoot = "."
)

$ErrorActionPreference = 'Stop'
$RepoRoot = (Resolve-Path $RepoRoot).Path
$utf8NoBom = New-Object System.Text.UTF8Encoding($false)

$targets = @(
    (Join-Path $RepoRoot 'src\ClassicUO.Client\Game\UI\Controls\ImageButtonControl.cs'),
    (Join-Path $RepoRoot 'src\ClassicUO.Client\Game\UI\Controls\TextureImageControl.cs')
)

foreach ($file in $targets) {
    if (-not (Test-Path $file)) {
        throw "Missing file: $file"
    }

    $content = [System.IO.File]::ReadAllText($file)
    $updated = $content

    $updated = $updated -replace 'AddToRenderLists\(\s*RenderLists\s+renderLists\s*,', 'AddToRenderLists(ClassicUO.Game.Scenes.RenderLists renderLists,'
    $updated = $updated -replace 'override\s+bool\s+AddToRenderLists\(\s*ClassicUO\.Renderer\.RenderLists\s+renderLists\s*,', 'override bool AddToRenderLists(ClassicUO.Game.Scenes.RenderLists renderLists,'
    $updated = $updated -replace 'override\s+bool\s+AddToRenderLists\(\s*ClassicUO\.Game\.Managers\.RenderLists\s+renderLists\s*,', 'override bool AddToRenderLists(ClassicUO.Game.Scenes.RenderLists renderLists,'

    if ($updated -ne $content) {
        [System.IO.File]::WriteAllText($file, $updated, $utf8NoBom)
        Write-Host "Patched: $file"
    } else {
        Write-Host "No changes needed: $file"
    }
}

Write-Host 'Valier Phase 3d direct control type fix complete.'
