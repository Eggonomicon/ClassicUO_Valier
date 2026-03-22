# Valier UI Phase 3C — Client Integration Prep

This pack maps the current Phase 3B production assets to ClassicUO implementation targets.

## Scope
This is an integration-prep package, not the final client code pass.

It answers:
- which assets should be treated as textures
- which assets should be treated as reusable panels
- which assets belong to button state families
- which assets should be wired as scalable frames
- which assets should become custom gumps or custom gump shells

## Integration philosophy
1. Keep static branded art separate from reusable UI assets.
2. Treat 9-slice panels as the base system for resizable Valier windows.
3. Treat button strips and state exports as reusable families, not one-off screen art.
4. Build custom gumps around shared frame/panel assets instead of baking whole screens into one image.
5. Approve 2x placement first, then create the 4x set after layout is stable.

## Repo context
Your repo already has the top-level areas needed for this workflow:
- `branding/`
- `launch/`
- `settings/`
- `src/`
- `scripts/`

The current build wrapper also copies branding into `src/ClassicUO.Client/` and writes `src/ClassicUO.Client/Directory.Build.props`, then outputs a Windows release folder for distribution.

## Recommended implementation target buckets

### 1. Textures
Use for fixed art assets that do not need scaling logic:
- spellbook shell
- chest open art
- reagent bag art
- inventory grid frame when used at fixed size
- hotbar fixed variants
- logo / branded background / icon

### 2. Panels
Use for reusable window surfaces:
- login main panel
- tall side container panel
- chat frame base

### 3. Buttons
Use for interactive controls with states:
- login primary buttons
- secondary buttons
- quit / danger button
- future chat tabs and option toggles

### 4. Scalable frames
Use 9-slice folders for:
- login shell variants
- container side panel
- persistent chat frame
- future options/config windows

### 5. Custom gumps
Use shared art assets to build Valier-specific windows:
- Valier login screen
- shard select card/list shell
- persistent global chat gump
- container/inventory shells
- spellbook shell
- hotbar/action bar shell

## Recommended target naming inside the client
These are proposed integration names for the client pass:

### Shared art families
- `ValierTextureSet`
- `ValierPanelSet`
- `ValierButtonSet`
- `ValierFrameSet`

### Shared UI shells
- `ValierWindowFrame`
- `ValierScalablePanel`
- `ValierButton`
- `ValierChatShell`

### Custom gumps
- `ValierLoginGump`
- `ValierShardSelectGump`
- `ValierPersistentChatGump`
- `ValierInventoryGump`
- `ValierContainerGump`
- `ValierSpellbookGump`
- `ValierHotbarGump`

## Recommended client-integration order
1. shared texture/panel loader
2. scalable frame helper
3. login gump
4. persistent chat gump
5. inventory/container shells
6. spellbook shell
7. hotbar shell
