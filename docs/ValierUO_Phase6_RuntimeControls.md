# ValierUO Phase 6 — Runtime Controls

PURPOSE
This phase adds compile-safe shared runtime controls for the Valier UI pipeline so the client can start using texture-backed art in reusable controls instead of one-off gump experiments.

WHAT THIS PHASE ADDS
- Valier asset id enum
- Valier asset catalog
- Valier runtime path helper
- Valier texture cache
- Valier theme constants
- Valier texture image control
- Valier panel control
- Valier button control
- scaffold Valier gumps:
  - persistent chat
  - inventory
  - container
  - spellbook
  - hotbar

CURRENT INTENT
This phase is plumbing + first renderable controls.
It is NOT the final polished UI pass.

RUNTIME ASSET ROOT
Expected runtime art root:
Data\Client\ValierUI\

Suggested buckets:
- Login
- Panels
- Buttons
- Frames
- Chat
- Inventory
- Spellbook
- Hotbar
- Icons

NEXT PHASE
Phase 7 should wire real prepared art into:
- ValierPersistentChatGump
- ValierInventoryGump
- ValierContainerGump

and optionally add a debug/test command or menu entry to open the new shells in-client.
