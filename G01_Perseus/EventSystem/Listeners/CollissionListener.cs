using G01_Perseus.EventSystem.Events;

namespace G01_Perseus.EventSystem.Listeners
{
    public interface CollissionListener : EventListener
    {
        void Collision(CollissionEvent e);
    }
}