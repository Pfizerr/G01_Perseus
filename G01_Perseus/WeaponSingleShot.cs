using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class WeaponSingleShot : Weapon
    {
        public WeaponSingleShot(int iD, float powerLevel, float fireRate, bool available) : base(iD, powerLevel, fireRate, available)
        {
            name = "Singleshot";
            baseDamagePerShot = 5;
            baseFireTimer = 1000;
            SetDamagePerShot(powerLevel);
            SetFireTimer(fireRate);
            
        }

        public override void Fire(Vector2 center, Vector2 target, float rotation, TypeOfBullet type, GameTime gameTime)
        {
            if (fireTimer.IsDone(gameTime))
            {

                EntityManager.CreateBullet(type, center, target, damagePerShot);

                fireTimer.Reset(gameTime);
            }
        }

    }
}