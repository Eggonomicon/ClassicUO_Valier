// SPDX-License-Identifier: BSD-2-Clause

using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using ClassicUO.Assets;
using ClassicUO.Configuration;
using ClassicUO.Game.Scenes;
using ClassicUO.Game.UI.Controls;
using ClassicUO.Input;
using ClassicUO.Resources;
using ClassicUO.Utility;
using SDL3;

namespace ClassicUO.Game.UI.Gumps.Login
{
    internal sealed class ServerSelectionGump : Gump
    {
        private const ushort SELECTED_COLOR = 0x0021;
        private const ushort NORMAL_COLOR = 0x0481;
        private const int PANEL_WIDTH = 720;
        private const int PANEL_HEIGHT = 470;
        private const int RIGHT_MARGIN = 36;

        private int _lastBoundsWidth = -1;
        private int _lastBoundsHeight = -1;

        public ServerSelectionGump(World world) : base(world, 0, 0)
        {
            Add(new TextureImageControl(Loader.GetValierServerFrame(), PANEL_WIDTH, PANEL_HEIGHT));
            Add(new TextureImageControl(Loader.GetCuoLogo(), 86, 86) { X = 34, Y = 24 });

            Add(new Label("Choose your shard", false, 0x0481, font: 9) { X = 130, Y = 38 });
            Add(new Label("ValierUO server selection", false, 0x0481, font: 9) { X = 130, Y = 74 });
            Add(new Label("Double-click a shard or press Enter to continue.", false, 0x0481, font: 9) { X = 42, Y = 118 });

            Add(new Label("Shard", false, 0x0481, font: 9) { X = 58, Y = 160 });
            Add(new Label("Latency", false, 0x0481, font: 9) { X = 530, Y = 160 });
            Add(new Label("Loss", false, 0x0481, font: 9) { X = 616, Y = 160 });

            ScrollArea scrollArea = new ScrollArea(48, 188, 624, 156, true);
            DataBox databox = new DataBox(0, 0, 1, 1) { WantUpdateSize = true };
            LoginScene loginScene = Client.Game.GetScene<LoginScene>();

            scrollArea.ScissorRectangle.Y = 8;
            scrollArea.ScissorRectangle.Height = -16;

            foreach (ServerListEntry server in loginScene.Servers)
            {
                databox.Add(new ServerEntryControl(server, 9, NORMAL_COLOR, SELECTED_COLOR));
            }

            databox.ReArrangeChildren();

            Add(scrollArea);
            scrollArea.Add(databox);

            if (loginScene.Servers.Length != 0)
            {
                int index = loginScene.GetServerIndexFromSettings();
                Add(new Label($"Current shard: {loginScene.Servers[index].Name}", false, 0x0481, font: 9) { X = 46, Y = 390 });
            }

            Add(new ImageButtonControl((int)Buttons.Quit, Loader.GetValierQuitButton()) { X = 576, Y = 346 });

            AcceptKeyboardInput = true;
            CanCloseWithRightClick = false;

            UpdatePanelPosition();
        }

        public override void Update()
        {
            base.Update();
            UpdatePanelPosition();
            Client.Game.Window.Title = $"ValierUO - {CUOEnviroment.Version}";
        }

        private void UpdatePanelPosition()
        {
            int width = Client.Game.ClientBounds.Width;
            int height = Client.Game.ClientBounds.Height;

            if (_lastBoundsWidth == width && _lastBoundsHeight == height)
            {
                return;
            }

            _lastBoundsWidth = width;
            _lastBoundsHeight = height;

            X = System.Math.Max(20, width - PANEL_WIDTH - RIGHT_MARGIN);
            Y = System.Math.Max(20, (height - PANEL_HEIGHT) / 2);
        }

        public override void OnButtonClick(int buttonID)
        {
            LoginScene loginScene = Client.Game.GetScene<LoginScene>();

            if (buttonID >= (int)Buttons.Server)
            {
                int index = buttonID - (int)Buttons.Server;
                loginScene.SelectServer((byte)index);
                return;
            }

            switch ((Buttons)buttonID)
            {
                case Buttons.Quit:
                case Buttons.Prev:
                    loginScene.StepBack();
                    break;
            }
        }

