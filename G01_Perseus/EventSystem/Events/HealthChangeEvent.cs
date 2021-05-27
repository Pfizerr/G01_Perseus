using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class HealthChangeEvent : Event
    {
        public override void Dispatch(EventListener listener)
        {
            // This if statement should be present in every Dispatch-method
            // However, the class "PlayerShootListener" will be changed to the relevant listener class
            if (!(listener is HealthChangeListener)) 
            {
                return;
            }

            // Type cast to correct listener class, "PlayerShootListener" is because this is the PlayerShootEvent
            HealthChangeListener l = (HealthChangeListener)listener;

            // Call the correct method
            l.HealthChange(this);
        }
    }
}
