// SPDX-License-Identifier: BSD-2-Clause

using ClassicUO.Game.UI.Controls;
using ClassicUO.Game.UI.Valier;

namespace ClassicUO.Game.UI.Gumps.Valier
{
    internal sealed class ValierPersistentChatGump : Gump
    {
        public ValierPersistentChatGump(World world) : base(world, 0, 0)
        {
            CanMove = true;
            CanCloseWithRightClick = true;
            AcceptKeyboardInput = false;

            Add(new ValierPanelControl(ValierAssetId.ChatPanel, ValierTheme.ChatWidth, ValierTheme.ChatHeight));
            Add(new Label("Valier Chat", false, 0x0481, font: 9) { X = 16, Y = 12 });
            Add(new AlphaBlendControl(0.35f) { X = 14, Y = 42, Width = ValierTheme.ChatWidth - 28, Height = 124 });
            Add(new Label("Persistent chat shell scaffold", false, 0x0481, font: 1) { X = 20, Y = 50 });
            Add(new AlphaBlendControl(0.35f) { X = 14, Y = 176, Width = ValierTheme.ChatWidth - 28, Height = 28 });
            Add(new Label("Input row placeholder", false, 0x0481, font: 1) { X = 20, Y = 183 });

            X = 24;
            Y = System.Math.Max(24, Client.Game.ClientBounds.Height - ValierTheme.ChatHeight - 48);
        }

        public override void Update()
        {
            base.Update();
            Y = System.Math.Max(24, Client.Game.ClientBounds.Height - ValierTheme.ChatHeight - 48);
        }
    }
}