        protected override void OnKeyDown(SDL.SDL_Keycode key, SDL.SDL_Keymod mod)
        {
            if (key == SDL.SDL_Keycode.SDLK_RETURN || key == SDL.SDL_Keycode.SDLK_KP_ENTER)
            {
                LoginScene loginScene = Client.Game.GetScene<LoginScene>();

                if (loginScene.Servers?.Any(s => s != null) ?? false)
                {
                    int index = loginScene.GetServerIndexFromSettings();
                    loginScene.SelectServer((byte)loginScene.Servers[index].Index);
                }
            }
        }

        private enum Buttons
        {
            Prev = 1,
            Quit = 2,
            Server = 99
        }

        private sealed class ServerEntryControl : Control
        {
            private readonly int _buttonId;
            private readonly ServerListEntry _entry;
            private readonly HoveredLabel _serverLoss;
            private readonly HoveredLabel _serverPing;
            private readonly HoveredLabel _serverName;
            private uint _pingCheckTime;

            public ServerEntryControl(ServerListEntry entry, byte font, ushort normalHue, ushort selectedHue)
            {
                _entry = entry;
                _buttonId = entry.Index;

                Add
                (
                    _serverName = new HoveredLabel(entry.Name, false, normalHue, selectedHue, selectedHue, font: font)
                    {
                        X = 10,
                        Y = 4,
                        AcceptMouseInput = false
                    }
                );

                Add
                (
                    _serverPing = new HoveredLabel(CUOEnviroment.NoServerPing ? string.Empty : "-", false, normalHue, selectedHue, selectedHue, font: font)
                    {
                        X = 482,
                        Y = 4,
                        AcceptMouseInput = false
                    }
                );

                Add
                (
                    _serverLoss = new HoveredLabel(CUOEnviroment.NoServerPing ? string.Empty : "-", false, normalHue, selectedHue, selectedHue, font: font)
                    {
                        X = 568,
                        Y = 4,
                        AcceptMouseInput = false
                    }
                );

                AcceptMouseInput = true;
                Width = 600;
                Height = 32;
                WantUpdateSize = false;
            }

            protected override void OnMouseEnter(int x, int y)
            {
                base.OnMouseEnter(x, y);
                _serverName.IsSelected = true;
                _serverLoss.IsSelected = true;
                _serverPing.IsSelected = true;
            }

            protected override void OnMouseExit(int x, int y)
            {
                base.OnMouseExit(x, y);
                _serverName.IsSelected = false;
                _serverLoss.IsSelected = false;
                _serverPing.IsSelected = false;
            }

            protected override void OnMouseUp(int x, int y, MouseButtonType button)
            {
                if (button == MouseButtonType.Left)
                {
                    OnButtonClick((int)Buttons.Server + _buttonId);
                }
            }

            public override void Update()
            {
                base.Update();

                if (!CUOEnviroment.NoServerPing && _pingCheckTime < Time.Ticks)
                {
                    _pingCheckTime = Time.Ticks + 2000;
                    _entry.DoPing();

                    switch (_entry.PingStatus)
                    {
                        case IPStatus.Success:
                            _serverPing.Text = _entry.Ping == -1 ? "-" : _entry.Ping.ToString();
                            break;
                        case IPStatus.DestinationNetworkUnreachable:
                        case IPStatus.DestinationHostUnreachable:
                        case IPStatus.DestinationProtocolUnreachable:
                        case IPStatus.DestinationPortUnreachable:
                        case IPStatus.DestinationUnreachable:
                            _serverPing.Text = "unreach.";
                            break;
                        case IPStatus.TimedOut:
                            _serverPing.Text = "time out";
                            break;
                        default:
                            _serverPing.Text = $"unk. [{(int)_entry.PingStatus}]";
                            break;
                    }

                    _serverLoss.Text = $"{_entry.PacketLoss}%";
                }
            }
        }
    }
}
