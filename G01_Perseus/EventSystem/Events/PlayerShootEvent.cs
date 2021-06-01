using G01_Perseus.EventSystem.Listeners;
using Microsoft.Xna.Framework;

namespace G01_Perseus.EventSystem.Events
{
    public class PlayerShootEvent : Event
    {
        public Vector2 position;
        public int damage;
        public Weapon weapon;

        public PlayerShootEvent(Vector2 position, int damage)
        {
            this.position = position;
            this.damage = damage;
        }

        // DONT FORGET: To call EventManager.Register(this) from the listening class constructor
        // DONT FORGET: To add the listener class as derived class on the listening class        

        public override void Dispatch(EventListener listener)
        {
            // This if statement should be present in every Dispatch-method
            // However, the class "PlayerShootListener" will be changed to the relevant listener class
            if (!(listener is PlayerShootListener)) 
            {
                return;
            }

            // Type cast to correct listener class, "PlayerShootListener" is because this is the PlayerShootEvent
            PlayerShootListener l = (PlayerShootListener)listener;

            // Call the correct method
            l.PlayerFired(this);
        }
    }
}
