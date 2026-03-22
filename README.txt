Valier resizable login patch

What this changes
- keeps the ClassicUO login scene resizable
- stops LoginScene from forcing the window back to 640x480
- keeps a 640x480 minimum login canvas
- centers the login-step gumps inside larger windows
- expands the login background to fill the resized client area

Files touched by the patcher
- src/ClassicUO.Client/Game/Scenes/LoginScene.cs
- src/ClassicUO.Client/Game/UI/Gumps/Login/LoginBackground.cs

Suggested next build command
powershell -ExecutionPolicy Bypass -File .\scripts\build_release_windows.ps1
