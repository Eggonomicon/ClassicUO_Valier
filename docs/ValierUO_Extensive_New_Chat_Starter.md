# ValierUO Extensive New Chat Starter

PROJECT
ValierUO custom ClassicUO fork for the Valier server. Continue from the working build/package stage and use the production runtime PNG pack that has already been prepared.

REPO
X:\\UO\\Client\\Dev\\ClassicUO_Valier
GitHub: https://github.com/Eggonomicon/ClassicUO_Valier.git

CURRENT STATUS
- Local Windows build, publish, packaging, and release zip creation work.
- Shared Valier asset/runtime pipeline is already in code.
- Debug hotkeys open the Valier gumps.
- The remaining work is real UI integration using runtime PNG art, not build troubleshooting.

HOTKEYS
Ctrl+Shift+F6 -> Persistent chat
Ctrl+Shift+F7 -> Inventory
Ctrl+Shift+F8 -> Container
Ctrl+Shift+F9 -> Spellbook
Ctrl+Shift+F10 -> Hotbar

THIS ART PACK
A production-ready drop-in PNG pack was created from the uploaded Phase 3B assets and the additional reference PNGs.
It lives under Data\\Client\\ValierUI and includes:
- Chat
- Inventory
- Spellbook
- Hotbar
- Buttons
- Frames
- Icons
- References

IMPORTANT
A few state alternatives were missing from the source art and were synthesized so the current client has complete state families. Treat them as usable interim production assets unless replaced later by better hand-authored art.

PRIMARY CLIENT FILES TO LOOK AT NEXT
- src\\ClassicUO.Client\\Game\\UI\\Valier\\ValierAssetCatalog.cs
- src\\ClassicUO.Client\\Game\\UI\\Valier\\ValierTextureCache.cs
- src\\ClassicUO.Client\\Game\\UI\\Controls\\ValierTextureImageControl.cs
- src\\ClassicUO.Client\\Game\\UI\\Controls\\ValierPanelControl.cs
- src\\ClassicUO.Client\\Game\\UI\\Controls\\ValierButtonControl.cs
- src\\ClassicUO.Client\\Game\\UI\\Gumps\\Valier\\ValierPersistentChatGump.cs
- src\\ClassicUO.Client\\Game\\UI\\Gumps\\Valier\\ValierInventoryGump.cs
- src\\ClassicUO.Client\\Game\\UI\\Gumps\\Valier\\ValierContainerGump.cs
- src\\ClassicUO.Client\\Game\\UI\\Gumps\\Valier\\ValierSpellbookGump.cs
- src\\ClassicUO.Client\\Game\\UI\\Gumps\\Valier\\ValierHotbarGump.cs

NEXT IMPLEMENTATION ORDER
1. Verify the runtime PNGs show in-client instead of fallback black boxes.
2. Make ValierPersistentChatGump the first actually usable UI shell.
3. Update ValierButtonControl to select idle/hover/pressed textures from the new button families.
4. Update ValierInventoryGump to use the new inventory shell, grid, and bag assets.
5. Update ValierContainerGump to use the new container panel and chest states.
6. Update ValierSpellbookGump to use spellbook_shell/open assets.
7. Update ValierHotbarGump to use the hotbar panel assets.
8. Revisit login/server-select only after the shared system is stable.

DO NOT RESTART FROM SCRATCH
Do not go back to build troubleshooting or restart art planning. The next chat should assume the runtime pack exists and focus on integrating it cleanly into the actual gump code.
