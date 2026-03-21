// SPDX-License-Identifier: BSD-2-Clause

using ClassicUO.Configuration;
using ClassicUO.Game.Managers;
using ClassicUO.Game.Scenes;
using ClassicUO.Game.UI.Controls;
using ClassicUO.Input;
using ClassicUO.Renderer;
using ClassicUO.Resources;
using ClassicUO.Utility;
using Microsoft.Xna.Framework;
using SDL3;

namespace ClassicUO.Game.UI.Gumps.Login
{
    internal class LoginGump : Gump
    {
        private readonly Checkbox _checkboxAutologin;
        private readonly Checkbox _checkboxSaveAccount;
        private readonly Checkbox _checkboxMusic;
        private readonly Button _creditsButton;
        private readonly Label _footerCenterLine1;
        private readonly Label _footerCenterLine2;
        private readonly HtmlControl _footerLeft;
        private readonly HtmlControl _footerRightLine1;
        private readonly HtmlControl _footerRightLine2;
        private readonly Label _headerLine;
        private readonly Button _loginButton;
        private readonly Label _loginButtonLabel;
        private readonly HSliderBar _musicSlider;
        private readonly Label _passwordLabel;
        private readonly PasswordStbTextBox _passwordTextBox;
        private readonly Button _quitButton;
        private readonly AlphaBlendControl _panelShade;
        private readonly ResizePic _panelFrame;
        private readonly ResizePic _accountBackground;
        private readonly ResizePic _passwordBackground;
        private readonly Label _serverHint;
        private readonly Label _subtitleLabel;
        private readonly Label _titleLabel;
        private readonly StbTextBox _usernameTextBox;

        private int _lastWindowHeight;
        private int _lastWindowWidth;

        public LoginGump(World world, LoginScene scene) : base(world, 0, 0)
        {
            CanCloseWithRightClick = false;
            AcceptKeyboardInput = true;
            WantUpdateSize = false;

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
                _titleLabel = new Label("ValierUO", false, 0x0481, font: 9)
                {
                    X = 0,
                    Y = 0
                }
            );

            Add
            (
                _subtitleLabel = new Label("Enter the shard", false, 0x0481, font: 9)
                {
                    X = 0,
                    Y = 0
                }
            );

            Add
            (
                _serverHint = new Label($"{Settings.GlobalSettings.IP}:{Settings.GlobalSettings.Port}", false, 0x034F, font: 9)
                {
                    X = 0,
                    Y = 0
                }
            );

            Add
            (
                _headerLine = new Label("Account access", false, 0x0481, font: 9)
                {
                    X = 0,
                    Y = 0
                }
            );

            Add
            (
                _accountBackground = new ResizePic(0x0BB8)
                {
                    X = 0,
                    Y = 0,
                    Width = 100,
                    Height = 36
                }
            );

            Add
            (
                _passwordBackground = new ResizePic(0x0BB8)
                {
                    X = 0,
                    Y = 0,
                    Width = 100,
                    Height = 36
                }
            );

            Add
            (
                _usernameTextBox = new StbTextBox(9, 64, 512, false, hue: 0x034F)
                {
                    X = 0,
                    Y = 0,
                    Width = 100,
                    Height = 26
                }
            );
            _usernameTextBox.SetText(Settings.GlobalSettings.Username);

            Add
            (
                _passwordTextBox = new PasswordStbTextBox(9, 64, 512, false, hue: 0x034F)
                {
                    X = 0,
                    Y = 0,
                    Width = 100,
                    Height = 26
                }
            );
            _passwordTextBox.RealText = Crypter.Decrypt(Settings.GlobalSettings.Password);

            Add
            (
                _passwordLabel = new Label("Password", false, 0x0481, font: 9)
                {
                    X = 0,
                    Y = 0
                }
            );

            Add
            (
                _checkboxAutologin = new Checkbox(0x00D2, 0x00D3, ResGumps.Autologin, 9, 0x0481, false)
                {
                    X = 0,
                    Y = 0,
                    IsChecked = Settings.GlobalSettings.AutoLogin
                }
            );

            Add
            (
                _checkboxSaveAccount = new Checkbox(0x00D2, 0x00D3, ResGumps.SaveAccount, 9, 0x0481, false)
                {
                    X = 0,
                    Y = 0,
                    IsChecked = Settings.GlobalSettings.SaveAccount
                }
            );

