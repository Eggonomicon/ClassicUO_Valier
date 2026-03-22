using ClassicUO.Game.Managers;
// SPDX-License-Identifier: BSD-2-Clause

using ClassicUO.Renderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace ClassicUO.Game.UI.Controls
{
    internal sealed class TextureImageControl : Control
    {
        private Texture2D _texture;

        public TextureImageControl(ReadOnlySpan<byte> bytes, int width = 0, int height = 0)
        {
            using var ms = new MemoryStream(bytes.ToArray());
            _texture = Texture2D.FromStream(Client.Game.GraphicsDevice, ms);

            Width = width > 0 ? width : _texture.Width;
            Height = height > 0 ? height : _texture.Height;

            AcceptMouseInput = false;
            AcceptKeyboardInput = false;
        }

        public float ImageAlpha { get; set; } = 1.0f;

        public override bool AddToRenderLists(ClassicUO.Game.Scenes.RenderLists renderLists, int x, int y, ref float layerDepthRef)
        {
            if (IsDisposed || _texture == null)
            {
                return false;
            }

            float layerDepth = layerDepthRef;
            Vector3 hueVector = ShaderHueTranslator.GetHueVector(0, false, ImageAlpha);

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

            return base.AddToRenderLists(renderLists, x, y, ref layerDepthRef);
        }

        public override void Dispose()
        {
            _texture?.Dispose();
            _texture = null;

            base.Dispose();
        }
    }
}
