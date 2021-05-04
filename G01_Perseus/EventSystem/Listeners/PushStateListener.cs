using G01_Perseus.EventSystem.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus.EventSystem.Listeners
{
    public interface PushStateListener : EventListener
    {

        void OnPushState(PushStateEvent e);

    }
}
