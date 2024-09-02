using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace Collecticopter
{
    public class Inventory
    {
        private List<Item> _items;
        private List<Bitmap> _bitmaps;
        private List<int> _itemCount;
        
        public Inventory()
        {
            _items = new List<Item>();
            _bitmaps = new List<Bitmap>() { SplashKit.LoadBitmap("Carrot Icon", "carrot_icon.png"), SplashKit.LoadBitmap("Brown Mushroom Icon", "mushroom_brown_icon.png"), SplashKit.LoadBitmap("Red Mushroom Icon", "mushroom_red_icon.png") };
            _itemCount = new List<int>() { 0, 0, 0 };
            //_itemCount = new List<int>() { 1, 3, 5 };
        }

        public void Draw()
        {
            double width = SplashKit.ScreenWidth();
            double height = SplashKit.ScreenHeight();

            // draw inventory bar
            SplashKit.FillRectangle(Color.LightGray, 20, height - 60, width - 40, 50, SplashKit.OptionToScreen());

            // draw bitmap and item count for each item type
            int i = 0;
            while (i < _bitmaps.Count)
            {
                SplashKit.DrawBitmap(_bitmaps[i], 30 + 75 * i, height - 50, SplashKit.OptionToScreen());
                SplashKit.DrawText("" + _itemCount[i], Color.Black, 40 + 75 * i + _bitmaps[i].Width, height - 20, SplashKit.OptionToScreen());
                i++;
            }
        }

        // return integer amount of a type of item that is in the list _items
        public int ItemCount(int typIndex)
        {
            return _itemCount[typIndex];
        }

        // return boolean value indicating whether _items list contains items of given type
        public bool HasItem(ItemType type)
        {
            foreach (Item itm in _items)
            {
                if (itm.Type == type)
                {
                    return true;
                }
            }
            return false;
        }

        // add item to _items list and increase count for that type in _itemCount list
        public void Collect(Item itm)
        {
            _items.Add(itm);
            _itemCount[(int)itm.Type]++;
        }

        // remove item from _items list and decrease count for that type in _itemCount list
        public Item Give(ItemType typ)
        {
            foreach (Item itm in _items)
            {
                if (itm.Type == typ)
                {
                    _items.Remove(itm);
                    _itemCount[(int)itm.Type]--;
                    return itm;
                }
            }
            return null;
        }
    }
}
