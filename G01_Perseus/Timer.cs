using Microsoft.Xna.Framework;

namespace G01_Perseus
{
    public class Timer
    {
        private float interval;
        private double start;

        public Timer(float interval)
        {
            this.interval = interval;
        }

        public bool IsDone(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds >= interval + start)
            {
                return true;
            }

            return false;
        }

        public void Start(GameTime gameTime)
        {
            start = gameTime.TotalGameTime.TotalMilliseconds;
        }

        public void Reset(GameTime gameTime)
        {
            Start(gameTime);
        }
    }
}
