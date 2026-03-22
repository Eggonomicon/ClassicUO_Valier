// SPDX-License-Identifier: BSD-2-Clause

using ClassicUO.Game.UI.Controls;
using ClassicUO.Game.UI.Valier;

namespace ClassicUO.Game.UI.Gumps.Valier
{
    internal sealed class ValierInventoryGump : Gump
    {
        public ValierInventoryGump(World world) : base(world, 0, 0)
        {
            CanMove = true;
            CanCloseWithRightClick = true;

            Add(new ValierPanelControl(ValierAssetId.InventoryPanel, ValierTheme.InventoryWidth, ValierTheme.InventoryHeight));
            Add(new Label("Valier Inventory", false, 0x0481, font: 9) { X = 16, Y = 12 });
            Add(new AlphaBlendControl(0.35f) { X = 14, Y = 42, Width = ValierTheme.InventoryWidth - 28, Height = ValierTheme.InventoryHeight - 56 });

            X = System.Math.Max(24, Client.Game.ClientBounds.Width - ValierTheme.InventoryWidth - 36);
            Y = 80;
        }
    }
}
