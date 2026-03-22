using ClassicUO.Game.Managers;
// SPDX-License-Identifier: BSD-2-Clause

using ClassicUO.Input;
using ClassicUO.Renderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace ClassicUO.Game.UI.Controls
{
    internal sealed class ImageButtonControl : Control
    {
        private Texture2D _texture;
        private bool _isHovered;

        public ImageButtonControl(int buttonId, ReadOnlySpan<byte> bytes, int width = 0, int height = 0)
        {
            ButtonId = buttonId;

            using var ms = new MemoryStream(bytes.ToArray());
            _texture = Texture2D.FromStream(Client.Game.GraphicsDevice, ms);

            Width = width > 0 ? width : _texture.Width;
            Height = height > 0 ? height : _texture.Height;
        }

        public int ButtonId { get; }

        public override bool Contains(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Width && y < Height;
        }

        protected override void OnMouseEnter(int x, int y)
        {
            _isHovered = true;
            base.OnMouseEnter(x, y);
        }

        protected override void OnMouseExit(int x, int y)
        {
            _isHovered = false;
            base.OnMouseExit(x, y);
        }

        protected override void OnMouseUp(int x, int y, MouseButtonType button)
        {
            base.OnMouseUp(x, y, button);

            if (button == MouseButtonType.Left)
            {
                OnButtonClick(ButtonId);
            }
        }

        public override bool AddToRenderLists(ClassicUO.Game.Scenes.RenderLists renderLists, int x, int y, ref float layerDepthRef)
        {
            if (IsDisposed || _texture == null)
            {
                return false;
            }

            float layerDepth = layerDepthRef;
            float alpha = _isHovered ? 1.0f : 0.92f;
            Vector3 hueVector = ShaderHueTranslator.GetHueVector(0, false, alpha);

            renderLists.AddGumpNoAtlas
            (
                batcher =>
                {
                    batcher.Draw
                    (
                        _texture,
                        new Rectangle(x, y, Width, Height),
                        hueVector,
                        layerDepth
                    );

                    return true;
                }
            );

            return true;
        }

        public override void Dispose()
        {
            _texture?.Dispose();
            _texture = null;

            base.Dispose();
        }
    }
}
