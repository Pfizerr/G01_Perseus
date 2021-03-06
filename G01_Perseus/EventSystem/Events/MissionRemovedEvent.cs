using G01_Perseus.EventSystem.Listeners;

namespace G01_Perseus.EventSystem.Events
{
    public class MissionRemovedEvent : Event
    {
        public MissionRemovedEvent (Mission mission)
        {
            Mission = mission;
        }

        public Mission Mission
        {
            get;
            private set;
        }

        public override void Dispatch(EventListener listener)
        {
            if(!(listener is MissionRemovedListener))
            {
                return;
            }

            MissionRemovedListener l = (MissionRemovedListener)listener;
            l.OnRemoved(this);
        }
    }
}
