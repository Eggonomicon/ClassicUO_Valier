using ClassicUO.Game.Scenes;
using ClassicUO.Game.UI.Valier;
using ClassicUO.Renderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClassicUO.Game.UI.Controls
{
    internal class ValierPanelControl : Control
    {
        public ValierPanelControl(ValierAssetId assetId, int width, int height)
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
                Vector3 hueVector = ShaderHueTranslator.GetHueVector(0, false, Alpha, true);
                Rectangle source = new Rectangle(0, 0, texture.Width, texture.Height);

                renderLists.AddGumpNoAtlas(batcher =>
                {
                    batcher.Draw(texture, new Rectangle(x, y, Width, Height), source, hueVector, layerDepth);
                    return true;
                });
            }
            else
            {
                Vector3 hueVector = ShaderHueTranslator.GetHueVector(0, false, 0.75f, true);

                renderLists.AddGumpNoAtlas(batcher =>
                {
                    batcher.Draw(
                        SolidColorTextureCache.GetTexture(Color.Black),
                        new Rectangle(x, y, Width, Height),
                        hueVector,
                        layerDepth
                    );
                    return true;
                });
            }

            return base.AddToRenderLists(renderLists, x, y, ref layerDepthRef);
        }
    }
}
