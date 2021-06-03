using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G01_Perseus
{
    public abstract class Weapon : Equipment
    {
        protected float baseDamagePerShot;
        protected float powerLevel;
        protected float damagePerShot;
        protected Timer fireTimer;

        public Weapon(int iD, float powerLevel) : base(iD)
        {
            this.powerLevel = powerLevel;
            //damagePerShot = powerLevel * baseDamagePerShot;
        }

        

        

        public void SetDamagePerShot(float powerLevel)
        {
            damagePerShot = powerLevel * baseDamagePerShot;
        }
            

        public abstract void Fire(Vector2 origin, Vector2 target, TypeOfBullet typeOfBullet, GameTime gameTime);
        
    }
}
