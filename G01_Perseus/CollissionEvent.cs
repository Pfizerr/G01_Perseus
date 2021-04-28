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

            Debug.WriteLine(entity.ToString() + " " + otherEntity.ToString());
            entity.HandleCollision(otherEntity);
            //otherEntity.HandleCollision(entity);

        }

        public override void Dispatch(EventListener listener)
        {
            if (!(listener is CollissionListener))
            {
                return;
            }

            CollissionListener l = (CollissionListener)listener;

            l.Collision(this);
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

            return false;
        }
    }
}
