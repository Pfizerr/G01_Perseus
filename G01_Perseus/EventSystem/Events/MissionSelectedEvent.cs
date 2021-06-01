using G01_Perseus.EventSystem.Listeners;

namespace G01_Perseus.EventSystem.Events
{
    public class MissionSelectedEvent : Event
    {
        public Mission Mission { get; private set; }
        public int SlotIndex { get; private set; }

        public MissionSelectedEvent(Mission mission, int slotIndex)
        {
            Mission = mission;
            SlotIndex = slotIndex;
        }

        public override void Dispatch(EventListener listener)
        {
            if (!(listener is MissionSelectedListener))
            {
                return;
            }

            MissionSelectedListener l = (listener as MissionSelectedListener);

            l.OnSelect(this);
        }
    }
}
