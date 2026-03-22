# ValierUO Chat Starter

PROJECT
ValierUO custom ClassicUO fork and launcher/patcher project.

REPO
X:\UO\Client\Dev\ClassicUO_Valier

REPO LAYOUT
- src
- branding
- scripts
- patch
- docs

IMPORTANT USER PREFERENCES
- Deliver zip files whenever practical.
- Put PowerShell/run commands in code blocks.
- Match repo root directly.
- Do not use an overlay folder.
- Use direct .cs file replacements for substantial UI phases.
- Keep Windows-first workflow.

CURRENT STATE
- Build pipeline is stable.
- Packaged client zip output is stable.
- Shared UI pipeline scaffolding is in place.
- Shared asset registry scaffolding is in place.
- Experimental login/server-select branch builds but is visually broken and is not the current focus.
- Current focus is reusable Valier UI plumbing for persistent chat, inventory, containers, spellbook, and hotbar.

DONE SO FAR
1. Build/package pipeline stabilized.
2. Branding/bootstrap/settings packaging stabilized.
3. Shared UI pipeline scaffold added under Game\UI\Valier.
4. Shared asset registry scaffold added.
5. Phase 6 runtime controls added:
   - ValierTextureImageControl
   - ValierPanelControl
   - ValierButtonControl
   - scaffold Valier gumps

NEXT WORK
1. Wire actual prepared art into runtime controls.
2. Implement real ValierPersistentChatGump first.
3. Implement ValierInventoryGump and ValierContainerGump.
4. Add reusable 9-slice or art-panel logic where needed.
5. Return to login later with a cleaner shared-system-based reboot.

NOTES
- Avoid destructive edits to source art.
- Prefer reusable components over one-off screens.
- Use direct file replacement zips for major UI phases.
