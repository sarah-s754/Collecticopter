using System;
using SplashKitSDK;

namespace Collecticopter
{
    public class Program
    {
        public static void Main()
        {
            Window myWindow = new Window("Collecticopter", 800, 600);
            Game myGame = Game.GetGame();

            while (!myWindow.CloseRequested && myGame.Quit == false)
            {
                SplashKit.ProcessEvents();
                myGame.HandleInput();
                myGame.Update();
                myWindow.Clear(Color.LightSkyBlue);
                myGame.Draw();
                myWindow.Refresh(60);
            }
        }
    }
}
