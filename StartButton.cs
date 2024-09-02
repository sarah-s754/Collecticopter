using System;
using SplashKitSDK;

namespace Collecticopter
{
    public class StartButton : Button
    {
        private Timer _flashTimer;

        public StartButton()
        {
            _flashTimer = SplashKit.CreateTimer("Flash Timer");
            _flashTimer.Start();
        }

        public override Bitmap MyBitmap()
        {
            return SplashKit.LoadBitmap("Start Button", "startButton.png");
        }

        public override Point2D MyPosition()
        {
            return new Point2D() { X = SplashKit.ScreenWidth() / 2 - Bitmap.Width / 2, Y = SplashKit.ScreenHeight() / 5 * 2 - Bitmap.Height / 2 + 10 };
        }

        public override void Draw()
        {
            // draw flashing of button highligh and highlight in response to user hovering over button
            if (Hovered || Flash()) DrawHighlight();
            // draw button
            SplashKit.DrawBitmap(Bitmap, Position.X, Position.Y, SplashKit.OptionToScreen());
        }

        // flash button highlight at regular intevals
        public bool Flash()
        {
            if (_flashTimer.Ticks > 2300)
            {
                _flashTimer.Reset();
                return true;
            }
            else if (_flashTimer.Ticks > 1500) return true;
            else return false;
        }
    }
}
