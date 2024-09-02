using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace Collecticopter
{
    public class Message
    {
        private List<string> _message;
        private MessageType _type;
        private Point2D _position;
        private int[] _dimentions;
        private int[] _padding;

        public Message(List<string> msg, MessageType type)
        {
            _message = msg;
            _type = type;
            _position = TypePosition();
            _dimentions = TypeDimentions();
            _padding = new int[] {20, 10};
        }

        // return the position of a message of _type
        public Point2D TypePosition()
        {
            switch (_type)
            {
                case MessageType.MenuMessage:
                    return new Point2D() { X = 80, Y = SplashKit.ScreenHeight() / 2 + 10 };
                case MessageType.LevelMessage:
                    return new Point2D() { X = SplashKit.ScreenWidth() / 3, Y = 20 };
                default:
                    return new Point2D() { X = SplashKit.ScreenWidth() / 3, Y = 10 };
            }
        }

        // return the message box dimentions of a message of _type
        public int[] TypeDimentions()
        {
            switch (_type)
            {
                case MessageType.MenuMessage:
                    return new int[] { SplashKit.ScreenWidth() - 160, SplashKit.ScreenHeight() / 2 - 70 };
                case MessageType.LevelMessage:
                    return new int[] { SplashKit.ScreenWidth() / 3 + 30, 60 };
                default:
                    return new int[] { SplashKit.ScreenWidth() / 3, 60 };
            }
        }

        public void Draw()
        {
            // draw message box
            SplashKit.FillRectangle(Color.LightGray, _position.X - 1, _position.Y - 1, _dimentions[0] + 2, _dimentions[1] + 2, SplashKit.OptionToScreen());
            SplashKit.FillRectangle(Color.White, _position.X, _position.Y, _dimentions[0], _dimentions[1], SplashKit.OptionToScreen());

            // draw each line of _message
            int i = 0;
            foreach (string msg in _message)
            {
                SplashKit.DrawText(msg, Color.Black, _position.X + _padding[(int) _type], _position.Y + _padding[(int)_type] + 30 * i, SplashKit.OptionToScreen());
                i++;
            }
        }
    }
}
