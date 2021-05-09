using Microsoft.Xna.Framework;

namespace G01_Perseus
{
    public class ExtendedTimer
    {
        public float Duration { get; private set; }
        public float Counter { get; private set; }
        public bool IsCounting { get; private set; }
        public bool IsDoneCounting { get; private set; }
        public float TimeRemaining { get { return (Duration - Counter); } private set { Duration = Counter + value; } }

        public ExtendedTimer(float duration, bool start)
        {
            Duration = duration;
            if (start)
            {
                Start();
            }
        }

        public void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds;

            if (IsCounting && !IsDoneCounting)
            {
                Counter += elapsedTime;

                if (Counter >= Duration)
                {
                    Counter = Duration;
                    IsDoneCounting = true;
                    Stop();
                }
            }
        }

        public void Start() => IsCounting = true;
        public void Stop() => IsCounting = false;

        public void Reset(float duration, bool start)
        {
            Counter = 0;
            Duration = duration;
            IsDoneCounting = false;

            if (start)
            {
                Start();
            }
        }
    }
}