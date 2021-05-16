using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class CollissionEvent : Event
    {
        private Entity entity;
        private Entity otherEntity;

        public CollissionEvent(Entity entity, Entity otherEntity)
        {
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

            //Debug.WriteLine(entity.ToString() + " " + otherEntity.ToString());
            //entity.HandleCollision(otherEntity);
            //otherEntity.HandleCollision(entity);

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
