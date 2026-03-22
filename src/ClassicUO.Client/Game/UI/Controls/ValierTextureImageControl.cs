using ClassicUO.Game.Scenes;
using ClassicUO.Game.UI.Valier;
using ClassicUO.Renderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        public override bool AddToRenderLists(RenderLists renderLists, int x, int y, ref float layerDepthRef)
        {
            float layerDepth = layerDepthRef;

            if (IsDisposed)
            {
                return false;
            }

            if (ValierTextureCache.TryGet(AssetId, out Texture2D texture))
            {
                int drawWidth = Width > 0 ? Width : texture.Width;
                int drawHeight = Height > 0 ? Height : texture.Height;

                if (Width <= 0)
                {
                    Width = drawWidth;
                }

                if (Height <= 0)
                {
                    Height = drawHeight;
                }

                Vector3 hueVector = ShaderHueTranslator.GetHueVector(0, false, Alpha, true);
                Rectangle source = new Rectangle(0, 0, texture.Width, texture.Height);

                renderLists.AddGumpNoAtlas(batcher =>
                {
                    batcher.Draw(texture, new Rectangle(x, y, drawWidth, drawHeight), source, hueVector, layerDepth);
                    return true;
                });
            }

            return base.AddToRenderLists(renderLists, x, y, ref layerDepthRef);
        }
    }