            Add
            (
                _checkboxMusic = new Checkbox(0x00D2, 0x00D3, "Music", 9, 0x0481, false)
                {
                    X = 0,
                    Y = 0,
                    IsChecked = Settings.GlobalSettings.LoginMusic
                }
            );

            Add
            (
                _musicSlider = new HSliderBar
                (
                    0,
                    0,
                    140,
                    0,
                    100,
                    Settings.GlobalSettings.LoginMusicVolume,
                    HSliderBarStyle.MetalWidgetRecessedBar,
                    true,
                    9,
                    0x0481,
                    false
                )
            );

            Add
            (
                _loginButton = new Button((int)Buttons.Login, 0x05CD, 0x05CC, 0x05CB)
                {
                    X = 0,
                    Y = 0,
                    ButtonAction = ButtonAction.Activate
                }
            );

            Add
            (
                _loginButtonLabel = new Label("Play", false, 0x0481, font: 9)
                {
                    X = 0,
                    Y = 0
                }
            );

            Add
            (
                _quitButton = new Button((int)Buttons.Quit, 0x05CA, 0x05C9, 0x05C8)
                {
                    X = 0,
                    Y = 0,
                    ButtonAction = ButtonAction.Activate
                }
            );

            Add
            (
                _creditsButton = new Button((int)Buttons.Credits, 0x05D0, 0x05CF, 0x05CE)
                {
                    X = 0,
                    Y = 0,
                    ButtonAction = ButtonAction.Activate
                }
            );

            Add
            (
                _footerLeft = new HtmlControl
                (
                    0,
                    0,
                    180,
                    18,
                    false,
                    false,
                    false,
                    "<body link=\"#FF00FF00\" vlink=\"#FF00FF00\"><a href=\"https://www.classicuo.eu/support.php\">Support ClassicUO!",
                    0x32,
                    true,
                    isunicode: true,
                    style: FontStyle.BlackBorder
                )
            );

            Add
            (
                _footerRightLine1 = new HtmlControl
                (
                    0,
                    0,
                    100,
                    18,
                    false,
                    false,
                    false,
                    "<body link=\"#FF00FF00\" vlink=\"#FF00FF00\"><a href=\"https://www.classicuo.eu\">Website",
                    0x32,
                    true,
                    isunicode: true,
                    style: FontStyle.BlackBorder
                )
            );

            Add
            (
                _footerRightLine2 = new HtmlControl
                (
                    0,
                    0,
                    110,
                    18,
                    false,
                    false,
                    false,
                    "<body link=\"#FF00FF00\" vlink=\"#FF00FF00\"><a href=\"https://discord.gg/VdyCpjQ\">Join Discord",
                    0x32,
                    true,
                    isunicode: true,
                    style: FontStyle.BlackBorder
                )
            );

            Add
            (
                _footerCenterLine1 = new Label($"UO Version {Settings.GlobalSettings.ClientVersion}.", false, 0x0481, font: 9)
                {
                    X = 0,
                    Y = 0
                }
            );

            Add
            (
                _footerCenterLine2 = new Label($"ValierUO Client {CUOEnviroment.Version}", false, 0x0481, font: 9)
                {
                    X = 0,
                    Y = 0
                }
            );

            _checkboxMusic.ValueChanged += (sender, e) =>
            {
                Settings.GlobalSettings.LoginMusic = _checkboxMusic.IsChecked;
                Client.Game.Audio.UpdateCurrentMusicVolume(true);
                _musicSlider.IsVisible = Settings.GlobalSettings.LoginMusic;
            };

            _musicSlider.ValueChanged += (sender, e) =>
            {
                Settings.GlobalSettings.LoginMusicVolume = _musicSlider.Value;
                Client.Game.Audio.UpdateCurrentMusicVolume(true);
            };

            _musicSlider.IsVisible = Settings.GlobalSettings.LoginMusic;

            if (!string.IsNullOrEmpty(_usernameTextBox.Text))
            {
                _passwordTextBox.SetKeyboardFocus();
            }
            else
            {
                _usernameTextBox.SetKeyboardFocus();
            }

            UpdateLayout(force: true);
        }

        public override void OnKeyboardReturn(int textID, string text)
        {
            SaveCheckboxStatus();

            LoginScene loginScene = Client.Game.GetScene<LoginScene>();

            if (loginScene.CurrentLoginStep == LoginSteps.Main)
            {
                loginScene.Connect(_usernameTextBox.Text, _passwordTextBox.RealText);
            }
        }

