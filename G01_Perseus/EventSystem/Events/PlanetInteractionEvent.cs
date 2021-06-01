using G01_Perseus.EventSystem.Listeners;

namespace G01_Perseus.EventSystem.Events
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
