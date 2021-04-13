using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< HEAD

namespace G01_Perseus
{
    class WeaponTripleShot
    {
=======
using Microsoft.Xna.Framework;

namespace G01_Perseus
{
    public class WeaponTripleShot : Weapon
    {
        private Vector2 secondBulletTarget, thirdBulletTarget;
        public WeaponTripleShot(int iD, float powerLevel) : base(iD, powerLevel)
        {
            name = "Tripleshot";
            rateOfFire = 1.5f;
            baseDamagePerShot = 2;
            damagePerShot = baseDamagePerShot * powerLevel;
        }

        public override void Fire(Vector2 center, Vector2 target, float rotation)
        {
            if (timeSinceLastFire >= rateOfFire)
            {
                secondBulletTarget.X = (float)Math.Cos(rotation - (float)Math.PI * 1.1f) * 100 + center.X;
                secondBulletTarget.Y = (float)Math.Sin(rotation - (float)Math.PI * 1.1f) * 100 + center.Y;
                thirdBulletTarget.X = (float)Math.Cos(rotation + (float)Math.PI * 1.1f) * 100 + center.X;
                thirdBulletTarget.Y = (float)Math.Sin(rotation + (float)Math.PI * 1.1f) * 100 + center.Y;

                EntityManager.AddBullet(new Bullet(center, target, 7f, Color.Red, new Point(20, 20), 10, false));
                EntityManager.AddBullet(new Bullet(center, secondBulletTarget, 7f, Color.Red, new Point(20, 20), 10, false));
                EntityManager.AddBullet(new Bullet(center, thirdBulletTarget, 7f, Color.Red, new Point(20, 20), 10, false));
                timeSinceLastFire = 0;
            }
        }

>>>>>>> parent of be71ea6 (Revert "Merge branch 'Weapon1' into main")
    }
}
