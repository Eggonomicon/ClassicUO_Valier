# ValierUO Phase 7 — Runtime Art Wiring and First Usable Chat Shell

This phase keeps the repo build-safe and shifts the pipeline from abstract scaffolding to usable runtime controls.

## Added
- Shared runtime asset ids and catalog
- Runtime root path helper for `Data/Client/ValierUI`
- Cached file-backed texture loading
- Texture-backed Valier controls with safe rectangle fallbacks when art is missing
- First usable `ValierPersistentChatGump`
- Scaffold gumps for inventory, container, spellbook, and hotbar
- Debug entry points you can call later from a hotkey or one-line hook

## Expected runtime asset layout
- `Data/Client/ValierUI/Chat/persistent_chat_panel.png`
- `Data/Client/ValierUI/Inventory/inventory_panel.png`
- `Data/Client/ValierUI/Inventory/container_panel.png`
- `Data/Client/ValierUI/Spellbook/spellbook_shell.png`
- `Data/Client/ValierUI/Hotbar/hotbar_panel.png`
- `Data/Client/ValierUI/Buttons/primary_button.png`
- `Data/Client/ValierUI/Buttons/secondary_button.png`
- `Data/Client/ValierUI/Buttons/danger_button.png`

## Debug/test entry points
Callable from a debugger or a temporary hook:

- `ClassicUO.Game.UI.Valier.ValierDebugEntryPoints.OpenPersistentChat()`
- `ClassicUO.Game.UI.Valier.ValierDebugEntryPoints.OpenInventory()`
- `ClassicUO.Game.UI.Valier.ValierDebugEntryPoints.OpenContainer()`
- `ClassicUO.Game.UI.Valier.ValierDebugEntryPoints.OpenSpellbook()`
- `ClassicUO.Game.UI.Valier.ValierDebugEntryPoints.OpenHotbar()`

## Notes
- Controls intentionally fall back to dark rectangles if art is not present yet.
- This phase does not auto-open any new gumps to keep the current stable build path intact.
- The next phase should wire a temporary debug hotkey and start replacing the old chat shell with the Valier shell.