        public override void Update()
        {
            if (IsDisposed)
            {
                return;
            }

            base.Update();
            UpdateLayout();

            if (_passwordTextBox.HasKeyboardFocus)
            {
                if (_passwordTextBox.Hue != 0x0021)
                {
                    _passwordTextBox.Hue = 0x0021;
                }
            }
            else if (_passwordTextBox.Hue != 0)
            {
                _passwordTextBox.Hue = 0;
            }

            if (_usernameTextBox.HasKeyboardFocus)
            {
                if (_usernameTextBox.Hue != 0x0021)
                {
                    _usernameTextBox.Hue = 0x0021;
                }
            }
            else if (_usernameTextBox.Hue != 0)
            {
                _usernameTextBox.Hue = 0;
            }

            _serverHint.Text = $"{Settings.GlobalSettings.IP}:{Settings.GlobalSettings.Port}";
            _footerCenterLine1.Text = $"UO Version {Settings.GlobalSettings.ClientVersion}.";
        }

        public override void OnButtonClick(int buttonID)
        {
            switch ((Buttons)buttonID)
            {
                case Buttons.Login:
                    SaveCheckboxStatus();

                    if (!_usernameTextBox.IsDisposed)
                    {
                        Client.Game.GetScene<LoginScene>().Connect(_usernameTextBox.Text, _passwordTextBox.RealText);
                    }

                    break;

                case Buttons.Quit:
                    Client.Game.Exit();
                    break;

                case Buttons.Credits:
                    UIManager.Add(new CreditsGump(World));
                    break;
            }
        }

        private void SaveCheckboxStatus()
        {
            Settings.GlobalSettings.SaveAccount = _checkboxSaveAccount.IsChecked;
            Settings.GlobalSettings.AutoLogin = _checkboxAutologin.IsChecked;
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

            int panelWidth = Math.Clamp((int)(windowWidth * 0.52f), 720, 980);
            int panelHeight = Math.Clamp((int)(windowHeight * 0.44f), 340, 430);

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

            _titleLabel.X = panelX + 28;
            _titleLabel.Y = panelY + 20;

            _subtitleLabel.X = panelX + 30;
            _subtitleLabel.Y = panelY + 48;

            _serverHint.X = panelX + 30;
            _serverHint.Y = panelY + 74;

            _headerLine.X = panelX + 30;
            _headerLine.Y = panelY + 112;

            int inputLabelX = panelX + 34;
            int inputBackgroundX = panelX + 220;
            int inputWidth = panelWidth - 260;

            _accountBackground.X = inputBackgroundX;
            _accountBackground.Y = panelY + 108;
            _accountBackground.Width = inputWidth;
            _accountBackground.Height = 36;

            _usernameTextBox.X = inputBackgroundX + 10;
            _usernameTextBox.Y = panelY + 113;
            _usernameTextBox.Width = inputWidth - 22;
            _usernameTextBox.Height = 24;

            _passwordLabel.X = inputLabelX;
            _passwordLabel.Y = panelY + 164;

            _passwordBackground.X = inputBackgroundX;
            _passwordBackground.Y = panelY + 158;
            _passwordBackground.Width = inputWidth;
            _passwordBackground.Height = 36;

            _passwordTextBox.X = inputBackgroundX + 10;
            _passwordTextBox.Y = panelY + 163;
            _passwordTextBox.Width = inputWidth - 22;
            _passwordTextBox.Height = 24;

            _checkboxAutologin.X = panelX + 30;
            _checkboxAutologin.Y = panelY + panelHeight - 92;

            _checkboxSaveAccount.X = _checkboxAutologin.X + _checkboxAutologin.Width + 18;
            _checkboxSaveAccount.Y = _checkboxAutologin.Y;

            _checkboxMusic.X = _checkboxSaveAccount.X + _checkboxSaveAccount.Width + 18;
            _checkboxMusic.Y = _checkboxAutologin.Y;

            _musicSlider.X = _checkboxMusic.X + _checkboxMusic.Width + 14;
            _musicSlider.Y = _checkboxMusic.Y + 4;

            int buttonRowY = panelY + panelHeight - 142;

            _quitButton.X = panelX + 30;
            _quitButton.Y = buttonRowY;

            _creditsButton.X = panelX + 86;
            _creditsButton.Y = buttonRowY;

            _loginButton.X = panelX + panelWidth - 118;
            _loginButton.Y = buttonRowY + 4;

            _loginButtonLabel.X = _loginButton.X - 4;
            _loginButtonLabel.Y = buttonRowY - 16;

            int footerY = panelY + panelHeight - 48;

            _footerLeft.X = panelX + 30;
            _footerLeft.Y = footerY;

            _footerCenterLine1.X = panelX + (panelWidth / 2) - 80;
            _footerCenterLine1.Y = footerY;

            _footerCenterLine2.X = panelX + (panelWidth / 2) - 96;
            _footerCenterLine2.Y = footerY + 16;

            _footerRightLine1.X = panelX + panelWidth - 110;
            _footerRightLine1.Y = footerY;

            _footerRightLine2.X = panelX + panelWidth - 110;
            _footerRightLine2.Y = footerY + 18;
        }

