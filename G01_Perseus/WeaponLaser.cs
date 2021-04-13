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
    class WeaponLaser
    {
=======
    class WeaponLaser : Weapon
    {
        private Vector2 distance;
        private float laserLength;
        public WeaponLaser(int iD, float powerLevel) : base(iD, powerLevel)
        {
            name = "Laser";
            rateOfFire = 0.1f;
            baseDamagePerShot = 5;
            damagePerShot = baseDamagePerShot * powerLevel;
        }

        public override void Fire(Vector2 center, Vector2 target, float rotation)
        {
            if (timeSinceLastFire >= rateOfFire)
            {
                distance = target - center;
                laserLength = Vector2.Distance(center, target);
                EntityManager.AddBullet(new Bullet(center, target, 0f, Color.Red, new Point((int)laserLength, 10), 1, true));

                timeSinceLastFire = 0;
            }
        }
>>>>>>> parent of be71ea6 (Revert "Merge branch 'Weapon1' into main")
    }
}
