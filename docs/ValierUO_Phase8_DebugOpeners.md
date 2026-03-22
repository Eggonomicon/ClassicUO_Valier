# ValierUO Phase 8 — Debug Openers and First Visible Chat Shell

This phase adds a simple debug path to show the first visible Valier gumps in-client.

## Hotkeys
Hold `Ctrl+Shift` and press:

- `F6` — Toggle Valier Persistent Chat
- `F7` — Toggle Valier Inventory
- `F8` — Toggle Valier Container
- `F9` — Toggle Valier Spellbook
- `F10` — Toggle Valier Hotbar

## Runtime art folders
Run:

```powershell
powershell -ExecutionPolicy Bypass -File .\scripts\Create-ValierUI-RuntimeFolders.ps1 -RepoRoot .
```

This creates:

- `Data\Client\ValierUI\Chat`
- `Data\Client\ValierUI\Inventory`
- `Data\Client\ValierUI\Spellbook`
- `Data\Client\ValierUI\Hotbar`
- `Data\Client\ValierUI\Buttons`
- `Data\Client\ValierUI\Frames`
- `Data\Client\ValierUI\Icons`

## Test checklist
1. Build and package succeed.
2. Launch the client.
3. Press `Ctrl+Shift+F6` to show/hide the persistent chat shell.
4. Resize the window and confirm the chat shell stays bottom-left.
5. Press `Ctrl+Shift+F7/F8/F9/F10` to show/hide the other placeholder shells.
6. Confirm no crashes when opening/closing the gumps repeatedly.
7. Confirm clean packaged zip still launches.

## Notes
This phase is intentionally focused on a visible test path, not final polish.
