param(
    [string]$RepoRoot = (Get-Location).Path
)

$target = Join-Path $RepoRoot 'src\ClassicUO.Client\Game\UI\Gumps\Login\LoginGump.cs'

if (!(Test-Path $target)) {
    throw "Missing target file: $target"
}

$utf8NoBom = New-Object System.Text.UTF8Encoding($false)
$content = [System.IO.File]::ReadAllText($target)
$original = $content

# Fully qualify symbols so the file does not depend on fragile using directives.
$content = $content -replace '(?<![\w\.])Crypter\.', 'ClassicUO.IO.Crypter.'
$content = $content -replace '(?<![\w\.])UIManager\.', 'ClassicUO.Game.Managers.UIManager.'

if ($content -ne $original) {
    [System.IO.File]::WriteAllText($target, $content, $utf8NoBom)
    Write-Host "Patched: $target"
}
else {
    Write-Host "No changes needed: $target"
}

Write-Host "Valier Phase 3e LoginGump symbol fix complete."
