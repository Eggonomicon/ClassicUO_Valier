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
- Zip files are preferred.
- Direct-root repo layout only.
- Do not use overlay folders.
- Put commands in code blocks.
- For major UI work, prefer direct .cs replacements over PowerShell mutation patches.

CURRENT STATUS
- Windows build/package pipeline works.
- Branding icon works.
- Release folder, zip, and patch manifest generation work.
- Shared Valier UI pipeline scaffolding builds.
- Shared Valier runtime controls and asset registry build path is in progress.
- Phase 7 initially failed because ValierTextureImageControl.cs was missing the closing namespace brace.

CURRENT IMMEDIATE TASK
- Replace ValierTextureImageControl.cs with the fixed file.
- Rebuild and confirm Phase 7 compiles and packages.
- Then proceed to real runtime art hookup and first usable ValierPersistentChatGump.

NEXT TASKS
1. Confirm Phase 7 build succeeds.
2. Wire runtime art under Data\Client\ValierUI\...
3. Open/test ValierPersistentChatGump in-client.
4. Flesh out ValierInventoryGump and ValierContainerGump.
5. Continue toward a clean login reboot later on top of the shared system.
