using G01_Perseus.EventSystem.Listeners;

namespace G01_Perseus.EventSystem.Events
{
    public class MissionAcceptedClickEvent : Event
    {
        public Mission Mission { get; private set; }

        public MissionAcceptedClickEvent(Mission mission)
        {
            Mission = mission;
        }

        public override void Dispatch(EventListener listener)
        {
            if(!(listener is MissionAcceptedClickListener))
            {
                return;
            }

            MissionAcceptedClickListener l = (listener as MissionAcceptedClickListener);

            l.OnAccepted(this);
        }
    }
}
