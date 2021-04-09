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
        public static Vector2 posOutput;
        private Vector2 direction;
        private float damage;

        private PlayerStatus status;

        public Enemy(Texture2D texture, Vector2 position, Vector2 maxVelocity, Vector2 scale, Rectangle? source, SpriteEffects spriteEffects, Color color, float rotation, float layerDepth, bool isCollidable, float health, float damage) 
            : base(texture, position, maxVelocity, scale, source, spriteEffects, color, rotation, layerDepth, isCollidable)
        {
            this.health = health;
            this.damage = damage;
            
            origin = size / 2;
            status = new PlayerStatus(health, 0);
        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            spriteBatch.Draw(texture, hitbox, null, Color.White, rotation, size / 2, SpriteEffects.None, 0.8f);
        }

        public override void Update(GameTime gameTime)
        {
            Pursue();

            position += direction;
            posOutput = position;
            hitbox.Location = Center.ToPoint();


            if ((Vector2.Distance(position, EntityManager.Player.Position)) < 200)
            {
                AdjustAngleTowardsTarget();
            }
            base.Update(gameTime);
        }

        public override void Destroy()
        {
            //Code to execute when destroyed..

            System.Console.WriteLine("{0} has been killed.", this.ToString());
            return;
        }

        public void AdjustAngleTowardsTarget()
        {

            Vector2 dPos = (position + origin) - EntityManager.Player.Position;

            rotation = (float)Math.Atan2(dPos.Y, dPos.X);
        }

        public void Pursue() // Moves toward the player
        {
            direction = Vector2.Zero;
            Vector2 vectorResult = EntityManager.Player.Position - position;
            vectorResult.Normalize();
            direction = vectorResult;
        }

        public void Roam() // Moves following a set / random path
        {


        }

        public void Strafe() // Moves horizontally in relation to the facing direction
        {

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

            if(health <= 0)
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
