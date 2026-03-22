# Valier Patcher Phase Plan

## Goal

Create a Windows-first Valier patcher that updates the client before launch and gives you a real branded launcher instead of a BAT file.

## Deliverables for the next phase

1. `ValierLauncher.exe`
2. `latest.json` manifest endpoint support
3. delta/full file download logic
4. verify/repair mode
5. launch profiles for Test / Production shards
6. MOTD / patch notes / announcements panel

## Suggested patch flow

1. launcher starts
2. reads local installed manifest
3. downloads remote manifest
4. compares file hashes
5. downloads changed files
6. verifies hashes after download
7. writes installed manifest
8. launches `ValierClassicUO.exe`

## Suggested manifest model

- version
- channel
- published_utc
- bootstrap executable name
- launch arguments
- required files list with:
  - relative path
  - sha256
  - size
  - download URL

## Hosting options

- GitHub Releases assets
- simple HTTPS web host
- CDN in front of static files

GitHub Releases is the easiest first host because your build pipeline can already publish zip artifacts there.

