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
        public WeaponSingleShot(int iD, float powerLevel) : base(iD, powerLevel)
        {
            name = "Singleshot";
            rateOfFire = 1f;
            baseDamagePerShot = 5;
            damagePerShot = baseDamagePerShot * powerLevel;
            fireTimer = new Timer(1000);
        }

        public override void Fire(Vector2 center, Vector2 target, float rotation, TypeOfBullet type, GameTime gameTime)
        {
            if (fireTimer.IsDone(gameTime))
            {

                EntityManager.CreateBullet(type, center, target, false, damagePerShot);

                fireTimer.Reset(gameTime);
            }
        }

    }
}