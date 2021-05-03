﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace G01_Perseus
{
    public enum TypeOfBullet { Player, Enemy}
    public class Bullet : MovingEntity
    {
        private float timeToLive;
        public float damage;
        //private Entity parent;
        private Vector2 direction;
        public TypeOfBullet Type { get; private set; }

        public Bullet(Vector2 position, Vector2 targetPosition, Vector2 velocity, Vector2 scale, Rectangle? source, float rotation, float layerDepth, bool isCollidable, TypeOfBullet typeOfBullet, float damage, float timeToLive) 
            : base(velocity, position, scale, source, rotation, layerDepth, isCollidable)
        {
            Type = typeOfBullet;
            this.damage = damage;
            this.timeToLive = timeToLive;
            velocity = maxVelocity;
            Center = position;
            Origin = Size / 2;
            //if (isLaser)
            //{
            //    Origin = new Vector2(Vector2.Distance(position, targetPosition), Size.Y / 2);
            //}
            //else
            //{
            //    Origin = Size / 2;
            //}
         
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
            spriteBatch.Draw(texture, hitbox, source, Color.White, rotation, Origin, SpriteEffects.None, layerDepth);
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

        //public Entity Parent
        //{
        //    get => parent;
        //    private set => parent = value;
        //}
    }
}