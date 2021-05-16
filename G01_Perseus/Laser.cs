using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace G01_Perseus
{
    public class Laser : Bullet
    {

        public TypeOfBullet Type { get; private set; }

        public Laser(Vector2 position, Vector2 targetPosition, Vector2 maxVelocity, Vector2 scale, TypeOfBullet type, float damage, float timeToLive) : base(position, targetPosition, maxVelocity, scale, type, damage, timeToLive)
        {
            Type = type;
            this.damage = damage;
            this.timeToLive = timeToLive;
            Center = position;
            //Origin = Size / 2;
            rotation = 0f;
            layerDepth = 0.8f;
            source = null;
            direction = Vector2.Normalize(targetPosition - position);
            Origin = new Vector2(Vector2.Distance(position, targetPosition), Size.Y / 2);
        }

        public override void Destroy(Event e)
        {
            base.Destroy(e);
        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            spriteBatch.Draw(texture, Center, source, Color.Green, rotation, texture.Bounds.Size.ToVector2() * 0.5f, scale, SpriteEffects.None, 0.9f);
        }

        public override void HandleCollision(Entity other)
        {
            base.HandleCollision(other);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void DefaultTexture()
        {
            texture = AssetManager.TextureAsset("GradientBar");
        }
    }
}
