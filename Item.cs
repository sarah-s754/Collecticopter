using System;
using SplashKitSDK;

namespace Collecticopter
{
    public class Item
    {
        private ItemType _type;
        protected Point2D _position;
        private Vector2D _velocity;
        private Sprite _sprite;
        private float _rotation;
        private int _value;

        public Item()
        {
            _position.X = SplashKit.Rnd() * Game.GetGame().StageWidth;
            _position.Y = SplashKit.Rnd() / 5 * 4 * Game.GetGame().StageHeight;
            _velocity.X = SplashKit.Rnd(1, 8);
            _velocity.Y = SplashKit.Rnd(1, 8);
            _type = (ItemType)SplashKit.Rnd(3);
            _sprite = SplashKit.CreateSprite(TypeBitmap());
            _rotation = SplashKit.Rnd() * 360;
            _value = Value();

            SplashKit.MoveSpriteTo(_sprite, _position.X, _position.Y);
            SplashKit.SpriteSetVelocity(_sprite, _velocity);
            SplashKit.SpriteSetRotation(_sprite, _rotation);
        }

        public Sprite Sprite
        {
            get { return _sprite; }
        }
        public ItemType Type
        {
            get { return _type; }
        }

        public void Update()
        {
            SplashKit.UpdateSprite(_sprite);
        }

        public void Draw()
        {
            SplashKit.DrawSprite(_sprite);
        }

        // bounce item if collided with the game stage
        public void CheckCollisions()
        {
            if (!OnStage())
            {
                Bounce();
            }
        }

        // return boolean value indicating whether creature is on game stage
        public bool OnStage()
        {
            if (Sprite.X < 0 || Sprite.X > Game.GetGame().StageWidth - Sprite.Width || Sprite.Y < 0 || Sprite.Y > Game.GetGame().StageHeight - Sprite.Height - 60)
            {
                return false;
            }
            return true;
        }

        // invert item velocity and increase rotation
        public void Bounce()
        {
            SplashKit.SpriteSetVelocity(Sprite, SplashKit.VectorInvert(SplashKit.SpriteVelocity(Sprite)));
            SplashKit.SpriteSetRotation(Sprite, SplashKit.SpriteRotation(Sprite) + 13);
        }

        public Bitmap TypeBitmap()
        {
            switch (_type)
            {
                case ItemType.Carrot:
                    return SplashKit.LoadBitmap("Carrot", "carrot.png");
                case ItemType.RedMushroom:
                    return SplashKit.LoadBitmap("Red Mushroom", "mushroom_red.png");
                case ItemType.BrownMushroom:
                    return SplashKit.LoadBitmap("Brown Mushroom", "mushroom_brown.png");
                default:
                    return SplashKit.LoadBitmap("Carrot", "carrot.png");
            }
        }

        public int Value()
        {
            switch (_type)
            {
                case ItemType.Carrot:
                    return +1;
                case ItemType.RedMushroom:
                    return -1;
                case ItemType.BrownMushroom:
                    return +1;
                default:
                    return 0;
            }
        }
    }
}
