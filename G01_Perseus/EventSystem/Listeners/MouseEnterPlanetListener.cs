using G01_Perseus.EventSystem.Events;

namespace G01_Perseus.EventSystem.Listeners
{
    public interface MouseEnterPlanetListener : EventListener
    {
        void OnMouseEnter(MouseEnterPlanetEvent e);
    }
}
