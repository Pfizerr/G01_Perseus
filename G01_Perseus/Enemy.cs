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
    public class Enemy : Entity
    {
        private Vector2 direction;
        private Vector2 friction;
        private Vector2 acceleration;
        private float damage;
        private float strafeTimer = 2;
        private float timeStrafed;
        private Weapon equippedWeapon = new WeaponSingleShot(1, 1);
        private Vector2 strafeVector = Vector2.Zero;
        private Random random = new Random();

        private PlayerStatus status;

        public Enemy(Texture2D texture, Vector2 position, Vector2 maxVelocity, Vector2 scale, Rectangle? source, SpriteEffects spriteEffects, Color color, float rotation, float layerDepth, bool isCollidable, float health, float damage)
            : base(texture, position, maxVelocity, scale, source, spriteEffects, color, rotation, layerDepth, isCollidable)
        {
            this.health = health;
            this.damage = damage;

            origin = size / 2;
            status = new PlayerStatus(health, 0);

            #region TEMP //Copied from Player
            friction = new Vector2(0.99f, 0.99f); // move to Level.cs ?
            acceleration = new Vector2(4, 4); // move to constructor ?
            #endregion 
        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            spriteBatch.Draw(texture, hitbox, null, Color.White, rotation, texture.Bounds.Size.ToVector2() / 2, SpriteEffects.None, 0.8f);
        }

        public override void Update(GameTime gameTime)
        {
            equippedWeapon.Fire(Center, EntityManager.Player.Position, rotation, this);
            equippedWeapon.Update(gameTime);
            AdjustAngleTowardsTarget();

            if (Vector2.Distance(position, EntityManager.Player.Position) > 500)
            {
                Pursue();

            }
            else if (Vector2.Distance(position, EntityManager.Player.Position) < 100)
            {
                Retreat();
            }
            else
                Strafe(gameTime);
            Movement(gameTime);
            hitbox.Location = Center.ToPoint();
            base.Update(gameTime);
        }

        public void Movement(GameTime gameTime)
        {
            if (velocity.Length() < 3f && velocity.Length() > -3f)
                velocity = Vector2.Zero;

            velocity = (velocity + direction * acceleration) * friction;
            Util.Clamp(velocity, -maxVelocity, maxVelocity); // clamp velocity to values between min value -maxVelocity and max value -maxVelocity
            position += velocity * deltaTime;
            hitbox.Location = Center.ToPoint();
        }

        public override void Destroy()
        {
            //Code to execute when destroyed..

            System.Console.WriteLine("{0} has been killed.", this.ToString());
            return;
        }

        private void AdjustAngleTowardsTarget()
        {
            Vector2 dPos = (position + origin) - EntityManager.Player.Position;
            rotation = (float)Math.Atan2(dPos.Y, dPos.X) - MathHelper.ToRadians(90);
        }

        private void Pursue() // Moves toward the player
        {
            //direction = Vector2.Zero;
            Vector2 vectorResult = EntityManager.Player.Position - position;
            vectorResult.Normalize();
            direction = vectorResult;
        }

        private void Roam() // Moves following a set / random path
        {


        }

        private void Retreat() // Moves away from the player
        {
            Vector2 vectorResult = new Vector2((float)Math.Cos(rotation + (float)Math.PI / 2), (float)Math.Sin(rotation + (float)Math.PI / 2));
            vectorResult.Normalize();
            direction = vectorResult;
        }

        private void Strafe(GameTime gameTime) // Moves horizontally in relation to the facing direction
        {

            if (timeStrafed == 0)
            {
                //direction = Vector2.Zero;
                double rand = random.NextDouble();
                strafeVector = Vector2.Zero;
                if (rand > 0.49)
                {
                    strafeVector = new Vector2((float)Math.Cos(rotation + (float)Math.PI), (float)Math.Sin(rotation + (float)Math.PI));
                }
                else
                {
                    strafeVector = new Vector2((float)Math.Cos(rotation + (float)Math.PI * 2), (float)Math.Sin(rotation + (float)Math.PI * 2));
                }

                timeStrafed += (float)gameTime.ElapsedGameTime.TotalSeconds;
                strafeVector.Normalize();
                direction = strafeVector;
            }

            if (timeStrafed < strafeTimer)
            {
                timeStrafed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (timeStrafed > strafeTimer)
                {
                    timeStrafed = 0;
                }
            }
        }

        public override void HandleCollision(Entity other)
        {
            if (other is Player)
            {
                (other as Player).RecieveDamage(damage);
                isAlive = false;
            }
        }

        public void RecieveDamage(float damage)
        {
            health -= damage;

            if (health <= 0)
            {
                isAlive = false;
            }
        }

        protected override void DefaultTexture()
        {
            this.texture = AssetManager.TextureAsset("enemy_ship");
        }
    }
}
