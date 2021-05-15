using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus.UI
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
