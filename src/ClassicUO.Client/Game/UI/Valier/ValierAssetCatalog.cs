// SPDX-License-Identifier: BSD-2-Clause

namespace ClassicUO.Game.UI.Valier
{
    internal static class ValierAssetCatalog
    {
        public static string GetRelativePath(ValierAssetId id)
        {
            return id switch
            {
                ValierAssetId.LoginBackground => @"Login\background.png",
                ValierAssetId.LoginFrame => @"Frames\login-frame.png",
                ValierAssetId.ServerSelectionFrame => @"Frames\server-frame.png",

                ValierAssetId.ChatPanel => @"Chat\chat-panel.png",
                ValierAssetId.InventoryPanel => @"Inventory\inventory-panel.png",
                ValierAssetId.ContainerPanel => @"Inventory\container-panel.png",
                ValierAssetId.SpellbookPanel => @"Spellbook\spellbook-panel.png",
                ValierAssetId.HotbarPanel => @"Hotbar\hotbar-panel.png",

                ValierAssetId.PrimaryButtonNormal => @"Buttons\primary-normal.png",
                ValierAssetId.PrimaryButtonHover => @"Buttons\primary-hover.png",
                ValierAssetId.PrimaryButtonPressed => @"Buttons\primary-pressed.png",

                ValierAssetId.SecondaryButtonNormal => @"Buttons\secondary-normal.png",
                ValierAssetId.SecondaryButtonHover => @"Buttons\secondary-hover.png",
                ValierAssetId.SecondaryButtonPressed => @"Buttons\secondary-pressed.png",

                ValierAssetId.DangerButtonNormal => @"Buttons\danger-normal.png",
                ValierAssetId.DangerButtonHover => @"Buttons\danger-hover.png",
                ValierAssetId.DangerButtonPressed => @"Buttons\danger-pressed.png",

                ValierAssetId.IconLogoSmall => @"Icons\logo-small.png",
                ValierAssetId.IconLogoLarge => @"Icons\logo-large.png",
                _ => null
            };
        }
    }
}
