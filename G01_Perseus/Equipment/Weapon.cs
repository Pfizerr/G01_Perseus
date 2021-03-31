using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public abstract class Weapon : Equipment
    {
        private float baseDamage;
        private float baseAttackSpeed;
        public Weapon(int baseValue, float baseDamage, float baseAttackSpeed, Entity parent) : base(baseValue, parent)
        {
            this.baseDamage = baseDamage;
            this.baseAttackSpeed = baseAttackSpeed;
        }

        public abstract void Activate();
    }
}
