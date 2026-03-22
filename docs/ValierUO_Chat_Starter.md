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
- Deliverables should prefer zip files.
- UI/code replacement work should prefer direct `.cs` file replacement over brittle PowerShell patch chains.
- Any patch/helper scripts should live directly under repo-root `patch` or `scripts`.
- Do not use an `overlay` folder.
- Put commands in code blocks.

CURRENT STATUS
- Windows build/package pipeline works.
- Branding icon works.
- Packaged client zip and patch manifest generation work.
- Login window was made resizable.
- A Valier-themed login/server-select integration pass was applied and now compiles.
- Latest successful package build produced:
  - `release\\ValierClassicUOClient`
  - `release\\ValierClassicUOClient-win-x64.zip`
  - `release\\patch-manifest.json`

CURRENT CLIENT INTEGRATION STATE
- `LoginGump.cs` is already customized toward Valier UI and currently includes:
  - `Loader.GetValierLoginFrame()`
  - `Loader.GetValierQuitButton()`
  - `Loader.GetValierCreditsButton()`
  - `Loader.GetValierLoginButton()`
  - `Loader.GetCuoLogo()` still in use for logo slot
  - title update line: `Client.Game.Window.Title = $"ValierUO - {CUOEnviroment.Version}";`
  - right-center login panel anchoring logic
- Previous runtime tests indicated issues before compile cleanup with:
  - title still showing ClassicUO
  - missing background/logo
  - grey gump panel still showing
  - login and shard selection anchored right-center and too small / not fully reflowed

WHAT WAS FIXED IN THIS CHAT
1. Build/package scripts stabilized for Windows.
2. Bash dependency removed from Windows build flow.
3. Patch-manifest generation fixed.
4. Settings/bootstrap launch issues fixed enough to package and run.
5. Login window resizing enabled.
6. Several compile blockers in custom login/server UI integration were resolved.
7. Latest package build now succeeds again.

KNOWN TECHNICAL DIRECTION
- For major UI work, stop stacking PS patches.
- Prefer direct replacement files for:
  - `LoginBackground.cs`
  - `LoginGump.cs`
  - `ServerSelectionGump.cs`
  - custom image controls
  - title/branding handling
- Keep deliverables zip-ready and repo-root aligned.

NEXT IMMEDIATE TASKS
1. Runtime test the newly compiled package.
2. Verify whether title now says `ValierUO` at runtime.
3. Verify whether custom background/logo/button art now renders.
4. Verify whether login and shard-select panels are still too small or partially clipped.
5. Replace remaining ClassicUO visuals with direct `.cs` file replacement if runtime still does not match the intended Valier UI.

NEXT BIG IMPLEMENTATION PHASE
- Deliver a direct-replacement UI pack for:
  - `src\\ClassicUO.Client\\Game\\UI\\Gumps\\Login\\LoginBackground.cs`
  - `src\\ClassicUO.Client\\Game\\UI\\Gumps\\Login\\LoginGump.cs`
  - `src\\ClassicUO.Client\\Game\\UI\\Gumps\\Login\\ServerSelectionGump.cs`
  - any required custom controls/resources
- Goal:
  - true ValierUO title
  - castle background
  - readable custom PNG frame
  - image buttons
  - right-center anchoring where desired
  - larger / cleaner login and shard selection layout

WHEN RESUMING
Start by asking for or reviewing runtime test results from the latest successful package build, then move to direct `.cs` replacement files for the login and shard-select experience.
