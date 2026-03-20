// SPDX-License-Identifier: BSD-2-Clause

using ClassicUO.Game.UI.Controls;
using ClassicUO.Utility;

namespace ClassicUO.Game.UI.Gumps.Login
{
    internal class LoginBackground : Gump
    {
        private readonly GumpPicTiled _background;
        private readonly Button _quitButton;

        public LoginBackground(World world) : base(world, 0, 0)
        {
            if (Client.Game.UO.Version >= ClientVersion.CV_706400)
            {
                _background = new GumpPicTiled
                (
                    0,
                    0,
                    Client.Game.Window.ClientBounds.Width,
                    Client.Game.Window.ClientBounds.Height,
                    0x0150
                ) { AcceptKeyboardInput = false };

                Add(_background);
                Add(new GumpPic(0, 4, 0x0151, 0) { AcceptKeyboardInput = false });
            }
            else
            {
                _background = new GumpPicTiled
                (
                    0,
                    0,
                    Client.Game.Window.ClientBounds.Width,
                    Client.Game.Window.ClientBounds.Height,
                    0x0E14
                ) { AcceptKeyboardInput = false };

                Add(_background);
                Add(new GumpPic(0, 0, 0x157C, 0) { AcceptKeyboardInput = false });
                Add(new GumpPic(0, 4, 0x15A0, 0) { AcceptKeyboardInput = false });

                Add
                (
                    _quitButton = new Button(0, 0x1589, 0x158B, 0x158A)
                    {
                        X = 555,
                        Y = 4,
                        ButtonAction = ButtonAction.Activate,
                        AcceptKeyboardInput = false
                    }
                );
            }

            CanCloseWithEsc = false;
            CanCloseWithRightClick = false;
            AcceptKeyboardInput = false;
            LayerOrder = UILayer.Under;

            RefreshLayout();
        }

        public void RefreshLayout()
        {
            if (_background != null)
            {
                _background.Width = Client.Game.Window.ClientBounds.Width;
                _background.Height = Client.Game.Window.ClientBounds.Height;
            }

            if (_quitButton != null)
            {
                _quitButton.X = System.Math.Max(555, Client.Game.Window.ClientBounds.Width - 85);
                _quitButton.Y = 4;
            }
        }

        public override void Update()
        {
            RefreshLayout();
            base.Update();
        }

        public override void OnButtonClick(int buttonID)
        {
            Client.Game.Exit();
        }
    }
}
