using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace Collecticopter
{
    public class Game
    {
        static Game instance;
        private GameState _currentState;
        private List<Bitmap> _background;
        private Player _player;
        private List<Item> _items;
        private int _level;
        private bool _gameOver;
        private bool _quit;

        protected Game()
        {
            _currentState = new StartState(this);
            _background = new List<Bitmap>() { SplashKit.LoadBitmap("Background 1", "Mountains.png"), SplashKit.LoadBitmap("Background 2", "Snow.png"), SplashKit.LoadBitmap("Background 3", "Rock.png") };
            _player = Player.GetPlayer();
            _items = new List<Item>();
            Level = 0;
            GameOver = false;
            Quit = false;
        }

        public static Game GetGame()
        {
            if (instance == null) instance = new Game();
            return instance;
        }

        public GameState CurrentState
        {
            get { return _currentState; }
            set { _currentState = value; }
        }
        public float StageWidth
        {
            get { return _background[Level].Width; }
        }
        public float StageHeight
        {
            get { return _background[Level].Height; }
        }
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }
        public Bitmap Background
        {
            get { return _background[Level]; }
        }
        public bool GameOver
        {
            get { return _gameOver; }
            set { _gameOver = value; }
        }
        public bool Quit
        {
            get { return _quit; }
            set { _quit = value; }
        }
        public List<Item> Items
        {
            get { return _items; }
        }

        public void Update()
        {
            _currentState.Update();            
        }

        public void Draw()
        {
            _currentState.Draw();
        }

        public void HandleInput()
        {
            CurrentState.HandleInput();
        }

        public void CheckCollisions()
        {
            // remove items player collided with
            List<Item> removableItems = new List<Item>();

            foreach (Item itm in _items)
            {
                if (SplashKit.SpriteCollision(itm.Sprite, _player.Sprite))
                {
                    _player.ItemCollision(itm);
                    removableItems.Add(itm);
                }
            }
            RemoveItems(removableItems);

            // bounce items that have collided with eachother
            List<Item> bounceableItems = new List<Item>();
            foreach (Item itm in _items)
            {
                foreach (Item otherItem in _items)
                {
                    if (SplashKit.SpriteCollision(itm.Sprite, otherItem.Sprite))
                    {
                        if (itm != otherItem && !bounceableItems.Contains(itm))
                        {
                            bounceableItems.Add(itm);
                        }
                    }
                }
            }
            BounceItems(bounceableItems);

            // bounce items that have collided with stage boundary
            foreach (Item itm in Items)
            {
                itm.CheckCollisions();
            }
        }

        // remove items in given list from the list Items
        public void RemoveItems(List<Item> items)
        {
            foreach (Item itm in items)
            {
                Items.Remove(itm);
                SplashKit.FreeSprite(itm.Sprite);
            }
        }

        // bounce each item in given list of items 
        public void BounceItems(List<Item> items)
        {
            foreach (Item itm in items)
            {
                itm.Bounce();
            }
        }

        // move camera to keep player on screen, or within the middle area of the screen when not near stage boarders
        public void ScrollCamera()
        {
            int PADDING = 130;

            // move camera in x-direction
            if (_player.Sprite.X - Camera.X < PADDING && _player.Sprite.X > PADDING)
            {
                SplashKit.MoveCameraTo(_player.Sprite.X - PADDING, Camera.Y);
            }
            else if (_player.Sprite.X - Camera.X > SplashKit.ScreenWidth() - _player.Sprite.Width - PADDING && _player.Sprite.X < StageWidth - PADDING - _player.Sprite.Width)
            {
                SplashKit.MoveCameraTo(_player.Sprite.X - SplashKit.ScreenWidth() + _player.Sprite.Width + PADDING, Camera.Y);
            }

            // move camera in y-direction
            if (_player.Sprite.Y - Camera.Y < PADDING && _player.Sprite.Y > PADDING)
            {
                SplashKit.MoveCameraTo(Camera.X, _player.Sprite.Y - PADDING);
            }
            else if (_player.Sprite.Y - Camera.Y > SplashKit.ScreenHeight() - _player.Sprite.Height - PADDING - 20 && _player.Sprite.Y < StageHeight - PADDING - 20 - _player.Sprite.Height)
            {
                SplashKit.MoveCameraTo(Camera.X, _player.Sprite.Y - SplashKit.ScreenHeight() + _player.Sprite.Height + PADDING + 20);
            }
        }

        // reset to initial game values
        public void ResetGame()
        {
            _items = new List<Item>();
            Level = 0;
            GameOver = false;
            SplashKit.MoveCameraTo(StageWidth / 2, StageHeight / 2);
        }
    }
}
