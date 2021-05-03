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
    public class Enemy : Ship, PlayerShootListener
    {

        public float damage; // Could be moved to the Moving object class if the player has some damage when it rams into other enemies
        private float strafeTimer = 2;
        private float timeStrafed;
        private Vector2 strafeVector = Vector2.Zero;
        private int healthBarHeight;
        public Rectangle healthPos;
        private Random random = new Random();
        public List<Bullet> bullets = new List<Bullet>();

        public Enemy(Vector2 position, Vector2 maxVelocity, Vector2 scale, Rectangle? source, float rotation, float layerDepth, bool isCollidable, float health, float damage, float shield)
            : base(position, maxVelocity, scale, source, rotation, layerDepth, isCollidable, health, shield)
        {
            this.damage = damage;
            healthBarHeight = 10;
            healthPos = new Rectangle((int)position.X, (int)position.Y - healthBarHeight, hitbox.Width, healthBarHeight);
            EventManager.Register(this);
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
            equipedWeapon.Fire(Center, EntityManager.Player.Position, rotation, TypeOfBullet.Enemy);
            equipedWeapon.Update(gameTime);
            AdjustAngleTowardsTarget(EntityManager.Player.Position);

            if (Vector2.Distance(Position, EntityManager.Player.Position) > 500)
            {
                Pursue();

            }
            else if (Vector2.Distance(Position, EntityManager.Player.Position) < 100)
            {
                Retreat();
            }
            else
            {
                Strafe(gameTime);
            }
                
            Movement(gameTime);
            hitbox.Location = Center.ToPoint();
            SetHealthPosition();
            base.Update(gameTime);
            
        }

        private void SetHealthPosition()
        {
            healthPos.Location = Position.ToPoint();
            healthPos.Y -= healthBarHeight;
        }

        private void Pursue() // Moves toward the player
        {
            //direction = Vector2.Zero;
            Vector2 vectorResult = EntityManager.Player.Position - Position;
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
            if (other is Player player)  
            {
                //RecieveDamage(10); //Replace with player.damage when this is implemented
            }
            else if(other is Bullet bullet)
            {
                RecieveDamage(bullet.damage);
            }
        }

        public override void RecieveDamage(float damage)
        {
            base.RecieveDamage(damage);
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
    }
}
