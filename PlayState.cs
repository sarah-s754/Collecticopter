using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace Collecticopter
{
    public class PlayState : GameState
    {
        private Game _game;
        private List<string> _creatureName;
        private Creature _creature;
        private Message _levelMessage;
        private bool _win;
        private Timer _msgTimer;
        
        public PlayState(Game game)
        {
            _game = game;
            _creatureName = new List<string>() { "Spring Creature", "Spike Creature", "Wing Creature" };
            _creature = MyCreature(_creatureName[_game.Level]);
            SplashKit.MoveCameraTo(_game.StageWidth / 2, _game.StageHeight / 2);
            _levelMessage = new Message(new List<string>() { "The creature on this level needs...", "   +" + _creature.Required[0] + " points     +" + _creature.Required[2] + " " + (ItemType) _creature.Required[1] }, MessageType.LevelMessage);
            _win = false;
            _msgTimer = SplashKit.CreateTimer("Level Message Timer");
            _msgTimer.Start();
        }

        public Creature MyCreature(string name)
        {
            switch (name)
            {
                case "Spring Creature":
                    return new SpringCreature();
                case "Spike Creature":
                    return new SpikeCreature();
                case "Wing Creature":
                    return new WingCreature();
                default:
                    return new SpringCreature();
            }
        }

       // move to win or lose EndState, or TransitionState
        public void NextLevel()
        {
            SplashKit.Delay(2000);

            if (!_win || (_game.Level >= 2 && _win))
            {
                _game.CurrentState = new EndState(_game, _win);
            }
            else
            {
                _game.Level++;
                Player.GetPlayer().ResetPosition();
                _creature = MyCreature(_creatureName[_game.Level]);
                _game.CurrentState = new TransitionState(_game);
            }
        }

        public void HandleInput()
        {
            if (SplashKit.KeyTyped(KeyCode.EscapeKey)) _game.Quit = true;

            // Move character and update image in response to user inout
            if (SplashKit.KeyDown(KeyCode.UpKey))
            {
                Player.GetPlayer().Move("up");
                Player.GetPlayer().ChangeImage("up");
            }
            if (SplashKit.KeyDown(KeyCode.DownKey))
            {
                Player.GetPlayer().Move("down");
                Player.GetPlayer().ChangeImage("down");
            }
            if (SplashKit.KeyDown(KeyCode.RightKey))
            {
                Player.GetPlayer().Move("right");
            }
            if (SplashKit.KeyDown(KeyCode.LeftKey))
            {
                Player.GetPlayer().Move("left");
            }
        }

        public void Update()
        {
            // move to EndState
            if (_game.GameOver) NextLevel();
            // move to TransitionState
            else if (_creature.Tamed)
            {
                _win = true;
                NextLevel();
            }
            else
            {
                Player.GetPlayer().Update();
                _creature.Update();

                foreach (Item itm in _game.Items)
                {
                    itm.Update();
                }

                _game.CheckCollisions();

                if (_game.Items.Count < 4) _game.Items.Add(new Item());

                _game.ScrollCamera();
            }
        }

        public void Draw()
        {
            SplashKit.DrawBitmap(_game.Background, 0, 0);
            _creature.Draw();

            foreach (Item itm in _game.Items)
            {
                itm.Draw();
            }

            Player.GetPlayer().Draw();

            // display message at the start of the level
            if (_msgTimer.Ticks > 400 && _msgTimer.Ticks < 4500) _levelMessage.Draw();
        }
    }
}
