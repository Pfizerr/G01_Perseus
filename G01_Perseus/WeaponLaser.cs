﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    class WeaponLaser : Weapon
    {
        public WeaponLaser(int iD, float powerLevel) : base(iD, powerLevel)
        {
            name = "Laser";
            rateOfFire = 0.1f;
            baseDamagePerShot = 5;
            damagePerShot = baseDamagePerShot * powerLevel;
        }

        public override void Fire(Vector2 center, Vector2 target, float rotation, TypeOfBullet type)
        {
            if (timeSinceLastFire >= rateOfFire)
            {
                EntityManager.CreateBullet(type, center, target, true);

                timeSinceLastFire = 0;
            }
        }
    }
}
