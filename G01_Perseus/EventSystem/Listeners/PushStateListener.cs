using G01_Perseus.EventSystem.Events;

namespace G01_Perseus.EventSystem.Listeners
{
    public interface PushStateListener : EventListener
    {
        void OnPushState(PushStateEvent e);
    }
}
