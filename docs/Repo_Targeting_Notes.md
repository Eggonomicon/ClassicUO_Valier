# Repo Targeting Notes

## Verified repo structure
- `branding/`
- `launch/`
- `settings/`
- `src/`
- `scripts/`

## Verified current build flow
The repo README says the wrapper:
- copies branding into `src/ClassicUO.Client/`
- writes `src/ClassicUO.Client/Directory.Build.props`
- runs the ClassicUO build script
- copies the build output into a release folder for distribution

## Proposed place for Valier UI integration
Because branding is already copied into `src/ClassicUO.Client/`, the clean next step is to keep Valier UI assets under a dedicated subpath during the code pass, for example:

`src/ClassicUO.Client/Resources/ValierUI/`

Suggested subfolders:
- `Textures/`
- `Panels/`
- `Buttons/`
- `Frames/`
- `Gumps/`

This folder structure is a proposed target for the client chat; it is not claimed as already existing in the repo.
