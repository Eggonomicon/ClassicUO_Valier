using ClassicUO.Game.UI.Controls;
using ClassicUO.Game.UI.Valier;

namespace ClassicUO.Game.UI.Gumps.Valier
{
    internal sealed class ValierInventoryGump : Gump
    {
        private const int PanelWidth = 360;
        private const int PanelHeight = 440;

        public ValierInventoryGump(World world) : base(world, 0, 0)
        {
            CanMove = true;
            CanCloseWithRightClick = true;
            AcceptMouseInput = true;

            Add(new ValierPanelControl(ValierAssetId.InventoryPanel, PanelWidth, PanelHeight));
            Add(new Label("Valier Inventory", false, ValierTheme.TextHue, font: 9) { X = 18, Y = 14 });
            Add(new AlphaBlendControl(0.55f) { X = 18, Y = 42, Width = 324, Height = 370 });
            Add(new Label("Inventory shell scaffold", false, ValierTheme.TextHue, font: 9) { X = 28, Y = 56 });
            Add(new Label("Expected: Data/Client/ValierUI/Inventory/inventory_panel.png", false, ValierTheme.TextHue, font: 9) { X = 28, Y = 80 });

            var p = ValierWindowPlacement.GetDefaultPosition(ValierAnchorPreset.LeftMiddle, PanelWidth, PanelHeight);
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
