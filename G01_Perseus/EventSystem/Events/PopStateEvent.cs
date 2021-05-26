using G01_Perseus.EventSystem.Listeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus.EventSystem.Events
{
    public class PopStateEvent : Event
    {
        public override void Dispatch(EventListener listener)
        {
            if (!(listener is PopStateListener))
            {
                return;
            }

            PopStateListener l = (PopStateListener)listener;
            l.OnPopState(this);
        }
    }
}