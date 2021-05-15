using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus.UI
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
