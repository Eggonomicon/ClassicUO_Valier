// SPDX-License-Identifier: BSD-2-Clause

using ClassicUO.Game.UI.Controls;
using ClassicUO.Game.UI.Valier;

namespace ClassicUO.Game.UI.Gumps.Valier
{
    internal sealed class ValierContainerGump : Gump
    {
        public ValierContainerGump(World world) : base(world, 0, 0)
        {
            CanMove = true;
            CanCloseWithRightClick = true;

            Add(new ValierPanelControl(ValierAssetId.ContainerPanel, ValierTheme.ContainerWidth, ValierTheme.ContainerHeight));
            Add(new Label("Valier Container", false, 0x0481, font: 9) { X = 16, Y = 12 });
            Add(new AlphaBlendControl(0.35f) { X = 14, Y = 42, Width = ValierTheme.ContainerWidth - 28, Height = ValierTheme.ContainerHeight - 56 });

            X = System.Math.Max(24, Client.Game.ClientBounds.Width - ValierTheme.ContainerWidth - 420);
            Y = 120;
        }
    }
}
