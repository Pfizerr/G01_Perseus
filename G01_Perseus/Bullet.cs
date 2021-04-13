﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace G01_Perseus
{
    public class Bullet : Entity
    {
        private float timeToLive;
        private Rectangle hitBox;
        private float rotation;
        private Vector2 origin;
        private float speed;
        private Color color;
        private Vector2 offset;
        private Vector2 dPos;

        public Bullet(Vector2 position, Vector2 targetPosition, float speed, Color color, Point size, float timeToLive, bool isLaser) : base()
        {
            this.parent = parent;
            this.damage = damage;
            this.timeToLive = timeToLive;
            velocity = maxVelocity;
            Center = position;
            origin = size / 2;
            health = 1; 

            isAlive = true;

            dPos = position - targetPosition;

            rotation = (float)Math.Atan2(dPos.Y, dPos.X);
            direction = Vector2.Normalize(targetPosition - position);

            texture = Util.CreateFilledRectangleTexture(color, size.X, size.Y);
            if (isLaser)
            {
                origin = new Vector2(Vector2.Distance(position, targetPosition), size.Y / 2);
            }
            else
            {
                origin = new Vector2(size.X / 2, size.Y / 2);
            }
            
            hitBox = new Rectangle(position.ToPoint(), size);
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