using G01_Perseus.EventSystem.Events;

namespace G01_Perseus.EventSystem.Listeners
{
    public interface MissionSelectedListener : EventListener
    {
        void OnSelect(MissionSelectedEvent e);
    }
}
