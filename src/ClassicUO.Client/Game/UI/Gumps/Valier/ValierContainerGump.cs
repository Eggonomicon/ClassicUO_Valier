using ClassicUO.Game.UI.Controls;
using ClassicUO.Game.UI.Valier;

namespace ClassicUO.Game.UI.Gumps.Valier
{
    internal sealed class ValierContainerGump : Gump
    {
        public ValierContainerGump(World world) : base(world, 0, 0)
        {
            CanMove = true;

            Add(new ValierPanelControl(ValierAssetId.ContainerPanel, 340, 400));
            Add(new Label("Valier Container", false, ValierTheme.TextHue, font: 9) { X = 18, Y = 14 });
            Add(new AlphaBlendControl(0.55f) { X = 18, Y = 42, Width = 304, Height = 330 });
            Add(new Label("Container shell scaffold", false, ValierTheme.TextHue, font: 9) { X = 28, Y = 56 });

            X = Client.Game.ClientBounds.Width - Width - ValierTheme.ScreenMargin - 30;
            Y = ValierTheme.ScreenMargin + 110;
        }
    }
}
