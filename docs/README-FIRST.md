# Valier ClassicUO Port Kit — Phase 1

This package is a drop-in **overlay kit** for your repo:

- Repo: `Eggonomicon/ClassicUO_Valier`
- Upstream base: `ClassicUO/ClassicUO`

It is built around the repo shape that already exists in your fork:

- `branding/`
- `launch/ValierClassicUOClient.bat`
- `settings/Valier.settings.template.json`
- `scripts/build-naot.sh`

## What this kit gives you now

1. A Windows build script that:
   - patches the bootstrap EXE name to `ValierClassicUO.exe`
   - applies icon/product branding
   - stages logo/background assets for later UI wiring
   - runs the repo's existing `build-naot.sh`
   - creates a release folder and zip
   - emits a patch manifest for a future patcher

2. A GitHub Actions workflow that can:
   - build on Windows
   - publish the packaged zip as an artifact
   - optionally publish a GitHub release on tag push

3. A future-proof patch-manifest seed so the patcher phase starts from a stable file layout.

## Important limits

- This kit **does not yet implement** the full custom patcher/launcher UI.
- It also **does not yet wire** your custom background/logo into the actual in-client login scene, because that requires targeted source edits in the UI layer.
- It **does** automate branding of the executable, build, packaging, and release output.

## Assumption I made

You wrote "custom cliq files". I treated that as **custom cliloc/cliloc-style text override files** for shard text/context overrides.
If you meant a different file format, update the docs in `docs/` before Phase 2.

## How to apply

1. Back up your fork.
2. Copy the contents of `overlay/` on top of your repo root.
3. Confirm your branding files exist in `branding/`:
   - `ValierIcon.ico`
   - `ValierLogo.png`
   - `ValierGameBackground.png`
4. Confirm `settings/Valier.settings.template.json` has your shard IP/port/client version.
5. Run:
   - `powershell -ExecutionPolicy Bypass -File .\scripts\build_release_windows.ps1`

## Output

- `release/ValierClassicUOClient/`
- `release/ValierClassicUOClient-win-x64.zip`
- `release/patch-manifest.json`

