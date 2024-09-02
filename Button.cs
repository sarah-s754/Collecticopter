using System;
using SplashKitSDK;

namespace Collecticopter
{
    public abstract class Button
    {
        private Bitmap _bitmap;
        private bool _hovered;
        private bool _pressed;
        private Point2D _position;

        public Button()
        {
            _bitmap = MyBitmap();
            Hovered = false;
            Pressed = false;
            Position = MyPosition();
        }

        public Bitmap Bitmap
        {
            get { return _bitmap; }
        }
        public bool Hovered
        {
            get { return _hovered; }
            set { _hovered = value; }
        }
        public bool Pressed
        {
            get { return _pressed; }
            set { _pressed = value; }
        }
        public Point2D Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public abstract Bitmap MyBitmap();

        public abstract Point2D MyPosition();

        public void Update()
        {
            if (SplashKit.BitmapPointCollision(Bitmap, Position, SplashKit.MousePosition())) Hovered = true;
            else Hovered = false;

            if (Hovered && SplashKit.MouseClicked(MouseButton.LeftButton)) Pressed = true;
        }

        public virtual void Draw()
        {
            if (Hovered) DrawHighlight();
            SplashKit.DrawBitmap(Bitmap, Position.X, Position.Y, SplashKit.OptionToScreen());
        }

        public void DrawHighlight()
        {
            SplashKit.FillRectangle(Color.White, Position.X - 2, Position.Y - 2, Bitmap.Width + 4, Bitmap.Height + 4, SplashKit.OptionToScreen());
        }
    }
}