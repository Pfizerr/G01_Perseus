using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public abstract class Ship : MovingEntity
    {
        public Vector2 direction;
        protected Vector2 friction;
        protected Vector2 acceleration;
        protected Vector2 velocity;
        protected Weapon equipedWeapon;
        //Can use wapon dependinng on the type of enemy
        protected Weapon trippleShot = new WeaponTripleShot(1, 1);
        protected Weapon singleShot = new WeaponSingleShot(1, 1);
        protected double hitTimer, hitTimerInterval, maxShields;
        protected List<Weapon> weapons;
        protected float health, maxHealth;

        //Components
        protected PlayerStatus playerStatus;
        public double Shields { get; protected set; }

        public Ship(Vector2 maxVelocity, Vector2 position, Vector2 scale, float health, float shield) : base(maxVelocity, position, scale)
        {
            this.health = health;
            maxHealth = health;
            Origin = Size / 2;
            layerDepth = 0.7f;
            rotation = 0f;
            source = null;
            playerStatus = new PlayerStatus(health, 0f);
            weapons = new List<Weapon>() { trippleShot, singleShot };
            equipedWeapon = weapons[1];
            Shields = shield;
            maxShields = Shields;
            hitTimer = 0;
            hitTimerInterval = 3;
            #region TEMP
            friction = new Vector2(0.99f, 0.99f); // move to Level.cs ?
            acceleration = new Vector2(4, 4); // move to constructor ?
            #endregion 
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public virtual void Movement(GameTime gameTime)
        {
            if (velocity.Length() < 3f && velocity.Length() > -3f)
                velocity = Vector2.Zero;

            velocity = (velocity + direction * acceleration) * friction;
            Util.Clamp(velocity, -maxVelocity, maxVelocity); // clamp velocity to values between min value -maxVelocity and max value -maxVelocity
            Position += velocity * deltaTime;
            hitbox.Location = Center.ToPoint();
        }

        public virtual void AdjustAngleTowardsTarget(Vector2 targetPos)
        {
            Vector2 dPos = (Position + Origin) - targetPos;
            rotation = (float)Math.Atan2(dPos.Y, dPos.X) - MathHelper.ToRadians(90);
        }

        /// <summary>
        /// The damage Recieved will be applied to the shields and then check is the shields are negative taking that to subtrect from the health of the player
        /// </summary>
        /// <param name="damage">This is the damage that the clided object have asssigned to it</param>
        public virtual void RecieveDamage(float damage)
        {
            if (Shields > 0)
            {
                Shields -= damage;
                if (Shields < 0)
                {
                    damage = Math.Abs((float)Shields);
                    Shields = 0.0;
                }
            }

            if (Shields <= 0)
            {
                health -= damage;
            }

            hitTimer = hitTimerInterval;
            if (health <= 0)
            {
                IsAlive = false;
            }
        }

        public virtual void ShieldRegeneration(GameTime gameTime)
        {
            if (hitTimer > 0)
            {
                hitTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (hitTimer <= 0 && Shields < maxShields)
            {
                Shields += 10;
                if (Shields > maxShields)
                {
                    Shields = maxShields;
                }
            }
        }
    }
}
