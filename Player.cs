using System;
using SplashKitSDK;

namespace Collecticopter
{
    public class Player
    {
        static Player instance;
        protected Point2D _position;
        private Vector2D _velocity;
        private Sprite _sprite;
        private Inventory _inventory;
        private Score _score;
        private SpriteReactions _reactions;
        private int _lifeCount;
        private bool _hurt;

        protected Player()
        {
            _sprite = SplashKit.CreateSprite("Player Images", SplashKit.LoadBitmap("Image 1", "flyMan1.png"));
            _sprite.AddLayer(SplashKit.LoadBitmap("Image 2", "flyMan2.png"), "two");
            _position.X = SplashKit.ScreenWidth() / 3 * 5;
            _position.Y = SplashKit.ScreenHeight();
            _velocity.X = 0;
            _velocity.Y = 0;
            _inventory = new Inventory();
            _score = new Score();
            _reactions = new SpriteReactions();
            _lifeCount = 3;
            Hurt = false;

            SplashKit.SpriteSetPosition(Sprite, _position);
            SplashKit.SpriteSetVelocity(Sprite, _velocity);
        }

        public static Player GetPlayer()
        {
            if (instance == null) instance = new Player();
            return instance;
        }

        public Sprite Sprite
        {
            get { return _sprite; }
        }
        public bool Hurt
        {
            get { return _hurt; }
            set { _hurt = value; }
        }

        public void Update()
        {
            SplashKit.SpriteSetVelocity(Sprite, _velocity);
            KeepOnScreen();
            SplashKit.MoveSpriteTo(_sprite, _position.X, _position.Y);

            _reactions.Update();

            if (_lifeCount == 0) Game.GetGame().GameOver = true;
        }
        
        public void Draw()
        {
            SplashKit.DrawSprite(_sprite);
            _inventory.Draw();
            _score.Draw();
            _reactions.Draw();
            DrawLives();
        }

        public void DrawLives()
        {
            // draw full and empty hearts to represent the _livesCount
            int i = 0;
            while (i < 3)
            {
                Bitmap btmp;

                if (i < _lifeCount) btmp = SplashKit.LoadBitmap("Life 1", "full_life.png");
                else btmp = SplashKit.LoadBitmap("Life 2", "empty_life.png");

                SplashKit.DrawBitmap(btmp, SplashKit.ScreenWidth() - (3 - i) * (btmp.Width + 4), 10, SplashKit.OptionToScreen());
                i++;
            }
        }
        
        // add item player collided with to _inventory, increment _score, and add reaction to reflect the value of the item
        public void ItemCollision(Item itm)
        {
            _inventory.Collect(itm);
            _score.IncrementPoints(itm.Value());
            
            if (itm.Value() == 1) _reactions.AddReaction(ReactionType.GainPoint, Sprite);
            else _reactions.AddReaction(ReactionType.LosePoint, Sprite);
        }

        // move player away from creature and add hurt reaction in response to collision, if previous hurt reaction has expired
        public void CreatureCollision(float head)
        {
            if (!_hurt)
            {
                _position.X += 100 * Math.Cos(head);
                _position.Y += 100 * Math.Sin(head);

                _lifeCount--;
                _reactions.AddReaction(ReactionType.Hurt, Sprite);
                _hurt = true;
            }
        }

        // return boolean value indicating whether player has the required items to tame creature
        public bool HasRequired(int[] req)
        {
            if (_score.Points >= req[0] && _inventory.HasItem((ItemType) req[1]) && _inventory.ItemCount(req[1]) >= req[2])
            {
                for (int i = 0; i < req[2]; i++)
                {
                    _inventory.Give((ItemType)req[1]);
                }
                return true;
            }
            else return false;
        }

        // change player image to reflect upward and downward movement
        public void ChangeImage(string direction)
        {
            switch (direction)
            {
                case "up":
                    _sprite.ShowLayer(0);
                    _sprite.HideLayer(1);
                    break;
                case "down":
                    _sprite.ShowLayer(1);
                    _sprite.HideLayer(0);
                    break;
            }
        }

        // move player in response to user input
        public void Move(string direction)
        {
            const int SPEED = 8;

            switch (direction)
            {
                case "up":
                    _velocity.Y = - SPEED;
                    _position.Y += _velocity.Y;
                    break;
                case "down":
                    _velocity.Y = SPEED;
                    _position.Y += _velocity.Y;
                    break;
                case "right":
                    _velocity.X = SPEED;
                    _position.X += _velocity.X;
                    break;
                case "left":
                    _velocity.X = - SPEED;
                    _position.X += _velocity.X;
                    break;
            }
        }

        // Prevents player moving offscreen
        public void KeepOnScreen()
        {
            int InventoryOffset = 60;
            
            // +X-direction
            if (_position.X > Game.GetGame().StageWidth - _sprite.Width)
            {
                _position.X = Game.GetGame().StageWidth - _sprite.Width;
            }
            // -X-direction
            else if (_position.X < 0) _position.X = 0;
            // +Y-direction
            if (_position.Y > Game.GetGame().StageHeight - InventoryOffset - _sprite.Height)
            {
                _position.Y = Game.GetGame().StageHeight - InventoryOffset - _sprite.Height;
            }
            // -Y-direction
            else if (_position.Y < 0) _position.Y = 0;
        }

        // reset player position to initial value
        public void ResetPosition()
        {
            _position.X = SplashKit.ScreenWidth() / 2;
            _position.Y = SplashKit.ScreenHeight() / 2;
            ChangeImage("up");
            SplashKit.MoveCameraTo(SplashKit.ScreenWidth() / 2, SplashKit.ScreenHeight() / 2);
        }

        // reset player fields to intial values
        public void ResetPlayer()
        {
            _inventory = new Inventory();
            _score = new Score();
            _lifeCount = 3;
            ResetPosition();
        }
    }
}
