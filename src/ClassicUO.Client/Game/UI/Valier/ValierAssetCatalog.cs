using System.Collections.Generic;

namespace ClassicUO.Game.UI.Valier
{
    internal static class ValierAssetCatalog
    {
        private static readonly Dictionary<ValierAssetId, string> _relativePaths = new()
        {
            { ValierAssetId.ChatPanel, "Chat/persistent_chat_panel.png" },
            { ValierAssetId.InventoryPanel, "Inventory/inventory_panel.png" },
            { ValierAssetId.ContainerPanel, "Inventory/container_panel.png" },
            { ValierAssetId.SpellbookShell, "Spellbook/spellbook_shell.png" },
            { ValierAssetId.HotbarPanel, "Hotbar/hotbar_panel.png" },
            { ValierAssetId.PrimaryButton, "Buttons/primary_button.png" },
            { ValierAssetId.SecondaryButton, "Buttons/secondary_button.png" },
            { ValierAssetId.DangerButton, "Buttons/danger_button.png" }
        };

        public static bool TryGetRelativePath(ValierAssetId id, out string relativePath)
        {
            return _relativePaths.TryGetValue(id, out relativePath);
        }
    }
}
