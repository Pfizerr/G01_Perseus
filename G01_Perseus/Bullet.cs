﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace G01_Perseus
{
    public class Bullet : Entity
    {
        private float timeToLive;
        private float damage;
        private Entity parent;
        private Vector2 direction;

        public Bullet(Texture2D texture, Vector2 position, Vector2 maxVelocity, Vector2 scale, Rectangle? source, SpriteEffects spriteEffects, Color color, float rotation, float layerDepth, bool isCollidable, Entity parent, Vector2 target, float damage, float timeToLive) 
            : base(texture, position, maxVelocity, scale, source, spriteEffects, color, rotation, layerDepth, isCollidable)
        {
            this.parent = parent;
            this.damage = damage;
            this.timeToLive = timeToLive;
            velocity = maxVelocity;
            Center = position;
            origin = size / 2;
            health = 1; 

            direction = Vector2.Normalize(target - position);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            timeToLive -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeToLive <= 0)
            {
                isAlive = false;
            }

            position += direction * velocity;
            hitbox.Location = Center.ToPoint();
        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            spriteBatch.Draw(texture, hitbox, source, color, rotation, origin, spriteEffects, layerDepth);
        }

        public override void Destroy()
        {
            //Code to execute when destroyed..

            System.Console.WriteLine("{0} has been killed.", this.ToString());
            return;
        }

        public override void HandleCollision(Entity other)
        {
            if (other is Enemy)
            {
                (other as Enemy).RecieveDamage(damage);
                isAlive = false;
            }
        }

        protected override void DefaultTexture()
        {
            texture = AssetManager.TextureAsset("projectile_green");
        }

        public Entity Parent
        {
            get => parent;
            private set => parent = value;
        }
    }
}