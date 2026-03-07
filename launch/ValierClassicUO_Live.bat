@echo off
setlocal
REM Valier ClassicUO - LIVE
set HERE=%~dp0
set TEMPLATE=%HERE%Valier.Live.settings.template.json
set SETTINGS=%HERE%Valier.Live.settings.json

if not exist "%SETTINGS%" copy /y "%TEMPLATE%" "%SETTINGS%" >nul

if exist "%HERE%ValierClassicUO.exe" (
  "%HERE%ValierClassicUO.exe" -settings "%SETTINGS%"
) else (
  "%HERE%ClassicUO.exe" -settings "%SETTINGS%"
)
endlocal
