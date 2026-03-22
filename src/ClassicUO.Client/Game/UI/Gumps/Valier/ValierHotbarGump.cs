using ClassicUO.Game.UI.Controls;
using ClassicUO.Game.UI.Valier;

namespace ClassicUO.Game.UI.Gumps.Valier
{
    internal sealed class ValierHotbarGump : Gump
    {
        public ValierHotbarGump(World world) : base(world, 0, 0)
        {
            CanMove = true;

            Add(new ValierPanelControl(ValierAssetId.HotbarPanel, 540, 86));
            Add(new Label("Valier Hotbar", false, ValierTheme.TextHue, font: 9) { X = 18, Y = 14 });

            for (int i = 0; i < 10; i++)
            {
                Add(new AlphaBlendControl(0.6f) { X = 18 + (i * 50), Y = 34, Width = 40, Height = 34 });
            }

            X = (Client.Game.ClientBounds.Width - Width) / 2;
            Y = Client.Game.ClientBounds.Height - Height - ValierTheme.ScreenMargin;
        }

        public override void Update()
        {
            base.Update();
            X = (Client.Game.ClientBounds.Width - Width) / 2;
            Y = Client.Game.ClientBounds.Height - Height - ValierTheme.ScreenMargin;
        }
    }
}
