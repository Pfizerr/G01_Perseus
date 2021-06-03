﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace G01_Perseus
{
    public enum TypeOfBullet { Player, Enemy}
    public class Bullet : MovingEntity
    {
        public float timeToLive;
        public float damage;
        protected Vector2 direction;
        public TypeOfBullet Type { get; private set; }

        public Bullet(Vector2 position, Vector2 targetPosition, Vector2 maxVelocity, Vector2 scale, TypeOfBullet type, float damage, float timeToLive, Texture2D texture) 
            : base(maxVelocity, position, scale, texture)
        {
            Type = type;
            SetTexture();
            this.damage = damage;
            this.timeToLive = timeToLive;
            Center = position;
            rotation = 0f;
            layerDepth = 0.8f;
            direction = Vector2.Normalize(targetPosition - position);
        }

        /// <summary>
        /// Draw update loop
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            timeToLive -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeToLive <= 0)
            {
                IsAlive = false;
            }

            Position += direction * maxVelocity;
            hitbox.Location = Position.ToPoint();
        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            spriteBatch.Draw(texture, Center, null, Color.White, rotation, texture.Bounds.Size.ToVector2() * 0.5f, scale, SpriteEffects.None, 0.9f);
        }

        /// <summary>
        /// Sets the projektile texture depending on who fired it
        /// </summary>
        protected void SetTexture()
        {
            if(Type == TypeOfBullet.Player) 
            {
                texture = AssetManager.TextureAsset("projectile_green");
            }
            else
            {
                texture = AssetManager.TextureAsset("projectile_pink");
            }            
        }

        public override void Destroy(Event e) //Can be removed?
        {
            base.Destroy(e);
            //Code to execute when destroyed..

            //System.Console.WriteLine("{0} has been killed.", this.ToString());
            return;
        }

        public override void HandleCollision(Entity other) //Think this can be removed since the IsAlive is handles in update with timeToLive?
        {
            IsAlive = false;
        }
    }
}