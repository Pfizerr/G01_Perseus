using G01_Perseus.EventSystem.Events;

namespace G01_Perseus.EventSystem.Listeners
{
    public interface MissionDeniedClickListener : EventListener
    {
        void OnDenied(MissionDeniedClickEvent e);
    }
}
