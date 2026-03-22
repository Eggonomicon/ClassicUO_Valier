// SPDX-License-Identifier: BSD-2-Clause

using ClassicUO.Game.Scenes;
using ClassicUO.Game.UI.Valier;
using ClassicUO.Renderer;
using Microsoft.Xna.Framework;

namespace ClassicUO.Game.UI.Controls
{
    internal class ValierTextureImageControl : Control
    {
        public ValierTextureImageControl(ValierAssetId assetId, int width = 0, int height = 0)
        {
            AssetId = assetId;
            Width = width;
            Height = height;
            AcceptMouseInput = false;
        }

        public ValierAssetId AssetId { get; set; }

        public override bool Contains(int x, int y)
        {
            return !IsDisposed && new Rectangle(0, 0, Width, Height).Contains(x, y);
        }

        public override bool AddToRenderLists(RenderLists renderLists, int x, int y, ref float layerDepthRef)
        {
            float layerDepth = layerDepthRef;

            if (IsDisposed)
            {
                return false;
            }

            if (ValierTextureCache.TryGet(AssetId, out var texture))
            {
                int drawWidth = Width > 0 ? Width : texture.Width;
                int drawHeight = Height > 0 ? Height : texture.Height;

                if (Width <= 0) Width = texture.Width;
                if (Height <= 0) Height = texture.Height;

                Vector3 hueVector = ShaderHueTranslator.GetHueVector(0, false, Alpha, true);

                renderLists.AddGumpNoAtlas(
                    batcher =>
                    {
                        batcher.Draw(texture, new Rectangle(x, y, drawWidth, drawHeight), hueVector, layerDepth);
                        return true;
                    }
                );
            }

            return base.AddToRenderLists(renderLists, x, y, ref layerDepthRef);
        }
    }
}
