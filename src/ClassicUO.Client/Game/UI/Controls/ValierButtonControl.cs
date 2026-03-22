using ClassicUO.Game.Scenes;
using ClassicUO.Game.UI.Valier;
using ClassicUO.Input;
using ClassicUO.Renderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ClassicUO.Game.UI.Controls
{
    internal class ValierButtonControl : Control
    {
        private readonly Action<int> _onClick;

        public ValierButtonControl(int buttonId, ValierAssetId assetId, int width, int height, Action<int> onClick = null)
        {
            ButtonId = buttonId;
            AssetId = assetId;
            Width = width;
            Height = height;
            _onClick = onClick;
            AcceptMouseInput = true;
        }

        public int ButtonId { get; }
        public ValierAssetId AssetId { get; set; }

        protected override void OnMouseUp(int x, int y, MouseButtonType button)
        {
            base.OnMouseUp(x, y, button);

            if (button == MouseButtonType.Left)
            {
                _onClick?.Invoke(ButtonId);
                OnButtonClick(ButtonId);
            }
        }

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
                Vector3 hueVector = ShaderHueTranslator.GetHueVector(0, false, MouseIsOver ? 1.0f : 0.8f, true);

                renderLists.AddGumpNoAtlas(batcher =>
                {
                    batcher.Draw(
                        SolidColorTextureCache.GetTexture(MouseIsOver ? Color.DarkGoldenrod : Color.DarkSlateGray),
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
