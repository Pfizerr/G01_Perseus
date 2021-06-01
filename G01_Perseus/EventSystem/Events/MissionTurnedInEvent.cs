using G01_Perseus.EventSystem.Listeners;

namespace G01_Perseus.EventSystem.Events
{
    public class MissionTurnedInEvent : Event
    {
        public MissionTurnedInEvent(Mission mission)
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
            if(!(listener is MissionTurnedInListener))
            {
                return;
            }

            MissionTurnedInListener l = (MissionTurnedInListener)listener;
            l.OnTurnIn(this);
        }
    }
}
