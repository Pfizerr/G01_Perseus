using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class PlanetHighlightEvent : Event
    {
        public Planet Planet
        {
            get;
            private set;
        }

        public PlanetHighlightEvent(Planet planet)
        {
            Planet = planet;
        }

        public override void Dispatch(EventListener listener)
        {
            
            if(!(listener is MouseEnterPlanetListener))
            {
                return;
            }

            MouseEnterPlanetListener l = (MouseEnterPlanetListener)listener;

            l.OnMouseEnter(this);
        }
    }
}
