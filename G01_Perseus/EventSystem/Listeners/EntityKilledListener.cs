using G01_Perseus.EventSystem.Events;

namespace G01_Perseus.EventSystem.Listeners
{
    public interface EntityKilledListener : EventListener
    {

        void OnKilled(EntityKilledEvent e);

    }
}