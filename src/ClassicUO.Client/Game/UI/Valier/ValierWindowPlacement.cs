using ClassicUO.Game.UI.Controls;
using Microsoft.Xna.Framework;

namespace ClassicUO.Game.UI.Valier
{
    internal enum ValierAnchorPreset
    {
        BottomLeft,
        LeftMiddle,
        Center,
        RightMiddle,
        BottomCenter
    }

    internal static class ValierWindowPlacement
    {
        public static Point GetDefaultPosition(ValierAnchorPreset preset, int width, int height)
        {
            int clientWidth = Client.Game.ClientBounds.Width;
            int clientHeight = Client.Game.ClientBounds.Height;
            int margin = ValierTheme.ScreenMargin;

            return preset switch
            {
                ValierAnchorPreset.BottomLeft => new Point(
                    margin,
                    clientHeight - height - margin),

                ValierAnchorPreset.LeftMiddle => new Point(
                    margin,
                    System.Math.Max(margin, (clientHeight - height) / 2)),

                ValierAnchorPreset.Center => new Point(
                    System.Math.Max(margin, (clientWidth - width) / 2),
                    System.Math.Max(margin, (clientHeight - height) / 2)),

                ValierAnchorPreset.RightMiddle => new Point(
                    System.Math.Max(margin, clientWidth - width - margin),
                    System.Math.Max(margin, (clientHeight - height) / 2)),

                ValierAnchorPreset.BottomCenter => new Point(
                    System.Math.Max(margin, (clientWidth - width) / 2),
                    clientHeight - height - margin),

                _ => new Point(margin, margin)
            };
        }

        public static void ClampToViewport(Control control)
        {
            if (control == null)
            {
                return;
            }

            int clientWidth = Client.Game.ClientBounds.Width;
            int clientHeight = Client.Game.ClientBounds.Height;
            int margin = ValierTheme.ScreenMargin;

            int maxX = System.Math.Max(margin, clientWidth - control.Width - margin);
            int maxY = System.Math.Max(margin, clientHeight - control.Height - margin);

            if (control.X < margin)
            {
                control.X = margin;
            }
            else if (control.X > maxX)
            {
                control.X = maxX;
            }

            if (control.Y < margin)
            {
                control.Y = margin;
            }
            else if (control.Y > maxY)
            {
                control.Y = maxY;
            }
        }
    }
}
