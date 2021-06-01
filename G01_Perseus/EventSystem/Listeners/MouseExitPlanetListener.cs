using G01_Perseus.EventSystem.Events;

namespace G01_Perseus.EventSystem.Listeners
{
    public interface MouseExitPlanetListener : EventListener
    {
        void OnMouseExit(MouseExitPlanetEvent e);
    }
}
