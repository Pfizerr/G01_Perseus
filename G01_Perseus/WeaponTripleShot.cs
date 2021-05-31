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
        public WeaponTripleShot(int iD, float powerLevel, int fireRate) : base(iD, powerLevel, fireRate)
        {
            name = "Tripleshot";
            baseDamagePerShot = 2;
            baseFireTimer = 1500;
            SetDamagePerShot(powerLevel);
            SetFireTimer(fireRate);

        }

        public override void Fire(Vector2 center, Vector2 target, float rotation, TypeOfBullet type, GameTime gameTime)
        {
            if (fireTimer.IsDone(gameTime))
            {
                // Calculates angles based on the 
                secondBulletTarget.X = (float)Math.Cos(rotation + (float)Math.PI * 1.3) + center.X;
                secondBulletTarget.Y = (float)Math.Sin(rotation + (float)Math.PI * 1.3) + center.Y;
                thirdBulletTarget.X = (float)Math.Cos(rotation + (float)Math.PI * 1.7) + center.X;
                thirdBulletTarget.Y = (float)Math.Sin(rotation + (float)Math.PI * 1.7) + center.Y;

                EntityManager.CreateBullet(type, center, target, damagePerShot);
                EntityManager.CreateBullet(type, center, secondBulletTarget, damagePerShot);
                EntityManager.CreateBullet(type, center, thirdBulletTarget, damagePerShot);
                fireTimer.Reset(gameTime);
            }
        }
    }
}
