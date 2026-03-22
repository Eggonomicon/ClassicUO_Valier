using ClassicUO.Game.UI.Controls;
using ClassicUO.Game.UI.Valier;

namespace ClassicUO.Game.UI.Gumps.Valier
{
    internal sealed class ValierSpellbookGump : Gump
    {
        private const int PanelWidth = 420;
        private const int PanelHeight = 520;

        public ValierSpellbookGump(World world) : base(world, 0, 0)
        {
            CanMove = true;
            CanCloseWithRightClick = true;
            AcceptMouseInput = true;

            Add(new ValierPanelControl(ValierAssetId.SpellbookShell, PanelWidth, PanelHeight));
            Add(new Label("Valier Spellbook", false, ValierTheme.TextHue, font: 9) { X = 18, Y = 14 });
            Add(new AlphaBlendControl(0.55f) { X = 18, Y = 42, Width = 384, Height = 450 });
            Add(new Label("Spellbook shell scaffold", false, ValierTheme.TextHue, font: 9) { X = 28, Y = 56 });
            Add(new Label("Expected: Data/Client/ValierUI/Spellbook/spellbook_shell.png", false, ValierTheme.TextHue, font: 9) { X = 28, Y = 80 });

            var p = ValierWindowPlacement.GetDefaultPosition(ValierAnchorPreset.RightMiddle, PanelWidth, PanelHeight);
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
