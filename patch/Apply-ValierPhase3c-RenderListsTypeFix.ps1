param(
    [string]$RepoRoot = "."
)

$ErrorActionPreference = "Stop"
$RepoRoot = (Resolve-Path $RepoRoot).Path
$utf8NoBom = New-Object System.Text.UTF8Encoding($false)

$files = @(
    (Join-Path $RepoRoot "src\ClassicUO.Client\Game\UI\Controls\ImageButtonControl.cs"),
    (Join-Path $RepoRoot "src\ClassicUO.Client\Game\UI\Controls\TextureImageControl.cs")
)

foreach ($file in $files) {
    if (-not (Test-Path $file)) {
        throw "Missing file: $file"
    }

    $content = [System.IO.File]::ReadAllText($file)
    $original = $content

    # Force the parameter type to resolve even if usings are wrong or duplicated.
    $content = [regex]::Replace(
        $content,
        '(?<![A-Za-z0-9_\.])RenderLists(?=\s+[A-Za-z_][A-Za-z0-9_]*\s*\))',
        'ClassicUO.Renderer.RenderLists'
    )

    # Also normalize any stray duplicate qualification.
    $content = $content -replace 'ClassicUO\.Renderer\.ClassicUO\.Renderer\.RenderLists', 'ClassicUO.Renderer.RenderLists'

    if ($content -ne $original) {
        [System.IO.File]::WriteAllText($file, $content, $utf8NoBom)
        Write-Host "Patched: $file"
    }
    else {
        Write-Host "No changes needed: $file"
    }
}

Write-Host "Valier Phase 3c RenderLists type fix complete."
