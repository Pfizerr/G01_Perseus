using G01_Perseus.EventSystem.Listeners;

namespace G01_Perseus.EventSystem.Events
{
    public class PopStateEvent : Event
    {
        public override void Dispatch(EventListener listener)
        {
            if (!(listener is PopStateListener))
            {
                return;
            }

            PopStateListener l = (PopStateListener)listener;
            l.OnPopState(this);
        }
    }
}