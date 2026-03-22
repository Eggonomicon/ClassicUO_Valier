# ValierUO Chat Starter

PROJECT
ValierUO custom ClassicUO fork and patchable Windows-first client.

REPO
`X:\UO\Client\Dev\ClassicUO_Valier`

REPO LAYOUT
- `src`
- `branding`
- `scripts`
- `patch`
- `docs`

CURRENT STATUS
- Build and packaging pipeline are stable.
- Release folder and release zip are being produced correctly.
- Shared Valier UI scaffolding and runtime controls now compile.
- The old login/server-select experiment is still visually broken and should not be the main focus.
- Current focus is reusable shared UI plumbing and first usable Valier shells.

COMPLETED PHASES
1. Build/package stabilization
2. Login resize experiments
3. Broken branded login experiment
4. Shared UI pipeline scaffolding
5. Shared asset registry scaffolding
6. Runtime control scaffolding
7. Runtime art wiring helpers + first usable persistent chat shell + debug entry points

ACTIVE DIRECTION
Prioritize:
1. Persistent chat
2. Inventory/container shells
3. Spellbook shell
4. Hotbar shell
5. Login reboot later, rebuilt cleanly on top of shared components

DEBUG ENTRY POINTS AVAILABLE
- `ClassicUO.Game.UI.Valier.ValierDebugEntryPoints.OpenPersistentChat()`
- `ClassicUO.Game.UI.Valier.ValierDebugEntryPoints.OpenInventory()`
- `ClassicUO.Game.UI.Valier.ValierDebugEntryPoints.OpenContainer()`
- `ClassicUO.Game.UI.Valier.ValierDebugEntryPoints.OpenSpellbook()`
- `ClassicUO.Game.UI.Valier.ValierDebugEntryPoints.OpenHotbar()`

EXPECTED RUNTIME ART ROOT
`Data\Client\ValierUI\`

NEXT PHASE
- Add a temporary debug hotkey or command to open the new Valier gumps in-client
- Flesh out `ValierPersistentChatGump`
- Start real inventory/container art pass
- Add 9-slice or panel-frame rendering once panel art is finalized
