[CmdletBinding()]
param(
    [string]$RepoRoot = "."
)

$ErrorActionPreference = 'Stop'

$repo = (Resolve-Path $RepoRoot).Path
$target = Join-Path $repo 'src\ClassicUO.Client\Game\UI\Gumps\Login\LoginGump.cs'

if (-not (Test-Path $target)) {
    throw "Target file not found: $target"
}

$content = Get-Content $target -Raw
$original = $content

# Remove the unsupported TEXT_ALIGN_TYPE parameter from the custom password textbox ctor.
$content = [regex]::Replace(
    $content,
    'TEXT_ALIGN_TYPE\s+align\s*=\s*TEXT_ALIGN_TYPE\.TS_LEFT\s*\)\s*:\s*base\(\s*font\s*,\s*maxCharCount\s*,\s*maxWidth\s*,\s*isunicode\s*,\s*style\s*,\s*hue\s*,\s*align\s*\)',
    ') : base(font, maxCharCount, maxWidth, isunicode, style, hue)',
    [System.Text.RegularExpressions.RegexOptions]::Singleline
)

# Remove align argument from RenderedText.Create calls.
$content = [regex]::Replace(
    $content,
    '_rendererText\s*=\s*RenderedText\.Create\(\s*string\.Empty\s*,\s*hue\s*,\s*font\s*,\s*isunicode\s*,\s*style\s*,\s*align\s*,\s*maxWidth\s*\)\s*;',
    '_rendererText = RenderedText.Create(string.Empty, hue, font, isunicode, style, maxWidth: maxWidth);',
    [System.Text.RegularExpressions.RegexOptions]::Singleline
)

$content = [regex]::Replace(
    $content,
    '_rendererCaret\s*=\s*RenderedText\.Create\(\s*"_"\s*,\s*hue\s*,\s*font\s*,\s*isunicode\s*,\s*\(style\s*&\s*FontStyle\.BlackBorder\)\s*!=\s*0\s*\?\s*FontStyle\.BlackBorder\s*:\s*FontStyle\.None\s*,\s*align\s*\)\s*;',
    '_rendererCaret = RenderedText.Create("_", hue, font, isunicode, (style & FontStyle.BlackBorder) != 0 ? FontStyle.BlackBorder : FontStyle.None);',
    [System.Text.RegularExpressions.RegexOptions]::Singleline
)

if ($content -eq $original) {
    Write-Host 'No changes were needed. LoginGump.cs already appears fixed.'
    exit 0
}

Set-Content -Path $target -Value $content -Encoding UTF8
Write-Host 'Patched LoginGump.cs successfully:'
Write-Host " - removed TEXT_ALIGN_TYPE dependency"
Write-Host " - updated RenderedText.Create calls"
