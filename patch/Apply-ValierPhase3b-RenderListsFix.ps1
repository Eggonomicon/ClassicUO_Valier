param(
    [string]$RepoRoot = "."
)

$ErrorActionPreference = "Stop"
$RepoRoot = (Resolve-Path $RepoRoot).Path

$files = @(
    (Join-Path $RepoRoot "src\ClassicUO.Client\Game\UI\Controls\ImageButtonControl.cs"),
    (Join-Path $RepoRoot "src\ClassicUO.Client\Game\UI\Controls\TextureImageControl.cs")
)

$utf8NoBom = New-Object System.Text.UTF8Encoding($false)

foreach ($file in $files) {
    if (-not (Test-Path $file)) {
        throw "Missing file: $file"
    }

    $content = [System.IO.File]::ReadAllText($file)

    if ($content -notmatch '(?m)^using\s+ClassicUO\.Game\.Managers;\s*$') {
        if ($content -match '^(//.*\r?\n)+using\s+') {
            $content = [regex]::Replace(
                $content,
                '^((?://.*\r?\n)+)(using\s+)',
                '$1using ClassicUO.Game.Managers;' + "`r`n" + '$2',
                [System.Text.RegularExpressions.RegexOptions]::Multiline
            )
        }
        elseif ($content -match '^(using\s+[^\r\n]+;\r?\n)+') {
            $content = [regex]::Replace(
                $content,
                '^((?:using\s+[^\r\n]+;\r?\n)+)',
                '$1using ClassicUO.Game.Managers;' + "`r`n",
                [System.Text.RegularExpressions.RegexOptions]::Multiline
            )
        }
        else {
            $content = 'using ClassicUO.Game.Managers;' + "`r`n" + $content
        }

        [System.IO.File]::WriteAllText($file, $content, $utf8NoBom)
        Write-Host "Patched: $file"
    }
    else {
        Write-Host "Already fixed: $file"
    }
}

Write-Host "Valier Phase 3b RenderLists fix complete."
