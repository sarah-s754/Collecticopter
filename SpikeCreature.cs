using System;
using SplashKitSDK;

namespace Collecticopter
{
    public class SpikeCreature : Creature
    {
        public SpikeCreature(Vector2D velocity) : base(velocity)
        {
        }

        public SpikeCreature() : this(new Vector2D() { X = 3 * (SplashKit.Rnd(-1, 1) * 2 - 1), Y = 2 * (SplashKit.Rnd(-1, 1) * 2 - 1) })
        {
        }

        public override Sprite MySprite()
        {
            Sprite mySprite = SplashKit.CreateSprite("Spike Creature Images", SplashKit.LoadBitmap("Spike Image 1", "spikeCreature1.png"));
            mySprite.AddLayer(SplashKit.LoadBitmap("Spike Image 2", "spikeCreature2.png"), "two");
            return mySprite;
        }

        // returns integer array containing the score, item type, and number of that item required to tame creature
        public override int[] MyRequired()
        {
            return new int[] { 5, 1, 3 };
        }
    }
}
