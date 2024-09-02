using System;
using SplashKitSDK;

namespace Collecticopter
{
    public class SpringCreature : Creature
    {
        public SpringCreature(Vector2D velocity) : base(velocity)
        {
        }

        public SpringCreature() : this(new Vector2D() { X = 2 * (SplashKit.Rnd(-1, 1) * 2 - 1), Y = SplashKit.Rnd(-1, 1) * 2 - 1 })
        {
        }

        public override Sprite MySprite()
        {
            Sprite mySprite = SplashKit.CreateSprite("Spring Creature Images", SplashKit.LoadBitmap("Spring Image 1", "springCreature1.png"));
            mySprite.AddLayer(SplashKit.LoadBitmap("Spring Image 2", "springCreature2.png"), "two");
            return mySprite;
        }

        // returns integer array containing the score, item type, and number of that item required to tame creature
        public override int[] MyRequired()
        {
            return new int[] { 3, 0, 1 };
        }
    }
}
