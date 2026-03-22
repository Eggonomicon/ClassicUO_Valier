param(
    [string]$RepoRoot = "."
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

function Get-NormalizedContent {
    param([string]$Path)
    if (!(Test-Path $Path)) {
        throw "Missing file: $Path"
    }

    $content = [System.IO.File]::ReadAllText((Resolve-Path $Path))
    return $content.Replace("`r`n", "`n")
}

function Set-NormalizedContent {
    param(
        [string]$Path,
        [string]$Content
    )

    $resolved = Resolve-Path $Path
    $normalized = $Content.Replace("`r`n", "`n")
    $windows = $normalized.Replace("`n", "`r`n")
    [System.IO.File]::WriteAllText($resolved, $windows, [System.Text.UTF8Encoding]::new($false))
}

function Replace-Exact {
    param(
        [string]$Content,
        [string]$OldValue,
        [string]$NewValue,
        [string]$Label
    )

    if ($Content.Contains($NewValue)) {
        return $Content
    }

    if (-not $Content.Contains($OldValue)) {
        throw "Could not find expected block for: $Label"
    }

    return $Content.Replace($OldValue, $NewValue)
}

$repo = (Resolve-Path $RepoRoot).Path

$loginScenePath = Join-Path $repo "src\ClassicUO.Client\Game\Scenes\LoginScene.cs"
$loginBackgroundPath = Join-Path $repo "src\ClassicUO.Client\Game\UI\Gumps\Login\LoginBackground.cs"

$loginScene = Get-NormalizedContent $loginScenePath

$oldLoad = @'
        public override void Load()
        {
            base.Load();

            Client.Game.Window.AllowUserResizing = false;

            _autoLogin = Settings.GlobalSettings.AutoLogin;

            UIManager.Add(new LoginBackground(_world));
            UIManager.Add(_currentGump = new LoginGump(_world, this));

            Client.Game.Audio.PlayMusic(Client.Game.Audio.LoginMusicIndex, false, true);

            if (CanAutologin && CurrentLoginStep != LoginSteps.Main || CUOEnviroment.SkipLoginScreen)
            {
                if (!string.IsNullOrEmpty(Settings.GlobalSettings.Username))
                {
                    // disable if it's the 2nd attempt
                    CUOEnviroment.SkipLoginScreen = false;
                    Connect(Settings.GlobalSettings.Username, Crypter.Decrypt(Settings.GlobalSettings.Password));
                }
            }

            if (Client.Game.IsWindowMaximized())
            {
                Client.Game.RestoreWindow();
            }

            int width = Client.Game.ScaleWithDpi(640);
            int height = Client.Game.ScaleWithDpi(480);
            SDL.SDL_SetWindowMinimumSize(Client.Game.Window.Handle, width, height);
            Client.Game.SetWindowSize(width, height);
        }
'@

$newLoad = @'
        public override void Load()
        {
            base.Load();

            Client.Game.Window.AllowUserResizing = true;

            _autoLogin = Settings.GlobalSettings.AutoLogin;

            UIManager.Add(new LoginBackground(_world));
            UIManager.Add(_currentGump = new LoginGump(_world, this));
            UpdateLoginCanvasLayout();

            Client.Game.Audio.PlayMusic(Client.Game.Audio.LoginMusicIndex, false, true);

            if (CanAutologin && CurrentLoginStep != LoginSteps.Main || CUOEnviroment.SkipLoginScreen)
            {
                if (!string.IsNullOrEmpty(Settings.GlobalSettings.Username))
                {
                    // disable if it's the 2nd attempt
                    CUOEnviroment.SkipLoginScreen = false;
                    Connect(Settings.GlobalSettings.Username, Crypter.Decrypt(Settings.GlobalSettings.Password));
                }
            }

            int minWidth = Client.Game.ScaleWithDpi(LOGIN_CANVAS_WIDTH);
            int minHeight = Client.Game.ScaleWithDpi(LOGIN_CANVAS_HEIGHT);
            SDL.SDL_SetWindowMinimumSize(Client.Game.Window.Handle, minWidth, minHeight);

            int targetWidth = Math.Max(Client.Game.Window.ClientBounds.Width, minWidth);
            int targetHeight = Math.Max(Client.Game.Window.ClientBounds.Height, minHeight);

            if (Client.Game.Window.ClientBounds.Width != targetWidth || Client.Game.Window.ClientBounds.Height != targetHeight)
            {
                Client.Game.SetWindowSize(targetWidth, targetHeight);
            }
        }
'@

$oldUpdate = @'
        public override void Update()
        {
            base.Update();

            if (_lastLoginStep != CurrentLoginStep)
            {
                Client.Game.UO.GameCursor.IsLoading = false;

                // this trick avoid the flickering
                Gump g = _currentGump;
                UIManager.Add(_currentGump = GetGumpForStep());
                g.Dispose();

                _lastLoginStep = CurrentLoginStep;
            }

            if (Reconnect && (CurrentLoginStep == LoginSteps.PopUpMessage || CurrentLoginStep == LoginSteps.Main) && !NetClient.Socket.IsConnected)
            {
                if (_reconnectTime < Time.Ticks)
                {
                    if (!string.IsNullOrEmpty(Account))
                    {
                        Connect(Account, Crypter.Decrypt(Settings.GlobalSettings.Password));
                    }
                    else if (!string.IsNullOrEmpty(Settings.GlobalSettings.Username))
                    {
                        Connect(Settings.GlobalSettings.Username, Crypter.Decrypt(Settings.GlobalSettings.Password));
                    }

                    int timeT = Settings.GlobalSettings.ReconnectTime * 1000;

                    if (timeT < 1000)
                    {
                        timeT = 1000;
                    }

                    _reconnectTime = (long)Time.Ticks + timeT;
                    _reconnectTryCounter++;
                }
            }

            if ((CurrentLoginStep == LoginSteps.CharacterCreation || CurrentLoginStep == LoginSteps.CharacterSelection) && Time.Ticks > _pingTime)
            {
                // Note that this will not be an ICMP ping, so it's better that this *not* be affected by -no_server_ping.

                if (NetClient.Socket.IsConnected)
                {
                    NetClient.Socket.Statistics.SendPing();
                }

                _pingTime = Time.Ticks + 60000;
            }
        }
'@

$newUpdate = @'
        public override void Update()
        {
            base.Update();

            if (_lastLoginStep != CurrentLoginStep)
            {
                Client.Game.UO.GameCursor.IsLoading = false;

                // this trick avoid the flickering
                Gump g = _currentGump;
                UIManager.Add(_currentGump = GetGumpForStep());
                g.Dispose();

                _lastLoginStep = CurrentLoginStep;
            }

            UpdateLoginCanvasLayout();

            if (Reconnect && (CurrentLoginStep == LoginSteps.PopUpMessage || CurrentLoginStep == LoginSteps.Main) && !NetClient.Socket.IsConnected)
            {
                if (_reconnectTime < Time.Ticks)
                {
                    if (!string.IsNullOrEmpty(Account))
                    {
                        Connect(Account, Crypter.Decrypt(Settings.GlobalSettings.Password));
                    }
                    else if (!string.IsNullOrEmpty(Settings.GlobalSettings.Username))
                    {
                        Connect(Settings.GlobalSettings.Username, Crypter.Decrypt(Settings.GlobalSettings.Password));
                    }

                    int timeT = Settings.GlobalSettings.ReconnectTime * 1000;

                    if (timeT < 1000)
                    {
                        timeT = 1000;
                    }

                    _reconnectTime = (long)Time.Ticks + timeT;
                    _reconnectTryCounter++;
                }
            }

            if ((CurrentLoginStep == LoginSteps.CharacterCreation || CurrentLoginStep == LoginSteps.CharacterSelection) && Time.Ticks > _pingTime)
            {
                // Note that this will not be an ICMP ping, so it's better that this *not* be affected by -no_server_ping.

                if (NetClient.Socket.IsConnected)
                {
                    NetClient.Socket.Statistics.SendPing();
                }

                _pingTime = Time.Ticks + 60000;
            }
        }
'@

$anchorField = @'
        private readonly World _world;
'@

$fieldWithConstants = @'
        private readonly World _world;
        private const int LOGIN_CANVAS_WIDTH = 640;
        private const int LOGIN_CANVAS_HEIGHT = 480;
'@

$helperAnchor = @'
        private Gump GetGumpForStep()
'@

$helperBlock = @'
        private void UpdateLoginCanvasLayout()
        {
            if (_currentGump == null || _currentGump.IsDisposed)
            {
                return;
            }

            _currentGump.X = Math.Max(0, (Client.Game.ClientBounds.Width - LOGIN_CANVAS_WIDTH) / 2);
            _currentGump.Y = Math.Max(0, (Client.Game.ClientBounds.Height - LOGIN_CANVAS_HEIGHT) / 2);
        }

        private Gump GetGumpForStep()
'@

$loginScene = Replace-Exact -Content $loginScene -OldValue $oldLoad -NewValue $newLoad -Label "LoginScene.Load"
$loginScene = Replace-Exact -Content $loginScene -OldValue $oldUpdate -NewValue $newUpdate -Label "LoginScene.Update"
$loginScene = Replace-Exact -Content $loginScene -OldValue $anchorField -NewValue $fieldWithConstants -Label "LoginScene constants"
$loginScene = Replace-Exact -Content $loginScene -OldValue $helperAnchor -NewValue $helperBlock -Label "LoginScene.UpdateLoginCanvasLayout"

Set-NormalizedContent -Path $loginScenePath -Content $loginScene

$loginBackground = @'
﻿// SPDX-License-Identifier: BSD-2-Clause

using ClassicUO.Game.UI.Controls;
using ClassicUO.Utility;
using System;

namespace ClassicUO.Game.UI.Gumps.Login
{
    internal class LoginBackground : Gump
    {
        private const int LOGIN_CANVAS_WIDTH = 640;
        private const int LOGIN_CANVAS_HEIGHT = 480;

        private readonly GumpPicTiled _background;
        private readonly GumpPic _border;
        private readonly GumpPic _flag;
        private readonly Button _quitButton;

        public LoginBackground(World world) : base(world, 0, 0)
        {
            if (Client.Game.UO.Version >= ClientVersion.CV_706400)
            {
                _background = new GumpPicTiled(0, 0, LOGIN_CANVAS_WIDTH, LOGIN_CANVAS_HEIGHT, 0x0150)
                {
                    AcceptKeyboardInput = false
                };
                Add(_background);

                _flag = new GumpPic(0, 4, 0x0151, 0)
                {
                    AcceptKeyboardInput = false
                };
                Add(_flag);
            }
            else
            {
                _background = new GumpPicTiled(0, 0, LOGIN_CANVAS_WIDTH, LOGIN_CANVAS_HEIGHT, 0x0E14)
                {
                    AcceptKeyboardInput = false
                };
                Add(_background);

                _border = new GumpPic(0, 0, 0x157C, 0)
                {
                    AcceptKeyboardInput = false
                };
                Add(_border);

                _flag = new GumpPic(0, 4, 0x15A0, 0)
                {
                    AcceptKeyboardInput = false
                };
                Add(_flag);

                _quitButton = new Button(0, 0x1589, 0x158B, 0x158A)
                {
                    X = 555,
                    Y = 4,
                    ButtonAction = ButtonAction.Activate,
                    AcceptKeyboardInput = false
                };
                Add(_quitButton);
            }

            CanCloseWithEsc = false;
            CanCloseWithRightClick = false;
            AcceptKeyboardInput = false;

            LayerOrder = UILayer.Under;
        }

        public override void Update()
        {
            base.Update();
            ResizeLoginFrame();
        }

        private void ResizeLoginFrame()
        {
            int clientWidth = Math.Max(LOGIN_CANVAS_WIDTH, Client.Game.ClientBounds.Width);
            int clientHeight = Math.Max(LOGIN_CANVAS_HEIGHT, Client.Game.ClientBounds.Height);

            _background.Width = clientWidth;
            _background.Height = clientHeight;

            int offsetX = Math.Max(0, (clientWidth - LOGIN_CANVAS_WIDTH) / 2);
            int offsetY = Math.Max(0, (clientHeight - LOGIN_CANVAS_HEIGHT) / 2);

            if (_border != null)
            {
                _border.X = offsetX;
                _border.Y = offsetY;
            }

            if (_flag != null)
            {
                _flag.X = offsetX;
                _flag.Y = offsetY + 4;
            }

            if (_quitButton != null)
            {
                _quitButton.X = offsetX + 555;
                _quitButton.Y = offsetY + 4;
            }
        }

        public override void OnButtonClick(int buttonID)
        {
            Client.Game.Exit();
        }
    }
}
'@

Set-NormalizedContent -Path $loginBackgroundPath -Content $loginBackground

Write-Host "Patched login window behavior:"
Write-Host " - login scene remains resizable"
Write-Host " - login scene no longer forces 640x480 every load"
Write-Host " - login UI is centered inside larger windows"
Write-Host " - login background expands with the client size"
