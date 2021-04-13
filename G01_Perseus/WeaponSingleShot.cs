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
    class WeaponSingleShot
    {
=======
    public class WeaponSingleShot : Weapon
    {
        public WeaponSingleShot(int iD, float powerLevel) : base(iD, powerLevel)
        {
            name = "Singleshot";
            rateOfFire = 1f;
            baseDamagePerShot = 5;
            damagePerShot = baseDamagePerShot * powerLevel;
        }

        public override void Fire(Vector2 center, Vector2 target, float rotation)
        {
            if (timeSinceLastFire >= rateOfFire)
            {

                EntityManager.AddBullet(new Bullet(center, target, 7f, Color.Red, new Point(20, 20), 10, false));
                
                timeSinceLastFire = 0;
            }
        }
>>>>>>> parent of be71ea6 (Revert "Merge branch 'Weapon1' into main")
    }
}
