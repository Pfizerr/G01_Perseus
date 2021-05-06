﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace G01_Perseus
{
    public enum TypeOfBullet { Player, Enemy}
    public class Bullet : MovingEntity
    {
        public float timeToLive;
        public float damage;
        private Vector2 direction;
        public TypeOfBullet Type { get; private set; }

        public Bullet(Vector2 position, Vector2 targetPosition, Vector2 maxVelocity, Vector2 scale, TypeOfBullet type, float damage, float timeToLive) 
            : base(maxVelocity, position, scale)
        {
            Type = type;
            this.damage = damage;
            this.timeToLive = timeToLive;
            Center = position;
            Origin = Size / 2;
            rotation = 0f;
            layerDepth = 0.8f;
            source = null;
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

            Position += direction * maxVelocity;
            hitbox.Location = Center.ToPoint();
        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            spriteBatch.Draw(texture, hitbox, source, Color.White, rotation, Origin, SpriteEffects.None, layerDepth);
            //spriteBatch.Draw(AssetManager.TextureAsset("gradient_bar"), hitbox, source, Color.White, rotation, Origin, SpriteEffects.None, 0.9f);
        }


        public override void HandleCollision(Entity other)
        {
            IsAlive = false;                         
        }

        protected override void DefaultTexture()
        {
            texture = AssetManager.TextureAsset("projectile_green");
        }

        public override void Destroy()
        {
            throw new System.NotImplementedException();
        }
    }
}