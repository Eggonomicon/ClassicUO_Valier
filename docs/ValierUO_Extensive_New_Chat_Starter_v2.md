# ValierUO Extensive New Chat Starter

PROJECT
ValierUO is a custom ClassicUO fork for the Valier server.

PRIMARY GOALS
1. Keep the Windows-first build/package pipeline stable.
2. Replace placeholder/fallback UI shells with production runtime PNG art under Data/Client/ValierUI.
3. Rebuild login/shard-select cleanly from production art, not by salvaging the earlier broken experimental login branch.
4. Continue implementing reusable Valier UI systems for chat, inventory, container, spellbook, and hotbar.

REPO
X:\UO\Client\Dev\ClassicUO_Valier

IMPORTANT USER PREFERENCES
- Deliverables should be zip-first.
- Direct file replacement zips are preferred over long chains of PowerShell patch scripts for substantial UI work.
- Repo layout assumptions:
  - src
  - branding
  - scripts
  - patch
  - docs
  - Data\Client\ValierUI
- Windows PowerShell commands should be placed in code blocks.
- Keep extensive handoff docs so work can resume in a fresh chat/project.

CURRENT BUILD STATUS
- Local Windows build succeeds.
- Publish succeeds.
- Release folder is created.
- Release zip is created.
- Patch manifest is created.
- GitHub workflow signing issue was addressed by disabling/removing SignPath-related deployment behavior.
- Remaining C# / analyzer warnings are non-blocking for current UI integration work.

CURRENT UI CODE STATUS
- Shared Valier runtime controls compile.
- Debug hotkeys open Valier gumps:
  - Ctrl+Shift+F6: Persistent Chat
  - Ctrl+Shift+F7: Inventory
  - Ctrl+Shift+F8: Container
  - Ctrl+Shift+F9: Spellbook
  - Ctrl+Shift+F10: Hotbar
- The visible Valier gumps were opening and movable.
- Black boxes were observed because the fallback renderer is working, but some runtime PNGs were not yet wired in at the exact filenames/paths the code expects.
- Phase 9 focused on anchor/clamp behavior and movable shells.

IMPORTANT ART CONTEXT
The user provided:
- Valier_UI_Phase3B_Production_Assets.zip
- multiple layout atlas PNGs
- Book.png / SpellBook.png references
- Valier_Login.png mockup reference
- castle background / logo branding

CONCLUSION FROM SOURCE ASSETS
- The previous “production ready” UI elements were only partially correct.
- Some state families were incomplete or inconsistent.
- The login mockup itself is a reference composition, not a clean runtime asset sheet.
- A corrected drop-in runtime pack was produced that maps the strongest clean source assets into Data/Client/ValierUI and adds integration aliases.

CURRENT RUNTIME ART PACK
Use:
- Data\Client\ValierUI\PRODUCTION_ASSET_MANIFEST_v2.json
- Data\Client\ValierUI\Login\...
- Data\Client\ValierUI\Buttons\...
- Data\Client\ValierUI\Inventory\...
- Data\Client\ValierUI\Spellbook\...
- Data\Client\ValierUI\Chat\...
- Data\Client\ValierUI\Hotbar\...
- Data\Client\ValierUI\References\...

HIGH VALUE LOGIN FILES
- Login/login_background.png
- Login/login_panel_main.png
- Login/login_frame_card.png
- Login/login_login_idle.png
- Login/login_login_hover.png
- Login/login_login_pressed.png
- Login/login_credits_idle.png
- Login/login_credits_hover.png
- Login/login_credits_pressed.png
- Login/login_quit_idle.png
- Login/login_quit_hover.png
- Login/login_quit_pressed.png
- Login/valier_logo.png
- Login/login_mockup_reference.png

CURRENT CLIENT-EXPECTED FILES
The code path currently expects these exact runtime files at minimum:
- Chat/persistent_chat_panel.png
- Inventory/inventory_panel.png
- Inventory/container_panel.png
- Spellbook/spellbook_shell.png
- Hotbar/hotbar_panel.png
- Buttons/primary_button.png
- Buttons/secondary_button.png
- Buttons/danger_button.png

WHY BLACK BOXES HAPPENED
The Valier runtime controls intentionally fall back to dark/black placeholder panels when the PNG is missing or not resolved at the exact runtime path.
So “black boxes” means:
- the gump/control pipeline is alive
- the file lookup/path/name did not yet match what the control requested

WHAT NEEDS TO HAPPEN NEXT
1. Use the corrected runtime PNG pack as the source of truth.
2. Update ValierButtonControl so it can switch between idle / hover / pressed / disabled state PNGs.
3. Wire login/shard-select to the corrected login asset family in Data/Client/ValierUI/Login.
4. Make ValierPersistentChatGump the first truly polished in-client Valier shell.
5. Then update ValierInventoryGump and ValierContainerGump to consume the drop-in art.
6. Revisit spellbook after the general shell/button/state pipeline is stable.

RECOMMENDED NEXT IMPLEMENTATION ORDER
Stage A — button states
- teach ValierButtonControl to consume per-state PNGs
- support idle / hover / pressed / disabled

Stage B — login/shard-select rebuild
- rebuild login from:
  - login_background.png
  - login_panel_main.png
  - login_frame_card.png
  - login_login_* states
  - login_credits_* states
  - login_quit_* states
- do NOT continue patching the earlier broken login salvage branch
- treat the mockup as layout reference only

Stage C — visible shell polish
- ValierPersistentChatGump
- ValierInventoryGump
- ValierContainerGump

Stage D — spellbook/hotbar
- wire spellbook open/close assets
- wire hotbar active/alt states

TEST CHECKLIST FOR NEXT CHAT
- build still succeeds
- release zip still packages
- runtime PNGs resolve instead of fallback black boxes
- button hover/pressed visuals actually change
- login uses production runtime assets
- shard-select uses matching panel family
- all Valier debug gumps remain movable and on-screen

IMPORTANT RULES
- Preserve original silhouettes and outlines.
- Prefer reusable panel/button/frame systems over one-off baked screens.
- Keep Windows-first packaging stable.
- Use zip-based deliverables when possible.
- Keep handoff docs updated every major phase.
