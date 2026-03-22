using ClassicUO.Game.UI.Controls;
using ClassicUO.Game.UI.Valier;

namespace ClassicUO.Game.UI.Gumps.Valier
{
    internal sealed class ValierInventoryGump : Gump
    {
        public ValierInventoryGump(World world) : base(world, 0, 0)
        {
            CanMove = true;

            Add(new ValierPanelControl(ValierAssetId.InventoryPanel, 360, 440));
            Add(new Label("Valier Inventory", false, ValierTheme.TextHue, font: 9) { X = 18, Y = 14 });
            Add(new AlphaBlendControl(0.55f) { X = 18, Y = 42, Width = 324, Height = 370 });
            Add(new Label("Inventory shell scaffold", false, ValierTheme.TextHue, font: 9) { X = 28, Y = 56 });

            X = Client.Game.ClientBounds.Width - Width - ValierTheme.ScreenMargin;
            Y = ValierTheme.ScreenMargin + 60;
        }
    }
}
