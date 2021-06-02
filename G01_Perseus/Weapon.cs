﻿using System;
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

        public Weapon(int iD, float powerLevel, float fireRate, bool available) : base(iD)
        {
            this.powerLevel = powerLevel;
            this.fireTimer = new Timer(baseFireTimer);
            this.available = available;
        }  
        
        public void SetDamagePerShot(float powerLevel)
        {
            damagePerShot = powerLevel * baseDamagePerShot;
        }
            

        public abstract void Fire(Vector2 center, Vector2 target, float rotation, TypeOfBullet typeOfBullet, GameTime gameTime);
        

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
