using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    class WeaponLaser : Weapon // Currently non-functional
    {
        //private TypeOfLaser laserType;
        public WeaponLaser(int iD, float powerLevel) : base(iD, powerLevel)
        {
            name = "Laser";
            baseDamagePerShot = 5;
            SetDamagePerShot(powerLevel);
            fireTimer = new Timer(100);
        }

        public override void Fire(Vector2 center, Vector2 target, float rotation, TypeOfBullet type, GameTime gameTime)
        {
            //if (fireTimer.IsDone(gameTime))
            //{
            //    if (type == TypeOfBullet.Enemy)
            //        laserType = TypeOfLaser.Enemy;
            //    else
            //        laserType = TypeOfLaser.Player;

            //    EntityManager.CreateLaser(laserType, center, target, damagePerShot);

            //    fireTimer.Reset(gameTime);
            //}
        }
    }
}
