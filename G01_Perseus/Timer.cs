using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class Timer
    {
        float interval;
        double start;

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
