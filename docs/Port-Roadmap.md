# Valier ClassicUO Port Roadmap

## Phase 1 — Build, brand, package, upload

Delivered in this kit:

- Branded bootstrap EXE name: `ValierClassicUO.exe`
- Icon metadata patching
- Windows packaging automation
- GitHub Actions build/release workflow
- Release zip and patch manifest generation

## Phase 2 — Real Valier launcher / patcher shell

Target:

- replace the BAT launcher with a native Valier launcher
- server profile support
- channel/news/status panel
- patch manifest download
- delta/full update logic
- self-heal / verify files

Recommended stack:

- C# / WPF or WinUI 3 for Windows-only launcher
- JSON manifest over HTTPS
- SHA-256 verification
- optional CDN or direct web host

## Phase 3 — Branded login scene and shell elements

Target:

- custom Valier background
- custom Valier logo
- shard branding splash/login shell
- shard-specific links/help/discord buttons

## Phase 4 — Persistent global chat / journal docking

Target:

- persistent chat panel instead of transient top-line behavior
- dockable / resizable chat panel
- per-channel filtering
- saved panel state in settings

## Phase 5 — AI container gump framework

Target:

- custom container rendering pipeline
- container skin registry keyed by gump/item/container type
- AI-authored layouts exported into a consumable data format
- safe fallback to stock behavior

## Phase 6 — Cliloc / shard text override layer

Target:

- load Valier override entries from a shard-owned text pack
- layered fallback: Valier override -> stock cliloc -> literal fallback text
- versioned text packs bundled in patch manifest

## Phase 7 — Live patch delivery

Target:

- players launch Valier patcher
- patcher checks manifest endpoint
- downloads changed files only
- verifies hashes
- launches `ValierClassicUO.exe`

