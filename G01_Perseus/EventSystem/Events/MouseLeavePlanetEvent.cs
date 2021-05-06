using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class MouseLeavePlanetEvent : Event
    {
        public override void Dispatch(EventListener listener)
        {
            if(!(listener is MouseLeavePlanetListener))
            {
                return;
            }

            MouseLeavePlanetListener l = (MouseLeavePlanetListener)listener;

            l.OnMouseLeave(this);
        }
    }
}
