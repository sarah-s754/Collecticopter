using System;
using SplashKitSDK;

namespace Collecticopter
{
    public class ResetButton : Button
    {
        public override Bitmap MyBitmap()
        {
            return SplashKit.LoadBitmap("Reset Button", "resetButton.png");
        }

        public override Point2D MyPosition()
        {
            return new Point2D() { X = SplashKit.ScreenWidth() / 2 - Bitmap.Width / 2, Y = SplashKit.ScreenHeight() / 3 * 2 - Bitmap.Height - 30 };
        }
    }
}
