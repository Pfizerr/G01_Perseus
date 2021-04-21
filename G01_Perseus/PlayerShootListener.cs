using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public interface PlayerShootListener : EventListener
    {

        // The method handling the event when it's been dispatched
        void PlayerFired(PlayerShootEvent e);


    }
}
