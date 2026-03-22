using System;

namespace ClassicUO.Resources
{
    public partial class Loader
    {
        [FileEmbed.FileEmbed("valier-login-frame.png")]
        public static partial ReadOnlySpan<byte> GetValierLoginFrame();

        [FileEmbed.FileEmbed("valier-server-frame.png")]
        public static partial ReadOnlySpan<byte> GetValierServerFrame();

        [FileEmbed.FileEmbed("valier-btn-quit.png")]
        public static partial ReadOnlySpan<byte> GetValierQuitButton();

        [FileEmbed.FileEmbed("valier-btn-credits.png")]
        public static partial ReadOnlySpan<byte> GetValierCreditsButton();

        [FileEmbed.FileEmbed("valier-btn-login.png")]
        public static partial ReadOnlySpan<byte> GetValierLoginButton();
    }
}
