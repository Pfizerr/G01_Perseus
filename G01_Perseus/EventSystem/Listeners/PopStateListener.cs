using G01_Perseus.EventSystem.Events;

namespace G01_Perseus.EventSystem.Listeners
{
    public interface PopStateListener : EventListener
    {
        void OnPopState(PopStateEvent e);
    }
}
