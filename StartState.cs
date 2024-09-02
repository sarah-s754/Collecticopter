using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace Collecticopter
{
    public class StartState : GameState
    {
        private Game _game;
        private List<string> _instructions;
        private StartButton _startButton;
        private Title _startTitle;
        private Message _startMessage;

        public StartState(Game game)
        {
            _game = game;
            _instructions = new List<string>() { "Instructions:" };
            _instructions.Add("- Increase score by collecting items");
            _instructions.Add("          (some items take points rather than giving them!)");
            _instructions.Add("- Complete leves by taming the creature for each level");
            _instructions.Add("          (taming creatures requires specific items and minimum score)");
            _instructions.Add("Goal: tame all the creature and finish with the highest score possible");
            _instructions.Add("- You will loses lives if you collide with an untamed creature");
            _startButton = new StartButton();
            _startTitle = new Title(TitleType.Start, new Point2D() { X = SplashKit.ScreenWidth() / 2 + 20, Y = SplashKit.ScreenHeight() / 5 + 10 });
            _startMessage = new Message(_instructions, MessageType.MenuMessage);
        }

        public void HandleInput()
        {
            if (SplashKit.KeyTyped(KeyCode.EscapeKey)) _game.Quit = true;
            // move to PlayState in response to space key pressed
            if (SplashKit.KeyTyped(KeyCode.SpaceKey)) _game.CurrentState = new PlayState(_game);
        }

        public void Update()
        {
            _startButton.Update();
            // move to PlayState in response to start button pressed
            if (_startButton.Pressed) _game.CurrentState = new PlayState(_game);
        }

        public void Draw()
        {
            _startTitle.Draw();
            _startButton.Draw();
            // draw game instructions
            _startMessage.Draw();
        }
    }
}
