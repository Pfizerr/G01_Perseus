using G01_Perseus.EventSystem.Listeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus.EventSystem.Events
{
    public class PushStateEvent : Event
    {

        public PushStateEvent(GameState state)
        {
            this.State = state;
        }

        public GameState State
        {
            get;
            private set;
        }

        public override void Dispatch(EventListener listener)
        {
            if (!(listener is PushStateListener))
            {
                return;
            }

            PushStateListener l = (PushStateListener)listener;
            l.OnPushState(this);
        }
    }
}