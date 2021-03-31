using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class BasicTurret : Weapon
    {
        public BasicTurret(int baseValue, float baseDamage, float baseAttackSpeed, Entity parent) : base(baseValue, baseDamage, baseAttackSpeed, parent)
        {
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public override void Activate()
        {
            EntityManager.AddBullet(new Bullet(parent.Center, Input.MouseWorldPosition, 10f, Color.Red, new Point(4, 4), 15));
        }
    }
}
