param(
    [string]$RepoRoot = "."
)

$RepoRoot = (Resolve-Path $RepoRoot).Path

$targets = @(
    (Join-Path $RepoRoot 'src\ClassicUO.Client\Game\UI\Gumps\Login\LoginGump.cs'),
    (Join-Path $RepoRoot 'src\ClassicUO.Client\Game\UI\Gumps\Login\ServerSelectionGump.cs')
)

$utf8NoBom = New-Object System.Text.UTF8Encoding($false)
$changed = $false

foreach ($file in $targets) {
    if (-not (Test-Path $file)) {
        throw "Missing target file: $file"
    }

    $content = [System.IO.File]::ReadAllText($file)

    if ($content -match '(?m)^using\s+System;\s*$') {
        Write-Host "Already fixed: $file"
        continue
    }

    if ($content -match '^(\uFEFF)?//.*?(\r?\n)+using\s+') {
        $content = [regex]::Replace(
            $content,
            '^(\uFEFF)?((?://.*\r?\n)+)(using\s+)',
            '$1$2using System;' + "`r`n" + '$3',
            [System.Text.RegularExpressions.RegexOptions]::Multiline
        )
    }
    elseif ($content -match '^(\uFEFF)?using\s+') {
        $content = [regex]::Replace(
            $content,
            '^(\uFEFF)?(using\s+)',
            '$1using System;' + "`r`n" + '$2',
            [System.Text.RegularExpressions.RegexOptions]::Multiline
        )
    }
    else {
        $content = 'using System;' + "`r`n" + $content
    }

    [System.IO.File]::WriteAllText($file, $content, $utf8NoBom)
    Write-Host "Patched: $file"
    $changed = $true
}

if (-not $changed) {
    Write-Host 'No changes were needed. Files already appear fixed.'
}
