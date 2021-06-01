using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus.EventSystem.Events
{
    public class RespawnPlayerEvent : Event
    {
        public override void Dispatch(EventListener listener)
        {
            if(!(listener is GameOverListener))
            {
                return;
            }

            GameOverListener l = (listener as GameOverListener);
            l.OnRespawnPlayer();
        }
    }
}
