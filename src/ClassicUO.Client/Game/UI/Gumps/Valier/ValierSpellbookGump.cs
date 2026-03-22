// SPDX-License-Identifier: BSD-2-Clause

using ClassicUO.Game.UI.Controls;
using ClassicUO.Game.UI.Valier;

namespace ClassicUO.Game.UI.Gumps.Valier
{
    internal sealed class ValierSpellbookGump : Gump
    {
        public ValierSpellbookGump(World world) : base(world, 0, 0)
        {
            CanMove = true;
            CanCloseWithRightClick = true;

            Add(new ValierPanelControl(ValierAssetId.SpellbookPanel, ValierTheme.SpellbookWidth, ValierTheme.SpellbookHeight));
            Add(new Label("Valier Spellbook", false, 0x0481, font: 9) { X = 16, Y = 12 });
            Add(new AlphaBlendControl(0.35f) { X = 14, Y = 42, Width = ValierTheme.SpellbookWidth - 28, Height = ValierTheme.SpellbookHeight - 56 });

            X = 60;
            Y = 80;
        }
    }
}
