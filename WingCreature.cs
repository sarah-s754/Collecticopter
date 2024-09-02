using System;
using SplashKitSDK;

namespace Collecticopter
{
    public class WingCreature : Creature
    {
        private int _visibleLayer;

        public WingCreature(Vector2D velocity) : base(velocity)
        {
            _visibleLayer = 0;
        }

        public WingCreature() : this(new Vector2D() { X = 2 * (SplashKit.Rnd(-1, 1) * 2 - 1), Y = 3 * (SplashKit.Rnd(-1, 1) * 2 - 1) })
        {
        }

        public override Sprite MySprite()
        {
            Sprite mySprite = SplashKit.CreateSprite("Wing Creature Images", SplashKit.LoadBitmap("Wing Image 1", "wingCreature1.png"));
            mySprite.AddLayer(SplashKit.LoadBitmap("Wing Image 2", "wingCreature2.png"), "two");
            mySprite.AddLayer(SplashKit.LoadBitmap("Wing Image 3", "wingCreature3.png"), "three");
            mySprite.AddLayer(SplashKit.LoadBitmap("Wing Image 4", "wingCreature4.png"), "four");
            mySprite.AddLayer(SplashKit.LoadBitmap("Wing Image 5", "wingCreature5.png"), "five");
            return mySprite;
        }

        // returns integer array containing the score, item type, and number of that item required to tame creature
        public override int[] MyRequired()
        {
            return new int[] { 10, 2, 5 };
        }

        // periodically change which layer of sprite is visible
        public override void ChangeImage()
        {
            if (Timer.Ticks > 300)
            {
                Sprite.ToggleLayerVisible(_visibleLayer);

                if (_visibleLayer < Sprite.LayerCount - 1) _visibleLayer++;
                else _visibleLayer = 0;

                Sprite.ToggleLayerVisible(_visibleLayer);
                Timer.Reset();
            }
        }
    }
}
