
namespace G01_Perseus
{
    public class MouseExitPlanetEvent : Event
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
