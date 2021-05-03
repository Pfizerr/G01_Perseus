using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public abstract class MovingEntity : Entity
    {
        protected float health, maxHealth, deltaTime, totalTimeLastFrame;
        protected Vector2 velocity, maxVelocity;

        public MovingEntity(Vector2 velocity, Vector2 position, Vector2 scale, Rectangle? source, float rotation, float layerDepth, bool isCollidable) 
            : base (position, scale, source, rotation, layerDepth, isCollidable)
        {
            this.velocity = velocity;
        }

        public override void Update(GameTime gameTime)
        {
            var totalTimeThisFrame = (float)gameTime.TotalGameTime.TotalSeconds;
            deltaTime = totalTimeThisFrame - totalTimeLastFrame;
            totalTimeLastFrame = totalTimeThisFrame;

            if (health <= 0)
            {
                IsAlive = false;
            }
        }
    }
}
