using System;
using SplashKitSDK;

namespace Collecticopter
{
    public class Reaction
    {
        private ReactionType _type;
        private Sprite _mainSprite;
        private Timer _duration;

        public Reaction(ReactionType type, Sprite spt)
        {
            _type = type;
            _mainSprite = spt;
            _duration = SplashKit.CreateTimer("duration");
            _duration.Start();
        }

        // return boolean value indicating whether reaction has expired
        public bool Current()
        {
            if (_duration.Ticks > 400 && _type != ReactionType.Hurt)
            {
                if (_type == ReactionType.Hurt) Player.GetPlayer().Hurt = false;
                return false;
            }
            else if (_duration.Ticks > 600)
            {
                if (_type == ReactionType.Hurt) Player.GetPlayer().Hurt = false;
                return false;
            }

            return true;
        }
        
        // draw type of reaction at the sprite's position
        public void Draw()
        {
            double xPos = _mainSprite.X;
            double yPos = _mainSprite.Y - 20;

            switch (_type)
            {
                case ReactionType.GainPoint:
                    SplashKit.DrawText("+1", Color.Green, xPos, yPos);
                    break;
                case ReactionType.LosePoint:
                    SplashKit.DrawText("-1", Color.Red, xPos, yPos);
                    break;
                case ReactionType.Hurt:
                    DrawBitmap(SplashKit.LoadBitmap("Hurt Reaction", "hurtReaction.png"));
                    break;
                case ReactionType.Angry:
                    DrawBitmap(SplashKit.LoadBitmap("Angry Reaction", "angryReaction.png"));
                    break;
                case ReactionType.Tamed:
                    DrawBitmap(SplashKit.LoadBitmap("Tamed Reaction", "tamedReaction.png"));
                    break;
            }
        }

        public void DrawBitmap(Bitmap bmp)
        {
            SplashKit.DrawBitmap(bmp, (_mainSprite.X + _mainSprite.Width / 2), (_mainSprite.Y - bmp.Height));
        }
    }
}
