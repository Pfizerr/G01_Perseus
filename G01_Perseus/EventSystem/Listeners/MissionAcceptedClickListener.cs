using G01_Perseus.EventSystem.Events;

namespace G01_Perseus.EventSystem.Listeners
{
    public interface MissionAcceptedClickListener : EventListener
    {
        void OnAccepted(MissionAcceptedClickEvent e);
    }
}
