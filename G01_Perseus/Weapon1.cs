using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace G01_Perseus
{
    public class Weapon1 : Weapon
    {
        private Vector2 secondBulletTarget, thirdBulletTarget, distance;
        Vector2 bulletOffset = new Vector2(100, 100);
        float rotation2;
        public Weapon1(int iD) : base(iD)
        {
            name = "Weapon1";
            rateOfFire = 0.1f;
        }

        public override void Fire(Vector2 center, Vector2 target, float rotation)
        {
            if (timeSinceLastFire >= rateOfFire)
            {
                distance.X = target.X - center.X;
                distance.Y = target.Y - center.Y;
                rotation2 = (float)Math.Atan2(distance.Y, distance.X);
                secondBulletTarget.X = (float)Math.Cos(rotation - (float)Math.PI * 1.1f) * 100 + center.X;
                secondBulletTarget.Y = (float)Math.Sin(rotation - (float)Math.PI * 1.1f) * 100 + center.Y;
                thirdBulletTarget.X = (float)Math.Cos(rotation + (float)Math.PI * 1.1f) * 100 + center.X;
                thirdBulletTarget.Y = (float)Math.Sin(rotation + (float)Math.PI * 1.1f) * 100 + center.Y;

                EntityManager.AddBullet(new Bullet(center, target, 7f, Color.Red, new Point(20, 20), 10));
                EntityManager.AddBullet(new Bullet(center, secondBulletTarget, 7f, Color.Red, new Point(20, 20), 10));
                EntityManager.AddBullet(new Bullet(center, thirdBulletTarget, 7f, Color.Red, new Point(20, 20), 10));
                timeSinceLastFire = 0;
            }
        }

    }
}
