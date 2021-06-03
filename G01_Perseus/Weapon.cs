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
        protected float baseFireTimer;
        public bool available;

        public virtual string Name => "NONAME";

        public Weapon(int iD, float powerLevel, float fireRate, bool available) : base(iD)
        {
            this.powerLevel = powerLevel;
            this.fireTimer = new Timer((int)baseFireTimer);
            this.available = available;
        }  
        
        public void SetDamagePerShot(float powerLevel)
        {
            damagePerShot = powerLevel * baseDamagePerShot;
        }
            

        public abstract void Fire(Vector2 origin, Vector2 target, TypeOfBullet typeOfBullet, GameTime gameTime);
        

        public void SetFireTimer(float fireRate)
        {
            if (baseFireTimer - fireRate < 0)
            {
                this.fireTimer = new Timer(0);
            }
            else
            {                
                this.fireTimer = new Timer((int)(baseFireTimer - baseFireTimer / 100 * fireRate));
            }
            
        }
    }
}
