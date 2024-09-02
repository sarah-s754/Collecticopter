using System;

namespace Collecticopter
{
    public interface GameState
    {
        public abstract void HandleInput();

        public abstract void Update();

        public abstract void Draw();
    }
}
