using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class Weapon : Equipment
    {
        protected float rateOfFire;
        protected float timeSinceLastFire;

        public Weapon(int iD) : base(iD)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (timeSinceLastFire <= rateOfFire)
            {
                timeSinceLastFire += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public virtual void Fire(Vector2 center, Vector2 target, float rotation)
        {

        }
    }
}
