using ClassicUO.Game.UI.Controls;
using ClassicUO.Game.UI.Valier;

namespace ClassicUO.Game.UI.Gumps.Valier
{
    internal sealed class ValierSpellbookGump : Gump
    {
        public ValierSpellbookGump(World world) : base(world, 0, 0)
        {
            CanMove = true;

            Add(new ValierPanelControl(ValierAssetId.SpellbookShell, 420, 520));
            Add(new Label("Valier Spellbook", false, ValierTheme.TextHue, font: 9) { X = 18, Y = 14 });
            Add(new AlphaBlendControl(0.55f) { X = 18, Y = 42, Width = 384, Height = 450 });
            Add(new Label("Spellbook shell scaffold", false, ValierTheme.TextHue, font: 9) { X = 28, Y = 56 });

            X = ValierTheme.ScreenMargin + 40;
            Y = ValierTheme.ScreenMargin + 40;
        }
    }
}
