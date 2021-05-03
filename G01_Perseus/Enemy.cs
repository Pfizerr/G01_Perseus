using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace G01_Perseus
{
    public class Enemy : Entity, PlayerShootListener, CollissionListener
    {
        public Vector2 direction; //Should not be public, is for test purposes
        private Vector2 friction;
        private Vector2 acceleration;
        public float damage; // temp
        private float strafeTimer = 2;
        private float timeStrafed;
        private Weapon equippedWeapon = new WeaponSingleShot(1, 1);
        private Vector2 strafeVector = Vector2.Zero;
        int healthBarHeight;
        public Rectangle healthPos;
        private Random random = new Random();
        public List<Bullet> bullets = new List<Bullet>();

        private PlayerStatus status;
        private EnemyBehavior behavior;

        public Enemy(Texture2D texture, Vector2 position, Vector2 maxVelocity, Vector2 scale, Rectangle? source, SpriteEffects spriteEffects, Color color, float rotation, float layerDepth, bool isCollidable, float health, float damage, EnemyBehavior behavior)
            : base(texture, position, maxVelocity, scale, source, spriteEffects, color, rotation, layerDepth, isCollidable)
        {
            this.health = health;
            this.damage = damage;
            maxHealth = health; //This should be initiated in the Entity class for better consistency along with health as an input if there is to be a moving entity class!!!
            Origin = Size / 2;
            status = new PlayerStatus(health, 0);
            healthBarHeight = 10;
            healthPos = new Rectangle((int)position.X, (int)position.Y - healthBarHeight, hitbox.Width, healthBarHeight);
            EventManager.Register(this);

            #region TEMP //Copied from Player
            friction = new Vector2(0.99f, 0.99f); // move to Level.cs ?
            acceleration = new Vector2(4, 4); // move to constructor ?
            #endregion 

            this.behavior = behavior;
            behavior.enemy = this;
        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            spriteBatch.Draw(texture, hitbox, null, Color.White, rotation, texture.Bounds.Size.ToVector2() / 2, SpriteEffects.None, 0.8f);
            spriteBatch.Draw(AssetManager.TextureAsset("gradient_bar"), healthPos, null, Color.Blue, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
            //Vector2 drawPosition = new Vector2((tileX * tileWidth) + position.X - (ix * tileWidth), (tileY * tileWidth) + position.Y - (iy * tileHeight));
            //spriteBatch.Draw(texture, drawPosition, null, Color.White, rotation, size / 2, Vector2.One, SpriteEffects.None, 0.5f);
        }

        public override void Update(GameTime gameTime)
        {
            this.behavior.Update(gameTime, rotation);
            Movement(gameTime);
            
           
            hitbox.Location = Center.ToPoint();
            base.Update(gameTime);
            SetHealthPosition();
        }

        public void FireWeapon(GameTime gameTime)
        {
            equippedWeapon.Fire(Center, EntityManager.Player.Position, rotation, TypeOfBullet.Enemy, gameTime);
            equippedWeapon.Update(gameTime);
        }

        private void SetHealthPosition()
        {
            healthPos.Location = Position.ToPoint();
            healthPos.Y -= healthBarHeight;
        }

        public void Movement(GameTime gameTime)
        {
            if (velocity.Length() < 3f && velocity.Length() > -3f)
                velocity = Vector2.Zero;

            velocity = (velocity + direction * acceleration) * friction;
            Util.Clamp(velocity, -maxVelocity, maxVelocity); // clamp velocity to values between min value -maxVelocity and max value -maxVelocity
            Position += velocity * deltaTime;
            hitbox.Location = Center.ToPoint();
        }

        public override void Destroy()
        {
            //Code to execute when destroyed..

            //Console.WriteLine("{0} has been killed.", this.ToString());
            return;
        }

        public void AdjustAngleTowardsTarget()
        {
            Vector2 dPos = (Position + Origin) - EntityManager.Player.Position;
            rotation = (float)Math.Atan2(dPos.Y, dPos.X) - MathHelper.ToRadians(90);
        }       

        public override void HandleCollision(Entity other)
        {
            if (other is Player player)  
            {
                //RecieveDamage(10); //Replace with player.damage when this is implemented
            }
            else if(other is Bullet bullet)
            {
                RecieveDamage(bullet.damage);
                bullet.timeToLive = 0;
            }

        }

        public void RecieveDamage(float damage)
        {
            health -= damage;

            if (health <= 0)
            {
                IsAlive = false;
            }
            healthPos.Width = (int)(hitbox.Width * (health / maxHealth));
        }

        protected override void DefaultTexture()
        {
            texture = AssetManager.TextureAsset("enemy_ship");
        }

        public void PlayerFired(PlayerShootEvent e)
        {
            //System.Diagnostics.Debug.WriteLine("Player has fired at position "+e.position +" with damage: "+e.damage);
        }

        public void Collision(CollissionEvent e)
        {
            System.Diagnostics.Debug.WriteLine("Entity: "+e.Entity + " collided with: " + e.OtherEntity); //debug

            HandleCollision(e.OtherEntity);
        }
    }
}
