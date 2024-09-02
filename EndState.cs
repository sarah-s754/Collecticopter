using System;
using SplashKitSDK;

namespace Collecticopter
{
    public class EndState : GameState
    {
        private Game _game;
        private bool _win;
        private ResetButton _resetButton;
        private Title _endTitle;

        public EndState(Game game, bool win)
        {
            _game = game;
            _win = win;
            _resetButton = new ResetButton();
            _endTitle = MyTitle();
        }

        public Title MyTitle()
        {
            if (_win) return new Title(TitleType.WinEnd, new Point2D() { X = SplashKit.ScreenWidth() / 2 + 40, Y = SplashKit.ScreenHeight() / 2 - 110});
            else return new Title(TitleType.LoseEnd, new Point2D() { X = SplashKit.ScreenWidth() / 2, Y = SplashKit.ScreenHeight() / 2 - 60 });
        }
        
        public void HandleInput()
        {
            if (SplashKit.KeyTyped(KeyCode.EscapeKey)) _game.Quit = true;
            if (SplashKit.KeyTyped(KeyCode.SpaceKey)) ResetGameState();
        }

        public void Update()
        {
            _resetButton.Update();
            if (_resetButton.Pressed) ResetGameState();
        }

        // reset game values back to initial levels
        public void ResetGameState()
        {
            _game.ResetGame();
            Player.GetPlayer().ResetPlayer();
            _game.CurrentState = new PlayState(_game);
        }

        public void Draw()
        {
            _endTitle.Draw();
            _resetButton.Draw();        
        }
    }
}
