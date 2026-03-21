// SPDX-License-Identifier: BSD-2-Clause

using ClassicUO.Game.UI.Controls;

namespace ClassicUO.Game.UI.Gumps.Login
{
    internal class LoginBackground : Gump
    {
        private readonly AlphaBlendControl _overlay;

        public LoginBackground(World world) : base(world, 0, 0)
        {
            WantUpdateSize = false;
            Width = Client.Game.Window.ClientBounds.Width;
            Height = Client.Game.Window.ClientBounds.Height;

            Add
            (
                _overlay = new AlphaBlendControl(0.18f)
                {
                    X = 0,
                    Y = 0,
                    Width = Width,
                    Height = Height
                }
            );

            CanCloseWithEsc = false;
            CanCloseWithRightClick = false;
            AcceptKeyboardInput = false;
            AcceptMouseInput = false;
            LayerOrder = UILayer.Under;
        }

        public override void Update()
        {
            base.Update();

            int width = Client.Game.Window.ClientBounds.Width;
            int height = Client.Game.Window.ClientBounds.Height;

            if (Width != width)
            {
                Width = width;
            }

            if (Height != height)
            {
                Height = height;
            }

            if (_overlay.Width != width)
            {
                _overlay.Width = width;
            }

            if (_overlay.Height != height)
            {
                _overlay.Height = height;
            }
        }
    }
}
