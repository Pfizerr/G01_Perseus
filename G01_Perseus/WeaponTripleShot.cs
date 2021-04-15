using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    class WeaponTripleShot : Weapon
    {
        private Vector2 secondBulletTarget, thirdBulletTarget;
        public WeaponTripleShot(int iD, float powerLevel) : base(iD, powerLevel)
        {
            name = "Tripleshot";
            rateOfFire = 1.5f;
            baseDamagePerShot = 2;
            damagePerShot = baseDamagePerShot * powerLevel;
        }

        public override void Fire(Vector2 center, Vector2 target, float rotation, Entity parent)
        {
            if (timeSinceLastFire >= rateOfFire)
            {
                
                secondBulletTarget.X = (float)Math.Cos(rotation + (float)Math.PI * 1.3) + center.X;
                secondBulletTarget.Y = (float)Math.Sin(rotation + (float)Math.PI * 1.3) + center.Y;
                thirdBulletTarget.X = (float)Math.Cos(rotation + (float)Math.PI * 1.7) + center.X;
                thirdBulletTarget.Y = (float)Math.Sin(rotation + (float)Math.PI * 1.7) + center.Y;

                EntityManager.CreateBullet(parent, center, target, false);
                EntityManager.CreateBullet(parent, center, secondBulletTarget, false);
                EntityManager.CreateBullet(parent, center, thirdBulletTarget, false);
                timeSinceLastFire = 0;
            }
        }
    }
}
