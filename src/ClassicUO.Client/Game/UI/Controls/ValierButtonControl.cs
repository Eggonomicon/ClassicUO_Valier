// SPDX-License-Identifier: BSD-2-Clause

using ClassicUO.Game.Scenes;
using ClassicUO.Game.UI.Valier;
using ClassicUO.Input;
using ClassicUO.Renderer;
using Microsoft.Xna.Framework;

namespace ClassicUO.Game.UI.Controls
{
    internal sealed class ValierButtonControl : Control
    {
        private bool _isHovered;
        private bool _isPressed;

        public ValierButtonControl(int buttonId, ValierAssetId normal, ValierAssetId hover, ValierAssetId pressed, int width = 0, int height = 0)
        {
            ButtonID = buttonId;
            NormalAsset = normal;
            HoverAsset = hover;
            PressedAsset = pressed;
            Width = width;
            Height = height;
            AcceptMouseInput = true;
            CanMove = false;
        }

        public int ButtonID { get; }
        public ValierAssetId NormalAsset { get; set; }
        public ValierAssetId HoverAsset { get; set; }
        public ValierAssetId PressedAsset { get; set; }

        public override bool Contains(int x, int y)
        {
            return !IsDisposed && new Rectangle(0, 0, Width, Height).Contains(x, y);
        }

        protected override void OnMouseEnter(int x, int y)
        {
            base.OnMouseEnter(x, y);
            _isHovered = true;
        }

        protected override void OnMouseExit(int x, int y)
        {
            base.OnMouseExit(x, y);
            _isHovered = false;
            _isPressed = false;
        }

        protected override void OnMouseDown(int x, int y, MouseButtonType button)
        {
            base.OnMouseDown(x, y, button);

            if (button == MouseButtonType.Left)
            {
                _isPressed = true;
            }
        }

        protected override void OnMouseUp(int x, int y, MouseButtonType button)
        {
            base.OnMouseUp(x, y, button);

            if (button == MouseButtonType.Left)
            {
                bool shouldActivate = _isPressed;
                _isPressed = false;

                if (shouldActivate)
                {
                    OnButtonClick(ButtonID);
                }
            }
        }

        public override bool AddToRenderLists(RenderLists renderLists, int x, int y, ref float layerDepthRef)
        {
            float layerDepth = layerDepthRef;

            if (IsDisposed)
            {
                return false;
            }

            ValierAssetId asset = _isPressed ? PressedAsset : (_isHovered ? HoverAsset : NormalAsset);

            if (ValierTextureCache.TryGet(asset, out var texture))
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
