using G01_Perseus.EventSystem.Listeners;
using Microsoft.Xna.Framework;

namespace G01_Perseus.EventSystem.Events
{
    public class SectorEnteredEvent : Event
    {

        public SectorEnteredEvent(Entity entity, Point sector)
        {
            Entity = entity;
            Sector = sector;
        }

        public Entity Entity
        {
            get;
            private set;
        }

        public Point Sector
        {
            get;
            private set;
        }

        public override void Dispatch(EventListener listener)
        {
            if(!(listener is SectorEnteredListener))
            {
                return;
            }

            SectorEnteredListener l = (SectorEnteredListener)listener;
            l.OnSectorEntered(this);
        }
    }
}
