using G01_Perseus.EventSystem.Events;

namespace G01_Perseus.EventSystem.Listeners
{
    public interface MissionTurnedInListener
    {
        void OnTurnIn(MissionTurnedInEvent e);
    }
}
