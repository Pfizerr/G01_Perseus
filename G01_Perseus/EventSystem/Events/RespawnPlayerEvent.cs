using G01_Perseus.EventSystem.Listeners;

namespace G01_Perseus.EventSystem.Events
{
    public class RespawnPlayerEvent : Event
    {
        public override void Dispatch(EventListener listener)
        {
            if(!(listener is GameOverListener))
            {
                return;
            }

            GameOverListener l = (listener as GameOverListener);
            l.OnRespawnPlayer();
        }
    }
}
