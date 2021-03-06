using G01_Perseus.EventSystem.Listeners;

namespace G01_Perseus.EventSystem.Events
{
    public class CollissionEvent : Event
    {
        private Entity entity;
        private Entity otherEntity;

        public CollissionEvent(Entity entity, Entity otherEntity)
        {
            if (entity is Explosion || otherEntity is Explosion)
            {
                return;
            }
            if (!entity.IsCollidable || !otherEntity.IsCollidable || !entity.IsAlive || !otherEntity.IsAlive)
            {
                return;
            }

            if(CheckParent(entity, otherEntity) || CheckParent(otherEntity, entity))
            {
                return;
            }

            this.entity = entity;
            this.otherEntity = otherEntity;
        }

        public Entity Entity => entity;
        public Entity OtherEntity => otherEntity;

        public override void Dispatch(EventListener listener)
        {
            if (!(listener is CollissionListener))
            {
                return;
            }

            CollissionListener l = (CollissionListener)listener;

            if(listener.Equals(entity))
            {
                l.Collision(this);
            }
        }

        private bool CheckParent(Entity entity, Entity otherEntity)
        {
            if (entity is Bullet bullet)
            {
                if (bullet.Type == TypeOfBullet.Player && otherEntity is Player)
                {
                    return true;
                }
                else if (bullet.Type == TypeOfBullet.Enemy && otherEntity is Enemy)
                {
                    return true;
                }               
            }
            if (entity is Laser laser)
            {
                if (laser.Type == TypeOfLaser.Player && otherEntity is Player)
                {
                    return true;
                }
                else if (laser.Type == TypeOfLaser.Enemy && otherEntity is Enemy)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
