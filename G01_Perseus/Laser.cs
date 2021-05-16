using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace G01_Perseus
{
    public enum TypeOfLaser { Player, Enemy}
    public class Laser : Entity
    {
        public float damage;

        Timer lifeTimer;
        Vector2 tempOrigin;

        public TypeOfLaser Type { get; private set; }

    public Laser(Vector2 position, Vector2 targetPosition, Vector2 scale, TypeOfLaser type, float damage) : base(position, scale)
        {
            Type = type;
            this.damage = damage;
            Center = position;
            hitbox.Height = (int)Vector2.Distance(position, targetPosition);
            tempOrigin = new Vector2(Size.X / 2, hitbox.Height - position.Y);
            Origin = tempOrigin;
            layerDepth = 0.8f;
            source = null;
            Vector2 dPos = position - targetPosition;
            rotation = (float)Math.Atan2(dPos.Y, dPos.X) - MathHelper.ToRadians(90);
           
        }

        //public TypeOfBullet Type { get; private set; }

        //public Laser(Vector2 position, Vector2 targetPosition, Vector2 maxVelocity, Vector2 scale, TypeOfBullet type, float damage, float timeToLive) : base(position, targetPosition, maxVelocity, scale, type, damage, timeToLive)
        //{

        //    Type = type;
        //    this.damage = damage;
        //    this.timeToLive = timeToLive;
        //    //Center = position;
        //    Origin = Size;
        //    layerDepth = 0.8f;
        //    source = null;
        //    direction = Vector2.Normalize(targetPosition - position);
        //    Vector2 dPos = (Center) - targetPosition;
        //    rotation = (float)Math.Atan2(dPos.Y, dPos.X) - MathHelper.ToRadians(90);
        //    //Origin = new Vector2(Vector2.Distance(position, targetPosition), Size.Y / 2);
        //}

        //public override void Destroy(Event e)
        //{
        //    base.Destroy(e);
        //}

        //public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        //{
        //    spriteBatch.Draw(texture, Center, source, Color.Green, rotation, texture.Bounds.Size.ToVector2() * 0.5f, scale, SpriteEffects.None, 0.9f);
        //}

        //public override void HandleCollision(Entity other)
        //{
        //    base.HandleCollision(other);
        //}

        //public override void Update(GameTime gameTime)
        //{
        //    base.Update(gameTime);
        //}

        //protected override void DefaultTexture()
        //{
        //    texture = AssetManager.TextureAsset("GradientBar");
        //}
        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            spriteBatch.Draw(texture, hitbox, null, Color.Green, rotation, Origin, SpriteEffects.None, 0.1f);
        }

        public override void HandleCollision(Entity other)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {

        }

        protected override void DefaultTexture()
        {
            texture = AssetManager.TextureAsset("GradientBar");
        }
    }
}
