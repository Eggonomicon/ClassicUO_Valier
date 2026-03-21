// SPDX-License-Identifier: BSD-2-Clause

using System.Linq;
using System.Net.NetworkInformation;
using ClassicUO.Configuration;
using ClassicUO.Game.Scenes;
using ClassicUO.Game.UI.Controls;
using ClassicUO.Input;
using ClassicUO.Resources;
using ClassicUO.Utility;
using SDL3;

namespace ClassicUO.Game.UI.Gumps.Login
{
    internal class ServerSelectionGump : Gump
    {
        private const ushort SELECTED_COLOR = 0x0021;
        private const ushort NORMAL_COLOR = 0x034F;

        private readonly Label _headerLabel;
        private readonly Label _hintLabel;
        private readonly Button _nextButton;
        private readonly AlphaBlendControl _panelShade;
        private readonly ResizePic _panelFrame;
        private readonly Button _prevButton;
        private readonly ScrollArea _scrollArea;
        private readonly DataBox _serverBox;
        private readonly Label _selectedServerLabel;
        private readonly ServerEntryGump[] _serverEntries;
        private readonly Label _subHeaderLabel;

        private int _lastWindowHeight;
        private int _lastWindowWidth;

        public ServerSelectionGump(World world) : base(world, 0, 0)
        {
            WantUpdateSize = false;
            AcceptKeyboardInput = true;
            CanCloseWithRightClick = false;

            LoginScene loginScene = Client.Game.GetScene<LoginScene>();

            Add
            (
                _panelShade = new AlphaBlendControl(0.52f)
                {
                    X = 0,
                    Y = 0,
                    Width = 100,
                    Height = 100
                }
            );

            Add
            (
                _panelFrame = new ResizePic(0x13BE)
                {
                    X = 0,
                    Y = 0,
                    Width = 100,
                    Height = 100
                }
            );

            Add
            (
                _headerLabel = new Label("Choose your shard", false, 0x0481, font: 9)
                {
                    X = 0,
                    Y = 0
                }
            );

            Add
            (
                _subHeaderLabel = new Label("ValierUO server selection", false, 0x0481, font: 9)
                {
                    X = 0,
                    Y = 0
                }
            );

            Add
            (
                _hintLabel = new Label("Double-click a shard or press Enter to continue.", false, 0x034F, font: 9)
                {
                    X = 0,
                    Y = 0
                }
            );

            Add
            (
                _selectedServerLabel = new Label(string.Empty, false, 0x0481, font: 9)
                {
                    X = 0,
                    Y = 0
                }
            );

            _scrollArea = new ScrollArea(0, 0, 100, 100, true);
            _scrollArea.ScissorRectangle.Y = 12;
            _scrollArea.ScissorRectangle.Height = -24;

            _serverBox = new DataBox(0, 0, 1, 1) { WantUpdateSize = true };
            _serverEntries = loginScene.Servers.Select(s => new ServerEntryGump(s, 9, NORMAL_COLOR, SELECTED_COLOR)).ToArray();

            foreach (ServerEntryGump entry in _serverEntries)
            {
                _serverBox.Add(entry);
            }

            _serverBox.ReArrangeChildren();

            Add(_scrollArea);
            _scrollArea.Add(_serverBox);

            Add
            (
                _prevButton = new Button((int)Buttons.Prev, 0x05CA, 0x05C9, 0x05C8)
                {
                    X = 0,
                    Y = 0,
                    ButtonAction = ButtonAction.Activate
                }
            );

            Add
            (
                _nextButton = new Button((int)Buttons.Next, 0x05CD, 0x05CC, 0x05CB)
                {
                    X = 0,
                    Y = 0,
                    ButtonAction = ButtonAction.Activate
                }
            );

            UpdateLayout(force: true);
        }

