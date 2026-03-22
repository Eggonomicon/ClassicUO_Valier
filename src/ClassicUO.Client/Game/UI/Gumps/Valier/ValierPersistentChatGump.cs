using ClassicUO.Game.UI.Controls;
using ClassicUO.Game.UI.Valier;
using Microsoft.Xna.Framework;

namespace ClassicUO.Game.UI.Gumps.Valier
{
    internal sealed class ValierPersistentChatGump : Gump
    {
        private const int PanelWidth = 460;
        private const int PanelHeight = 220;

        public ValierPersistentChatGump(World world) : base(world, 0, 0)
        {
            CanMove = true;
            CanCloseWithRightClick = true;
            AcceptMouseInput = true;

            Add(new ValierPanelControl(ValierAssetId.ChatPanel, PanelWidth, PanelHeight));
            Add(new Label("Valier Chat", false, ValierTheme.TextHue, font: 9) { X = 18, Y = 14 });

            Add(new AlphaBlendControl(0.55f) { X = 16, Y = 42, Width = PanelWidth - 32, Height = 126 });
            Add(new Label("Persistent chat shell - runtime art ready.", false, ValierTheme.TextHue, font: 9) { X = 26, Y = 54 });
            Add(new Label("Messages can be rendered into this body area next.", false, ValierTheme.TextHue, font: 9) { X = 26, Y = 78 });
            Add(new Label("This gump is intended to stay open in play.", false, ValierTheme.TextHue, font: 9) { X = 26, Y = 102 });

            Add(new AlphaBlendControl(0.65f) { X = 16, Y = 176, Width = PanelWidth - 120, Height = 26 });
            Add(new Label("Type here...", false, ValierTheme.TextHue, font: 9) { X = 24, Y = 182 });

            ValierButtonControl sendButton = new ValierButtonControl(1, ValierAssetId.PrimaryButton, 82, 26)
            {
                X = PanelWidth - 96,
                Y = 176
            };
            Add(sendButton);
            Add(new Label("Send", false, ValierTheme.TextHue, font: 9) { X = PanelWidth - 72, Y = 182 });

            X = ValierTheme.ScreenMargin;
            Y = Client.Game.ClientBounds.Height - PanelHeight - ValierTheme.ScreenMargin;
        }

        public override void Update()
        {
            base.Update();
            Y = Client.Game.ClientBounds.Height - Height - ValierTheme.ScreenMargin;
        }
    }
}