        private enum Buttons
        {
            Login,
            Quit,
            Credits
        }

        private class PasswordStbTextBox : StbTextBox
        {
            private new Point _caretScreenPosition;
            private new readonly RenderedText _rendererCaret;
            private new readonly RenderedText _rendererText;

            public PasswordStbTextBox
            (
                byte font,
                int maxCharCount = -1,
                int maxWidth = 0,
                bool isunicode = true,
                FontStyle style = FontStyle.None,
                ushort hue = 0
            ) : base(font, maxCharCount, maxWidth, isunicode, style, hue)
            {
                _rendererText = RenderedText.Create(string.Empty, hue, font, isunicode, style, maxWidth: maxWidth);
                _rendererCaret = RenderedText.Create("_", hue, font, isunicode, (style & FontStyle.BlackBorder) != 0 ? FontStyle.BlackBorder : FontStyle.None);
                NoSelection = true;
            }

            internal string RealText
            {
                get => Text;
                set => SetText(value);
            }

            public new ushort Hue
            {
                get => _rendererText.Hue;
                set
                {
                    if (_rendererText.Hue != value)
                    {
                        _rendererText.Hue = value;
                        _rendererCaret.Hue = value;
                        _rendererText.CreateTexture();
                        _rendererCaret.CreateTexture();
                    }
                }
            }

            protected override void DrawCaret(UltimaBatcher2D batcher, int x, int y, float layerDepth)
            {
                if (HasKeyboardFocus)
                {
                    _rendererCaret.Draw(batcher, x + _caretScreenPosition.X, y + _caretScreenPosition.Y, layerDepth);
                }
            }

            protected override void OnMouseDown(int x, int y, MouseButtonType button)
            {
                base.OnMouseDown(x, y, button);

                if (button == MouseButtonType.Left)
                {
                    UpdateCaretScreenPosition();
                }
            }

            protected override void OnKeyDown(SDL.SDL_Keycode key, SDL.SDL_Keymod mod)
            {
                base.OnKeyDown(key, mod);
                UpdateCaretScreenPosition();
            }

            public override void Dispose()
            {
                _rendererText?.Destroy();
                _rendererCaret?.Destroy();
                base.Dispose();
            }

            protected override void OnTextChanged(string previousText)
            {
                _rendererText.Text = Text.Length > 0 ? new string('*', Text.Length) : string.Empty;
                base.OnTextChanged(previousText);
                UpdateCaretScreenPosition();
            }

            internal override void OnFocusEnter()
            {
                base.OnFocusEnter();
                CaretIndex = Text?.Length ?? 0;
                UpdateCaretScreenPosition();
            }

            private new void UpdateCaretScreenPosition()
            {
                _caretScreenPosition = _rendererText.GetCaretPosition(Stb.CursorIndex);
            }

            public override bool AddToRenderLists(RenderLists renderLists, int x, int y, ref float layerDepthRef)
            {
                float layerDepth = layerDepthRef;

                renderLists.AddGumpNoAtlas
                (
                    batcher =>
                    {
                        if (batcher.ClipBegin(x, y, Width, Height))
                        {
                            DrawSelection(batcher, x, y, layerDepth);
                            _rendererText.Draw(batcher, x, y, layerDepth);
                            DrawCaret(batcher, x, y, layerDepth);
                            batcher.ClipEnd();
                        }

                        return true;
                    }
                );

                return true;
            }
        }
    }
}
