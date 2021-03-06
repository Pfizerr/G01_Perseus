using G01_Perseus.EventSystem.Listeners;

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