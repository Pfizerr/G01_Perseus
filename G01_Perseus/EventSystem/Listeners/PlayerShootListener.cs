
namespace G01_Perseus
{
    public interface PlayerShootListener : EventListener
    {

        // The method handling the event when it's been dispatched
        void PlayerFired(PlayerShootEvent e);


    }
}
