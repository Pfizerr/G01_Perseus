using G01_Perseus.EventSystem.Listeners;

namespace G01_Perseus.EventSystem.Events
{
    public class MouseEnterPlanetEvent : Event
    {
        public Planet Planet
        {
            get;
            private set;
        }

        public MouseEnterPlanetEvent(Planet planet)
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
