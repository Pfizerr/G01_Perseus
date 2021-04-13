<<<<<<< HEAD
﻿using System;
=======
﻿using Microsoft.Xna.Framework;
using System;
>>>>>>> parent of be71ea6 (Revert "Merge branch 'Weapon1' into main")
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
<<<<<<< HEAD
    class Weapon
    {
=======
    public class Weapon : Equipment
    {
        protected float rateOfFire;
        protected float timeSinceLastFire;
        protected float baseDamagePerShot;
        protected float powerLevel;
        protected float damagePerShot;

        public Weapon(int iD, float powerLevel) : base(iD)
        {
            this.powerLevel = powerLevel;
            damagePerShot = powerLevel * baseDamagePerShot;
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
>>>>>>> parent of be71ea6 (Revert "Merge branch 'Weapon1' into main")
    }
}
