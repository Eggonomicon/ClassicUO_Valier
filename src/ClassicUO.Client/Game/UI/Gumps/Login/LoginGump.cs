// SPDX-License-Identifier: BSD-2-Clause

using ClassicUO.Configuration;
using ClassicUO.Game.Scenes;
using ClassicUO.Game.UI.Controls;
using ClassicUO.Input;
using ClassicUO.Renderer;
using ClassicUO.Resources;
using Microsoft.Xna.Framework;
using SDL3;
using System;

namespace ClassicUO.Game.UI.Gumps.Login
{
    internal sealed class LoginGump : Gump
    {
        private const int PANEL_WIDTH = 620;
        private const int PANEL_HEIGHT = 430;
        private const int RIGHT_MARGIN = 40;

        private readonly Checkbox _checkboxAutologin;
        private readonly Checkbox _checkboxSaveAccount;
        private readonly StbTextBox _textboxAccount;
        private readonly PasswordStbTextBox _passwordFake;
        private readonly ImageButtonControl _quitButton;
        private readonly ImageButtonControl _creditsButton;
        private readonly ImageButtonControl _loginButton;
        private readonly HSliderBar _musicSlider;

        private int _lastBoundsWidth = -1;
        private int _lastBoundsHeight = -1;

        public LoginGump(World world, LoginScene scene) : base(world, 0, 0)
        {
            CanCloseWithRightClick = false;
            AcceptKeyboardInput = false;

            Add(new TextureImageControl(Loader.GetValierLoginFrame(), PANEL_WIDTH, PANEL_HEIGHT));
            Add(new TextureImageControl(Loader.GetCuoLogo(), 86, 86) { X = 32, Y = 24 });

            Add(new Label("ValierUO", false, 0x0481, font: 9) { X = 130, Y = 34 });
            Add(new Label("Enter the shard", false, 0x0481, font: 9) { X = 130, Y = 72 });
            Add(new Label($"{Settings.GlobalSettings.IP}:{Settings.GlobalSettings.Port}", false, 0x0481, font: 9) { X = 130, Y = 100 });

            Add(new Label("Account access", false, 0x0481, font: 9) { X = 42, Y = 156 });
            Add(new Label("Password", false, 0x0481, font: 9) { X = 42, Y = 232 });

            Add(new ResizePic(0x0BB8) { X = 270, Y = 142, Width = 285, Height = 42 });
            Add(new ResizePic(0x0BB8) { X = 270, Y = 218, Width = 285, Height = 42 });

            Add
            (
                _textboxAccount = new StbTextBox(5, 32, 260, false, hue: 0x034F)
                {
                    X = 282,
                    Y = 150,
                    Width = 260,
                    Height = 28
                }
            );
            _textboxAccount.SetText(Settings.GlobalSettings.Username);

            Add
            (
                _passwordFake = new PasswordStbTextBox(5, 32, 260, false, hue: 0x034F)
                {
                    X = 282,
                    Y = 226,
                    Width = 260,
                    Height = 28
                }
            );
            _passwordFake.RealText = ClassicUO.Utility.Crypter.Decrypt(Settings.GlobalSettings.Password);

            Add(_quitButton = new ImageButtonControl((int)Buttons.Quit, Loader.GetValierQuitButton()) { X = 36, Y = 294 });
            Add(_creditsButton = new ImageButtonControl((int)Buttons.Credits, Loader.GetValierCreditsButton()) { X = 172, Y = 311 });
            Add(_loginButton = new ImageButtonControl((int)Buttons.Login, Loader.GetValierLoginButton()) { X = 390, Y = 305 });

            Add
            (
                _checkboxAutologin = new Checkbox(0x00D2, 0x00D3, "Autologin", 9, 0x0481, false)
                {
                    X = 44,
                    Y = 382
                }
            );

            Add
            (
                _checkboxSaveAccount = new Checkbox(0x00D2, 0x00D3, "Save Account", 9, 0x0481, false)
                {
                    X = 190,
                    Y = 382
                }
            );

            Checkbox musicCheckbox = new Checkbox(0x00D2, 0x00D3, "Music", 9, 0x0481, false)
            {
                X = 372,
                Y = 382,
                IsChecked = Settings.GlobalSettings.LoginMusic
            };
            Add(musicCheckbox);

            Add
            (
                _musicSlider = new HSliderBar
                (
                    460,
                    386,
                    118,
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

            _checkboxSaveAccount.IsChecked = Settings.GlobalSettings.SaveAccount;
            _checkboxAutologin.IsChecked = Settings.GlobalSettings.AutoLogin;
            _musicSlider.IsVisible = Settings.GlobalSettings.LoginMusic;

            musicCheckbox.ValueChanged += (sender, e) =>
            {
                Settings.GlobalSettings.LoginMusic = musicCheckbox.IsChecked;
                _musicSlider.IsVisible = musicCheckbox.IsChecked;
                Client.Game.Audio.UpdateCurrentMusicVolume(true);
            };

            _musicSlider.ValueChanged += (sender, e) =>
            {
                Settings.GlobalSettings.LoginMusicVolume = _musicSlider.Value;
                Client.Game.Audio.UpdateCurrentMusicVolume(true);
            };

            if (!string.IsNullOrEmpty(_textboxAccount.Text))
            {
                _passwordFake.SetKeyboardFocus();
            }
            else
            {
                _textboxAccount.SetKeyboardFocus();
            }

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

        public override void OnKeyboardReturn(int textID, string text)
        {
            SaveCheckboxStatus();

            LoginScene ls = Client.Game.GetScene<LoginScene>();

            if (ls.CurrentLoginStep == LoginSteps.Main)
            {
                ls.Connect(_textboxAccount.Text, _passwordFake.RealText);
            }
        }

        protected override void OnKeyDown(SDL.SDL_Keycode key, SDL.SDL_Keymod mod)
        {
            base.OnKeyDown(key, mod);

            if (key == SDL.SDL_Keycode.SDLK_RETURN || key == SDL.SDL_Keycode.SDLK_KP_ENTER)
            {
                OnButtonClick((int)Buttons.Login);
            }
        }

        private void SaveCheckboxStatus()
        {
            Settings.GlobalSettings.SaveAccount = _checkboxSaveAccount.IsChecked;
            Settings.GlobalSettings.AutoLogin = _checkboxAutologin.IsChecked;
        }

        public override void OnButtonClick(int buttonID)
        {
            switch ((Buttons)buttonID)
            {
                case Buttons.Login:
                    SaveCheckboxStatus();

                    if (!_textboxAccount.IsDisposed)
                    {
                        Client.Game.GetScene<LoginScene>().Connect(_textboxAccount.Text, _passwordFake.RealText);
                    }
                    break;

                case Buttons.Quit:
                    Client.Game.Exit();
                    break;

                case Buttons.Credits:
                    ClassicUO.Game.Managers.UIManager.Add(new CreditsGump(World));
                    break;
            }
        }

        private enum Buttons
        {
            Login = 1,
            Quit = 2,
            Credits = 3
        }

        private sealed class PasswordStbTextBox : StbTextBox
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
