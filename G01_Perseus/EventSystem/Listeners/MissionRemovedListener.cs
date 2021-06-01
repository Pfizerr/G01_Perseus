using G01_Perseus.EventSystem.Events;

namespace G01_Perseus.EventSystem.Listeners
{
    public interface MissionRemovedListener : EventListener
    {
        void OnRemoved(MissionRemovedEvent e);
    }
}
