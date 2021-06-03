using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class PlayerDeathEvent : Event
    {

        public override void Dispatch(EventListener listener)
        {
            if (!(listener is PlayerDeathListener))
            {
                return;
            }

            PlayerDeathListener l = (PlayerDeathListener)listener;

            l.PlayerDeath(this);
        }
    }
}
