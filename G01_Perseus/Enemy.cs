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
    public class Enemy : Ship, PlayerShootListener, CollissionListener
    {
        private Vector2 strafeVector = Vector2.Zero;
        private int healthBarHeight;
        private Rectangle healthPos, shieldPos;
        private Random random = new Random();
        public float leashDistance;
        public Vector2 startingPosition;

        private EnemyBehavior behavior;

        public Enemy(Vector2 position, Vector2 maxVelocity, Vector2 scale, float health, float shield, EnemyBehavior behavior, float leashDistance, Texture2D texture, float acceleration) : base(position, maxVelocity, scale, health, shield) 
        {
            healthBarHeight = 10;            
            healthPos = new Rectangle((int)position.X, (int)position.Y - healthBarHeight, (int)((Health / TotalHealth) * hitbox.Width), healthBarHeight);
            shieldPos = new Rectangle(healthPos.X + healthPos.Width, healthPos.Y, (int)((Shields / TotalHealth) * hitbox.Width), healthBarHeight);
            EventManager.Register(this);
            startingPosition = position;
            //friction = new Vector2(0.97f, 0.97f);
            this.acceleration = new Vector2(acceleration, acceleration);

            this.behavior = behavior;
            behavior.Enemy = this;            
            

            this.leashDistance = leashDistance;
            this.texture = texture;

        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            spriteBatch.Draw(texture, hitbox, null, Color.White, rotation, texture.Bounds.Size.ToVector2() / 2, SpriteEffects.None, 0.8f);
            spriteBatch.Draw(AssetManager.TextureAsset("gradient_bar"), healthPos, null, Color.Crimson, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
            spriteBatch.Draw(AssetManager.TextureAsset("gradient_bar"), shieldPos, null, Color.Cyan, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
            //Vector2 drawPosition = new Vector2((tileX * tileWidth) + position.X - (ix * tileWidth), (tileY * tileWidth) + position.Y - (iy * tileHeight));
            //spriteBatch.Draw(texture, drawPosition, null, Color.White, rotation, size / 2, Vector2.One, SpriteEffects.None, 0.5f);*/
        }

        public override void Update(GameTime gameTime)
        {
            //this.behavior.Update(gameTime);

            Movement(gameTime);
                       
            hitbox.Location = Center.ToPoint();
            if(Shields < MaxShields)
            {
                ShieldRegeneration(gameTime);
            }                       
            SetHealthPosition();
            base.Update(gameTime);

        }

        public void FireWeapon(GameTime gameTime)
        {
            equipedWeapon.Fire(Center, EntityManager.Player.Center, TypeOfBullet.Enemy, gameTime);
        }

        private void SetHealthPosition() //This is a bit clunky
        {
            healthPos.Location = Position.ToPoint();
            healthPos.Y -= healthBarHeight;
            shieldPos.Location = healthPos.Location;
            shieldPos.X += healthPos.Width;
        }

        public override void HandleCollision(Entity other)
        {
            if (other is Player player)
            {
                //RecieveDamage(10); //Replace with player.damage when this is implemented
            }
            else if (other is Bullet bullet)
            {
                RecieveDamage(other, bullet.damage);
                bullet.timeToLive = 0;
            }
            else if (other is Laser laser)
            {
                RecieveDamage(other, laser.damage);
            }
        }
            

        


        public override void RecieveDamage(Entity other, float damage)
        {
            base.RecieveDamage(other, damage);
            healthPos.Width = (int)((Health / TotalHealth) * hitbox.Width);
            shieldPos.Width = (int)((Shields / TotalHealth) * hitbox.Width);
            Console.WriteLine("Projectile had damage: {0}.", damage);
        }

        public override void ShieldRegeneration(GameTime gameTime) //This should not be made to run at every update!!!
        {
            base.ShieldRegeneration(gameTime);
            shieldPos.Width = (int)((Shields / TotalHealth) * hitbox.Width);
        }

        protected override void DefaultTexture()
        {
            texture = AssetManager.TextureAsset("enemy_ship");
        }

        public void PlayerFired(PlayerShootEvent e)
        {
            //System.Diagnostics.Debug.WriteLine("Player has fired at position "+e.position +" with damage: "+e.damage);
        }

        //public void Collision(CollissionEvent e)
        //{
        //    //System.Diagnostics.Debug.WriteLine("Entity: "+e.Entity + " collided with: " + e.OtherEntity); //debug

        //    HandleCollision(e.OtherEntity);
        //}

    }
}