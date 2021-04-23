﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace G01_Perseus
{
    public enum TypeOfBullet { Player, Enemy}
    public class Bullet : Entity
    {
        private float timeToLive;
        public float damage;
        private Entity parent;
        private Vector2 direction;
        public TypeOfBullet TypeOfBullet { get; private set; }

        public Bullet(Texture2D texture, Vector2 position, Vector2 targetPosition, Vector2 maxVelocity, Vector2 scale, Rectangle? source, SpriteEffects spriteEffects, Color color, float rotation, float layerDepth, bool isCollidable, TypeOfBullet typeOfBullet, float damage, float timeToLive, bool isLaser) 
            : base(texture, position, maxVelocity, scale, source, spriteEffects, color, rotation, layerDepth, isCollidable)
        {
            TypeOfBullet = typeOfBullet;
            this.damage = damage;
            this.timeToLive = timeToLive;
            velocity = maxVelocity;
            Center = position;
            if (isLaser)
            {
                Origin = new Vector2(Vector2.Distance(position, targetPosition), Size.Y / 2);
            }
            else
            {
                Origin = Size / 2;
            }
            health = 1; 
            direction = Vector2.Normalize(targetPosition - position);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            timeToLive -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeToLive <= 0)
            {
                IsAlive = false;
            }

            Position += direction * velocity;
            hitbox.Location = Center.ToPoint();
        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            spriteBatch.Draw(texture, hitbox, source, color, rotation, Origin, spriteEffects, layerDepth);
        }

        public override void Destroy()
        {
            //Code to execute when destroyed..

            System.Console.WriteLine("{0} has been killed.", this.ToString());
            return;
        }

        public override void HandleCollision(Entity other)
        {
            
                IsAlive = false;
               
            
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