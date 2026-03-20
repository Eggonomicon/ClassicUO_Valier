@echo off
setlocal enabledelayedexpansion

set HERE=%~dp0
set SETTINGS=%HERE%Valier.settings.json
set TEMPLATE=%HERE%Valier.settings.template.json

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

if exist "%HERE%ValierClassicUO.exe" (
  set EXE=%HERE%ValierClassicUO.exe
) else if exist "%HERE%ClassicUO.exe" (
  set EXE=%HERE%ClassicUO.exe
) else (
  echo Could not find ValierClassicUO.exe or ClassicUO.exe in:
  echo   %HERE%
  pause
  exit /b 1
)

start "" "%EXE%" settings "%SETTINGS%"
endlocal
