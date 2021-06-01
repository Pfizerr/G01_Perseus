using G01_Perseus.EventSystem.Listeners;

namespace G01_Perseus.EventSystem.Events
{
    public class MissionDeniedClickEvent : Event
    {
        public Mission Mission { get; private set; }

        public MissionDeniedClickEvent(Mission mission)
        {
            Mission = mission;
        }

        public override void Dispatch(EventListener listener)
        {
            if(!(listener is MissionDeniedClickListener))
            {
                return;
            }

            MissionDeniedClickListener l = (listener as MissionDeniedClickListener);

            l.OnDenied(this);
        }
    }
}
