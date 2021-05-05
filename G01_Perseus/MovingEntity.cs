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
        protected float deltaTime, totalTimeLastFrame;
        protected Vector2 maxVelocity;

        public MovingEntity(Vector2 maxVelocity, Vector2 position, Vector2 scale) : base (position, scale)
        {
            this.maxVelocity = maxVelocity;
        }

        public override void Update(GameTime gameTime)
        {
            var totalTimeThisFrame = (float)gameTime.TotalGameTime.TotalSeconds;
            deltaTime = totalTimeThisFrame - totalTimeLastFrame;
            totalTimeLastFrame = totalTimeThisFrame;

        }
    }
}
