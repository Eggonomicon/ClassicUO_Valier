using ClassicUO.Game.UI.Controls;
using ClassicUO.Game.UI.Valier;

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
            Add(new Label("Ctrl+Shift+F6 toggles this shell.", false, ValierTheme.TextHue, font: 9) { X = 170, Y = 14 });

            Add(new AlphaBlendControl(0.55f) { X = 16, Y = 42, Width = PanelWidth - 32, Height = 126 });
            Add(new Label("Persistent chat shell - runtime art ready.", false, ValierTheme.TextHue, font: 9) { X = 26, Y = 54 });
            Add(new Label("Black panel fallback means the runtime PNG is missing.", false, ValierTheme.TextHue, font: 9) { X = 26, Y = 78 });
            Add(new Label("Expected: Data/Client/ValierUI/Chat/persistent_chat_panel.png", false, ValierTheme.TextHue, font: 9) { X = 26, Y = 102 });

            Add(new AlphaBlendControl(0.65f) { X = 16, Y = 176, Width = PanelWidth - 120, Height = 26 });
            Add(new Label("Type here...", false, ValierTheme.TextHue, font: 9) { X = 24, Y = 182 });

            ValierButtonControl sendButton = new ValierButtonControl(1, ValierAssetId.PrimaryButton, 82, 26)
            {
                X = PanelWidth - 96,
                Y = 176
            };
            Add(sendButton);
            Add(new Label("Send", false, ValierTheme.TextHue, font: 9) { X = PanelWidth - 72, Y = 182 });

            var p = ValierWindowPlacement.GetDefaultPosition(ValierAnchorPreset.BottomLeft, PanelWidth, PanelHeight);
            X = p.X;
            Y = p.Y;
        }

        public override void Update()
        {
            base.Update();
            ValierWindowPlacement.ClampToViewport(this);
        }
    }
}
