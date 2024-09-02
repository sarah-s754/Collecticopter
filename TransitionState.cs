using System;
using SplashKitSDK;

namespace Collecticopter
{
    public class TransitionState : GameState
    {
        private Game _game;
        private Timer _timer;
        private Title _nextLevelTitle;
        
        public TransitionState(Game game)
        {
            _game = game;
            _timer = SplashKit.CreateTimer("timer");
            _timer.Start();
            _nextLevelTitle = MyTitle();
        }

        public Title MyTitle()
        {
            if (_game.Level == 1) return new Title(TitleType.NextLevel, new Point2D() { X = SplashKit.ScreenWidth() / 2 - 40, Y = SplashKit.ScreenHeight() / 2 - 60 });
            else return new Title(TitleType.FinalLevel, new Point2D() { X = SplashKit.ScreenWidth() / 2 + 50, Y = SplashKit.ScreenHeight() / 2 });
        }

        public void HandleInput()
        {
            if (SplashKit.KeyTyped(KeyCode.EscapeKey)) _game.Quit = true;
        }

        public void Update()
        {
            // move to PlayState after a period of time
            if (_timer.Ticks > 2500)
            {
                _game.CurrentState = new PlayState(_game);
                _timer.Dispose();
            }
        }

        public void Draw()
        {
            _nextLevelTitle.Draw();
        }
    }
}
