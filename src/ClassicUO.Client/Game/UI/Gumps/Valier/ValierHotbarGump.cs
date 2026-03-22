using ClassicUO.Game.UI.Controls;
using ClassicUO.Game.UI.Valier;

namespace ClassicUO.Game.UI.Gumps.Valier
{
    internal sealed class ValierHotbarGump : Gump
    {
        private const int PanelWidth = 540;
        private const int PanelHeight = 86;

        public ValierHotbarGump(World world) : base(world, 0, 0)
        {
            CanMove = true;
            CanCloseWithRightClick = true;
            AcceptMouseInput = true;

            Add(new ValierPanelControl(ValierAssetId.HotbarPanel, PanelWidth, PanelHeight));
            Add(new Label("Valier Hotbar", false, ValierTheme.TextHue, font: 9) { X = 18, Y = 14 });

            for (int i = 0; i < 10; i++)
            {
                Add(new AlphaBlendControl(0.6f) { X = 18 + (i * 50), Y = 34, Width = 40, Height = 34 });
            }

            var p = ValierWindowPlacement.GetDefaultPosition(ValierAnchorPreset.BottomCenter, PanelWidth, PanelHeight);
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
