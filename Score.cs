using System;
using SplashKitSDK;

namespace Collecticopter
{
    public class Score
    {
        private int _points;

        public Score()
        {
            _points = 0;
            //_points = 15;
        }

        public int Points
        {
            get { return _points; }
        }

        public void Draw()
        {
            SplashKit.DrawText("Score: " + _points, Color.Black, 10, 10, SplashKit.OptionToScreen());
        }

        public void IncrementPoints(int value)
        {
            _points += value;
        }
    }
}
