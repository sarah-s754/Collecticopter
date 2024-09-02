using System;
using SplashKitSDK;

namespace Collecticopter
{
    public class Title
    {
        protected Point2D _position;
        private TitleType _type;
        private Bitmap _bitmap;

        public Title(TitleType type, Point2D position)
        {
            _position = position;
            _type = type;
            _bitmap = TypeBitmap();
        }

        public Bitmap TypeBitmap()
        {
            switch (_type)
            {
                case TitleType.Start:
                    return SplashKit.LoadBitmap("Start Title", "startTitle.png");
                case TitleType.NextLevel:
                    return SplashKit.LoadBitmap("Next Level Title", "nextLevelTitle.png");
                case TitleType.FinalLevel:
                    return SplashKit.LoadBitmap("Final Level Title", "finalLevelTitle.png");
                case TitleType.LoseEnd:
                    return SplashKit.LoadBitmap("Lose End Title", "loseEndTitle.png");
                case TitleType.WinEnd:
                    return SplashKit.LoadBitmap("Wind End Title", "winEndTitle.png");
                default:
                    return SplashKit.LoadBitmap("Start Title", "startTitle.png");
            }
        }

        // draw title bitmap with center at the specified position
        public void Draw()
        {
            SplashKit.DrawBitmap(_bitmap, _position.X - _bitmap.Width / 2, _position.Y - _bitmap.Height / 2, SplashKit.OptionToScreen());
        }
    }
}
