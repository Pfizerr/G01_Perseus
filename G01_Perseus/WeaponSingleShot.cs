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
        }

        public override void Fire(Vector2 center, Vector2 target, float rotation)
        {
            if (timeSinceLastFire >= rateOfFire)
            {

                EntityManager.AddBullet(new Bullet(center, target, 7f, Color.Red, new Point(20, 20), 10, false));
                
                timeSinceLastFire = 0;
            }
        }
    }
}
