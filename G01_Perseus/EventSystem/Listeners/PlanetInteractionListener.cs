using G01_Perseus.EventSystem.Events;

namespace G01_Perseus.EventSystem.Listeners
{
    public interface PlanetInteractionListener : EventListener
    {
        void OnMouseClick(PlanetInteractionEvent e);
    }
}
