using G01_Perseus.EventSystem.Events;

namespace G01_Perseus.EventSystem.Listeners
{
    public interface PlayerShootListener : EventListener
    {
        // The method handling the event when it's been dispatched
        void PlayerFired(PlayerShootEvent e);
    }
}
