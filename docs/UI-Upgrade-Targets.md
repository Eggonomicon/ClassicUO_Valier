# UI Upgrade Targets for the Valier Client

This file is intentionally conservative: it describes the work buckets you need next without pretending that exact UI class names were fully audited here.

## 1. Branding shell

Needed:

- login / splash scene background image hook
- logo draw hook
- shard-name text style
- optional external links (Discord, patch notes, website)

Implementation direction:

- keep all Valier brand assets in a dedicated branding path
- add one branding service or config object so art paths are centralized
- avoid hard-coding file paths in multiple UI screens

## 2. Persistent chat

Needed:

- a retained chat history panel
- a dedicated channel model for global chat
- saved position, size, filters, opacity
- scrollback independent from ephemeral journal top-lines

Implementation direction:

- treat global chat as a first-class dock/panel
- persist its layout inside the same settings file family as shard profile settings
- keep a fallback to stock journal behavior for debugging

## 3. Custom container gumps

Needed:

- data-driven container layout definitions
- a registry mapping art/container identifiers to layout packs
- runtime toggle to disable custom layouts for troubleshooting

Implementation direction:

- do not hard-code every container skin directly into gameplay logic
- use a Valier container skin layer that wraps stock container behavior
- define safe fallback to stock container rendering whenever a layout is missing or invalid

## 4. Text / context overrides

Needed:

- support for Valier-controlled text overrides
- versioned text pack files distributed with the patch manifest
- a clear source precedence order

Implementation direction:

- treat this as a content layer, not a code layer
- keep override files patchable without requiring a full client rebuild

