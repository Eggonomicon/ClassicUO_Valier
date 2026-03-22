param(
    [Parameter(Mandatory = $true)]
    [string]$SettingsPath,

    [string]$TemplatePath = "",

    [string]$DefaultClientVersion = "7.0.0.0"
)

$ErrorActionPreference = 'Stop'

function Read-JsonFile {
    param([string]$Path)

    if (-not (Test-Path -LiteralPath $Path)) {
        return $null
    }

    $raw = Get-Content -LiteralPath $Path -Raw -Encoding UTF8
    if ([string]::IsNullOrWhiteSpace($raw)) {
        return $null
    }

    return ($raw | ConvertFrom-Json)
}

function Save-JsonFile {
    param(
        [string]$Path,
        [Parameter(Mandatory = $true)]
        $Object
    )

    $parent = Split-Path -Parent $Path
    if (-not [string]::IsNullOrWhiteSpace($parent) -and -not (Test-Path -LiteralPath $parent)) {
        New-Item -ItemType Directory -Path $parent -Force | Out-Null
    }

    $Object | ConvertTo-Json -Depth 10 | Set-Content -LiteralPath $Path -Encoding UTF8
}

function Ensure-Property {
    param(
        [Parameter(Mandatory = $true)]$Object,
        [Parameter(Mandatory = $true)][string]$Name,
        $DefaultValue
    )

    if ($null -eq $Object.PSObject.Properties[$Name]) {
        $Object | Add-Member -NotePropertyName $Name -NotePropertyValue $DefaultValue
    }
}

if (-not (Test-Path -LiteralPath $SettingsPath)) {
    if (-not [string]::IsNullOrWhiteSpace($TemplatePath) -and (Test-Path -LiteralPath $TemplatePath)) {
        Copy-Item -LiteralPath $TemplatePath -Destination $SettingsPath -Force
    } else {
        '{}' | Set-Content -LiteralPath $SettingsPath -Encoding UTF8
    }
}

$settings = Read-JsonFile -Path $SettingsPath
if ($null -eq $settings) {
    $settings = [pscustomobject]@{}
}

Ensure-Property -Object $settings -Name 'username' -DefaultValue ''
Ensure-Property -Object $settings -Name 'password' -DefaultValue ''
Ensure-Property -Object $settings -Name 'ip' -DefaultValue 'YOUR_SERVER_IP_OR_HOSTNAME'
Ensure-Property -Object $settings -Name 'port' -DefaultValue 2593
Ensure-Property -Object $settings -Name 'ultimaonlinedirectory' -DefaultValue ''
Ensure-Property -Object $settings -Name 'clientversion' -DefaultValue $DefaultClientVersion
Ensure-Property -Object $settings -Name 'saveaccount' -DefaultValue $false
Ensure-Property -Object $settings -Name 'autologin' -DefaultValue $false
Ensure-Property -Object $settings -Name 'reconnect' -DefaultValue $true
Ensure-Property -Object $settings -Name 'reconnect_time' -DefaultValue 3
Ensure-Property -Object $settings -Name 'plugins' -DefaultValue @()

$uoPath = [string]$settings.ultimaonlinedirectory
while ([string]::IsNullOrWhiteSpace($uoPath) -or -not (Test-Path -LiteralPath $uoPath)) {
    Write-Host ''
    Write-Host 'Valier setup needs your Ultima Online Classic folder.' -ForegroundColor Cyan
    Write-Host 'Example: E:\Electronic Arts\Ultima Online Classic' -ForegroundColor DarkGray
    $entered = Read-Host 'Enter Ultima Online folder path'

    if (-not [string]::IsNullOrWhiteSpace($entered) -and (Test-Path -LiteralPath $entered)) {
        $uoPath = $entered
        break
    }

    Write-Host 'That path was blank or does not exist. Please try again.' -ForegroundColor Yellow
}

$clientVersion = [string]$settings.clientversion
while ([string]::IsNullOrWhiteSpace($clientVersion)) {
    $enteredVersion = Read-Host "Enter clientversion or press Enter for $DefaultClientVersion"
    if ([string]::IsNullOrWhiteSpace($enteredVersion)) {
        $clientVersion = $DefaultClientVersion
    } else {
        $clientVersion = $enteredVersion.Trim()
    }
}

$settings.ultimaonlinedirectory = $uoPath
$settings.clientversion = $clientVersion

Save-JsonFile -Path $SettingsPath -Object $settings

Write-Host "Saved settings: $SettingsPath" -ForegroundColor Green
Write-Host "ultimaonlinedirectory = $($settings.ultimaonlinedirectory)" -ForegroundColor DarkGray
Write-Host "clientversion         = $($settings.clientversion)" -ForegroundColor DarkGray
