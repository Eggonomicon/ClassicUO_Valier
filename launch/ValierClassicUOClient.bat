@echo off
setlocal enabledelayedexpansion

REM Valier ClassicUO Client launcher (Windows)
REM First run will create Valier.settings.json from Valier.settings.template.json
REM You MUST set your UO data path (uopath) before the client can run.

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

REM Example with overrides:
REM "%EXE%" settings "%SETTINGS%" uopath "C:\Ultima Online" ip shard.example.com port 2593
start "" "%EXE%" settings "%SETTINGS%"
endlocal
