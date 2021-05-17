using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public abstract class Ship : MovingEntity, CollissionListener
    {
        public Vector2 direction;
        protected Vector2 friction;
        protected Vector2 acceleration;
        protected Vector2 velocity;
        protected Weapon equipedWeapon;
        //Can use wapon dependinng on the type of enemy
        protected Weapon trippleShot = new WeaponTripleShot(1, 1);
        protected Weapon singleShot = new WeaponSingleShot(1, 1);
        protected double hitTimer, hitTimerInterval;
        protected List<Weapon> weapons;        

        //Components
        protected PlayerStatus playerStatus;
        public float Shields { get; protected set; }
        public float Health { get; protected set; }
        public float TotalHealth { get; protected set; }
        public virtual float MaxHealth { get; protected set; }
        public virtual float MaxShields { get; protected set; }
        public virtual float PowerLevel { get; protected set; }

        public Ship(Vector2 maxVelocity, Vector2 position, Vector2 scale, float health, float shield) : base(maxVelocity, position, scale)
        {
            Health = health;
            MaxHealth = health;
            Origin = Size / 2;
            layerDepth = 0.7f;
            rotation = 0f;
            source = null;
            playerStatus = new PlayerStatus(health, 0f);
            weapons = new List<Weapon>() { trippleShot, singleShot };
            equipedWeapon = weapons[1];
            Shields = shield;
            MaxShields = Shields;
            TotalHealth = health + shield;
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
                    damage = Math.Abs(Shields);
                    Shields = 0f;
                }
            }

            if (Shields <= 0)
            {
                Health -= damage;
            }

            hitTimer = hitTimerInterval;
            if (Health <= 0)
            {
                IsAlive = false;
                if(this is Enemy)
                {
                    Resources.AddXP(1100);
                }
            }
            
        }

        public virtual void ShieldRegeneration(GameTime gameTime)
        {
            if (hitTimer > 0)
            {
                hitTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (hitTimer <= 0 && Shields < MaxShields)
            {
                Shields += 1;
                if (Shields > MaxShields)
                {
                    Shields = MaxShields;
                }
            }
        }

        public override void HandleCollision(Entity other)
        {
            if (other is Bullet bullet)
            {
                RecieveDamage(bullet.damage);
                bullet.timeToLive = 0;
            }
            
        }

        public void Collision(CollissionEvent e)
        {
            HandleCollision(e.OtherEntity);
        }

        public override void Destroy()
        {
            //Code to execute when destroyed..

            //Console.WriteLine("{0} has been killed.", this.ToString());
            return;
        }

        
    }
}
