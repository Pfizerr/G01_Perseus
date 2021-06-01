using G01_Perseus.EventSystem.Listeners;

namespace G01_Perseus.EventSystem.Events
{
    public class MouseExitPlanetEvent : Event
    {
        public override void Dispatch(EventListener listener)
        {
            if(!(listener is MouseExitPlanetListener))
            {
                return;
            }

            MouseExitPlanetListener l = (MouseExitPlanetListener)listener;

            l.OnMouseExit(this);
        }
    }
}
