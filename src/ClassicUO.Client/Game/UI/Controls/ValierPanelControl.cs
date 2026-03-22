// SPDX-License-Identifier: BSD-2-Clause

using ClassicUO.Game.UI.Valier;

namespace ClassicUO.Game.UI.Controls
{
    internal sealed class ValierPanelControl : ValierTextureImageControl
    {
        public ValierPanelControl(ValierAssetId assetId, int width, int height)
            : base(assetId, width, height)
        {
            AcceptMouseInput = true;
        }

        public int ContentPadding { get; set; } = ValierTheme.DefaultPadding;
    }
}
