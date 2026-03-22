# ValierUO Phase 9 — Anchors, Clamping, and Movable Gumps

This phase fixes the first visible Valier gumps so they open in sane positions, remain movable, and clamp to the viewport.

## What changed
- All Valier debug gumps are movable.
- All Valier debug gumps clamp to the viewport in `Update()`.
- Default spawn positions:
  - Persistent Chat → bottom-left
  - Inventory → left-middle
  - Container → center
  - Spellbook → right-middle
  - Hotbar → bottom-center

## Important note about black boxes
The black boxes are the intended fallback when the runtime PNG is missing.
Put real art at these exact paths:

- `Data/Client/ValierUI/Chat/persistent_chat_panel.png`
- `Data/Client/ValierUI/Inventory/inventory_panel.png`
- `Data/Client/ValierUI/Inventory/container_panel.png`
- `Data/Client/ValierUI/Spellbook/spellbook_shell.png`
- `Data/Client/ValierUI/Hotbar/hotbar_panel.png`
- `Data/Client/ValierUI/Buttons/primary_button.png`
- `Data/Client/ValierUI/Buttons/secondary_button.png`
- `Data/Client/ValierUI/Buttons/danger_button.png`

## Test checklist
- `Ctrl+Shift+F6` chat opens bottom-left and can be dragged.
- `Ctrl+Shift+F7` inventory opens left-middle and can be dragged.
- `Ctrl+Shift+F8` container opens center and can be dragged.
- `Ctrl+Shift+F9` spellbook opens right-middle and can be dragged.
- `Ctrl+Shift+F10` hotbar opens bottom-center and can be dragged.
- Resize larger.
- Resize smaller.
- Maximize.
- Restore.
- Confirm no gump remains partly off-screen after resize.
