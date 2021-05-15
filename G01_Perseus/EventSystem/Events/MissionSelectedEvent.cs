using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus.UI
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
