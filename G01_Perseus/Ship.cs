using G01_Perseus.EventSystem.Events;
using G01_Perseus.EventSystem.Listeners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        protected double hitTimer, hitTimerInterval;                
        public PlayerStatus playerStatus;

        //Properites
        public float Shields { get; set; }
        public float Health { get; set; }
        public float TotalHealth { get; protected set; }
        public virtual float MaxHealth { get; protected set; }
        public virtual float MaxShields { get; protected set; }
        public virtual float PowerLevel { get; protected set; }
        public virtual float FireRate { get; protected set; }

        public Weapon EquipedWeapon => equipedWeapon;

        public Ship(Vector2 position, Vector2 maxVelocity, Vector2 scale, float health, float shield, Texture2D texture, float powerLevel, float fireRate) : base(maxVelocity, position, scale, texture)
        {
            Health = health;
            MaxHealth = health;
            
            layerDepth = 0.7f;
            rotation = 0f;
            playerStatus = new PlayerStatus(health, 0f);
            equipedWeapon = new WeaponSingleShot(1, powerLevel, fireRate, true); 
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

        /// <summary>
        /// Update loop for all ships in the game
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Shields < MaxShields)
            {
                ShieldRegeneration(gameTime);
            }
            Movement(gameTime);
        }

        /// <summary>
        /// HAndles the movement of the ships with respective velocity and direction that it has
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Movement(GameTime gameTime)
        {
            if (velocity.Length() < 3f && velocity.Length() > -3f)
                velocity = Vector2.Zero;

            velocity = (velocity + direction * acceleration) * friction;
            velocity = Util.Clamp(velocity, -maxVelocity, maxVelocity); // clamp velocity to values between min value -maxVelocity and max value -maxVelocity
            Position += velocity * deltaTime;
            hitbox.Location = Position.ToPoint();
        }

        /// <summary>
        /// Sets the direction towards a target
        /// </summary>
        /// <param name="targetPos">Position of the target which can be the mouse or another player depending on the type of ship</param>
        public virtual void AdjustAngleTowardsTarget(Vector2 targetPos)
        {
            Vector2 dPos = (Center) - targetPos;
            rotation = (float)Math.Atan2(dPos.Y, dPos.X) - MathHelper.ToRadians(90);
        }

        /// <summary>
        /// The damage Recieved will be applied to the shields and then check is the shields are negative taking that to subtrect from the health of the player
        /// </summary>
        /// <param name="damage">This is the damage that the clided object have asssigned to it</param>
        public virtual void RecieveDamage(Entity other, float damage)
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
                Destroy(new EntityKilledEvent(this, other));
                IsAlive = false;
                if(this is Enemy)
                {
                    Resources.AddXP(1100);
                    Resources.AddCurrency(10);
                    EventManager.Dispatch(new GainXpEvent());
                }
            }
            
        }

        /// <summary>
        /// HAndles the regeneration of the shield when the ships hit timer reaches 0 or if not counts down the timer
        /// </summary>
        /// <param name="gameTime">Elapsed time of the game</param>
        public virtual void ShieldRegeneration(GameTime gameTime)
        {
            if (hitTimer > 0)
            {
                hitTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (hitTimer <= 0)
            {
                Shields += 1;
                if (Shields > MaxShields)
                {
                    Shields = MaxShields;
                }
            }
        }

        /// <summary>
        /// Check that the other entity was a bullet and assignes damage to this ship
        /// </summary>
        /// <param name="other">Entity that this entity has collided with</param>
        public override void HandleCollision(Entity other)
        {
            if (other is Bullet bullet)
            {
                RecieveDamage(other, bullet.damage);
                bullet.timeToLive = 0;
            }            
        }

        /// <summary>
        /// Collission event for the listener
        /// </summary>
        /// <param name="e"></param>
        public void Collision(CollissionEvent e)
        {
            HandleCollision(e.OtherEntity);
        }

        public void Destroy() //Can be removed
        {
            //Code to execute when destroyed..

            //Console.WriteLine("{0} has been killed.", this.ToString());
            return;
        }
        
    }
}
