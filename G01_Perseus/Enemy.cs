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

        public Enemy(Vector2 position, Vector2 maxVelocity, Vector2 scale, float health, float shield, EnemyBehavior behavior, float leashDistance, Texture2D texture, float acceleration, float powerLevel, int fireRate) : base(position, maxVelocity, scale, health, shield, texture, powerLevel, fireRate) 
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
        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            spriteBatch.Draw(texture, Center, null, Color.White, rotation, texture.Bounds.Size.ToVector2() / 2, scale, SpriteEffects.None, 0.8f);
            spriteBatch.Draw(AssetManager.TextureAsset("gradient_bar"), healthPos, null, Color.Crimson, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
            spriteBatch.Draw(AssetManager.TextureAsset("gradient_bar"), shieldPos, null, Color.Cyan, 0, Vector2.Zero, SpriteEffects.None, 0.8f);            
        }

        public override void Update(GameTime gameTime)
        {
            this.behavior.Update(gameTime);
                     
            base.Update(gameTime);
            SetHealthPosition();           
        }

        public void FireWeapon(GameTime gameTime)
        {
            equipedWeapon.Fire(Center, EntityManager.Player.Center, rotation, TypeOfBullet.Enemy, gameTime);
        }

        private void SetHealthPosition()
        {
            healthPos.Location = hitbox.Location;
            healthPos.Y -= healthBarHeight;
            shieldPos.Location = hitbox.Location;
            shieldPos.X += healthPos.Width;
        }
                   
        public override void RecieveDamage(Entity other, float damage)
        {
            base.RecieveDamage(other, damage);
            healthPos.Width = (int)((Health / TotalHealth) * hitbox.Width);
            shieldPos.Width = (int)((Shields / TotalHealth) * hitbox.Width);
            Console.WriteLine("Projectile had damage: {0}.", damage);
        }

        public override void ShieldRegeneration(GameTime gameTime)
        {
            base.ShieldRegeneration(gameTime);
            shieldPos.Width = (int)((Shields / TotalHealth) * hitbox.Width);
        }

        public void PlayerFired(PlayerShootEvent e)
        {
            //System.Diagnostics.Debug.WriteLine("Player has fired at position "+e.position +" with damage: "+e.damage);
        }

    }
}