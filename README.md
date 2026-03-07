# Valier ClassicUO Client (Windows-only) — ValierUO

This is a **Windows-only** build + branding wrapper around ClassicUO.

It produces a branded client named:

**ValierClassicUO.exe** (Product: *Valier ClassicUO Client*)

ClassicUO does **not** ship Ultima Online art/sound assets; users must legally obtain UO data files. (ClassicUO README)

## Quick start (Windows)

1) Put your branding files in `branding/`:
   - `ValierIcon.ico`
   - `ValierLogo.png`
   - `ValierGameBackground.png`

2) Set your shard in `settings/Valier.settings.template.json`:
   - `ip`, `port`, `clientversion`

3) Build:
```powershell
cd .\scripts
.\build_release_windows.ps1
```

Output:
- `release/ValierClassicUOClient/` (zip this folder to distribute)

## What the scripts do
- Clone ClassicUO with submodules
- Copy branding onto `src/ClassicUO.Client/` (same filenames)
- Write `src/ClassicUO.Client/Directory.Build.props` to set:
  - AssemblyName = ValierClassicUO (so the EXE is ValierClassicUO.exe)
  - Product/Company/Description metadata
  - Win32Icon
- Run ClassicUO's `build-naot.sh` using Git Bash
- Copy ClassicUO `bin/dist` to `release/ValierClassicUOClient`
- Drop in `ValierClassicUOClient.bat` + settings template

## License
ClassicUO is BSD-licensed; include their LICENSE if distributing binaries.
