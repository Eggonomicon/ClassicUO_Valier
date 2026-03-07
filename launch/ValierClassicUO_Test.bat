@echo off
setlocal
REM Valier ClassicUO - TEST
REM Change port in Valier.Test.settings.json if your test server uses a different port.
set HERE=%~dp0
set TEMPLATE=%HERE%Valier.Test.settings.template.json
set SETTINGS=%HERE%Valier.Test.settings.json

if not exist "%SETTINGS%" copy /y "%TEMPLATE%" "%SETTINGS%" >nul

if exist "%HERE%ValierClassicUO.exe" (
  "%HERE%ValierClassicUO.exe" -settings "%SETTINGS%"
) else (
  "%HERE%ClassicUO.exe" -settings "%SETTINGS%"
)
endlocal