        public override void Update()
        {
            base.Update();
            UpdateLayout();

            LoginScene loginScene = Client.Game.GetScene<LoginScene>();

            if (loginScene.Servers.Length != 0)
            {
                int index = loginScene.GetServerIndexFromSettings();
                _selectedServerLabel.Text = $"Current shard: {loginScene.Servers[index].Name}";
            }
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
                case Buttons.Next:
                    if (loginScene.Servers.Length != 0)
                    {
                        int index = loginScene.GetServerIndexFromSettings();
                        loginScene.SelectServer((byte)loginScene.Servers[index].Index);
                    }

                    break;

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

        private void UpdateLayout(bool force = false)
        {
            int windowWidth = Client.Game.Window.ClientBounds.Width;
            int windowHeight = Client.Game.Window.ClientBounds.Height;

            if (!force && windowWidth == _lastWindowWidth && windowHeight == _lastWindowHeight)
            {
                return;
            }

            _lastWindowWidth = windowWidth;
            _lastWindowHeight = windowHeight;

            Width = windowWidth;
            Height = windowHeight;

            int panelWidth = System.Math.Clamp((int)(windowWidth * 0.60f), 760, 1120);
            int panelHeight = System.Math.Clamp((int)(windowHeight * 0.62f), 420, 760);

            int panelX = (windowWidth - panelWidth) / 2;
            int panelY = (windowHeight - panelHeight) / 2;

            _panelShade.X = panelX;
            _panelShade.Y = panelY;
            _panelShade.Width = panelWidth;
            _panelShade.Height = panelHeight;

            _panelFrame.X = panelX;
            _panelFrame.Y = panelY;
            _panelFrame.Width = panelWidth;
            _panelFrame.Height = panelHeight;

            _headerLabel.X = panelX + 28;
            _headerLabel.Y = panelY + 20;

            _subHeaderLabel.X = panelX + 30;
            _subHeaderLabel.Y = panelY + 48;

            _hintLabel.X = panelX + 30;
            _hintLabel.Y = panelY + 76;

            int listX = panelX + 30;
            int listY = panelY + 108;
            int listWidth = panelWidth - 60;
            int listHeight = panelHeight - 180;

            _scrollArea.X = listX;
            _scrollArea.Y = listY;
            _scrollArea.Width = listWidth;
            _scrollArea.Height = listHeight;
            _scrollArea.ScissorRectangle.Y = 12;
            _scrollArea.ScissorRectangle.Height = -24;

            _serverBox.Width = listWidth - 24;

            foreach (ServerEntryGump entry in _serverEntries)
            {
                entry.SetLayout(_serverBox.Width - 12);
            }

            _serverBox.ReArrangeChildren();

            _selectedServerLabel.X = panelX + 30;
            _selectedServerLabel.Y = panelY + panelHeight - 52;

            _prevButton.X = panelX + panelWidth - 118;
            _prevButton.Y = panelY + panelHeight - 64;

            _nextButton.X = panelX + panelWidth - 62;
            _nextButton.Y = panelY + panelHeight - 60;
        }

        private enum Buttons
        {
            Prev,
            Next,
            Server = 100
        }

        private class ServerEntryGump : Control
        {
            private readonly int _buttonId;
            private readonly ServerListEntry _entry;
            private readonly HoveredLabel _serverName;
            private readonly HoveredLabel _serverPacketLoss;
            private readonly HoveredLabel _serverPing;
            private uint _pingCheckTime;

            public ServerEntryGump(ServerListEntry entry, byte font, ushort normalHue, ushort selectedHue)
            {
                _entry = entry;
                _buttonId = entry.Index;

                Add
                (
                    _serverName = new HoveredLabel(entry.Name, false, normalHue, selectedHue, selectedHue, font: font)
                    {
                        X = 16,
                        Y = 8,
                        AcceptMouseInput = false
                    }
                );

                Add
                (
                    _serverPing = new HoveredLabel(CUOEnviroment.NoServerPing ? string.Empty : "-", false, normalHue, selectedHue, selectedHue, font: font)
                    {
                        X = 0,
                        Y = 8,
                        AcceptMouseInput = false
                    }
                );

                Add
                (
                    _serverPacketLoss = new HoveredLabel(CUOEnviroment.NoServerPing ? string.Empty : "-", false, normalHue, selectedHue, selectedHue, font: font)
                    {
                        X = 0,
                        Y = 8,
                        AcceptMouseInput = false
                    }
                );

                AcceptMouseInput = true;
                Width = 420;
                Height = 34;
                WantUpdateSize = false;

                SetLayout(420);
            }

            public void SetLayout(int width)
            {
                Width = width;
                Height = 34;

                _serverName.X = 18;
                _serverName.Y = 8;

                _serverPing.X = System.Math.Max(Width - 210, 260);
                _serverPing.Y = 8;

                _serverPacketLoss.X = System.Math.Max(Width - 100, 360);
                _serverPacketLoss.Y = 8;
            }

            protected override void OnMouseEnter(int x, int y)
            {
                base.OnMouseEnter(x, y);
                _serverName.IsSelected = true;
                _serverPacketLoss.IsSelected = true;
                _serverPing.IsSelected = true;
            }

            protected override void OnMouseExit(int x, int y)
            {
                base.OnMouseExit(x, y);
                _serverName.IsSelected = false;
                _serverPacketLoss.IsSelected = false;
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

                if (CUOEnviroment.NoServerPing == false && _pingCheckTime < Time.Ticks)
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

                    _serverPacketLoss.Text = $"{_entry.PacketLoss}%";
                }
            }
        }
    }
}
