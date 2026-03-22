using ClassicUO.Game.UI.Controls;
using ClassicUO.Game.UI.Valier;

namespace ClassicUO.Game.UI.Gumps.Valier
{
    internal sealed class ValierContainerGump : Gump
    {
        private const int PanelWidth = 340;
        private const int PanelHeight = 400;

        public ValierContainerGump(World world) : base(world, 0, 0)
        {
            CanMove = true;
            CanCloseWithRightClick = true;
            AcceptMouseInput = true;

            Add(new ValierPanelControl(ValierAssetId.ContainerPanel, PanelWidth, PanelHeight));
            Add(new Label("Valier Container", false, ValierTheme.TextHue, font: 9) { X = 18, Y = 14 });
            Add(new AlphaBlendControl(0.55f) { X = 18, Y = 42, Width = 304, Height = 330 });
            Add(new Label("Container shell scaffold", false, ValierTheme.TextHue, font: 9) { X = 28, Y = 56 });
            Add(new Label("Expected: Data/Client/ValierUI/Inventory/container_panel.png", false, ValierTheme.TextHue, font: 9) { X = 28, Y = 80 });

            var p = ValierWindowPlacement.GetDefaultPosition(ValierAnchorPreset.Center, PanelWidth, PanelHeight);
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
