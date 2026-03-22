
param(
    [string]$RepoRoot = "."
)

$ErrorActionPreference = "Stop"
$RepoRoot = (Resolve-Path $RepoRoot).Path

$utf8NoBom = New-Object System.Text.UTF8Encoding($false)

function Ensure-Using {
    param(
        [string]$FilePath,
        [string]$UsingLine
    )

    if (!(Test-Path $FilePath)) {
        Write-Warning "Missing file: $FilePath"
        return
    }

    $content = [System.IO.File]::ReadAllText($FilePath)

    if ($content -match ("(?m)^using\s+" + [regex]::Escape(($UsingLine -replace '^using\s+','' -replace ';$','')) + ";\s*$")) {
        Write-Host "Already present: $UsingLine in $FilePath"
        return
    }

    if ($content -match '^(//.*\r?\n)+using\s+') {
        $content = [regex]::Replace(
            $content,
            '^((?://.*\r?\n)+)(using\s+)',
            ('$1' + $UsingLine + "`r`n" + '$2'),
            [System.Text.RegularExpressions.RegexOptions]::Multiline
        )
    }
    else {
        $content = $UsingLine + "`r`n" + $content
    }

    [System.IO.File]::WriteAllText($FilePath, $content, $utf8NoBom)
    Write-Host "Patched using in $FilePath"
}

$controlFiles = @(
    (Join-Path $RepoRoot 'src\ClassicUO.Client\Game\UI\Controls\ImageButtonControl.cs'),
    (Join-Path $RepoRoot 'src\ClassicUO.Client\Game\UI\Controls\TextureImageControl.cs')
)

foreach ($file in $controlFiles) {
    Ensure-Using -FilePath $file -UsingLine 'using ClassicUO.Renderer;'
}

Write-Host "Valier Phase 3a compile/resource fix complete."
