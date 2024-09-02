using System;
using SplashKitSDK;

namespace Collecticopter
{
    public abstract class Creature
    {
        private Sprite _sprite;
        private SpriteReactions _reactions;
        private Timer _timer;
        private int[] _required;
        private bool _tamed;

        public Creature(Vector2D velocity)
        {
            _sprite = MySprite();
            _reactions = new SpriteReactions();
            _timer = new Timer("Image Timer");
            _required = MyRequired();
            Tamed = false;

            Point2D position;
            position.X = Game.GetGame().StageWidth / 3 + SplashKit.Rnd() * (Game.GetGame().StageWidth / 3);
            position.Y = Game.GetGame().StageHeight / 3 + SplashKit.Rnd() * (Game.GetGame().StageHeight / 3);

            SplashKit.SpriteSetPosition(Sprite, position);
            SplashKit.SpriteSetVelocity(Sprite, velocity);
            _timer.Start();
        }

        public Sprite Sprite
        {
            get { return _sprite; }
        }
        public Timer Timer
        {
            get { return _timer; }
        }
        public bool Tamed
        {
            get { return _tamed; }
            set { _tamed = value; }
        }
        public int[] Required
        {
            get { return _required; }
        }

        public abstract Sprite MySprite();

        public abstract int[] MyRequired();

        public void Update()
        {
            SplashKit.MoveSprite(Sprite);
            CheckCollisions();
            ChangeImage();
            _reactions.Update();
        }

        public void Draw()
        {
            SplashKit.DrawSprite(Sprite);
            _reactions.Draw();
        }

        // periodically change which layer of sprite is visible
        public virtual void ChangeImage()
        {
            if (Timer.Ticks > 600)
            {
                Sprite.ToggleLayerVisible(0);
                Sprite.ToggleLayerVisible(1);
                Timer.Reset();
            }
        }

        public void CheckCollisions()
        {
            // collision with edge of stage
            if (!OnStage()) BoundaryCollision();
            // collision with player
            else if (SplashKit.SpriteCollision(Sprite, Player.GetPlayer().Sprite)) PlayerCollision();
        }

        public void PlayerCollision()
        {
            // creature tamed
            if (Player.GetPlayer().HasRequired(_required))
            {
                Tamed = true;
                _reactions.AddReaction(ReactionType.Tamed, Sprite);
            }
            // player hurt if cool-off period has ended
            else if (!Player.GetPlayer().Hurt)
            {
                _reactions.AddReaction(ReactionType.Angry, Sprite);

                float head = Sprite.Heading;
                Sprite.Heading = Player.GetPlayer().Sprite.Heading;
                Player.GetPlayer().CreatureCollision(head);
                if (Math.Sign(Sprite.X) == Math.Sign(Player.GetPlayer().Sprite.X) && Math.Sign(Sprite.Y) == Math.Sign(Player.GetPlayer().Sprite.Y))
                {
                    SplashKit.MoveSprite(Sprite, 10);
                }
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

        // invert creature's velocity
        public void BoundaryCollision()
        {
            SplashKit.SpriteSetVelocity(Sprite, SplashKit.VectorInvert(SplashKit.SpriteVelocity(Sprite)));
        }
    }
}
