// SPDX-License-Identifier: BSD-2-Clause

using ClassicUO.Game.UI.Controls;
using ClassicUO.Game.UI.Valier;

namespace ClassicUO.Game.UI.Gumps.Valier
{
    internal sealed class ValierHotbarGump : Gump
    {
        public ValierHotbarGump(World world) : base(world, 0, 0)
        {
            CanMove = true;
            CanCloseWithRightClick = true;

            Add(new ValierPanelControl(ValierAssetId.HotbarPanel, ValierTheme.HotbarWidth, ValierTheme.HotbarHeight));
            Add(new Label("Valier Hotbar", false, 0x0481, font: 9) { X = 16, Y = 12 });
            Add(new AlphaBlendControl(0.35f) { X = 14, Y = 42, Width = ValierTheme.HotbarWidth - 28, Height = 30 });

            X = System.Math.Max(24, (Client.Game.ClientBounds.Width - ValierTheme.HotbarWidth) / 2);
            Y = System.Math.Max(24, Client.Game.ClientBounds.Height - ValierTheme.HotbarHeight - 100);
        }

        public override void Update()
        {
            base.Update();
            X = System.Math.Max(24, (Client.Game.ClientBounds.Width - ValierTheme.HotbarWidth) / 2);
            Y = System.Math.Max(24, Client.Game.ClientBounds.Height - ValierTheme.HotbarHeight - 100);
        }
    }
}
