using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class PlanetInteractionEvent : Event
    {

        public Planet Planet
        {
            get;
            private set;
        }

        public PlanetInteractionEvent(Planet planet)
        {
            Planet = planet;
        }

        public override void Dispatch(EventListener listener)
        {
            if(!(listener is PlanetInteractionListener))
            {
                return;
            }

            PlanetInteractionListener l = (PlanetInteractionListener)listener;

            l.OnMouseClick(this);
        }
    }
}
