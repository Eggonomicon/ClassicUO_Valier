// SPDX-License-Identifier: BSD-2-Clause

using ClassicUO.Game.UI.Controls;
using ClassicUO.Resources;

namespace ClassicUO.Game.UI.Gumps.Login
{
    internal sealed class LoginBackground : Gump
    {
        private readonly TextureImageControl _background;

        public LoginBackground(World world) : base(world, 0, 0)
        {
            _background = Add
            (
                new TextureImageControl
                (
                    Loader.GetBackgroundImage(),
                    Client.Game.ClientBounds.Width,
                    Client.Game.ClientBounds.Height
                )
                {
                    AcceptKeyboardInput = false,
                    AcceptMouseInput = false
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

            X = 0;
            Y = 0;
            Width = Client.Game.ClientBounds.Width;
            Height = Client.Game.ClientBounds.Height;

            _background.Width = Width;
            _background.Height = Height;

            Client.Game.Window.Title = $"ValierUO - {CUOEnviroment.Version}";
        }
    }
}
