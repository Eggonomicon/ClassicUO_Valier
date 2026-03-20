@echo off
setlocal

set "HERE=%~dp0"
set "SETTINGS=%HERE%Valier.settings.json"
set "TEMPLATE=%HERE%Valier.settings.template.json"
set "ENSURE=%HERE%Ensure-ValierSettings.ps1"

if not exist "%SETTINGS%" (
  if exist "%TEMPLATE%" (
    copy /y "%TEMPLATE%" "%SETTINGS%" >nul
    echo Created "%SETTINGS%"
  ) else (
    echo Missing template: "%TEMPLATE%"
    pause
    exit /b 1
  )
)

if exist "%ENSURE%" (
  powershell -ExecutionPolicy Bypass -File "%ENSURE%" -SettingsPath "%SETTINGS%" -TemplatePath "%TEMPLATE%"
  if errorlevel 1 (
    echo Settings validation failed.
    pause
    exit /b 1
  )
)

if exist "%HERE%ValierClassicUO.exe" (
  set "EXE=%HERE%ValierClassicUO.exe"
) else if exist "%HERE%ClassicUO.exe" (
  set "EXE=%HERE%ClassicUO.exe"
) else (
  echo Could not find ValierClassicUO.exe or ClassicUO.exe in:
  echo   %HERE%
  pause
  exit /b 1
)

start "" "%EXE%" settings "%SETTINGS%"
endlocal
