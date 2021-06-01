using G01_Perseus.EventSystem.Listeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
